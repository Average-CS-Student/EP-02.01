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
    public partial class ClientAccount : Form
    {
        private int id;
        public ClientAccount(int id)
        {
            InitializeComponent();

            string[] data = SQL.GetClientData(id);
            label12.Text = data[0];
            label11.Text = data[1];
            label10.Text = data[2];
            label9.Text = data[3];
            label8.Text = data[4];
            label7.Text = data[5];

            this.id = id;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dialres = MessageBox.Show("Отправить запрос на изменение личных данных", "Изменение личных данных", MessageBoxButtons.YesNo);

            if (dialres == DialogResult.Yes)
            {
                SQL.ExecuteQuery($"insert into Notifications (message, client) values ('запрос на обновление личных данных', (select top 1 id from Clients where userId = {id}))");
                MessageBox.Show("Запрос отправлен");
            }
        }
    }
}
