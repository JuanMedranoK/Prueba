using BusinessLayer;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RedSocial
{
    public partial class frmFriends : Form
    {
        private SqlConnection _connection;
        private readonly IService<Friend> _service;
        private readonly IService<Comment> _commentService;
        public string userName;
        private readonly HomeService _homeService;
        private FriendService _innerService;
        private CommentService _innerCommentService;
        frmHome home;
        public frmFriends()
        {
            InitializeComponent();
            string connectionString = ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString;
            _connection = new SqlConnection(connectionString);
            _service = new FriendService(_connection);
            _innerService = new FriendService(_connection);
            _commentService = new CommentService(_connection);
            _innerCommentService = new CommentService(_connection);
            _homeService = new HomeService(_connection);

        }

        private void inicioToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void LoadData()
        {
            DataSet ds = _service.GetAll(userName);
    
            FriendsList[] listItems = new FriendsList[_innerService.GetbyId(userName)];
            for (int i = 0; i < listItems.Length; i++)
            {
                listItems[i] = new FriendsList();
                listItems[i].Profile = ds.Tables[0].Rows[i]["Photo"].ToString().Trim();
                listItems[i].UserName = ds.Tables[0].Rows[i]["UserName"].ToString().Trim();
                listItems[i].FriendName = ds.Tables[0].Rows[i]["Name"].ToString().Trim() + " "+ ds.Tables[0].Rows[i]["LastName"].ToString().Trim();
                flowLayoutPanel1.Controls.Add(listItems[i]);
            
                listItems[i].ButtonClick += new EventHandler(Delete);
            }

        }

        private void LoadPost()
        {
            DataSet ds = _innerService.PostList(userName);

            friendsPostList[] listItems = new friendsPostList[_innerService.GetbyPostId(userName)];
            for (int i = 0; i < listItems.Length; i++)
            {
                listItems[i] = new friendsPostList();
                listItems[i].Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Id"].ToString().Trim());
                listItems[i].Profile = ds.Tables[0].Rows[i]["Photo"].ToString().Trim();
                listItems[i].UserName = ds.Tables[0].Rows[i]["UserName"].ToString().Trim();
                listItems[i].Post = ds.Tables[0].Rows[i]["Content"].ToString().Trim();
                           
                flowLayoutPanel3.Controls.Add(listItems[i]);
                listItems[i].ButtonClick += new EventHandler(AddComent);

            }
        }
        public void LoadImage()
        {
        

            var posts = _innerService.ListDataPhoto();



            PublishImage[] listItems = new PublishImage[posts.Rows.Count];

            for (int i = 0; i < posts.Rows.Count; i++)
            {
                var row = posts.Rows[i];

                var postControl = new PublishImage(home, _homeService);
              
                postControl.PostId = row.Field<int>("postId");
                postControl.UserName = row.Field<string>("username");
                postControl.Profile = row.Field<string>("PhotoPublish");
                //postControl.Click += new EventHandler(AddComment);
                flowLayoutPanel3.Controls.Add(postControl);
            }
        }
        private void LoadComments()
        {
            DataSet ds = _innerCommentService.ListDataFriends();

            UserComments[] listItems = new UserComments[_innerCommentService.GetbyIdFriends()];
            for (int i = 0; i < listItems.Length; i++)
            {
                listItems[i] = new UserComments();
                listItems[i].Post = ds.Tables[0].Rows[i]["Comment"].ToString().Trim();
                listItems[i].UserName = ds.Tables[0].Rows[i]["username"].ToString().Trim();
                flowLayoutPanel2.Controls.Add(listItems[i]);
                listItems[i].ButtonClick += new EventHandler(AddComent);

            }


        }
        private void frmFriends_Load(object sender, EventArgs e)
        {
            //txtPost.GotFocus += new EventHandler(RemoveText);
            //txtPost.LostFocus += new EventHandler(AddText);
            LoadData();
            LoadPost();
            miUserName.Text = userName;
            LoadComments();
            LoadImage();

        }
        public void Delete(object sender, EventArgs e)
        {
            FriendsList friend = sender as FriendsList;
            _innerService.DeleteFriend(userName, friend.UserName);
            flowLayoutPanel1.Controls.Clear();
            flowLayoutPanel3.Controls.Clear();
            LoadData();
            LoadPost();
         



        }
        public void AddComent(object sender, EventArgs e)
        {
            friendsPostList uc = sender as friendsPostList;
            if (uc != null)
            {
                Comment comment = new Comment(uc.Id, userName, uc.Comment,uc.UserName);
                _commentService.Add(comment);
            }
            else
            {
                UserComments pl= sender as UserComments;
                Comment comment = new Comment();
                comment.UserName = userName;
                comment.Comments = pl.Comments;
                comment.FriendId = userName;
                _commentService.Add(comment);
                comment.Id = pl.Id;
            }

            flowLayoutPanel2.Controls.Clear();
            DataSet ds = _innerCommentService.ListDataFriends();

            UserComments[] listItems = new UserComments[_innerCommentService.GetbyIdFriends()];
            for (int i = 0; i < listItems.Length; i++)
            {
                listItems[i] = new UserComments();
                listItems[i].UserName = ds.Tables[0].Rows[i]["username"].ToString().Trim();
                listItems[i].Post = ds.Tables[0].Rows[i]["Comment"].ToString().Trim();
                flowLayoutPanel2.Controls.Add(listItems[i]);
                listItems[i].ButtonClick += new EventHandler(AddComent);

            }
        }
        //public void RemoveText(object sender, EventArgs e)
        //{
        //    if (txtPost.Text == "¿Que quieres expresar?")
        //    {
        //        txtPost.Text = "";
        //    }
        //}

        //public void AddText(object sender, EventArgs e)
        //{
        //    if (string.IsNullOrWhiteSpace(txtPost.Text))
        //        txtPost.Text = "¿Que quieres expresar?";
        //}

        private void cerrarSesionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLogin frmLogin = new frmLogin();
            this.Hide();
            frmLogin.Show();
        }

        private void tableLayoutPanel1_Layout(object sender, LayoutEventArgs e)
        {
       
        }

        private void btnAddFriends_Click(object sender, EventArgs e)
        {
            frmAddFriends frmAddFriends = new frmAddFriends();
            frmAddFriends.userName = userName;
            this.Hide();
            frmAddFriends.Show();
        }

        private void inicioToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmHome frmHome = new frmHome();
            frmHome.Username = userName;
            this.Hide();
            frmHome.Show();
        }
    }
}
