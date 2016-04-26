using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common.Enums;
using System.ComponentModel.DataAnnotations;
using WebUI.Models;

namespace WebUI.Infrastructure.Validation
{
    public class DateSmallerOrEqualTodayAttribute : ValidationAttribute
    {
        public DateSmallerOrEqualTodayAttribute(params string[] propertyNames)
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                DateTime inputDate = (DateTime)value;
                inputDate = new DateTime(inputDate.Year, inputDate.Month, inputDate.Day, 0, 0, 0);

                if (inputDate > DateTime.Now)
                {
                    return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
                }
            }
            return null;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(Resources.MyGlobalErrors.DateSmallerOrEqualNow, name);
        }
    }
}