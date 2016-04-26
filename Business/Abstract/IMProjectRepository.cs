using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using Common.Enums;
using Business.Infrastructure;
using Business.Entities;

namespace Business.Abstract
{
    public interface IMProjectRepository
    {
		List<m_project> FindAll(int? skip = null, int? take = null, List<SortingInfo> sortings = null, FilterInfo filters = null);
        m_project FindByPk(int id);
        int Count(FilterInfo filters = null);
        void Save(m_project dbItem);
        void Delete(m_project dbItem);

		int GetIdByNameAndInsert(string name);

	}
}