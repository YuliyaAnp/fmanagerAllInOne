using System.Collections.Generic;
using fmanagerFull.Models;
using MySql.Data.MySqlClient;

namespace fmanagerFull
{
    public class SqlDbClient
    {
        public SqlDbClient(string databaseName, string userName, string password)
        {
            connstring = string.Format($"server=localhost;UID={userName};password={password}");
            this.databaseName = databaseName;
        }

        private readonly string connstring;
        private readonly string databaseName;

        public IList<TransactionRecord> GetTransactions()
        {
            var result = new List<TransactionRecord>();

            using (var connection = new MySqlConnection(connstring))
            {
                connection.Open();

                var commandString = $"SELECT * FROM {databaseName}.TestTransactions;";
                var command = new MySqlCommand(commandString, connection);
                command.ExecuteNonQuery();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var tr = new TransactionRecord
                        {
                            Id = reader.GetInt32(0),
                            Sum = reader.GetInt32(1),
                            Description = reader.GetString(2),
                            DateTime = reader.GetString(3),
                            AccountToIncreaseId = reader.GetInt32(4),
                            AccountToSubstractId = reader.GetInt32(5)
                        };
                        result.Add(tr);
                    }
                }
            }

            return result;
        }

		public IList<Account> GetAllAccounts()
        {
            var result = new List<Account>();

            using (var connection = new MySqlConnection(connstring))
            {
                connection.Open();

                var commandString = $"SELECT * FROM {databaseName}.TestAccounts;";
                var command = new MySqlCommand(commandString, connection);
                command.ExecuteNonQuery();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var ac = new Account();
                        ac.Id = reader.GetInt32(0);
                        ac.Balance = reader.GetInt32(1);
                        ac.Name = reader.GetString(2);
                        result.Add(ac);
                    }
                }
            }

            return result;
        }

        internal void InsertTransactionRecord(TransactionRecord transactionRecord)
        {
            using (var connection = new MySqlConnection(connstring))
            {
                connection.Open();

                var commandString =
                $"INSERT INTO {databaseName}.TestTransactions (sum, description, date, accounttoincreasename, accounttodecreasename) VALUES ({transactionRecord.Sum}, '{transactionRecord.Description}', '{transactionRecord.DateTime}', {transactionRecord.AccountToIncreaseId}, {transactionRecord.AccountToSubstractId});";
                var command = new MySqlCommand(commandString, connection);
                command.ExecuteNonQuery();
            }
        }

        public TransactionRecord GetTransactionById(int id)
		{
            var transactionRecord = new TransactionRecord();

		    using (var connection = new MySqlConnection(connstring))
		    {
		        connection.Open();

                var commandString = $"SELECT * FROM {databaseName}.TestTransactions WHERE Id = {id};";
		        var command = new MySqlCommand(commandString, connection);
		        command.ExecuteNonQuery();

		        using (var reader = command.ExecuteReader())
		        {
		            reader.Read();
		            transactionRecord.Id = reader.GetInt32(0);
		            transactionRecord.Sum = reader.GetInt32(1);
		            transactionRecord.Description = reader.GetString(2);
		            transactionRecord.DateTime = reader.GetString(3);
		            transactionRecord.AccountToIncreaseId = reader.GetInt32(4);
		            transactionRecord.AccountToSubstractId = reader.GetInt32(5);
		        }
		    }

            return transactionRecord;
		}

		public Account GetAccountById(int id)
        {
            var account = new Account();

            using (var connection = new MySqlConnection(connstring))
            {
                connection.Open();

                var commandString = $"SELECT * FROM {databaseName}.TestAccounts WHERE Id = {id};";
                var command = new MySqlCommand(commandString, connection);
                command.ExecuteNonQuery();

                using (var reader = command.ExecuteReader())
                {
                    reader.Read();
                    account.Id = reader.GetInt32(0);
                    account.Balance = reader.GetDouble(1);
                    account.Name = reader.GetString(2);
                }
            }

            return account;
        }

        public void UpdateAccount(Account account)
        {
            using (var connection = new MySqlConnection(connstring))
            {
                connection.Open();

                var commandString = $"UPDATE {databaseName}.TestAccounts SET Balance = {account.Balance} WHERE Id = {account.Id};";
                var command = new MySqlCommand(commandString, connection);
                command.ExecuteNonQuery();
            }
        }
    }
}
