using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseSite.App.UserManagement;

namespace BaseSite.App.Services
{
    public class UserService
    {

        private static object _syncObject = new object();
        private static UserService _instance;
        public static UserService Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_syncObject)
                    {
                        if (_instance == null)
                        {
                            _instance = new UserService();
                        }
                    }
                }
                return _instance;
            }
        }

        public UserStore Users { get; private set; }

        public List<String> Permissions
        {
            get => new List<string>() { "Manage Users", "Manage Displays", "Login" };
        }

        private UserService()
        {
            Users = new UserStore();
        }
    }
}
