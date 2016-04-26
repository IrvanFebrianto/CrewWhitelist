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
    public class EFProjectRepository : IProjectRepository
    {
		private ditgas_pmoEntities context = new ditgas_pmoEntities();

        #region project

        public List<project> FindAll(int? skip = null, int? take = null, List<SortingInfo> sortings = null, FilterInfo filters = null)
        {
            IQueryable<project> list = context.projects;

            if (filters != null && (filters.Filters != null && filters.Filters.Count > 0))
            {
                filters.FormatFieldToUnderscore();
                GridHelper.ProcessFilters<project>(filters, ref list);
            }

            if (sortings != null && sortings.Count > 0)
            {
                foreach (var s in sortings)
                {
                    s.FormatSortOnToUnderscore();
                    list = list.OrderBy<project>(s.SortOn + " " + s.SortOrder);
                }
            }
            else
            {
                list = list.OrderBy<project>("id desc"); //default, wajib ada atau EF error
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
            List<project> result = takeList.ToList();
            return result;
        }

        public project FindByPk(int id)
        {
            return context.projects.Find(id);
        }

        public int Count(FilterInfo filters = null)
        {
            IQueryable<project> items = context.projects;

            if (filters != null && (filters.Filters != null && filters.Filters.Count > 0))
            {
                GridHelper.ProcessFilters<project>(filters, ref items);
            }

            return items.Count();
        }

        public void Save(project dbItem)
        {
            if (dbItem.id == 0) //create
            {
                context.projects.Add(dbItem);
            }
            else //edit
            {
                var entry = context.Entry(dbItem);
                entry.State = EntityState.Modified;
            }
            context.SaveChanges();
        }

        public void Delete(project dbItem)
        {
            context.projects.Remove(dbItem);
            context.SaveChanges();
        }

		public int GetIdByNameAndInsert(string name)
        {
            int id;
            project data = (from a in context.projects where a.name == name select a).FirstOrDefault();
            if (data == null)
            {
				//Uncomment the following function if inserting new data by using just name is possible
                //project insert = new project();
                //insert.name = name;
                //context.projects.Add(insert);
                //context.SaveChanges();
                //id = insert.id;

				id=0;
            }
            else
            {
                id = data.id;
            }

            return id;

        }

        #endregion
	}
}