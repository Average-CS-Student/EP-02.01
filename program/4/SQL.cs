using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace _4
{
    internal class SQL
    {
        private static string connectionString = @"Data Source= ADCLG1; Initial catalog=♥; Integrated Security=True";
        private static SqlConnection MyConnection = new SqlConnection(connectionString);
        
        public static void ExecuteQuery(string query)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(query, MyConnection);
                MyConnection.Open();
                cmd.ExecuteNonQuery();
                MyConnection.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void ExecuteQuery<T>(string query, T[] data) where T : struct
        {
            try
            {
                SqlCommand cmd = new SqlCommand(query, MyConnection);
                for (int i = 0; i < data.Length; i++)
                {
                    cmd.Parameters.AddWithValue("@p" + i.ToString(), data[i]);
                }
                MyConnection.Open();
                cmd.ExecuteNonQuery();
                MyConnection.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static object ExecuteScalar(string query)
        {
            if (MyConnection.State == ConnectionState.Open)
                return null;

            try
            {
                MyConnection.Open();
                SqlCommand sqlCommand = new SqlCommand(query, MyConnection);
                object resut = sqlCommand.ExecuteScalar();
                return resut;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            finally
            { 
                MyConnection.Close();
            }
        }

        public static string[] GetTables()
        {
            MyConnection.Open();
            SqlCommand cmd = new SqlCommand($"SELECT S.name as Owner, T.name as TableName \r\nFROM  \r\n  sys.tables AS T\r\n    INNER JOIN sys.schemas AS S ON S.schema_id = T.schema_id\r\n    LEFT JOIN sys.extended_properties AS EP ON EP.major_id = T.[object_id]\r\nWHERE \r\n  T.is_ms_shipped = 0 AND \r\n  (EP.class_desc IS NULL OR (EP.class_desc <>'OBJECT_OR_COLUMN' AND \r\n  EP.[name] <> 'microsoft_database_tools_support'))", MyConnection);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataTable t = new DataTable(); 
            sqlDataAdapter.Fill(t);
            MyConnection.Close();
            return t.AsEnumerable().Select(r => r.Field<string>("TableName")).ToArray(); ;
        }

        public static void FillDataTable(DataTable data, string TableName)
        {
            try
            {
                MyConnection.Open();
                SqlCommand cmd = new SqlCommand($"select * from {TableName}", MyConnection);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                sqlDataAdapter.Fill(data);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                MyConnection.Close();
            }
        }

        public static void FillDataTableCustom(DataTable data, string query)
        {
            try
            {
                MyConnection.Open();
                SqlCommand cmd = new SqlCommand(query, MyConnection);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                sqlDataAdapter.Fill(data);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                MyConnection.Close();
            }
        }

        public static void UpdateData(DataTable data, string TableName)
        {
            try
            {
                MyConnection.Open();
                SqlCommand cmd = new SqlCommand($"select * from {TableName}", MyConnection);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                SqlCommandBuilder sqlBuilder = new SqlCommandBuilder(sqlDataAdapter);
                sqlDataAdapter.Update(data);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                MyConnection.Close();
            }
        }

        public static void UpdateDataCustom(DataTable data, string query)
        {
            try
            {
                MyConnection.Open();
                SqlCommand cmd = new SqlCommand(query, MyConnection);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                SqlCommandBuilder sqlBuilder = new SqlCommandBuilder(sqlDataAdapter);
                sqlDataAdapter.Update(data);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                MyConnection.Close();
            }
        }

        public static int Login(string login, string password, out int uid)
        {
            try
            {
                MyConnection.Open();
                SqlCommand cmd = new SqlCommand($"select role, id from Users where login = '{login}' and password = '{password}'", MyConnection);
                //object result = cmd.ExecuteScalar();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Access the values of the row
                        uid = (int)reader["id"];
                        return (int)reader["role"];
                    }
                    else
                    {
                        uid = -1;
                        return -1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                uid = -1;
                return -1;
            }
            finally
            {
                MyConnection.Close();
            }
        }

        public static string GetClientNameFromUId(int uid)
        {
            try
            {
                MyConnection.Open();
                SqlCommand cmd = new SqlCommand($"select fullname from Clients where userId = {uid}", MyConnection);
                object result = cmd.ExecuteScalar();
                return (string)result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return string.Empty;
            }
            finally
            {
                MyConnection.Close();
            }
        }

        public static int GetClientUId(string login)
        {
            try
            {
                MyConnection.Open();
                SqlCommand cmd = new SqlCommand($"select id from Users where login = '{login}'", MyConnection);
                object result = cmd.ExecuteScalar();
                return (int)result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
            finally
            {
                MyConnection.Close();
            }
        }

        public static string[] GetClientData(int cid)
        {
            string[] result = new string[6];
            try
            {
                MyConnection.Open();
                SqlCommand cmd = new SqlCommand($"select fullname, convert(varchar(10), dateOfBirth, 120) as dateOfBirth, phoneNumber, passportSeries, passportNumber, placeOfResidence from Clients where userId = {cid}", MyConnection);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Access the values of the row
                        result[0] = reader["fullname"].ToString();
                        result[1] = reader["dateOfBirth"].ToString();
                        result[2] = reader["phoneNumber"].ToString();
                        result[3] = reader["passportSeries"].ToString();
                        result[4] = reader["passportNumber"].ToString();
                        result[5] = reader["placeOfResidence"].ToString();
                        return result;
                    }
                    else return null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            finally
            {
                MyConnection.Close();
            }
        }

        public static string[,] GetStatistics()
        {
            string[,] result = new string[3, 2];
            result[0, 0] = "Законченые";
            result[1, 0] = "Продленные";
            result[2, 0] = "Использованные";
            try
            {
                MyConnection.Open();
                SqlCommand cmd = new SqlCommand("select (select count(*) from Statistic where result = 1), (select count(*) from Statistic where result = 2), (select count(*) from Statistic where result = 3)", MyConnection);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        result[0, 1] = reader[0].ToString();
                        result[1, 1] = reader[1].ToString();
                    }   result[2, 1] = reader[2].ToString();
                }
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            finally
            {
                MyConnection.Close();
            }
        }

        public static void FillCombobox(ComboBox element, string query)
        {
            try
            {
                MyConnection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(query, MyConnection);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                element.DisplayMember = "display";
                element.ValueMember = "value";
                element.DataSource = dt;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                MyConnection.Close();
            }
        }
    }
}
