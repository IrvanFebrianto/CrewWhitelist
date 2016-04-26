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
    public class EFMProjectRepository : IMProjectRepository
    {
		private ditgas_pmoEntities context = new ditgas_pmoEntities();

        #region m_project

        public List<m_project> FindAll(int? skip = null, int? take = null, List<SortingInfo> sortings = null, FilterInfo filters = null)
        {
            IQueryable<m_project> list = context.m_project;

            if (filters != null && (filters.Filters != null && filters.Filters.Count > 0))
            {
                filters.FormatFieldToUnderscore();
                GridHelper.ProcessFilters<m_project>(filters, ref list);
            }

            if (sortings != null && sortings.Count > 0)
            {
                foreach (var s in sortings)
                {
                    s.FormatSortOnToUnderscore();
                    list = list.OrderBy<m_project>(s.SortOn + " " + s.SortOrder);
                }
            }
            else
            {
                list = list.OrderBy<m_project>("id desc"); //default, wajib ada atau EF error
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
            List<m_project> result = takeList.ToList();
            return result;
        }

        public m_project FindByPk(int id)
        {
            return context.m_project.Find(id);
        }

        public int Count(FilterInfo filters = null)
        {
            IQueryable<m_project> items = context.m_project;

            if (filters != null && (filters.Filters != null && filters.Filters.Count > 0))
            {
                GridHelper.ProcessFilters<m_project>(filters, ref items);
            }

            return items.Count();
        }

        public void Save(m_project dbItem)
        {
            if (dbItem.id == 0) //create
            {
                context.m_project.Add(dbItem);
            }
            else //edit
            {
                var entry = context.Entry(dbItem);
                entry.State = EntityState.Modified;
            }
            context.SaveChanges();
        }

        public void Delete(m_project dbItem)
        {
            context.m_project.Remove(dbItem);
            context.SaveChanges();
        }

		public int GetIdByNameAndInsert(string name)
        {
            int id;
            m_project data = (from a in context.m_project where a.name == name select a).FirstOrDefault();
            if (data == null)
            {
				//Uncomment the following function if inserting new data by using just name is possible
                //m_project insert = new m_project();
                //insert.name = name;
                //context.m_project.Add(insert);
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