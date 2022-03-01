using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class GameData
{
    public string userName;
    public int player_final_score;
    public string player_final_time;
    public string date;
    public string selected_architecture;

    public GameData(string name1, int player_final_score1,string player_final_time1,string selected_architecture1)
    {
        date = System.DateTime.Now.ToString("yyyy/MM/dd");
        userName = name1;
        player_final_score = player_final_score1;
        player_final_time = player_final_time1;
        selected_architecture = selected_architecture1;
    }

}
