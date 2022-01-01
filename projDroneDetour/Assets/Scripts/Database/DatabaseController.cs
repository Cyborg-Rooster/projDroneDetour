using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

class DatabaseController
{
    public static void InitiateDatabase()
    {
        if (SQLiteHelper.CheckIfDatabaseExist())
        { 
            SQLiteHelper.SetDatabase();
            LoadData();
        }
        else
        {
            Configuration.SetLanguage();
            SQLiteHelper.CreateDatabase();
        }
    }

    public static void LoadData()
    {
        SQLiteHelper.SetDatabaseActive(true);
        Options.Sound = SQLiteHelper.ReturnValue(CommonQuery.Select("SOUND", "OPTIONS")) == "1";
        Options.Language = SQLiteHelper.ReturnValue(CommonQuery.Select("LANGUAGE", "OPTIONS"));
        Options.Tutorial = SQLiteHelper.ReturnValue(CommonQuery.Select("TUTORIAL", "OPTIONS")) == "1";

        Statistics.BestScore = SQLiteHelper.ReturnValueAsInt(CommonQuery.Select("BESTSCORE", "STATISTIC"));
        Statistics.Death = SQLiteHelper.ReturnValueAsInt(CommonQuery.Select("DEATHS", "STATISTIC"));
        Statistics.Falls = SQLiteHelper.ReturnValueAsInt(CommonQuery.Select("FALLS", "STATISTIC"));
        Statistics.Crashes = SQLiteHelper.ReturnValueAsInt(CommonQuery.Select("CRASHES", "STATISTIC"));
        Statistics.Clicks = SQLiteHelper.ReturnValueAsInt(CommonQuery.Select("CLICKS", "STATISTIC"));
        SQLiteHelper.SetDatabaseActive(false);
    }

    public static void SaveData()
    {
        SQLiteHelper.SetDatabaseActive(true);
        Update
        (
            "OPTIONS",
            $"SOUND = {CommonQuery.ConvertBoolToInt(Options.Sound)}, LANGUAGE = '{Options.Language}', TUTORIAL ={CommonQuery.ConvertBoolToInt(Options.Tutorial)}",
            "ID = 1"
        );

        Update
        (
            "STATISTIC",
            $"BESTSCORE = {Statistics.BestScore}, DEATHS = {Statistics.Death}, FALLS = {Statistics.Falls}, CRASHES = {Statistics.Crashes}, CLICKS = {Statistics.Clicks}",
            "ID = 1"
        );
        SQLiteHelper.SetDatabaseActive(false);
    }

    public static void Add(string table, string columns, string values)
    {
        SQLiteHelper.RunQuery(CommonQuery.Add(table, columns, values));
    }

    public static void Update(string table, string changes, string where)
    {
        SQLiteHelper.RunQuery(CommonQuery.Update(table, changes, where));
    }

    public static void CloseDatabase()
    {
        SQLiteHelper.SetDatabaseActive(false);
    }
}
