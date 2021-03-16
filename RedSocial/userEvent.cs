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
    public partial class userEvent : UserControl
    {
        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when user clicks button")]
        public event EventHandler ButtonClick;

        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when user clicks button")]
        public event EventHandler ShowButtonClick;

        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when user clicks button")]
        public event EventHandler DeleteButtonClick;

        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when user clicks button")]
        public event EventHandler DeleteFriendButtonClick;
        public userEvent()
        {
            InitializeComponent();
        }
        private int _id;
        private string _name;
        private string _state;
        private string _place;
        private string _date;
        private string _quantity;
        
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
        [Category("Custom Props")]
        public Button ButtonInvite
        {
            get { return btnInvite; }
            set { btnInvite = value;}
        }
        private void btnInvite_Click(object sender, EventArgs e)
        {
            if (this.ButtonClick != null)
                this.ButtonClick(this, e);
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            if (this.ShowButtonClick != null)
                this.ShowButtonClick(this, e);
        }

        private void btnDeleteEvent_Click(object sender, EventArgs e)
        {
            if (this.DeleteButtonClick != null)
                this.DeleteButtonClick(this, e);
        }

        private void btnDeleteFriend_Click(object sender, EventArgs e)
        {
            if (this.DeleteFriendButtonClick != null)
                this.DeleteFriendButtonClick(this, e);
        }
    }
}
