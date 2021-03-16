using BusinessLayer;
using DataAccessLayer;
using Microsoft.VisualBasic;
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
    public partial class frmEvents : Form
    {
        public string userName;
        private SqlConnection _connection;
        private readonly IService<Event> _service;
        private FriendService _innerFriendService;
        private EventService _innerService;
        public frmEvents()
        {
            InitializeComponent();
            string connectionString = ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString;
            _connection = new SqlConnection(connectionString);
            _innerService=new EventService(_connection);
            _service = new EventService(_connection);
            _innerFriendService = new FriendService(_connection);
        }

        private void cerrarSesionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmHome login = new frmHome();
            login.Username = userName;
            this.Hide();
            login.Show();
        }

        private void frmEvents_Load(object sender, EventArgs e)
        {
            loadEvent();
            loadFriendsEvent();
        }
      
        public void Add()
        {
     
            string theDate = eventPicker.Value.ToString("yyyy/MM/dd");
            if (eventPicker.Value.ToString("yyyy/MM/dd") == DateTime.Now.ToString("yyyy/MM/dd"))
            {
                MessageBox.Show("No puede elegir la fecha de hoy");
                return;
            }
            Event event1 = new Event(txtEventName.Text, Convert.ToDateTime(theDate), (int)EventState.InProcess,userName,txtPlace.Text);
            _service.Add(event1);

            loadEvent();
      
        }
        public void addInvitation(object sender, EventArgs e)
        {
            var entry = Interaction.InputBox("Nombre del Usuario", "Usuario", "");
            if (entry == userName)
            {
                MessageBox.Show("No puede Auto-Invitarse");
                return;
            }

            if (String.IsNullOrEmpty(entry))
            {
                MessageBox.Show("Debe colocar un usuario");
            }
            else
            {
                userEvent ue = sender as userEvent;
                if (_innerFriendService.exist(userName, entry))
                {
                    if (!_innerService.exist(entry, ue.Id))
                    {
                        Event invitation = new Event(ue.Id, entry, 0);
                        _innerService.AddEvenInvitation(invitation);
                        MessageBox.Show("Invitacion Enviada");
                    }
                    else
                    {
                        MessageBox.Show("Este usuario ya fue invitado");
                    }
                }
                else
                {
                    MessageBox.Show("Este usuario no existe en su lista de amigos");
                }
                loadEvent();
                loadFriendsEvent();
            }
        }

        public void Delete(object sender, EventArgs e)
        {
        
         userEvent ue = sender as userEvent;

         _service.Delete(ue.Id);
          MessageBox.Show("Evento Eliminado");
            
           loadEvent();
            
        }
        public void DeleteFriend(object sender, EventArgs e)
        {
            var entry = Interaction.InputBox("Nombre del Usuario", "Usuario", "");
            userEvent ue = sender as userEvent;
            if (String.IsNullOrEmpty(entry))
            {
                MessageBox.Show("Debe colocar un usuario");
            }
            else
            {
                if (_innerFriendService.exist(userName, entry))
                {
                    _innerService.DeleteFromEvent(entry, ue.Id);
                    MessageBox.Show("Usuario Eliminado");
                }
                else
                {
                    MessageBox.Show("Este usuario no existe en su lista de amigos");
                }
            }
   
          

            loadEvent();
            loadFriendsEvent();
            flowLayoutPanel2.Controls.Clear();


        }
        public void show()
        {

        }
        public void showInvitedFriends(object sender, EventArgs e)
        {
            flowLayoutPanel2.Controls.Clear();
            userEvent ue = sender as userEvent;
            DataSet ds = _innerService.GetFriendsList(ue.Id);

            eventsFriends[] listItems = new eventsFriends[_innerService.GetfriendsbyId(ue.Id)];
            for (int i = 0; i < listItems.Length; i++)
            {
                listItems[i] = new eventsFriends();
                listItems[i].Profile = ds.Tables[0].Rows[i]["Photo"].ToString().Trim();
                listItems[i].UserName = ds.Tables[0].Rows[i]["UserName"].ToString().Trim();
                listItems[i].FriendName = ds.Tables[0].Rows[i]["Name"].ToString().Trim() + " " + ds.Tables[0].Rows[i]["LastName"].ToString().Trim();
            
                switch(Convert.ToInt32(ds.Tables[0].Rows[i]["Accept"].ToString().Trim()))
                {
                    case 1:

                        listItems[i].Answer = "Asistira";
                        break;
                    case 2:
                        listItems[i].Answer = "No Asistira";
                        break;
                    default:
                        listItems[i].Answer = "Sin Respuesta";
                        break;
                }
               
                flowLayoutPanel2.Controls.Add(listItems[i]);

              
            }
            gbInvitedFriends.Visible = true;
        }

            void loadEvent()
            {
            flowLayoutPanel1.Controls.Clear();
            DataSet ds = _service.GetAll(userName);
            userEvent[] listItems = new userEvent[_innerService.GetbyId(userName)];
            for (int i = 0; i < listItems.Length; i++)
            {
                listItems[i] = new userEvent();
                listItems[i].Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Id"].ToString().Trim());
                listItems[i].EventName = ds.Tables[0].Rows[i]["Name"].ToString().Trim();
                listItems[i].EventPlace = ds.Tables[0].Rows[i]["Place"].ToString().Trim();
                listItems[i].Quantity = _innerService.GetInvitations(listItems[i].Id).ToString();
                //switch (Convert.ToInt32(ds.Tables[0].Rows[i]["State"].ToString().Trim()))
                //{
                //    case 1:
                //        listItems[i].EventState = "En Proceso";
                //        break;
                //}
                if (DateTime.Now.ToString("yyyy/MM/dd")== ds.Tables[0].Rows[i]["CreationDate"].ToString().Trim())
                {
                    listItems[i].EventState = "Evento ha Iniciado";
                }else if(Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd"))> Convert.ToDateTime(ds.Tables[0].Rows[i]["CreationDate"].ToString().Trim()))
                {
                    listItems[i].EventState = "Finalizado";
                    listItems[i].ButtonInvite.Visible = false;
                }
                listItems[i].EventDate = ds.Tables[0].Rows[i]["CreationDate"].ToString().Trim();
                flowLayoutPanel1.Controls.Add(listItems[i]);
                listItems[i].ButtonClick += new EventHandler(addInvitation);
                listItems[i].ShowButtonClick += new EventHandler(showInvitedFriends);
                listItems[i].DeleteButtonClick += new EventHandler(Delete);
                listItems[i].DeleteFriendButtonClick += new EventHandler(DeleteFriend);

            }
        }
        void loadFriendsEvent()
        {
            flowLayoutPanel3.Controls.Clear();
            DataSet ds = _innerService.GetFriendEventList(userName);
            userFriendsEvents[] listItems = new userFriendsEvents[_innerService.GetEventsFriendsCountById(userName)];
            for (int i = 0; i < listItems.Length; i++)
            {
                listItems[i] = new userFriendsEvents();
                listItems[i].Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Id"].ToString().Trim());
                listItems[i].EventName = ds.Tables[0].Rows[i]["Name"].ToString().Trim();
                listItems[i].EventPlace = ds.Tables[0].Rows[i]["Place"].ToString().Trim();
                listItems[i].Quantity = _innerService.GetInvitations(listItems[i].Id).ToString();
                listItems[i].UserName = ds.Tables[0].Rows[i]["UserName"].ToString().Trim();
                switch (Convert.ToInt32(ds.Tables[0].Rows[i]["State"].ToString().Trim()))
                {
                    case 1:
                        listItems[i].EventState = "En Proceso";
                        break;
                    case 2:
                        listItems[i].EventState = "Finalizado";
                        break;
                }
                listItems[i].EventDate = ds.Tables[0].Rows[i]["CreationDate"].ToString().Trim();
                flowLayoutPanel3.Controls.Add(listItems[i]);
                listItems[i].ButtonClickYes += new EventHandler(AddYes);
                listItems[i].ButtonClickNo += new EventHandler(AddNo);
            }
        }
        private void AddYes(object sender, EventArgs e)
        {
            userFriendsEvents ue = sender as userFriendsEvents;
            _innerService.UpdateInvitation(1, ue.Id);
            MessageBox.Show("Repuesta Enviada");
        }
        private void AddNo(object sender, EventArgs e)
        {
            userFriendsEvents ue = sender as userFriendsEvents;
            _innerService.UpdateInvitation(2, ue.Id);
            MessageBox.Show("Repuesta Enviada");
        }

        private void btnCreateEvent_Click_1(object sender, EventArgs e)
        {
            Add();
        }

        private void flowLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
