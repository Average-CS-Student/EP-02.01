using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _4
{
    public partial class EmployeeMakeWaranty : Form
    {
        private float price;

        public EmployeeMakeWaranty()
        {
            InitializeComponent();
            SQL.FillCombobox(comboBox1, "select fullname as display, id as value from Clients");
            SQL.FillCombobox(comboBox2, "select name as display, id as value from Insurances");
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            CalculatePrice();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SQL.ExecuteQuery($"insert into Contracts values ({comboBox2.SelectedValue}, {comboBox1.SelectedValue}, '{DateTime.Now.ToString("yyyy/MM/dd")}', '{DateTime.Now.AddMonths((int)numericUpDown1.Value).ToString("yyyy/MM/dd")}', {price})");
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void UpdatePrice(object sender, EventArgs e)
        {
            CalculatePrice();
        }

        private void CalculatePrice()
        {
            object res = SQL.ExecuteScalar($"select price from Insurances where id = {comboBox2.SelectedValue}");
            if (res != null)
            {
                price = float.Parse(res.ToString()) * (float)numericUpDown1.Value;
            }
            else
            {
                return;
            }
            label4.Text = $"Финальная стоимость: {price}";
        }
    }
}
