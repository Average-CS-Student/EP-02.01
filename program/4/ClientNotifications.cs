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
    public partial class ClientNotifications : Form
    {
        public ClientNotifications(int id)
        {
            InitializeComponent();

            DataTable dt = new DataTable();
            SQL.FillDataTableCustom(dt, $"select Contracts.id, Insurances.name as insurance, startDate, endDate, Contracts.price from Contracts inner join Clients on Contracts.client = Clients.id inner join Insurances on Contracts.insurance = Insurances.id where userId = {id} and endDate between GETDATE() and cast(DATEADD(week, 1, GETDATE()) as date)");
            dataGridView1.DataSource = dt;

            dataGridView1.Columns.Insert(5, new DataGridViewButtonColumn() { UseColumnTextForButtonValue = true, Text = "Продлить" });

            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            dataGridView1.CellClick += dataGridView_CellClick;
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                DialogResult dialres = MessageBox.Show("Отправить запрос на продление страхования", "Продление", MessageBoxButtons.YesNo);

                if (dialres == DialogResult.Yes)
                {
                    SQL.ExecuteQuery($"insert into Notifications (message, contract) values ('запрос на продление страховки', {dataGridView1.Rows[e.RowIndex].Cells[1].Value})");
                }
            }
        }
    }
}
