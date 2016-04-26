using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common.Enums;
using System.ComponentModel.DataAnnotations;
using WebUI.Models;

namespace WebUI.Infrastructure.Validation
{
    public class DateGreaterAttribute : ValidationAttribute
    {
        public string SmallDate { get; private set; }
        private string SmallDateDisplayName { get; set; }

        public DateGreaterAttribute(string smallDate, params string[] propertyNames)
        {
            this.SmallDate = smallDate;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                DateTime bigDate;
                DateTime? smallDate = null;

                //algoritma
                if (value.GetType() == typeof(string))
                {
                    bigDate = DateTime.Parse((string)value);
                }
                else
                {
                    bigDate = (DateTime)value;
                }

                //small date
                var basePropertyInfo = validationContext.ObjectType.GetProperty(SmallDate);
                if (basePropertyInfo.PropertyType == typeof(string))
                {
                    string smallDateString = (string)basePropertyInfo.GetValue(validationContext.ObjectInstance, null);
                    if (smallDateString != null && smallDateString != "")
                        smallDate = DateTime.Parse(smallDateString);
                }
                else if ((basePropertyInfo.PropertyType == typeof(DateTime)) || (basePropertyInfo.PropertyType == typeof(Nullable<DateTime>)))
                {
                    smallDate = (DateTime)basePropertyInfo.GetValue(validationContext.ObjectInstance, null);
                }

                if (smallDate != null)
                {

                    if (bigDate < smallDate)
                    {
                        var displayAttribute = basePropertyInfo.GetCustomAttributes(typeof(System.ComponentModel.DisplayNameAttribute), true).
                            FirstOrDefault() as System.ComponentModel.DisplayNameAttribute;
                        if (displayAttribute != null)
                            this.SmallDateDisplayName = displayAttribute.DisplayName;

                        return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
                    }
                }
            }
            return null;
        }

        public override string FormatErrorMessage(string name)
        {
            string displayName;
            if (this.SmallDateDisplayName == null)
                displayName = SmallDate;
            else
                displayName = SmallDateDisplayName;

            return string.Format(Resources.MyGlobalErrors.DateGreater, name, displayName);
        }
    }
}