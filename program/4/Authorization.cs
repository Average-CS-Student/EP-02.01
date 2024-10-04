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
    public partial class Authorization : Form
    {
        public Authorization()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string login = textBox1.Text;
            string password = textBox2.Text;

            int uid;
            int result = SQL.Login(login, password, out uid);

            if (result == -1)
            {
                MessageBox.Show("Login or password are wrong");
                return;
            }

            switch (result)
            {
                case 1:
                    Program.Context.MainForm = new AdminMain($"{login}, Admin");
                    this.Close();
                    Program.Context.MainForm.Show();
                    break;
                case 2:
                    Program.Context.MainForm = new EmployeeMain($"{login}, Employee");
                    this.Close();
                    Program.Context.MainForm.Show();
                    break;
                case 3:
                    string cname = SQL.GetClientNameFromUId(uid);
                    Program.Context.MainForm = new ClientMain(cname, uid);
                    this.Close();
                    Program.Context.MainForm.Show();
                    break;
                default:
                    MessageBox.Show("Error");
                    break;
            }
        }
    }
}
