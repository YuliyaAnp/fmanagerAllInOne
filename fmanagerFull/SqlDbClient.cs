
using System;
using System.Collections.Generic;
using fmanagerFull.Models;
using MySql.Data.MySqlClient;

namespace fmanagerFull
{
    public class SqlDbClient : IDisposable
    {
        public SqlDbClient(string databaseName, string userName, string password)
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

        public IList<TransactionRecord> GetTransactions()
        {
            var result = new List<TransactionRecord>();
            Connection.Open();

            string commandString = $"SELECT * FROM TestDatabase.TestTransactions;";
            var command = new MySqlCommand(commandString, Connection);
            command.ExecuteNonQuery();

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var tr = new TransactionRecord();
                    tr.Id = reader.GetInt32(0);
                    tr.Sum = reader.GetInt32(1);
                    tr.Description = reader.GetString(2);
                    tr.DateTime = DateTime.Parse(reader.GetString(3));
                    tr.AccountToIncreaseId = reader.GetInt32(4);
                    tr.AccountToSubstractId = reader.GetInt32(5);
                    result.Add(tr);
                }
            }
            Connection.Close();

            return result;
        }

        public Account GetAccountById(int id)
        {
            Connection.Open();

            string commandString = $"SELECT * FROM TestDatabase.TestAccounts WHERE Id = {id};";
            var command = new MySqlCommand(commandString, Connection);
            command.ExecuteNonQuery();

            var account = new Account();

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                reader.Read();
                account.Id = reader.GetInt32(0);
                account.Balance = reader.GetInt32(1);
                account.Name = reader.GetString(2);
            }

            Connection.Close();

            return account;
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

            string commandString = "INSERT INTO TestDatabase.TestAccounts VALUES (1, 0, 'Cafe'), (2, 0, 'Salary')";
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
