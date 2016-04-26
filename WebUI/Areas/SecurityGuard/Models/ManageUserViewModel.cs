using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Business.Entities;

namespace SecurityGuard.ViewModels
{
    public class ManageUserViewModel
    {
        public Guid guid { get; set; }
        public string userName { get; set; }

        public ManageUserViewModel() { }
        public ManageUserViewModel(User user)
        {
            this.guid = user.UserId;
            this.userName = user.UserName;
        }
    }
}