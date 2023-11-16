// EditContactForm.jsx
import React from 'react';
import './styles.css';

const EditContactForm = ({ contact, handleInputChange, saveEditContact, cancelEditContact,validationErrorsEditing }) => (
  <div>
    <strong>Имя:</strong>
    <input
      type="text"
      name="name"
      value={contact.name}
      onChange={(e) => handleInputChange(e, contact.id)}
    />
    <br />
    <strong>Мобильный телефон:</strong>
    <input
      type="text"
      name="mobilePhone"
      value={contact.mobilePhone}
      onChange={(e) => handleInputChange(e, contact.id)}
    />
    <br />
    <strong>Должность:</strong>
    <input
      type="text"
      name="jobTitle"
      value={contact.jobTitle}
      onChange={(e) => handleInputChange(e, contact.id)}
    />
    <br />
    <strong>Дата рождения:</strong>
    <input
      type="text"
      name="birthDate"
      // value={contact.birthDate}
      value={contact.birthDate.split('T')[0]} // Получить только дату, не время
      onChange={(e) => handleInputChange(e, contact.id)}
    />
    <br />
    <button onClick={() => saveEditContact(contact.id)}>Сохранить</button>
    <button onClick={() => cancelEditContact()}>Отменить</button>
    {validationErrorsEditing.length > 0 && (
      <div className="validation-errors">
        <ul>
          {validationErrorsEditing.map((error, index) => (
            <li key={index}>{error}</li>
          ))}
        </ul>
      </div>
    )}
  </div>
);

export default EditContactForm;