using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfClient.MVVM.Model
{
    public class UserModel
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public int Id { get; set; }

        public UserModel(string name, string password, string role, int id)
        {
            Name = name;
            Password = password;
            Role = role;
            Id = id;
        }
    }
}
