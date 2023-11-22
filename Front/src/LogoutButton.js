//LogoutButton.Js
import React from 'react';
import api from './api';

const LogoutButton = ({ onLogout }) => {
  const handleLogout = async () => {
    try {
      await api.post('logout');
      onLogout(); 
    } catch (error) {
      console.error('Error logging out:', error);
    }
  };

  return (
    <button onClick={handleLogout}>Logout</button>
  );
};

export default LogoutButton;
