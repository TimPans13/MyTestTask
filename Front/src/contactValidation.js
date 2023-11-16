// contactValidation.js

import { isNameValid, isJobTitleValid, isMobilePhoneValid, isBirthDateValid } from './validationUtils';

export const validateContactData = (contactData, isEdit) => {
  const errors = [];
  let BirthDate; 

  if (!isNameValid(contactData.name)) {
    errors.push('Имя и Фамилия не должны превышать 30 символов');
    console.log('Error: Имя и Фамилия не должны превышать 30 символов');
  }

  if (!isJobTitleValid(contactData.jobTitle)) {
    errors.push('Должность не должна превышать 30 символов');
    console.log('Error: Должность не должна превышать 30 символов');
  }

  if (!isMobilePhoneValid(contactData.mobilePhone)) {
    errors.push('Мобильный телефон должен начинаться с + и состоять из цифр и символа -');
    console.log('Error: Мобильный телефон должен начинаться с + и состоять из цифр и символа -');
  }

  if (isEdit) {
    BirthDate = contactData.birthDate;
  } else {
    BirthDate = `${contactData.birthYear}-${contactData.birthMonth}-${contactData.birthDay}`;
  }

  if (!isBirthDateValid(BirthDate)) {
    errors.push('Некорректная дата рождения');
    console.log('Error: Некорректная дата рождения');
  }

  return errors;
};
