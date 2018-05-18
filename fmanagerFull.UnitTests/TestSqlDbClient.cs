using System;
using MySql.Data.MySqlClient;

namespace fmanagerFull.UnitTests
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

        private readonly string databaseName;

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

            string commandString = "CREATE TABLE IF NOT EXISTS TestDatabase.TestTransactions (id INT NOT NULL AUTO_INCREMENT, sum INT, description NVARCHAR(200), date NVARCHAR(50), accounttoincreasename INT, accounttodecreasename INT, PRIMARY KEY (id));";
            var command = new MySqlCommand(commandString, Connection);
            command.ExecuteNonQuery();

            Connection.Close();
        }

        public void InsertTestTransactions()
        {
            Connection.Open();

            string commandString = "INSERT INTO TestDatabase.TestTransactions (sum, description, date, accounttoincreasename, accounttodecreasename) VALUES (-10, 'McDonalds', '2018-04-28', '1', '2')";
            var command = new MySqlCommand(commandString, Connection);
            command.ExecuteNonQuery();

            Connection.Close();
        }

        public void CreateTestAccountsTable()
        {
            Connection.Open();

            string commandString = "CREATE TABLE IF NOT EXISTS TestDatabase.TestAccounts (id INT NOT NULL AUTO_INCREMENT, balance DOUBLE, name NVARCHAR(200), PRIMARY KEY (id));";
            var command = new MySqlCommand(commandString, Connection);
            command.ExecuteNonQuery();

            Connection.Close();
        }

        public void InsertTestAccounts()
        {
            Connection.Open();

            string commandString = "INSERT INTO TestDatabase.TestAccounts (balance, name) VALUES (0, 'Cafe'), (0, 'Salary'), (0, 'Debit card')";
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
