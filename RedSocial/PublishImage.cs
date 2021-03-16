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
    public partial class PublishImage : UserControl
    {
        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when user clicks button")]
        public event EventHandler ButtonClick;
        HomeService service;
        frmHome home;

        public PublishImage(frmHome fomHome, HomeService service)
        {
            home = fomHome;
            this.service = service;
            InitializeComponent();
        }
        private string _username;
        private string _profile;
        private string _comment;

        [Category("Custom Props")]
        public string UserName
        {
            get { return _username; }
            set { _username = value; lblUserName.Text = value; }
        }
        [Category("Custom Props")]
   
        public string Comment
        {
            get { return txtComment.Text; }
            set { txtComment.Text = value; }
        }
        [Category("Custom Props")]
        public string Profile
        {
            get { return _profile; }
            set { _profile = value; pictureBox1.ImageLocation = value; }
        }
        public int PostId { get; set; }
        private void button1_Click(object sender, EventArgs e)
        {
            if (this.ButtonClick != null)
                this.ButtonClick(this, e);
        }
    }
}
