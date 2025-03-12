using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfClient.MVVM.Model
{
    public class UserService
    {
        private UserModel currentUser;

        public UserModel CurrentUser => currentUser;

        public bool IsAuthenticated => currentUser != null;

        public void SaveUser(string name, string password)
        {
            currentUser = new UserModel(name, password);
        }

        public void CloseSesion()
        {
            currentUser = null;
        }
    }
}
