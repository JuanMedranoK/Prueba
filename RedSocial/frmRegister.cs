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
    public partial class frmRegister : Form
    {

        private readonly IUserService<User> _service;
        private string _filename;
        private int _id;
        public frmRegister()
        {

            InitializeComponent();
            string connectionString = ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString;
            SqlConnection _connection = new SqlConnection(connectionString);
            _service = new UserService(_connection);
            _id = 0;
            _filename = "";
        }

        private bool ValidPassword(string password, string repassword)
        {
            if (password == repassword)
            {

                return true;
            }
            else
            {
                MessageBox.Show("Las contraseñas no coinciden, intente de nuevo");
                return false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Add();
        }
        public void Add()
        {
            if (!ValidPassword(txtPassword.Text, txtRePassword.Text))
            {

                return;
            }
            User user = new User(txtName.Text, txtLastName.Text, txtUserName.Text, txtPassword.Text, txtPhone.Text, txtMail.Text, "NA");

            var exist = _service.Get(txtUserName.Text, null);
            if (!exist)
            {
                if(string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtLastName.Text) || string.IsNullOrEmpty(txtMail.Text) || string.IsNullOrEmpty(txtPassword.Text) || string.IsNullOrEmpty(txtRePassword.Text) || string.IsNullOrEmpty(txtUserName.Text) || string.IsNullOrEmpty(txtPhone.Text))
                {
                    MessageBox.Show("No Aceptan campos vacios");
                    return;
                }
                _service.Add(user);
                if (!string.IsNullOrEmpty(_filename))
                {
                    SavePhoto();
                }
                frmLogin login = new frmLogin();
                login.username = txtUserName.Text;
                login.password = txtPassword.Text;
                this.Hide();
                login.Show();
            }
            else
            {
                MessageBox.Show("Este usuario ya existe, intente con uno diferente");
            }

        }

        private void btnAddPhoto_Click(object sender, EventArgs e)
        {
            addPhoto();
        }
        private void addPhoto()
        {
            DialogResult result = photoDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                string file = photoDialog.FileName;

                _filename = file;

                pbProfile.ImageLocation = _filename;
            }

        }

        private void SavePhoto()
        {

            int id = _id == 0 ? _service.GetLastId() : _id;
            string directory = @"Images\RedSocial\" + id + "\\";


            string[] fileNameSplit = _filename.Split('\\');
            string filename = fileNameSplit[(fileNameSplit.Length - 1)];

            CreateDirectory(directory);

            string destination = directory + filename;

            File.Copy(_filename, destination, true);

            _service.SavePhoto(id, destination);

        }
        private void CreateDirectory(string directory)
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        private void frmRegister_Load(object sender, EventArgs e)
        {

        }
    }
}

    

