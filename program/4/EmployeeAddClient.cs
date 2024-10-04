using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _4
{
    public partial class EmployeeAddClient : Form
    {
        public EmployeeAddClient()
        {
            InitializeComponent();
            maskedTextBox2.Mask = "0000/00/00";
            maskedTextBox3.Mask = "0-(000)-000-00-00";
            maskedTextBox4.Mask = "0000";
            maskedTextBox5.Mask = "000000";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] data = { textBox1.Text, Regex.Replace(maskedTextBox2.Text, "[^0-9/]", ""), Regex.Replace(maskedTextBox3.Text, "[^0-9]", ""), Regex.Replace(maskedTextBox4.Text, "[^0-9]", ""), Regex.Replace(maskedTextBox5.Text, "[^0-9]", ""), textBox6.Text };
            if ( data[0] == string.Empty || data[1].Length != 10 || data[2].Length != 11 ||
                 data[3].Length != 4 || data[4].Length != 6)
                return;

            string _chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789^&*_+";
            int _passwordLength = 8;

            var rng = new RNGCryptoServiceProvider();
            var bytes = new byte[_passwordLength];
            rng.GetBytes(bytes);

            string password = "";
            for (var i = 0; i < _passwordLength; i++)
            {
                password += _chars[bytes[i] % _chars.Length];
            }

            SQL.ExecuteQuery($"insert into Users values ('{maskedTextBox4.Text}_{maskedTextBox5.Text}', '{password}', 3)");
            int uid = SQL.GetClientUId($"{maskedTextBox4.Text}_{maskedTextBox5.Text}");
            if (uid != -1)
            {
                SQL.ExecuteQuery($"insert into clients values ('{textBox1.Text}', {uid}, '{maskedTextBox2.Text}', '{Regex.Replace(maskedTextBox3.Text, "[^0-9]", "")}', '{maskedTextBox4.Text}', '{maskedTextBox5.Text}', '{textBox6.Text}', 6)");
            }
            MessageBox.Show($"Login: {maskedTextBox4.Text}_{maskedTextBox5.Text}\n\rPassword: {password}");

            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
