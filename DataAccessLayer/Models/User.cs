using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }
        public string Photo { get; set; }


        public User(string username, string password)
        {
            this.UserName = username;
            this.Password = password;
        }
        public User(string name, string lastname, string username, string password, string phone, string mail, string photo )
        {
            this.Name = name;
            this.LastName = lastname;
            this.UserName = username;
            this.Password = password;
            this.Phone = phone;
            this.Mail = mail;
            this.Photo = photo;
        }
        public User()
        {

        }
    }
}

