import React from 'react';
import Select from 'react-select';

const styles = {
  control: (defaultStyles) => ({
    ...defaultStyles,
    backgroundColor: 'transparent',
    height: '64px',
    borderRadius: '5px',
    borderWidth: '2px',
  }),
  menu: (defaultStyles) => ({
    ...defaultStyles,
    backgroundColor: '#fff',
  }),
  singleValue: (defaultStyles) => ({
    ...defaultStyles,
    color: '#9ca3af',
  }),
  valueContainer: (defaultStyles) => ({
    ...defaultStyles,
    padding: '0 20px',
    fontSize: '20px',
  }),
  option: (defaultStyles, state) => ({
    ...defaultStyles,
    color: state.isSelected ? '#fff' : '#242424',
    backgroundColor: state.isSelected ? '#5d6ad1' : '#fff',
    ':hover': {
      color: '#fff',
      backgroundColor: '#5d6ad1',
    },
  }),
};

const Dropdown = ({ defaultValue, options, onChange, placeholder, isClearable }) => {
  return (
    <Select
      defaultValue={defaultValue}
      onChange={(selected) => onChange(selected ? selected.value : null)}
      styles={styles}
      options={options}
      placeholder={placeholder || 'Select...'}
      isClearable={isClearable}
    />
  );
};

export default Dropdown;