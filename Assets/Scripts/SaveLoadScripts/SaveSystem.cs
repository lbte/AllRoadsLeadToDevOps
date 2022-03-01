using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using BayatGames.SaveGameFree;

public class SaveSystem
{
    public static int version = 0;
    public static string auxVersion = "";

    //SaveSystem.SaveGameplay(name, player_final_score, player_final_time, selected_architecture;
    public static void SaveGameplay(string name1, int player_final_score1, string player_final_time1, string selected_architecture1)
    {
        try
        {
            auxVersion = SaveGame.Load<string>("./DevopsGamePae/LastVersion.txt", "version");
            Debug.Log("entro por acá");
        }
        catch (Exception e)
        {
            SaveGame.Save<string>("./DevopsGamePae/LastVersion.txt", "" + version); //identificador
            auxVersion = "0";
        }

        if (auxVersion == "" || auxVersion == "version")
        {
            version = 0;
            auxVersion = "" + version;
        }

        Debug.Log(auxVersion);
        version = int.Parse(auxVersion);
        version++;

        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/GameData_version" + version + ".gd";

        Debug.Log("El numero de partida golbal es: " + version);
        Debug.Log(path);

        FileStream stream = new FileStream(path, FileMode.Create);

        GameData data = new GameData(name1, player_final_score1, player_final_time1, selected_architecture1);

        formatter.Serialize(stream, data);

        SaveGame.Save<string>("./DevopsGamePae/LastVersion.txt", "" + version); //identificador

        stream.Close();

    }

    public static GameData LoadGameplay(int i, string name1, int player_final_score1, string player_final_time1, string selected_architecture1)
    {

        string path = Application.persistentDataPath + "/GameData_version" + i + ".gd";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            GameData data = formatter.Deserialize(stream) as GameData;
            stream.Close();

            return data;

        }
        else
        {
            SaveGameplay(name1, player_final_score1, player_final_time1, selected_architecture1);
            Debug.Log("Save file not found in " + path);
            Debug.Log("Try again");
            return null;
        }
    }


}
