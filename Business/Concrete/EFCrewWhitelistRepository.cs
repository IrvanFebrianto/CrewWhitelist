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
    public class EFCrewWhitelistRepository : ICrewWhitelistRepository
    {
		private crew_whitelistEntities context = new crew_whitelistEntities();

        #region crew whitelist

        public List<crew_whitelist> FindAll(int? skip = null, int? take = null, List<SortingInfo> sortings = null, FilterInfo filters = null)
        {
            IQueryable<crew_whitelist> list = context.crew_whitelist;

            if (filters != null && (filters.Filters != null && filters.Filters.Count > 0))
            {
                filters.FormatFieldToUnderscore();
                GridHelper.ProcessFilters<crew_whitelist>(filters, ref list);
            }

            if (sortings != null && sortings.Count > 0)
            {
                foreach (var s in sortings)
                {
                    s.FormatSortOnToUnderscore();
                    list = list.OrderBy<crew_whitelist>(s.SortOn + " " + s.SortOrder);
                }
            }
            else
            {
                list = list.OrderBy<crew_whitelist>("id desc"); //default, wajib ada atau EF error
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
            List<crew_whitelist> result = takeList.ToList();
            return result;
        }

        public crew_whitelist FindByPk(int id)
        {
            return context.crew_whitelist.Find(id);
        }

        public int Count(FilterInfo filters = null)
        {
            IQueryable<crew_whitelist> items = context.crew_whitelist;

            if (filters != null && (filters.Filters != null && filters.Filters.Count > 0))
            {
                GridHelper.ProcessFilters<crew_whitelist>(filters, ref items);
            }

            return items.Count();
        }

        public void Save(crew_whitelist dbItem)
        {
            if (dbItem.Id == 0) //create
            {
                context.crew_whitelist.Add(dbItem);
            }
            else //edit
            {
                var entry = context.Entry(dbItem);
                entry.State = EntityState.Modified;
            }
            context.SaveChanges();
        }

        public void Delete(crew_whitelist dbItem)
        {
            context.crew_whitelist.Remove(dbItem);
            context.SaveChanges();
        }

        #endregion
	}
}