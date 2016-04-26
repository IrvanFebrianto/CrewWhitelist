using Business.Entities;
using Business.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IModulesInRoleRepository
    {
        ModulesInRole FindByRoleAndModule(Guid roleId, Guid moduleId);
        void RemoveAction(Guid moduleId,Guid actionId);
        void DeleteByModule(Guid moduleId);
        void DeleteByRole(Guid roleId);
    }
}
