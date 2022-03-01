using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LastGameController : MonoBehaviour
{
    public static LastGameController LastGameController_Instance;

    public GameObject LastGameRecordsPopUp;

    public Text LastGames1Tittle;
    public Text UserName1ImageText;
    public Text FinalScore1ImageText;
    public Text TimeToComplete1ImageText;

    public Text LastGames2Tittle;
    public Text UserName2ImageText;
    public Text FinalScore2ImageText;
    public Text TimeToComplete2ImageText;

    void Awake()
    {
        if (LastGameController_Instance == null)
        {
            LastGameController_Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        disableVisualization();
    }

    public void enableVisualization()
    {
        LastGameController.LastGameController_Instance.LastGameRecordsPopUp.SetActive(true);



    }

    public void disableVisualization()
    {
        LastGameController.LastGameController_Instance.LastGameRecordsPopUp.SetActive(false);
    }

    public void LoadLastGameData(int i, string name1, int player_final_score1, string player_final_time1, string selected_architecture1)
    {
        enableVisualization();

        GameData data = SaveSystem.LoadGameplay(i,name1, player_final_score1, player_final_time1, selected_architecture1);
        if (SaveSystem.version > 1)
        {
            Debug.Log(i - 1);
            GameData data1 = SaveSystem.LoadGameplay(i - 1, name1, player_final_score1, player_final_time1, selected_architecture1);

            LastGameController.LastGameController_Instance.LastGames2Tittle.text = data1.date;
            LastGameController.LastGameController_Instance.UserName2ImageText.text = data1.userName;
            LastGameController.LastGameController_Instance.FinalScore2ImageText.text = "" + data1.player_final_score;
            LastGameController.LastGameController_Instance.TimeToComplete2ImageText.text = data1.player_final_time;
        }

        LastGameController.LastGameController_Instance.LastGames1Tittle.text = data.date;
        LastGameController.LastGameController_Instance.UserName1ImageText.text = data.userName;
        LastGameController.LastGameController_Instance.FinalScore1ImageText.text = "" + data.player_final_score;
        LastGameController.LastGameController_Instance.TimeToComplete1ImageText.text = data.player_final_time;

        LastGameController.LastGameController_Instance.LastGames2Tittle.text = "Default";
        LastGameController.LastGameController_Instance.UserName2ImageText.text = "Default";
        LastGameController.LastGameController_Instance.FinalScore2ImageText.text = "Default";
        LastGameController.LastGameController_Instance.TimeToComplete2ImageText.text = "Default";
    }

}
