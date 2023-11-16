import React, { useState, useEffect } from 'react';
import Modal from 'react-modal';
import api from './api';
import ContactItem from './ContactItem';
import EditContactForm from './EditContactForm';
import NewContactForm from './NewContactForm';
import './styles.css';
import { validateContactData } from './contactValidation';

Modal.setAppElement('#root');

const ContactList = () => {
  const [contacts, setContacts] = useState([]);
  const [newContact, setNewContact] = useState({
    name: '',
    mobilePhone: '',
    jobTitle: '',
    birthDate: '',
  });
  const [editingContact, setEditingContact] = useState(null);
  const [isEditing, setIsEditing] = useState(false);
  const [isAddingNewContact, setIsAddingNewContact] = useState(false);

  const [validationErrors, setValidationErrors] = useState([]);
  const [validationErrorsEditing, setValidationErrorsEditing] = useState([]);
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await api.get('Task-GetAll');
        setContacts(response.data);
      } catch (error) {
        console.error('Error fetching contacts:', error);
      } finally {
        setIsLoading(false);
      }
    };

    fetchData();
  }, []);

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setNewContact({ ...newContact, [name]: value });
  };

  const addContact = async () => {
    const validationErrors = validateContactData(newContact, false);
    setValidationErrors(validationErrors);

    if (validationErrors.length > 0) {
      return;
    }

    const birthDate = `${newContact.birthYear}-${newContact.birthMonth}-${newContact.birthDay}`;
    const formattedBirthDate = new Date(birthDate);

    try {
      const response = await api.post('Task-Add', { ...newContact, birthDate: formattedBirthDate });
      setContacts([...contacts, response.data]);
      setNewContact({
        name: '',
        mobilePhone: '',
        jobTitle: '',
        birthDay: '',
        birthMonth: '',
        birthYear: '',
      });
      setIsAddingNewContact(false);
    } catch (error) {
      console.error('Error adding contact:', error);

      if (error.response && error.response.data) {
        setValidationErrors([error.response.data.message]);
      }
    }
  };

  const deleteContact = async (id) => {
    try {
      await api.delete(`Task-Delete/${id}`);
      setContacts(contacts.filter((contact) => contact.id !== id));
    } catch (error) {
      console.error('Error deleting contact:', error);
    }
  };

  const editContact = async (id, updatedContact) => {
    const validationErrorsEditing = validateContactData(updatedContact, true);
    setValidationErrorsEditing(validationErrorsEditing);

    if (validationErrorsEditing.length > 0) {
      return;
    }

    try {
      await api.put(`Task-Update/${id}`, updatedContact);
      // const response = await api.get('Task-GetAll');
      setContacts((prevContacts) => {
        const updatedContacts = prevContacts.map((contact) => {
          if (contact.id === id) {
            // Обновите редактированный контакт
            return { ...contact, ...updatedContact };
          }
          return contact;
        });
  
        return updatedContacts;
      });
      // setContacts(response.data);
      setEditingContact(null);
      setIsEditing(false);
    } catch (error) {
      console.error('Error editing contact:', error);
    }
  };

  const startEditContact = (contact) => {
    setEditingContact({ ...contact });
    setIsEditing(true);
  };

  const handleEditInputChange = (e, id) => {
    const { name, value } = e.target;
    setEditingContact((prevState) => ({
      ...prevState,
      id,
      [name]: value,
    }));
  };

  const saveEditContact = (id) => {
    editContact(id, editingContact);
  };

  const cancelEditContact = () => {
    setValidationErrorsEditing([]);
    setEditingContact(null);
    setIsEditing(false);
  };

  const cancelAddContact = () => {
    setIsAddingNewContact(false);
  };

  const handleDateInputChange = (e, fieldName) => {
    const { value } = e.target;
    setNewContact({ ...newContact, [fieldName]: value });
  };

  if (isLoading) {
    return <div>Loading...</div>;
  }

  return (
    <div>
      <div className="header">
        <h1>Список контактов</h1>
        <button onClick={() => setIsAddingNewContact(true)}>Добавить новый контакт</button>
      </div>
      <div className="contact-list-container">
        <ul className="contact-list">
          {contacts.map((contact) => (
            <li key={contact.id}>
              <ContactItem
                contact={contact}
                deleteContact={() => deleteContact(contact.id)}
                startEditContact={() => startEditContact(contact)}
              />
            </li>
          ))}
        </ul>
      </div>

      <Modal isOpen={isAddingNewContact} onRequestClose={() => setIsAddingNewContact(false)}>
        <NewContactForm
          newContact={newContact}
          handleInputChange={handleInputChange}
          handleDateInputChange={handleDateInputChange}
          addContact={addContact}
          cancelEditContact={cancelAddContact}
          validationErrors={validationErrors}
        />
      </Modal>

      {editingContact && (
        <Modal isOpen={isEditing} onRequestClose={() => setIsEditing(false)}>
          <EditContactForm
            contact={editingContact}
            handleInputChange={handleEditInputChange}
            saveEditContact={() => saveEditContact(editingContact.id)}
            cancelEditContact={cancelEditContact}
            validationErrorsEditing={validationErrorsEditing}
          />
        </Modal>
      )}
    </div>
  );
};

export default ContactList;
