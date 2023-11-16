// NewContactForm.jsx
const NewContactForm = ({ newContact, handleInputChange, handleDateInputChange, addContact, cancelEditContact,validationErrors }) => (
  <div>
    <label>Имя:</label>
    <input type="text" name="name" placeholder="Albert Einstein" value={newContact.name} onChange={handleInputChange} />
    <br />
    <label>Мобильный телефон:</label>
    <input type="text" name="mobilePhone" placeholder="+375-33-777-44-22" value={newContact.mobilePhone} onChange={handleInputChange} />
    <br />
    <label>Должность:</label>
    <input type="text" name="jobTitle" placeholder="Senior Java DEV" value={newContact.jobTitle} onChange={handleInputChange} />
    <br />
    <label>Дата рождения:</label>
    <div>
      <input type="text" name="birthDay" placeholder="День" value={newContact.birthDay} onChange={(e) => handleDateInputChange(e, 'birthDay')} />
      <input type="text" name="birthMonth" placeholder="Месяц" value={newContact.birthMonth} onChange={(e) => handleDateInputChange(e, 'birthMonth')} />
      <input type="text" name="birthYear" placeholder="Год" value={newContact.birthYear} onChange={(e) => handleDateInputChange(e, 'birthYear')} />
    </div>
    <br />
    <button onClick={addContact}>Добавить контакт</button>
    <button onClick={cancelEditContact}>Отменить</button>

    {validationErrors.length > 0 && (
      <div className="validation-errors">
        <ul>
          {validationErrors.map((error, index) => (
            <li key={index}>{error}</li>
          ))}
        </ul>
      </div>
    )}
  </div>
);

 export default NewContactForm;


