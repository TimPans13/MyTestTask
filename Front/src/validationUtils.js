// validationUtils.js

export const isNameValid = (name) => name.length <= 30;

export const isJobTitleValid = (jobTitle) => jobTitle.length <= 30;

export const isMobilePhoneValid = (mobilePhone) => {
  const cleanedMobilePhone = mobilePhone.replace(/-/g, '');
  return cleanedMobilePhone.startsWith('+') && /^\+\d{1,12}$/.test(cleanedMobilePhone);
};

export const isBirthDateValid = (birthDate) => {
  const formattedBirthDate = new Date(birthDate);
  return Object.prototype.toString.call(formattedBirthDate) === '[object Date]' && !isNaN(formattedBirthDate.getTime());
};


