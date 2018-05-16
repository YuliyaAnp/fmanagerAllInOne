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

		public IList<Account> GetAllAccounts()
        {
            var result = new List<Account>();
            Connection.Open();

            string commandString = $"SELECT * FROM TestDatabase.TestAccounts;";
            var command = new MySqlCommand(commandString, Connection);
            command.ExecuteNonQuery();

            using (MySqlDataReader reader = command.ExecuteReader())
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
            Connection.Close();

            return result;
        }

        internal void InsertTransactionRecord(TransactionRecord transactionRecord)
        {
            throw new NotImplementedException();
        }

        public TransactionRecord GetTransactionById(int id)
		{
            var transactionRecord = new TransactionRecord();
            Connection.Open();

            string commandString = $"SELECT * FROM TestDatabase.TestTransactions WHERE Id = {id};";
            var command = new MySqlCommand(commandString, Connection);
            command.ExecuteNonQuery();

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                reader.Read();
                transactionRecord.Id = reader.GetInt32(0);
                transactionRecord.Sum = reader.GetInt32(1);
                transactionRecord.Description = reader.GetString(2);
                transactionRecord.DateTime = DateTime.Parse(reader.GetString(3));
                transactionRecord.AccountToIncreaseId = reader.GetInt32(4);
                transactionRecord.AccountToSubstractId = reader.GetInt32(5);
            }
            Connection.Close();

            return transactionRecord;
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
                account.Balance = reader.GetDouble(1);
                account.Name = reader.GetString(2);
            }

            Connection.Close();

            return account;
        }

        public void Dispose()
        {
            Connection.Dispose();
        }
    }
            
}
