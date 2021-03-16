using BusinessLayer;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RedSocial
{
    public partial class frmHome : Form
    {
        public string Username;
        DateTime date = DateTime.Today;
        private readonly HomeService service;
        private readonly UserService userService;
        private string _filename;
        private int _id;
        private CommentService _innerCommentService;
        private readonly IService<Comment> _commentService;
        public frmHome()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString;
            var con = new SqlConnection(connectionString);
            service = new HomeService(con);
            userService = new UserService(con);
            _id = 0;
            _filename = "";
            _commentService = new CommentService(con);
            _innerCommentService = new CommentService(con);
            InitializeComponent();
        }

        private void btnPublish_Click(object sender, EventArgs e)
        {

            Add();
            LoadData();
            LoadImage();
            LoadComments();

        }
        private void LoadComments()
        {
            flowLayoutPanel1.Controls.Clear();
            DataSet ds = _commentService.GetAll(service.getUserName());

            UserComments[] listItems = new UserComments[_innerCommentService.GetbyId(service.getUserName())];
            for (int i = 0; i < listItems.Length; i++)
            {
                listItems[i] = new UserComments();
                listItems[i].Post = ds.Tables[0].Rows[i]["Comment"].ToString().Trim();
                listItems[i].UserName = ds.Tables[0].Rows[i]["username"].ToString().Trim();
                flowLayoutPanel1.Controls.Add(listItems[i]);
                listItems[i].ButtonClick += new EventHandler(AddComent);

            }


        }
        public void AddComent(object sender, EventArgs e)
        {
            PostList uc = sender as PostList;
            if (uc != null)
            {
                Comment comment = new Comment(uc.PostId, service.getUserName(), uc.Comment, uc.UserName);
                _commentService.Add(comment);
            }
            else
            {
                UserComments pl = sender as UserComments;
                Comment comment = new Comment();
                comment.UserName = service.getUserName();
                comment.Comments = pl.Comments;
                comment.FriendId = service.getUserName();
                _commentService.Add(comment);
                comment.Id = pl.Id;
            }

            flowLayoutPanel1.Controls.Clear();
            DataSet ds = _commentService.GetAll(service.getUserName());

            UserComments[] listItems = new UserComments[_innerCommentService.GetbyId(service.getUserName())];
            for (int i = 0; i < listItems.Length; i++)
            {
                listItems[i] = new UserComments();
                listItems[i].UserName = ds.Tables[0].Rows[i]["username"].ToString().Trim();
                listItems[i].Post = ds.Tables[0].Rows[i]["Comment"].ToString().Trim();
                flowLayoutPanel1.Controls.Add(listItems[i]);
                listItems[i].ButtonClick += new EventHandler(AddComent);

            }
        }
        public void AddPhotoComent(object sender, EventArgs e)
        {
            PublishImage uc = sender as PublishImage;
            if (uc != null)
            {
                Comment comment = new Comment(uc.PostId, service.getUserName(), uc.Comment, uc.UserName);
                _commentService.Add(comment);
            }
            else
            {
                UserComments pl = sender as UserComments;
                Comment comment = new Comment();
                comment.UserName = service.getUserName();
                comment.Comments = pl.Comments;
                comment.FriendId = service.getUserName();
                _commentService.Add(comment);
                comment.Id = pl.Id;
            }

            flowLayoutPanel1.Controls.Clear();
            DataSet ds = _commentService.GetAll(service.getUserName());

            UserComments[] listItems = new UserComments[_innerCommentService.GetbyId(service.getUserName())];
            for (int i = 0; i < listItems.Length; i++)
            {
                listItems[i] = new UserComments();
                listItems[i].UserName = ds.Tables[0].Rows[i]["username"].ToString().Trim();
                listItems[i].Post = ds.Tables[0].Rows[i]["Comment"].ToString().Trim();
                flowLayoutPanel1.Controls.Add(listItems[i]);
                listItems[i].ButtonClick += new EventHandler(AddComent);

            }
        }
        private void frmHome_Load(object sender, EventArgs e)
        {
            txtPost.GotFocus += new EventHandler(RemoveText);
            txtPost.LostFocus += new EventHandler(AddText);

            LoadData();
            LoadImage();
            LoadComments();
        }

        public void LoadData()
        {

            CleanPost();
            var posts = service.GetAllData(service.getUserName());
      
     
            
            PostList[] listItems = new PostList[posts.Rows.Count];

                for (int i = 0; i < posts.Rows.Count; i++)
                {
                    var row = posts.Rows[i];

                    var postControl = new PostList(this, service);
                    postControl.PostId = row.Field<int>("postId");
                    postControl.UserName = row.Field<string>("username");
                    postControl.Post = row.Field<string>("content");
                    postControl.Profile = row.Field<string>("photo");
                    postControl.ButtonClick += new EventHandler(AddComent);
                    flowLayoutPanel2.Controls.Add(postControl);
                }
            }

        public void LoadImage()
        {

    
            var posts = service.GetAllDataPhoto(service.getUserName());


         
            PublishImage[] listItems = new PublishImage[posts.Rows.Count];

            for (int i = 0; i < posts.Rows.Count; i++)
            {
                var row = posts.Rows[i];

                var postControl = new PublishImage(this, service);
                postControl.PostId = row.Field<int>("postId");
                postControl.UserName = row.Field<string>("username");
                postControl.Profile = row.Field<string>("PhotoPublish");
                postControl.ButtonClick += new EventHandler(AddPhotoComent);
                flowLayoutPanel2.Controls.Add(postControl);
            }
     
       

        }


        public void CleanPost()
        {
            flowLayoutPanel2.Controls.Clear();
        }


        public void Add()
        {
         Home Post = new Home();
         Post.Content = txtPost.Text;
         Post.Date = date;
         Post.UserName = service.getUserName();
           service.Add(Post);
        }

        public void AddPhoto()
        {
            Home Post = new Home();
            Post.PhotoPublish = _filename;
            Post.Date = date;
            Post.UserName = service.getUserName();
            service.AddPhoto(Post);
            if (!string.IsNullOrEmpty(_filename))
            {
                SavePhoto();
            }
        }
        public void RemoveText(object sender, EventArgs e)
        {
            if (txtPost.Text == "¿Que quieres expresar?")
            {
                txtPost.Text = "";
            }
        }

        public void AddText(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPost.Text))
                txtPost.Text = "¿Que quieres expresar?";
        }



    
        

   

        private void cerrarSesionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Cerrando sesion");
            this.userService.Logout();
            this.Close();
            frmLogin.Instance.Show();
        }

        private void amigosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmFriends frmFriends = new frmFriends();
            frmFriends.userName = service.getUserName();
            this.Hide();
            frmFriends.Show();

        }

        private void eventosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEvents frmEvents = new frmEvents();
            frmEvents.userName = service.getUserName();
            this.Hide();
            frmEvents.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
            AddPhoto();
            LoadData();
            LoadImage();
        }
        private void addPhoto()
        {
            DialogResult result = opPhoto.ShowDialog();

            if (result == DialogResult.OK)
            {
                string file = opPhoto.FileName;

                _filename = file;

                pbAddPhoto.ImageLocation = _filename;
            }

        }
        private void SavePhoto()
        {

            int id = _id == 0 ? service.GetLastId() : _id;
            string directory = @"Images\Publicaciones\" + id + "\\";


            string[] fileNameSplit = _filename.Split('\\');
            string filename = fileNameSplit[(fileNameSplit.Length - 1)];

            CreateDirectory(directory);

            string destination = directory + filename;

            File.Copy(_filename, destination, true);

            service.SavePhoto(id, destination);

        }

        private void CreateDirectory(string directory)
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            addPhoto();
        }
    }
}
