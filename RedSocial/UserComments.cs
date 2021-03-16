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
    public partial class UserComments : UserControl
    {
        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when user clicks button")]
        public event EventHandler ButtonClick;
        public UserComments()
        {
            InitializeComponent();
        }
        private int _id;
        private string _comment;
        private string _post;
        private Button _btnPost;
        private string _username;

        [Category("Custom Props")]
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        [Category("Custom Props")]
        public string Comments
        {
            get { return txtComment.Text; }
            set { txtComment.Text = value; }
        }
        [Category("Custom Props")]
        public string Post
        {
            get { return _post; }
            set { _post = value; lblComment.Text = value; }
        }
        [Category("Custom Props")]
        public string UserName
        {
            get { return _username; }
            set { _username = value; lblUserName.Text = value; }
        }
        [Category("Custom Props")]
        public Button BtnPost
        {
            get { return _btnPost; }
            set { _btnPost = value;btnPost = value; }
        }

        private void btnPost_Click(object sender, EventArgs e)
        {
            if (this.ButtonClick != null)
                this.ButtonClick(this, e);
        }
    }
}
