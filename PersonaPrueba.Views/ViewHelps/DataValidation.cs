using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PersonaPrueba.Views.ViewHelps
{
    public class DataValidation
    {
        private ValidationContext _context;
        private List<ValidationResult> _result;
        private bool _valid;
        private string _message;

        public DataValidation(object instance)
        {
            _context = new ValidationContext(instance);
            _result = new List<ValidationResult>();
            _valid = Validator.TryValidateObject(instance, _context, _result, true);
        }

        public bool Validate()
        {
            if (_valid==false)
            {
                foreach (ValidationResult validation in _result)
                {
                    _message += $"{validation.ErrorMessage}\n";
                }
                MessageResult.LogErrors(_message);
            }
            return _valid;
        }
    }
}
