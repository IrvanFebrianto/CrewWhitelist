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
    public class EFLogRepository : ILogRepository
    {
        private Entities.Entities context = new Entities.Entities();

        /// <summary>
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="sortings"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        public List<log> Find(int? skip = null, int? take = null, List<SortingInfo> sortings = null, FilterInfo filters = null)
        {
            IQueryable<log> list = context.logs;

            if (filters != null && ((filters.Filters != null && filters.Filters.Count > 0) || (filters.Operator != null && filters.Operator != "")))
            {
                filters.FormatFieldToUnderscore();
                GridHelper.ProcessFilters<log>(filters, ref list);
            }

            if (sortings != null && sortings.Count > 0)
            {
                GridHelper.ProcessSorts<log>(sortings, ref list);
            }
            else
            {
                list = list.OrderByDescending(m => m.id); //default, wajib ada atau EF error
            }

            //filter
            //list = list.Where(i => i.show == 1);

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
            List<log> result = takeList.ToList();
            return result;
        }

        //public bank FindByPk(int id)
        //{
        //    return context.banks.Where(m => (m.id == id)).FirstOrDefault();
        //}

        /// <summary>
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        public int Count(FilterInfo filters = null)
        {
            IQueryable<log> items = context.logs;

            if (filters != null && (filters.Filters != null && filters.Filters.Count > 0))
            {
                GridHelper.ProcessFilters<log>(filters, ref items);
            }

            int count = items.Count();

            return count;
        }

        public long Save(log dbItem)
        {
            if (dbItem.id == 0) //create
            {
                context.logs.Add(dbItem);
            }
            else //edit
            {
                context.logs.Attach(dbItem);

                var entry = context.Entry(dbItem);
                entry.State = EntityState.Modified;

                //field yang tidak ditentukan oleh user
                //entry.Property(e => e.is_delete).IsModified = false;
            }
            context.SaveChanges();

            return dbItem.id;
        }

        public void Truncate()
        {
            context.Database.ExecuteSqlCommand("TRUNCATE TABLE log");
        }
    }
}
