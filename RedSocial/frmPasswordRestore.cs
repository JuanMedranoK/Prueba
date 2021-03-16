
using BusinessLayer;
using EmailHandle;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataAccessLayer.Models;
using System.Configuration;

namespace RedSocial
{
    public partial class frmPasswordRestore : Form
    {

        UserService service;
        private readonly EmailSender _emailSender;
        public frmPasswordRestore()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString;
            SqlConnection _connection = new SqlConnection(connectionString);
            service = new UserService(_connection);
            _emailSender = new EmailSender();
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string mail = "";
            if (service.exist(txtUser.Text, txtEmail.Text))
            {
                if (service.existMail(txtUser.Text, txtEmail.Text))
                {
                    User Edit = new User();
                    Edit.UserName = txtEmail.Text;
                    Edit.Password = Password();


                    service.PasswordRestore(Edit);
                    _emailSender.SendMail(txtEmail.Text, "Nueva contrasena", "Su nueva contrasena es:" + Password());
                    MessageBox.Show("La nueva contrasena fue envia a su correo electronico");
                    this.Close();
                    frmLogin.Instance.Show();
                }
                else
                {
                    MessageBox.Show("Este Correo no pertenece a este usuario");
                }
            }
            else
            {
                MessageBox.Show("Este usuario no existe");
            }
        


        }

        private string Password()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[8];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);

            return finalString;

        }

        private void frmPasswordRestore_Load(object sender, EventArgs e)
        {

        }
    }
}