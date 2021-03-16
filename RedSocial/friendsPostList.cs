using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RedSocial
{
    public partial class friendsPostList : UserControl
    {
        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when user clicks button")]
        public event EventHandler ButtonClick;
        public friendsPostList()
        {
            InitializeComponent();
        }

        private int _id;
        private string _username;
        private string _comment;
        private string _profile;
        private string _post;
        private Button _btnPost;
        private FlowLayoutPanel _postP;

        [Category("Custom Props")]
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

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
        public string Comment
        {
            get { return txtComments.Text; }
            set { txtComments.Text = value; }
        }

        [Category("Custom Props")]
        public Button BtnPost
        {
            get { return _btnPost; }
            set { _btnPost = value; btnPost = value; }
        }

        [Category("Custom Props")]
        public FlowLayoutPanel PostPanel
        {
            get { return _postP; }
            set { _postP = value; posPanel = value; }
        }



        private void btnPost_Click(object sender, EventArgs e)
        {
            if (this.ButtonClick != null)
                this.ButtonClick(this, e);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}