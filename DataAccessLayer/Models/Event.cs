using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public int State { get; set; }
        public int PeopleQuantity { get; set; }
        public string UserName { get; set; }
        public string Place { get; set; }
        public int Accept { get; set; }
        public Event()
        {

        }
        public Event(int Id, string username, int accept)
        {
            this.Id = Id;
            this.UserName = username;
            this.Accept = accept;
        }

        public Event(string name, DateTime date, int state, string username, string place)
        {
            this.Name = name;
            this.CreationDate = date;
            this.State = state;
            this.UserName = username;
            this.Place = place;
         
        }
    }
}
