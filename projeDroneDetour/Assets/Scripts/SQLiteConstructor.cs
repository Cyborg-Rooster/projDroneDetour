using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System;

public class SQLiteConstructor : MonoBehaviour
{
    static string connection;
    static public IDbConnection dbcon;

    // Start is called before the first frame update
    public static void Waked()
    {
        connection = "URI=file:" + Application.persistentDataPath + "/" + "DBDroneDetour.db";
        dbcon = new SqliteConnection(connection);

        int actualDatabaseVersion = 2;
        int databaseVersion = 0;
        int numberTable = 0;

        dbcon.Open();
        IDbCommand checkDatabase;
        checkDatabase = dbcon.CreateCommand();
        checkDatabase.CommandText = "SELECT COUNT(name) FROM sqlite_master WHERE type='table';";
        IDataReader reader = checkDatabase.ExecuteReader();

        while (reader.Read())
        {
            numberTable = Convert.ToInt32(reader[0].ToString());
            Debug.Log(numberTable);
        }

        dbcon.Close();

        if (numberTable == 0)
        {
            Debug.Log("Não existe banco, criando...");
            ExecuteCreation();
            Debug.Log("Banco criado");
        }
        else
        {
            dbcon.Open();

            IDbCommand checkVersion;
            checkVersion = dbcon.CreateCommand();
            checkVersion.CommandText = "SELECT version FROM Versao;";
            reader = checkVersion.ExecuteReader();

            while (reader.Read())
            {
                databaseVersion = Convert.ToInt32(reader[0].ToString());
                Debug.Log("Versão do banco: " + databaseVersion);
            }


            dbcon.Close();

            if (databaseVersion < actualDatabaseVersion)
            {
                Debug.Log("Banco não esta atualizado, atualizando...");
                UpdateData(actualDatabaseVersion);
                Debug.Log("O banco foi atualizado.");
            }
            //senao ele continua a aplicação
            else
                Debug.Log("Banco atualizado");

        }
    }

    static void ExecuteCreation()
    {
        string table, value;

        table = "create table versao(" +
                    "idVersion integer primary key autoincrement," +
                    "version integer)";

        value = "insert into versao values(" +
                    "null," +
                    "2)";
        CreateData(table, value);

        table = "create table estatisticas(" +
                    "idEstatistica integer primary key autoincrement," +
                    "nMelhorPont integer," +
                    "nMortes integer," +
                    "nMortesQueda integer," +
                    "nMortesObst integer," +
                    "nClicks integer)";

        value = "insert into estatisticas values(" +
                    "null," + 
	                "0," +
	                "0," +
	                "0," +
	                "0," +
                    "0)";

        CreateData(table, value);

        table = "create table opcoes(" +
                    "idOpcao integer primary key autoincrement," +
                    "som integer," +
                    "idioma varchar(8)," +
                    "tutorial integer)";

        value = "insert into opcoes values(" +
                    "null," +
                    "1," +
                    "'xxx'," +
                    "1)";

        CreateData(table, value);

        table = "CREATE TABLE temp_save(" +
                        "idSave integer primary key autoincrement," +
                        "pontos integer," +
                        "secondTry integer," +
                        "isNight integer)";

        value = "insert into temp_save values(" +
                    "null," +
                    "0," +
                    "0," +
                    "0)";

        CreateData(table, value);
    }

    static void CreateData(string table, string value)
    {
        dbcon.Open();

        
        IDbCommand createTable = dbcon.CreateCommand();
        createTable.CommandText = table;
        createTable.ExecuteNonQuery();
        

        IDbCommand createValue = dbcon.CreateCommand();
        createValue.CommandText = value;
        createValue.ExecuteNonQuery();

        dbcon.Close();
    }

    public static string LoadData(string table, int i)
    {
        string value = "";

        dbcon.Open();

        IDbCommand loadData = dbcon.CreateCommand();
        loadData.CommandText = "select * from " + table;
        IDataReader reader = loadData.ExecuteReader();

        while(reader.Read())
        {
            value = reader[i].ToString();
        }

        dbcon.Close();
        return value;
    }

    public static void CreateQuery(string query)
    {
        dbcon.Open();

        IDbCommand saveData = dbcon.CreateCommand();
        saveData.CommandText = query;
        saveData.ExecuteNonQuery();

        dbcon.Close();
    }

    static void UpdateData(int actualVersion)
    {
        string table, value;
        if(actualVersion == 1)
        {
            table = "CREATE TABLE temp_save(" +
                        "idSave integer primary key autoincrement," +
                        "pontos integer," +
                        "secondTry integer," +
                        "isNight integer)";

            value = "insert into temp_save values(" +
                        "null," +
                        "0," +
                        "0," +
                        "0)";

            CreateData(table, value);

            value = "update versao" +
                        " set version = 2";

            CreateQuery(value);
        }
    }
}
