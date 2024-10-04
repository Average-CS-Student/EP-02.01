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
    public partial class EmployeeMain : Form
    {
        private const string ClientQuery = "select fullname, dateOfBirth, phoneNumber, passportSeries, passportNumber, placeOfResidence, trustFactor from Clients";
        private string currentQuery = ClientQuery;

        public EmployeeMain()
        {
            InitializeComponent();
        }

        public EmployeeMain(string loginRole)
        {
            InitializeComponent();
            toolStripLabel1.Text = loginRole;
            UpdateTables();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EmployeeAddClient employeeAddClient = new EmployeeAddClient();
            employeeAddClient.ShowDialog();
            UpdateTables();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            EmployeeMakeWaranty employeeMakeWaranty = new EmployeeMakeWaranty();
            employeeMakeWaranty.ShowDialog();
            UpdateTables();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == string.Empty && textBox2.Text == string.Empty) return;
            string query = ClientQuery + " where";
            if (textBox1.Text != string.Empty)
            {
                query += $" fullname Like '%{textBox1.Text}%'";
                if (textBox2.Text != string.Empty)
                    query += " and";
            }
            if (textBox2.Text != string.Empty)
                query += $" phoneNumber Like '%{textBox2.Text}%'";
            currentQuery = query;
            UpdateTables();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            currentQuery = ClientQuery;
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            UpdateTables();
        }

        private void UpdateTables()
        {
            DataTable table1 = new DataTable();
            SQL.FillDataTableCustom(table1, currentQuery);
            dataGridView1.DataSource = table1;
            DataTable table2 = new DataTable();
            SQL.FillDataTableCustom(table2, "select Insurances.name as insurance, Clients.fullname as client, startDate, endDate, Contracts.price from Contracts inner join Clients on Contracts.client = Clients.id inner join Insurances on Contracts.insurance = Insurances.id");
            dataGridView2.DataSource = table2;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            EmployeeNotifications employeeNotifications = new EmployeeNotifications();
            employeeNotifications.ShowDialog();
        }
    }
}
