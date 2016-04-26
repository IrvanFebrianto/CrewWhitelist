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
    public interface IContractorRepository
    {
		List<contractor> FindAll(int? skip = null, int? take = null, List<SortingInfo> sortings = null, FilterInfo filters = null);
        contractor FindByPk(int id);
        int Count(FilterInfo filters = null);
        void Save(contractor dbItem);
        void Delete(contractor dbItem);
	}
}