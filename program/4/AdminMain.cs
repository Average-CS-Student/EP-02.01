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
    public partial class AdminMain : Form
    {
        public AdminMain()
        {
            InitializeComponent();
            TabPage tp = new TabPage("Test");
            tabControl1.TabPages.Add(tp);

            TextBox tb = new TextBox();
            tb.Dock = DockStyle.Fill;
            tb.Multiline = true;

            tp.Controls.Add(tb);
        }

        public AdminMain(string loginRole)
        {
            InitializeComponent();
            toolStripLabel1.Text = loginRole;
            LoadTables();
            //UpdateTables();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            AdminMenu adminMenu = new AdminMenu();
            adminMenu.ShowDialog();
        }

        private void LoadTables()
        {
            string[] tables = SQL.GetTables();
            foreach (string table in tables)
            {
                TabPage tp = new TabPage(table);
                tabControl1.TabPages.Add(tp);
                DataGridView dgv = new DataGridView();
                dgv.Validated += DataGridViewChanged;
                dgv.Dock = DockStyle.Fill;
                dgv.BackgroundColor = Color.FromArgb(255, 30, 33, 36);
                tp.Controls.Add(dgv);
                DataTable dt = new DataTable();
                SQL.FillDataTable(dt, table);
                dgv.DataSource = dt;
            }
        }

        private void UpdateTables()
        {
            foreach (TabPage page in tabControl1.TabPages)
            {
                DataTable dt = new DataTable();
                SQL.FillDataTable(dt, page.Text);
                foreach (DataGridView dgv in page.Controls)
                {
                    dgv.DataSource = dt;
                }
            }
        }

        private void DataGridViewChanged(object sender, EventArgs e)
        {
            SQL.UpdateData(((DataGridView)sender).DataSource as DataTable, tabControl1.SelectedTab.Text);
        }

    }
}
