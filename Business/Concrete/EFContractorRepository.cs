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
    public class EFContractorRepository : IContractorRepository
    {
		private ditgas_pmoEntities context = new ditgas_pmoEntities();

        #region contractor

        public List<contractor> FindAll(int? skip = null, int? take = null, List<SortingInfo> sortings = null, FilterInfo filters = null)
        {
            IQueryable<contractor> list = context.contractors;

            if (filters != null && (filters.Filters != null && filters.Filters.Count > 0))
            {
                filters.FormatFieldToUnderscore();
                GridHelper.ProcessFilters<contractor>(filters, ref list);
            }

            if (sortings != null && sortings.Count > 0)
            {
                foreach (var s in sortings)
                {
                    s.FormatSortOnToUnderscore();
                    list = list.OrderBy<contractor>(s.SortOn + " " + s.SortOrder);
                }
            }
            else
            {
                list = list.OrderBy<contractor>("id desc"); //default, wajib ada atau EF error
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
            List<contractor> result = takeList.ToList();
            return result;
        }

        public contractor FindByPk(int id)
        {
            return context.contractors.Find(id);
        }

        public int Count(FilterInfo filters = null)
        {
            IQueryable<contractor> items = context.contractors;

            if (filters != null && (filters.Filters != null && filters.Filters.Count > 0))
            {
                GridHelper.ProcessFilters<contractor>(filters, ref items);
            }

            return items.Count();
        }

        public void Save(contractor dbItem)
        {
            if (dbItem.id == 0) //create
            {
                context.contractors.Add(dbItem);
            }
            else //edit
            {
                var entry = context.Entry(dbItem);
                entry.State = EntityState.Modified;
            }
            context.SaveChanges();
        }

        public void Delete(contractor dbItem)
        {
            context.contractors.Remove(dbItem);
            context.SaveChanges();
        }

        #endregion
	}
}