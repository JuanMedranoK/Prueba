using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class UserSession
    {
        private static UserSession instance;

        public User User;

        private UserSession()
        {
        }

        public static UserSession SetUserSession(User user)
        {
            if (instance == null)
                instance = new UserSession();

            instance.User = user;

            return instance;
        }

        public static UserSession CurrentSession
        {
            get
            {
                return instance;
            }
        }

        public static bool UserLogedIn
        {
            get
            {
                return instance != null && instance.User != null;
            }
        }
    }
}