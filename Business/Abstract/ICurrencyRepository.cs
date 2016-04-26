using Business.Entities;
using Business.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Enums;

namespace Business.Abstract
{
    public interface ICurrencyRepository
    {
        List<currency> Find(int? skip = null, int? take = null, List<SortingInfo> sortings = null, FilterInfo filters = null);
        currency FindByPk(int id);
        int Count(FilterInfo filters);
        void Save(currency dbItem);
        void Delete(currency dbItem);
    }
}
