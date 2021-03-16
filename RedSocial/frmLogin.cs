using BusinessLayer;
using DataAccessLayer.Models;
using EmailHandle;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RedSocial
{
    public partial class frmLogin : Form
    {
        private readonly IUserService<User> _service;
        private readonly EmailSender _emailSender;
        public string username;
        public string password;

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        public static frmLogin Instance;
        public frmLogin()
        {
            Instance = this;
            InitializeComponent();
            string connectionString = ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString;
            SqlConnection _connection = new SqlConnection(connectionString);
            _service = new UserService(_connection);
            _emailSender = new EmailSender();
            LoadPreviousSession();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            valid();
        }
        void valid()
        {
            var exist = _service.Get(txtUserName.Text, null);
            var existPass = _service.validPassWord(txtUserName.Text, txtPassword.Text);

            if (txtUserName.Text != "Usuario")
            {
                if (txtPassword.Text != "contrasena")
                {
                    if (exist)
                    {
                        if (existPass)
                        {
                            MessageBox.Show("Usted ha iniciado seccion");
                            frmHome frmHome = new frmHome();
                            frmHome.Username = txtUserName.Text;
                            this.Hide();
                            frmHome.Show();
                        }
                        else
                        {
                            MessageBox.Show("Contraseña Incorrecta");
                        }
                    }
                    else
                    {
                        var result = MessageBox.Show("Este usuario no se encuentra en el sistema, ¿Desea Registrarse?", "Registrar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            frmRegister registration = new frmRegister();
                            this.Hide();
                            registration.Show();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Ingresar una contrasena");
                }
            }
            else
            {
                MessageBox.Show("Ingresar nombre de usuario");
            }
        }
        private void LoadPreviousSession()
        {
            if (_service.LoadSession()) SuccessLogin();
            else this.Show();
        }

        private void SuccessLogin()
        {
            frmHome home = new frmHome();

            home.Show();
        }
        protected override void SetVisibleCore(bool value)
        {
            if (!this.IsHandleCreated)
            {
                this.CreateHandle();
                value = false;
            }
            base.SetVisibleCore(value);
        }

        private void tableLayoutPanel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmRegister frmRegister = new frmRegister();
            this.Hide();
            frmRegister.Show();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmPasswordRestore restore = new frmPasswordRestore();
            this.Hide();
            restore.Show();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            txtUserName.Text = username;
            txtPassword.Text = password;
        }
    }
}