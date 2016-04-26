using Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common.Enums;
using System.Web.Script.Serialization;

namespace WebUI.Models
{
    public class CurrencyPresentationStub
    {
        public int Id { get; set; }
        public string Currency { get; set; }
        public double Value { get; set; }

        public CurrencyPresentationStub() { }

        public CurrencyPresentationStub(currency dbItem)
        {
            Id = dbItem.id;
            Currency = dbItem.currency1;
            Value = dbItem.value;
        }

        public List<CurrencyPresentationStub> MapList(List<currency> dbItems)
        {
            List<CurrencyPresentationStub> retList = new List<CurrencyPresentationStub>();

            foreach (currency c in dbItems)
                retList.Add(new CurrencyPresentationStub(c));

            return retList;
        } 
    }
}