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
    public partial class ClientMenu : Form
    {
        private int id;
        public ClientMenu(int id)
        {
            InitializeComponent();
            this.id = id;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClientAccount clientAccount = new ClientAccount(id);
            clientAccount.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dialres = MessageBox.Show("Отправить запрос на оформление страхования", "Запрос", MessageBoxButtons.YesNo);

            if (dialres == DialogResult.Yes)
            {
                SQL.ExecuteQuery($"insert into Notifications (message, client) values ('запрос на оформление страховки', (select top 1 id from Clients where userId = {id}))");
                MessageBox.Show("Запрос отправлен");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dialres = MessageBox.Show("Отправить запрос на продление страхования", "Запрос", MessageBoxButtons.YesNo);

            if (dialres == DialogResult.Yes)
            {
                SQL.ExecuteQuery($"insert into Notifications (message, client) values ('запрос на продление страховки', (select top 1 id from Clients where userId = {id}))");
                MessageBox.Show("Запрос отправлен");
            }
        }
    }
}
