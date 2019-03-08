using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseSite.App.Persistence;

namespace BaseSite.App.UserManagement
{
    public class UserStore : KeyedDataStore<String, User>
    {
        protected override string OutputFile => "userStore.json";

        public UserStore() : base()
        {

        }

    }
}
