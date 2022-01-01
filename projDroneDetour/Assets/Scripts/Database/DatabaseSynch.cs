using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class DatabaseSynch
{
    public static readonly int Version = 1;
    public static void Synch()
    {
        CreateTables();
        SetDatabaseVersion();
    }

    private static void CreateTables()
    {
        SQLiteHelper.SetDatabaseActive(true);
        SQLiteHelper.RunQuery("CREATE TABLE OPTIONS (ID INTEGER, SOUND INTEGER, LANGUAGE VARCHAR(20), TUTORIAL INTEGER)");
        SQLiteHelper.RunQuery("CREATE TABLE STATISTIC (ID INTEGER, BESTSCORE INTEGER, DEATHS INTEGER, FALLS INTEGER, CRASHES INTEGER, CLICKS INTEGER)");
        SQLiteHelper.RunQuery("CREATE TABLE VERSION (ID INTEGER, VERSION_CODE INTEGER)");

        SQLiteHelper.RunQuery
        (
            CommonQuery.Add
            (
                "OPTIONS",
                "ID, SOUND, LANGUAGE, TUTORIAL",
                $"1, {CommonQuery.ConvertBoolToInt(Options.Sound)}, '{Options.Language}', {CommonQuery.ConvertBoolToInt(Options.Tutorial)}"
            )
        );

        SQLiteHelper.RunQuery
        (
            CommonQuery.Add
            (
                "STATISTIC",
                "ID, BESTSCORE, DEATHS, FALLS, CRASHES, CLICKS",
                $"1, {Statistics.BestScore}, {Statistics.Death}, {Statistics.Falls}, {Statistics.Crashes}, {Statistics.Clicks}"
            )
        );
        SQLiteHelper.SetDatabaseActive(false);
    }

    private static void SetDatabaseVersion()
    {
        SQLiteHelper.SetDatabaseActive(true);
        SQLiteHelper.RunQuery(CommonQuery.Add("VERSION", "ID, VERSION_CODE", $"1, {Version}"));
        SQLiteHelper.SetDatabaseActive(false);
    }
}
