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
    public partial class frmAddFriends : Form
    {
        public string userName;
        private readonly IService<Friend> _service;
        private SqlConnection _connection;
        private FriendService _innerService;
        private UserService _userService;
        public frmAddFriends()
        {
            InitializeComponent();
            string connectionString = ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString;
            _connection = new SqlConnection(connectionString);
            _service = new FriendService(_connection);
            _innerService = new FriendService(_connection);
            _userService = new UserService(_connection);
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmFriends frmFriends = new frmFriends();
            frmFriends.userName = userName;
            this.Hide();
            frmFriends.Show();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Add();
        }

        private void Add()
        {
            if (txtSearch.Text == userName)
            {
                MessageBox.Show("No puedes Auto-Agregarte");

                return;
            }

            if (String.IsNullOrEmpty(txtSearch.Text))
            {
                MessageBox.Show("Debe colocar un usuario");
            }
           
            else
            {
                if (!_userService.exist(txtSearch.Text, null))
                {
                    MessageBox.Show("Este usuario no existe");
                }

                if (_innerService.exist(userName, txtSearch.Text)){
                    MessageBox.Show("Ya tiene este usuario en su lista de amigos, intente con uno diferente");
                }
             
                else
                {
                    Friend friendOne = new Friend(userName, txtSearch.Text);
                    Friend friendtwo = new Friend(txtSearch.Text, userName);
                    _service.Add(friendOne);
                    _service.Add(friendtwo);
                    frmFriends frmFriends = new frmFriends();
                    frmFriends.userName = userName;
                    this.Hide();
                    frmFriends.Show();
                }
              
                
            }
        
        }

        private void frmAddFriends_Load(object sender, EventArgs e)
        {

        }
    }
}
