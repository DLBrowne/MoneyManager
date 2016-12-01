﻿using DataAbstractionLayerSQLite;
using SQLitePCL;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MoneyManager_BL_DAL
{
    class AccountDAL
    {
        private static string databaseFile = "moneymanager.db";

        private static void Mapping(ISQLiteStatement statement, Account e)
        {
            e.id = (long)statement["id"];
            e.name = (string)statement["name"];
            e.balance = (double)statement["balance"];
            e.user_id = (long)statement["user_id"];
        }

        public static void CreateTable()
        {
            string sql = @"CREATE TABLE IF NOT EXISTS ""account""(
                            ""id"" INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
                            ""name"" VARCHAR(45) NOT NULL,
                            ""balance"" FLOAT NOT NULL,
                            ""user_id"" INTEGER NOT NULL,
                            CONSTRAINT ""name_UNIQUE"" UNIQUE(""name""),
                            CONSTRAINT ""fk_account_user1"" FOREIGN KEY(""user_id"") REFERENCES ""user""(""id"")
                            ON UPDATE CASCADE ON DELETE CASCADE
                         ); ";

            DB db = DB.getDB(databaseFile);
            db.NonQuery(sql);
        }

        public static void Create(Account account)
        {
            string sql = @"INSERT INTO account (name, balance, user_id) 
                            VALUES (@name, @balance, @user_id)";

            DB db = DB.getDB(databaseFile);
            Dictionary<string, object> parms = new Dictionary<string, object>();

            parms.Add("@name", account.name);
            parms.Add("@balance", account.balance);
            parms.Add("@user_id", account.user_id);
            if (db.NonQuery(sql, parms)) account.id = db.LastId();
        }

        public static void Update(Account account)
        {
            string sql = @"UPDATE account
                            SET name=@name, balance=@balance, user_id=@user_id
                            WHERE id=@id";

            DB db = DB.getDB(databaseFile);
            Dictionary<string, object> parms = new Dictionary<string, object>();

            parms.Add("@name", account.name);
            parms.Add("@balance", account.balance);
            parms.Add("@user_id", account.user_id);
            parms.Add("@id", account.id);
            db.NonQuery(sql, parms);
        }

        public static void Delete(Account account)
        {
            string sql = @"DELETE FROM account WHERE id=@id";

            DB db = DB.getDB(databaseFile);
            Dictionary<string, object> parms = new Dictionary<string, object>();

            parms.Add("@id", account.id);
            db.NonQuery(sql, parms);
        }

        public static ObservableCollection<Account> RetrieveAll()
        {
            ObservableCollection<Account> res = new ObservableCollection<Account>();

            string sql = "SELECT * FROM account";
            DB db = DB.getDB(databaseFile);
            ISQLiteStatement statement = db.Query(sql);

            while (statement.Step() == SQLiteResult.ROW)
            {
                Account e = new Account();

                Mapping(statement, e);
                res.Add(e);
            }

            return (res);
        }

        public static ObservableCollection<Account> RetrieveByName(string name)
        {
            ObservableCollection<Account> res = new ObservableCollection<Account>();

            string sql = "SELECT * FROM account WHERE name=@name";
            DB db = DB.getDB(databaseFile);
            Dictionary<string, object> parms = new Dictionary<string, object>();

            parms.Add("@name", name);
            ISQLiteStatement statement = db.Query(sql, parms);

            while (statement.Step() == SQLiteResult.ROW)
            {
                Account e = new Account();

                Mapping(statement, e);
                res.Add(e);
            }

            return (res);
        }

        public static ObservableCollection<Account> RetrieveById(long id)
        {
            ObservableCollection<Account> res = new ObservableCollection<Account>();

            string sql = "SELECT * FROM account WHERE id=@id";
            DB db = DB.getDB(databaseFile);
            Dictionary<string, object> parms = new Dictionary<string, object>();

            parms.Add("@id", id);
            ISQLiteStatement statement = db.Query(sql, parms);

            while (statement.Step() == SQLiteResult.ROW)
            {
                Account e = new Account();

                Mapping(statement, e);
                res.Add(e);
            }

            return (res);
        }

        public static ObservableCollection<Account> RetrieveByUser(long user_id)
        {
            ObservableCollection<Account> res = new ObservableCollection<Account>();

            string sql = "SELECT * FROM account WHERE user_id=@user_id";
            DB db = DB.getDB(databaseFile);
            Dictionary<string, object> parms = new Dictionary<string, object>();

            parms.Add("@user_id", user_id);
            ISQLiteStatement statement = db.Query(sql, parms);

            while (statement.Step() == SQLiteResult.ROW)
            {
                Account e = new Account();

                Mapping(statement, e);
                res.Add(e);
            }

            return (res);
        }
    }
}
