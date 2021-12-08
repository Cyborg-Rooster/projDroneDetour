using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefs : MonoBehaviour
{
    public static string[] options = new string[3];
    public static string[] statistics = new string[5];
    public static string[] tempData = new string[4];

    static string[] opcoesTeste = new string[3]
    {
        "som", "idioma", "tutorial"
    };
    static string[] estatisticaTeste = new string[5]
    {
        "nMelhorPont", "nMortes", "nMortesQueda", "nMortesObst", "nClicks"
    };

    public static void LoadData()
    {
        for(int i = 0; i < options.Length; i++)
        {
            options[i] = SQLiteConstructor.LoadData("opcoes", i + 1);
            Debug.Log(opcoesTeste[i] + ": " + options[i]);
        }
        Debug.Log("--------------------------------------");
        for(int i=0; i < statistics.Length; i++)
        {
            statistics[i] = SQLiteConstructor.LoadData("estatisticas", i + 1);
            Debug.Log(estatisticaTeste[i] + ": " + statistics[i]);
        }
    }

    public static void SaveData()
    {
        string query;
        
        query = "update estatisticas set " +
                    "nMelhorPont = " + statistics[0] +
                    ", nMortes = " + statistics[1] +
                    ", nMortesQueda = " + statistics[2] +
                    ", nMortesObst = " + statistics[3] +
                    ", nClicks = " + statistics[4];

        SQLiteConstructor.CreateQuery(query);

        query = "update opcoes set " +
                   "som = " + options[0] +
                   ", idioma = '" + options[1] +
                   "', tutorial = " + options[2];

        SQLiteConstructor.CreateQuery(query);
    }

    public static void LoadTempData()
    {
        for (int i = 0; i < tempData.Length; i++)
        {
            tempData[i] = SQLiteConstructor.LoadData("temp_save", i);
        }
        //Debug.LogError("foi usado ad: " + tempData[2] + ", pontos: " + tempData[1] + ", esta de noite: " + tempData[3]);
    }

    public static void SaveTempData()
    {
        string query;
        GameManager game = GameObject.Find("GameManager").GetComponent<GameManager>();

        query = "update temp_save set pontos = " + 
                                       tempData[1] + ", secondTry = " + 
                                       tempData[2] + ", isNight = " + 
                                       tempData[3];

        SQLiteConstructor.CreateQuery(query);
    }

    public static void ClearTempData()
    {
        tempData[1] = "0";
        tempData[2] = "0";
        tempData[3] = "0";

        SaveTempData();
    }
}
