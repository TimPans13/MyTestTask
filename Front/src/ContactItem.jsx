// ContactItem.jsx
import React from 'react';
import './styles.css';

const ContactItem = ({ contact, deleteContact, startEditContact }) => (
  <div>
    <strong>Имя:</strong> {contact.name}
    <br />
    <strong>Мобильный телефон:</strong> {contact.mobilePhone}
    <br />
    <strong>Должность:</strong> {contact.jobTitle}
    <br />
    <strong>BirthDate: {new Date(contact.birthDate).toLocaleDateString()}</strong>
    <br />
    <button onClick={() => deleteContact(contact.id)}>Удалить</button>
    <button onClick={() => startEditContact(contact)}>Редактировать</button>
  </div>
);

export default ContactItem;