
using System;
using System.Collections.Generic;
using fmanagerFull.Models;
using MySql.Data.MySqlClient;

namespace fmanagerFull
{
    public class TestSqlDbClient : IDisposable
    {
        public TestSqlDbClient(string databaseName, string userName, string password)
        {
            string connstring = string.Format($"server=localhost;UID={userName};password={password}");
            Connection = new MySqlConnection(connstring);
            this.databaseName = databaseName;
        }

        public MySqlConnection Connection { get; private set; }

        private string databaseName;

        public void CreateTestDatabase()
        {
            Connection.Open();

            string commandString = $"CREATE DATABASE IF NOT EXISTS `{databaseName}`;";
            var command = new MySqlCommand(commandString, Connection);
            command.ExecuteNonQuery();

            Connection.Close();
        }

        public void CreateTestTransactionTable()
        {
            Connection.Open();

            string commandString = "CREATE TABLE IF NOT EXISTS TestDatabase.TestTransactions (id INT, sum INT, description NVARCHAR(200), datetime DATE, accounttoincreasename INT, accounttodecreasename INT);";
            var command = new MySqlCommand(commandString, Connection);
            command.ExecuteNonQuery();

            Connection.Close();
        }

        public void InsertTestTransactions()
        {
            Connection.Open();

            string commandString = "INSERT INTO TestDatabase.TestTransactions VALUES (1, -10, 'McDonalds', '2018-04-28', '1', '2')";
            var command = new MySqlCommand(commandString, Connection);
            command.ExecuteNonQuery();

            Connection.Close();
        }

        public void CreateTestAccountsTable()
        {
            Connection.Open();

            string commandString = "CREATE TABLE IF NOT EXISTS TestDatabase.TestAccounts (id INT, balance DOUBLE, name NVARCHAR(200));";
            var command = new MySqlCommand(commandString, Connection);
            command.ExecuteNonQuery();

            Connection.Close();
        }

        public void InsertTestAccounts()
        {
            Connection.Open();

            string commandString = "INSERT INTO TestDatabase.TestAccounts VALUES (1, 0, 'Cafe'), (2, 0, 'Salary'), (3, 0, 'Debit card')";
            var command = new MySqlCommand(commandString, Connection);
            command.ExecuteNonQuery();

            Connection.Close();
        }

        public void CleanTransactionsTable()
        {
            Connection.Open();

            string commandString = "DROP TABLE TestDatabase.TestTransactions";
            var command = new MySqlCommand(commandString, Connection);
            command.ExecuteNonQuery();

            Connection.Close();
        }

        public void CleanAccountsTable()
        {
            Connection.Open();

            string commandString = "DROP TABLE TestDatabase.TestAccounts";
            var command = new MySqlCommand(commandString, Connection);
            command.ExecuteNonQuery();

            Connection.Close();
        }

        public void DropTestDatabase()
        {
            Connection.Open();

            string commandString = "DROP DATABASE TestDatabase;";
            var command = new MySqlCommand(commandString, Connection);
            command.ExecuteNonQuery();

            Connection.Close();
        }

        public void Dispose()
        {
            Connection.Dispose();
        }
    }
            
}
