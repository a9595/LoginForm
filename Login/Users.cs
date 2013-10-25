using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Login
{
    public class Users
    {
        private string _login;
        private string _password;

        public Users()
        {
            Login = "";
            Password = "";
        }
        public Users(string login, string password)
        {
            Login = login;
            Password = password;
        }
        internal string Login
        {
            get { return _login; }
            set { _login = value; }
        }

        internal string Password
        {
            get { return _password; }
            set { _password = value; }
        }
    }
}
