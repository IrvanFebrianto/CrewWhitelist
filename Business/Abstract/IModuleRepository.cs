using Business.Entities;
using Business.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IModuleRepository
    {
        void Create(Module module);
        void Delete(string moduleName, bool foreignKey);
        List<Module> Find(int skip = 0, int? take = null, List<SortingInfo> sortings = null, FilterInfo filters = null, string filterLogic = null);
        int Count(List<FilterInfo> filters, string filterLogic);
        Module FindByPk(Guid id);
        Module FindByName(string moduleName);

        void GetAllChildrenInModule(string moduleName, ref List<Module> result, int lvl);

        void addAction(Guid moduleId, Guid actionId);
        void removeAction(Guid moduleId, Guid actionId);

        List<Business.Entities.Action> GetActionsInModule(string moduleName);
    }
}
