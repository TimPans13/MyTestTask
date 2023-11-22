import React, { useState } from 'react';
import api from './api';

const LoginForm = ({ onLogin }) => {
  const [loginData, setLoginData] = useState({
    email: '',
    password: '',
  });

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setLoginData({ ...loginData, [name]: value });
  };

  const handleLogin = async (e) => {
    e.preventDefault();

    try {
      const response = await api.post('login', loginData);
      const authToken = response.data.token;
      onLogin(authToken);
    } catch (error) {
      console.error('Error logging in:', error);
    }
  };

  return (
    <form onSubmit={handleLogin}>
      <label>Email:</label>
      <input type="text" name="email" value={loginData.email} onChange={handleInputChange} />

      <label>Password:</label>
      <input type="password" name="password" value={loginData.password} onChange={handleInputChange} />

      <button type="submit">Login</button>
    </form>
  );
};

export default LoginForm;