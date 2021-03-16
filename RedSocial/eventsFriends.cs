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
    public partial class eventsFriends : UserControl
    {
        public eventsFriends()
        {
            InitializeComponent();
        }
        private string _username;
        private string _profile;
        private string _name;
        private string _answer;
    

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
        public string Answer
        {
            get { return _answer; }
            set { _answer = value; lblAnswer.Text = value; }
        }
    }
}
