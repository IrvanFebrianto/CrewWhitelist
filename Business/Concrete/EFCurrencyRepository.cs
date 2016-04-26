using Business.Abstract;
using Business.Entities;
using Business.Infrastructure;
using Business.Linq;
using Common.Enums;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class EFCurrencyRepository : ICurrencyRepository
    {
        private Entities.UserManagement context = new Entities.UserManagement();
		
		private IQueryable<currency> IQCurrency(List<SortingInfo> sortings = null, FilterInfo filters = null)
		{
            //kamus
            IQueryable<currency> list = context.currency;
            string sort = "";
            List<string> sortArr = new List<string>();

            //algoritma
            if (filters != null)
            {
                filters.FormatFieldToUnderscore();
                GridHelper.ProcessFilters<currency>(filters, ref list);
            }

            if (sortings != null && sortings.Count > 0)
            {
                foreach (var s in sortings)
                {
                    s.FormatSortOnToUnderscore();
                    sortArr.Add(s.SortOn + " " + s.SortOrder);
                }

                sort = string.Join(",", sortArr);
                list = list.OrderBy<currency>(sort);
            }
            else
            {
                list = list.OrderByDescending(m => m.id); //default, wajib ada atau EF error
            }

            list = list.Where(i => i.is_delete == false);

			return list;
		}

        #region company

        /// <summary>
        /// find where !is_delete
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="sortings"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        public List<currency> Find(int? skip = null, int? take = null, List<SortingInfo> sortings = null, FilterInfo filters = null)
        {
			IQueryable<currency> list = IQCurrency(sortings, filters);

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
            List<currency> result = takeList.ToList();
            return result;
        }

        public currency FindByPk(int id)
        {
            IQueryable<currency> list = IQCurrency(null, null);

            return context.currency.Where(m => m.id == id).FirstOrDefault();
        }

        /// <summary>
        /// count where !is_delete
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        public int Count(FilterInfo filters = null)
        {
            IQueryable<currency> list = IQCurrency(null, filters);

            int count = list.Count();

            return count;
        }

        public void Save(currency dbItem)
        {
            if (dbItem.id == 0) //create
            {                
                dbItem.is_delete = false;

                context.currency.Add(dbItem);
            }
            else //edit
            {
                context.currency.Attach(dbItem);

                var entry = context.Entry(dbItem);
                entry.State = EntityState.Modified;

                //field yang tidak ditentukan oleh user
                entry.Property(e => e.is_delete).IsModified = false;
            }

            context.SaveChanges();
        }

        public void Delete(currency dbItem)
        {
            dbItem.is_delete = true;
            //context.slideshows.Remove(dbItem);
            context.SaveChanges();
        }

        #endregion
    }
}
