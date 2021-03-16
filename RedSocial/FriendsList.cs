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
    public partial class FriendsList : UserControl
    {


        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when user clicks button")]
        public event EventHandler ButtonClick;

        public FriendsList()
        {
            InitializeComponent();
        }

        private string _username;
        private string _profile;
        private string _name;
        private Button _btnDelete;

        [Category("Custom Props")]
        public string UserName
        {
            get { return _username; }
            set { _username = value; lblUserName.Text = value; }
        }
        [Category("Custom Props")]
        public string FriendName
        {
            get { return _name; }
            set { _name = value; lblName.Text = value; }
        }
        [Category("Custom Props")]
        public string Profile
        {
            get { return _profile; }
            set { _profile = value; pictureBox1.ImageLocation = value; }
        }

        [Category("Custom Props")]
        public Button BtnDeleteFriend
        {
            get { return _btnDelete; }
            set { _btnDelete = value; btnDeleteFriend = value; }
        }

        private void btnDeleteFriend_Click(object sender, EventArgs e)
        {
            if (this.ButtonClick != null)
                this.ButtonClick(this, e);
        }
    }
}
