using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartController : MonoBehaviour
{
    public static string player_name;
    public GameObject enter_name_input_field;
    public Button accept_name_button;
    public Image enter_name_window;

    public Button start_button;
    public Button how_to_play_button;
    public Button credits_button;

    public Image how_to_play_window;
    public Button how_to_play_window_close_button;
    public Button how_to_play_next_button;
    public Sprite how_to_play_image_1;
    public Sprite how_to_play_image_2;

    public Image credits_window;
    public Button credits_window_close_button;
    
    void Start()
    {
        start_button.onClick.AddListener(StartButtonController);
        how_to_play_button.onClick.AddListener(HowToPlayButtonController);
        credits_button.onClick.AddListener(CreditsButtonController);
        how_to_play_window_close_button.onClick.AddListener(HowToPlayWindowCloseButtonController);
        credits_window_close_button.onClick.AddListener(CreditsWindowCloseButtonController);
        accept_name_button.onClick.AddListener(AcceptNameButtonController);
        how_to_play_next_button.onClick.AddListener(NextHowToPlay);

        enter_name_input_field = GameObject.Find("NameInputFieldText");

        how_to_play_window.gameObject.SetActive(false);
        credits_window.gameObject.SetActive(false);
        enter_name_window.gameObject.SetActive(false);
    }

    void StartButtonController(){
        enter_name_window.gameObject.SetActive(true);
    }

    void AcceptNameButtonController(){
        player_name = enter_name_input_field.GetComponent<Text>().text;
        SceneManager.LoadScene(0);
    }

    void HowToPlayButtonController(){
        how_to_play_window.gameObject.SetActive(true);
        how_to_play_window.sprite = how_to_play_image_1;
        how_to_play_next_button.gameObject.SetActive(true);
    }

    void HowToPlayWindowCloseButtonController(){
        how_to_play_window.gameObject.SetActive(false);
    }

    void CreditsWindowCloseButtonController(){
        credits_window.gameObject.SetActive(false);
    }

    void CreditsButtonController(){
        credits_window.gameObject.SetActive(true);
    }

    void NextHowToPlay()
    {
        how_to_play_window.sprite = how_to_play_image_2;
        how_to_play_next_button.gameObject.SetActive(false);
    }
}
