using Business.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common.Enums;
using WebUI.Infrastructure.Validation;

namespace WebUI.Models
{
    public class CurrencyFormStub
    {
        public int Id { get; set; }

        [DisplayName("Currency")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.MyGlobalErrors))]
        [StringLength(50, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(Resources.MyGlobalErrors))]
        public string Currency { get; set; }

        [DisplayName("Value")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.MyGlobalErrors))]
        public double Value { get; set; }

        public CurrencyFormStub() { }

        public CurrencyFormStub(currency dbItem)
        {
            Id = dbItem.id;
            Currency = dbItem.currency1;
            Value = dbItem.value;
        }

        public currency GetCurrency()
        {
            currency dbItem = new currency { id = this.Id, currency1 = Currency, value = Value };

            return dbItem;
        }
    }
}