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
    public partial class EmployeeNotifications : Form
    {
        public EmployeeNotifications()
        {
            InitializeComponent();

            UpdateTable();

            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            dataGridView1.CellClick += dataGridView_CellClick;
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                DialogResult dialres = MessageBox.Show("Пометить как выполненое", "", MessageBoxButtons.YesNo);

                if (dialres == DialogResult.Yes)
                {
                    SQL.ExecuteQuery($"update Notifications set isDone = 1 where id = {dataGridView1.Rows[e.RowIndex].Cells[0].Value}");
                    MessageBox.Show("Помечено как выполненое");
                    UpdateTable();
                }
            }
        }

        private void UpdateTable()
        {
            DataTable dt = new DataTable();
            SQL.FillDataTableCustom(dt, $"select Notifications.id, message, contract, fullname, isDone from Notifications inner join Clients on Notifications.client = Clients.id where isDone = 0");
            dataGridView1.DataSource = dt;
        }
    }
}
