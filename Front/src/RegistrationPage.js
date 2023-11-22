import React, { useState } from 'react';
import api from './api';

const RegistrationForm = ({ onRegister }) => {
  const [registerData, setRegisterData] = useState({
    email: '',
    password: '',
  });

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setRegisterData({ ...registerData, [name]: value });
  };

  const handleRegister = async (e) => {
    e.preventDefault();

    try {
      const response = await api.post('register', registerData);
      const authToken = response.data.token;
      onRegister(authToken);
    } catch (error) {
      console.error('Error registering:', error);

    }
  };

  return (
    <form onSubmit={handleRegister}>
      <label>Email:</label>
      <input type="text" name="email" value={registerData.email} onChange={handleInputChange} />

      <label>Password:</label>
      <input type="password" name="password" value={registerData.password} onChange={handleInputChange} />

      <button type="submit">Register</button>
    </form>
  );
};

export default RegistrationForm;
