using System;
using MySql.Data.MySqlClient;

namespace fmanagerFull.UnitTests
{
    public class TestSqlDbClient
    {
        public TestSqlDbClient(string databaseName, string userName, string password)
        {
            connstring = string.Format($"server=localhost;UID={userName};password={password}");
            this.databaseName = databaseName;
        }

        private readonly string connstring;
        private readonly string databaseName;

        public void CreateTestDatabase()
        {
            using (var connection = new MySqlConnection(connstring))
            {
                connection.Open();

                string commandString = $"CREATE DATABASE IF NOT EXISTS `{databaseName}`;";
                var command = new MySqlCommand(commandString, connection);
                command.ExecuteNonQuery();
            }
        }

        public void CreateTestTransactionTable()
        {
            using (var connection = new MySqlConnection(connstring))
            {
                connection.Open();

                string commandString = "CREATE TABLE IF NOT EXISTS TestDatabase.TestTransactions (id INT NOT NULL AUTO_INCREMENT, sum INT, description NVARCHAR(200), date NVARCHAR(50), accounttoincreasename INT, accounttodecreasename INT, PRIMARY KEY (id));";
                var command = new MySqlCommand(commandString, connection);
                command.ExecuteNonQuery();
            }
        }

        public void InsertTestTransactions()
        {
            using (var connection = new MySqlConnection(connstring))
            {
                connection.Open();

                string commandString = "INSERT INTO TestDatabase.TestTransactions (sum, description, date, accounttoincreasename, accounttodecreasename) VALUES (-10, 'McDonalds', '2018-04-28', '1', '2')";
                var command = new MySqlCommand(commandString, connection);
                command.ExecuteNonQuery();
            }
        }

        public void CreateTestAccountsTable()
        {
            using (var connection = new MySqlConnection(connstring))
            {
                connection.Open();

                string commandString = "CREATE TABLE IF NOT EXISTS TestDatabase.TestAccounts (id INT NOT NULL AUTO_INCREMENT, balance DOUBLE, name NVARCHAR(200), PRIMARY KEY (id));";
                var command = new MySqlCommand(commandString, connection);
                command.ExecuteNonQuery();
            }
        }

        public void InsertTestAccounts()
        {
            using (var connection = new MySqlConnection(connstring))
            {
                connection.Open();

                string commandString = "INSERT INTO TestDatabase.TestAccounts (balance, name) VALUES (0, 'Cafe'), (0, 'Salary'), (0, 'Debit card')";
                var command = new MySqlCommand(commandString, connection);
                command.ExecuteNonQuery();
            }
        }

        public void CleanTransactionsTable()
        {
            using (var connection = new MySqlConnection(connstring))
            {
                connection.Open();

                string commandString = "DROP TABLE TestDatabase.TestTransactions";
                var command = new MySqlCommand(commandString, connection);
                command.ExecuteNonQuery();
            }
        }

        public void CleanAccountsTable()
        {
            using (var connection = new MySqlConnection(connstring))
            {
                connection.Open();

                string commandString = "DROP TABLE TestDatabase.TestAccounts";
                var command = new MySqlCommand(commandString, connection);
                command.ExecuteNonQuery();
            }
        }

        public void DropTestDatabase()
        {
            using (var connection = new MySqlConnection(connstring))
            {
                connection.Open();

                string commandString = "DROP DATABASE TestDatabase;";
                var command = new MySqlCommand(commandString, connection);
                command.ExecuteNonQuery();
            }
        }
    }
            
}
