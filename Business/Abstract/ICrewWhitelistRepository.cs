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
    public interface ICrewWhitelistRepository
    {
		List<crew_whitelist> FindAll(int? skip = null, int? take = null, List<SortingInfo> sortings = null, FilterInfo filters = null);
        crew_whitelist FindByPk(int id);
        int Count(FilterInfo filters = null);
        void Save(crew_whitelist dbItem);
        void Delete(crew_whitelist dbItem);
	}
}