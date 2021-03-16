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
    public partial class userFriendsEvents : UserControl
    {
        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when user clicks button")]
        public event EventHandler ButtonClickYes;

        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when user clicks button")]
        public event EventHandler ButtonClickNo;
        public userFriendsEvents()
        {
            InitializeComponent();
        }
        private int _id;
        private string _name;
        private string _state;
        private string _place;
        private string _date;
        private string _quantity;
        private string _username;

        [Category("Custom Props")]
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }


        [Category("Custom Props")]
        public string EventName
        {
            get { return _name; }
            set { _name = value; lblName.Text = value; }
        }
        [Category("Custom Props")]
        public string UserName
        {
            get { return _username; }
            set { _username = value; lblUserName.Text = value; }
        }

        [Category("Custom Props")]
        public string EventState
        {
            get { return _state; }
            set { _state = value; lblState.Text = value; }
        }

        [Category("Custom Props")]
        public string EventPlace
        {
            get { return _place; }
            set { _place = value; lblPlace.Text = value; }
        }

        [Category("Custom Props")]
        public string EventDate
        {
            get { return _date; }
            set { _date = value; lblDate.Text = value; }
        }

        [Category("Custom Props")]
        public string Quantity
        {
            get { return _quantity; }
            set { _quantity = value; lblQuantity.Text = value; }
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            if (this.ButtonClickYes != null)
                this.ButtonClickYes(this, e);
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            if (this.ButtonClickNo != null)
                this.ButtonClickNo(this, e);
        }
    }
}