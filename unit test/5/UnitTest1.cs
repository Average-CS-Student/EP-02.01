using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace _5
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ExecuteScalarFunctionTest()
        {
            int actual = (int)SQL.ExecuteScalar("select id from Users where login = 'employee1'");
            int expected = 4;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetTablesFunctionTest()
        {
            string[] actual = SQL.GetTables();
            string[] expected = { "Roles", "Users", "Clients", "Insurances", "Contracts", "Results", "Statistic", "Notifications" };

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void LoginFunctionTest1()
        {
            int actual = SQL.Login("admin", "12345678", out int uid);
            int expected = 1;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void LoginFunctionTest2()
        {
            int role = SQL.Login("client1", "asdfgh", out int actual);
            int expected = 5;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetClientNameFromUIdFunctionTest()
        {
            string actual = SQL.GetClientNameFromUId(6);
            string expected = "Solid Snake";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetClientUIdFunctionTest1()
        {
            int actual = SQL.GetClientUId("client1");
            int expected = 5;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetClientUIdFunctionTest2()
        {
            int actual = SQL.GetClientUId("client3");
            int expected = 7;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetClientDataFunctionTest1()
        {
            string[] actual = SQL.GetClientData(5);
            string[] expected = { "Ryan Gosling", "1980-01-01", "2534534543", "4356", "253463", ""};

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetClientDataFunctionTest2()
        {
            string[] actual = SQL.GetClientData(7);
            string[] expected = { "Silver", "2001-03-03", "3465363436", "6345", "234364", "Night City" };

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetStatisticsFunctionTest()
        {
            string[,] actual = SQL.GetStatistics();
            string[,] expected = { { "Законченые", "1" }, { "Продленные", "1" }, { "Использованные", "1" } };

            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
