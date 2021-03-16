using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string UserName { get; set; }
        public string Comments { get; set; }
        public string FriendId { get; set; }

        public Comment()
        {

        }

        public Comment(int postId, string username,string comments, string friendId)
        {
            this.Comments = comments;
            this.PostId = postId;
            this.UserName = username;
            this.FriendId = friendId;
        }


    }
}
