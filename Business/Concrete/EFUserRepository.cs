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
    public class EFUserRepository : IUserRepository
    {
		private crew_whitelistEntities context = new crew_whitelistEntities();

        #region user

        public List<user_role> FindAll(int? skip = null, int? take = null, List<SortingInfo> sortings = null, FilterInfo filters = null)
        {
            IQueryable<user_role> list = context.user_role;

            if (filters != null && (filters.Filters != null && filters.Filters.Count > 0))
            {
                filters.FormatFieldToUnderscore();
                GridHelper.ProcessFilters<user_role>(filters, ref list);
            }

            if (sortings != null && sortings.Count > 0)
            {
                foreach (var s in sortings)
                {
                    s.FormatSortOnToUnderscore();
                    list = list.OrderBy<user_role>(s.SortOn + " " + s.SortOrder);
                }
            }
            else
            {
                list = list.OrderBy<user_role>("id desc"); //default, wajib ada atau EF error
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
            List<user_role> result = takeList.ToList();
            return result;
        }

        public user_role FindByPk(int id)
        {
            return context.user_role.Find(id);
        }

        public int Count(FilterInfo filters = null)
        {
            IQueryable<user_role> items = context.user_role;

            if (filters != null && (filters.Filters != null && filters.Filters.Count > 0))
            {
                GridHelper.ProcessFilters<user_role>(filters, ref items);
            }

            return items.Count();
        }

        public void Save(user_role dbItem)
        {
            if (dbItem.id == 0) //create
            {
                context.user_role.Add(dbItem);
            }
            else //edit
            {
                var entry = context.Entry(dbItem);
                entry.State = EntityState.Modified;
            }
            context.SaveChanges();
        }

        public void Delete(user_role dbItem)
        {
            context.user_role.Remove(dbItem);
            context.SaveChanges();
        }

        #endregion
	}
}