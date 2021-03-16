using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Friend
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string FriendId { get; set; }


        public Friend()
        {
           
        }
        public Friend(string userId, string friendId)
        {
            this.UserId = userId;
            this.FriendId = friendId;
            
        }
    }
}
