using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.IO;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine;

static class SQLiteHelper
{
    private static IDbConnection Database;
    private static string Connection = @$"{Application.persistentDataPath}\Database\Database.sqlite";
    public static bool CheckIfDatabaseExist()
    {
        if (File.Exists(@$"{Connection}")) return true;
        else return false;
    }

    public static void CreateDatabase()
    {
        Directory.CreateDirectory(@$"{Application.persistentDataPath}\Database\");
        SetDatabase();
        DatabaseSynch.Synch();
    }

    public static void SetDatabase()
    {
        Database = GetDatabaseConnection();
    }

    public static void RunQuery(string query)
    {
        IDbCommand cmd;

        cmd = Database.CreateCommand();
        cmd.CommandText = query;
        cmd.ExecuteReader();
    }

    public static string ReturnValue(string query)
    {
        string output = "";
        IDbCommand cmd;
        IDataReader reader;

        cmd = Database.CreateCommand();

        cmd.CommandText = query;
        reader = cmd.ExecuteReader();

        output = reader[0].ToString();

        return output;
    }

    public static int ReturnValueAsInt(string query)
    {
        int output = 0;
        IDbCommand cmd;
        IDataReader reader;

        cmd = Database.CreateCommand();

        cmd.CommandText = query;
        reader = cmd.ExecuteReader();

        output = Convert.ToInt32(reader[0]);

        return output;
    }

    public static void SetDatabaseActive(bool active)
    {
        if (active) Database.Open();
        else Database.Close();
    }

    private static SqliteConnection GetDatabaseConnection()
    {
        return new SqliteConnection(new SqliteConnection("URI=file:" + Connection));
    }
}
