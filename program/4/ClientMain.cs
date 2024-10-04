using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _4
{
    public partial class ClientMain : Form
    {
        private int userId;

        public ClientMain()
        {
            InitializeComponent();
        }

        public ClientMain(string clientName, int id)
        {
            InitializeComponent();
            toolStripLabel1.Text = clientName;
            DataTable dt = new DataTable();
            SQL.FillDataTableCustom(dt, $"select Insurances.name as insurance, startDate, endDate, Contracts.price from Contracts inner join Clients on Contracts.client = Clients.id inner join Insurances on Contracts.insurance = Insurances.id where userId = {id}");
            dataGridView1.DataSource = dt;
            userId = id;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ClientNotifications clientNotifications = new ClientNotifications(userId);
            clientNotifications.ShowDialog();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            ClientMenu clientMenu = new ClientMenu(userId);
            clientMenu.ShowDialog();
        }
    }
}
