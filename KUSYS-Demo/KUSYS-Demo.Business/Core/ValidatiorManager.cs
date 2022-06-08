using FluentValidation;
using KUSYS_Demo.Business.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUSYS_Demo.Business.Core
{
    public class ValidatiorManager
    {
        private readonly IServiceProvider _service;

        public ValidatiorManager(IServiceProvider service)
        {
            _service = service;
        }
        private T Get<T>() where T : IValidator
        {
            return (T)_service.GetService(typeof(T));
        }

        public List<ValidationErrorResult> Validate<T>(dynamic data) where T : IValidator
        {

            var validator = Get<T>();
            var errors = new List<ValidationErrorResult>();

            var valid = validator.Validate(data);

            if (!valid.IsValid)
            {
                foreach (var error in valid.Errors)
                {
                    errors.Add(new ValidationErrorResult
                    {
                        ValidationType = error.ErrorCode,
                        ValidationMessage = error.ErrorMessage
                    });
                }
            };

            return errors;
        }
    }
}
