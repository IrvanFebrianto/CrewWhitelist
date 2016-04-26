using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Net;
using Business.Infrastructure;
using Business.Linq;
using Business.Entities;
using Business.Abstract;

namespace Business.Concrete
{
    public class EFCompanyRepository : ICompanyRepository
    {
		private TestEntities context = new TestEntities();

        #region company

        public List<company> FindAll(int? skip = null, int? take = null, List<SortingInfo> sortings = null, FilterInfo filters = null)
        {
            IQueryable<company> list = context.companies;

            if (filters != null && (filters.Filters != null && filters.Filters.Count > 0))
            {
                filters.FormatFieldToUnderscore();
                GridHelper.ProcessFilters<company>(filters, ref list);
            }

            if (sortings != null && sortings.Count > 0)
            {
                foreach (var s in sortings)
                {
                    s.FormatSortOnToUnderscore();
                    list = list.OrderBy<company>(s.SortOn + " " + s.SortOrder);
                }
            }
            else
            {
                list = list.OrderBy<company>("id desc"); //default, wajib ada atau EF error
            }

            //take & skip
            var takeList = list;
            if (skip != null)
            {
                takeList = takeList.Skip(skip.Value);
            }
            if (take != null)
            {
                takeList = takeList.Take(take.Value);
            }

            //return result
            //var sql = takeList.ToString();
            List<company> result = takeList.ToList();
            return result;
        }

        public company FindByPk(int id)
        {
            return context.companies.Find(id);
        }

        public int Count(FilterInfo filters = null)
        {
            IQueryable<company> items = context.companies;

            if (filters != null && (filters.Filters != null && filters.Filters.Count > 0))
            {
                GridHelper.ProcessFilters<company>(filters, ref items);
            }

            return items.Count();
        }

        public void Save(company dbItem)
        {
            if (dbItem.id == 0) //create
            {
                context.companies.Add(dbItem);
            }
            else //edit
            {
                var entry = context.Entry(dbItem);
                entry.State = EntityState.Modified;
            }
            context.SaveChanges();
        }

        public void Delete(company dbItem)
        {
            context.companies.Remove(dbItem);
            context.SaveChanges();
        }

        #endregion
	}
}