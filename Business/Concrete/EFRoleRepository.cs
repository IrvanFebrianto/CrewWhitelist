using Business.Abstract;
using Business.Entities;
using Business.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Linq;
using System.Text.RegularExpressions;

namespace Business.Concrete
{
    public class EFRoleRepository : IRoleRepository
    {
        private Entities.UserManagement context = new Entities.UserManagement();

        public void AddModuleAndAction(string[] modules, string role)
        {
            Role r = context.Roles.Where(x => x.RoleName == role).FirstOrDefault();
            IEnumerable<ModulesInRole> listModule = r.ModulesInRoles;

            foreach (ModulesInRole mInRole in listModule)
            {
                if (mInRole.Actions.Count > 0)
                {
                    var actions = mInRole.Actions.ToList();
                    foreach (var a in actions)
                    {
                        mInRole.Actions.Remove(a);
                    }
                }
            }

            context.ModulesInRoles.RemoveRange(listModule);
            context.SaveChanges();

            foreach (string s in modules)
            {
                string[] temp = s.Split(';');
                ModulesInRole mr;
                Guid moduleId = new Guid(temp.First());
                Guid actionId = new Guid(temp.Last());

                ModulesInRole available = context.ModulesInRoles.Where(x => x.RoleId == r.RoleId).Where(x => x.ModuleId == moduleId).FirstOrDefault();
                Business.Entities.Action a = context.Actions.Find(actionId);

                if (available != null)
                {
                    if (!available.Actions.Contains(a))
                    {
                        available.Actions.Add(a);
                        context.SaveChanges();
                    }
                }
                else
                {
                    mr = new ModulesInRole()
                    {
                        Id = Guid.NewGuid(),
                        RoleId = r.RoleId,
                        ModuleId = new Guid(temp.First())
                    };
                    mr.Actions.Add(a);
                    r.ModulesInRoles.Add(mr);
                    context.SaveChanges();
                }
            }
        }

        public Role FindByName(string roleName) {
            return context.Roles.Where(x => x.RoleName == roleName).FirstOrDefault();
        }
        
    }
}
