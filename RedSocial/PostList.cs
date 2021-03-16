using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLayer;

namespace RedSocial
{
    public partial class PostList : UserControl
    {
        HomeService service;
        frmHome home;
        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when user clicks button")]
        public event EventHandler ButtonClick;
        public PostList(frmHome fomHome, HomeService service)
        {
            home = fomHome;
            this.service = service;
            InitializeComponent();
        }

        private string _username;
        private string _profile;
        private string _post;
        private Button _btnPost;
        private Button _btnDelete;

        [Category("Custom Props")]
        public string UserName
        {
            get { return _username; }
            set { _username = value; lblName.Text = value; }
        }

        [Category("Custom Props")]
        public string Profile
        {
            get { return _profile; }
            set { _profile = value; pictureBox1.ImageLocation = value; }
        }
        [Category("Custom Props")]
        public string Post
        {
            get { return _post; }
            set { _post = value; lblPost.Text = value; }
        }

        [Category("Custom Props")]
        public Button BtnPost
        {
            get { return _btnPost; }
            set { _btnPost = value; btnPost = value; }
        }

        [Category("Custom Props")]
        public Button BtnDelete
        {
            get { return _btnDelete; }
            set { _btnDelete = value; _btnDelete = value; }
        }
        public string Comment
        {
            get { return txtComments.Text; }
            set { txtComments.Text = value; }
        }


        public int PostId { get; set; }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            service.Delete(PostId);
            home.LoadData();
            home.LoadImage();
           
        }

        private void lblPost_Click(object sender, EventArgs e)
        {

        }

        private void btnPost_Click(object sender, EventArgs e)
        {
            if (this.ButtonClick != null)
                this.ButtonClick(this, e);
        }
    }
}