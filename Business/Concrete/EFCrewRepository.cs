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
    public class EFCrewRepository : ICrewRepository
    {
		private crew_whitelistEntities context = new crew_whitelistEntities();

        #region contractor

        public List<crew> FindAll(int? skip = null, int? take = null, List<SortingInfo> sortings = null, FilterInfo filters = null)
        {
            IQueryable<crew> list = context.crews;

            if (filters != null && (filters.Filters != null && filters.Filters.Count > 0))
            {
                filters.FormatFieldToUnderscore();
                GridHelper.ProcessFilters<crew>(filters, ref list);
            }

            if (sortings != null && sortings.Count > 0)
            {
                foreach (var s in sortings)
                {
                    s.FormatSortOnToUnderscore();
                    list = list.OrderBy<crew>(s.SortOn + " " + s.SortOrder);
                }
            }
            else
            {
                list = list.OrderBy<crew>("barcode desc"); //default, wajib ada atau EF error
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
            List<crew> result = takeList.ToList();
            return result;
        }

        public crew FindByPk(string barcode)
        {
            return context.crews.Find(barcode);
        }

        public int Count(FilterInfo filters = null)
        {
            IQueryable<crew> items = context.crews;

            if (filters != null && (filters.Filters != null && filters.Filters.Count > 0))
            {
                GridHelper.ProcessFilters<crew>(filters, ref items);
            }

            return items.Count();
        }

        public void Save(crew dbItem)
        {
            if (context.crews.FirstOrDefault(x => x.barcode == dbItem.barcode)==null) //create
            {
                context.crews.Add(dbItem);
            }
            else //edit
            {
                var entry = context.Entry(dbItem);
                entry.State = EntityState.Modified;
            }
            context.SaveChanges();
        }

        public void Delete(crew dbItem)
        {
            context.crews.Remove(dbItem);
            context.SaveChanges();
        }

        #endregion
	}
}