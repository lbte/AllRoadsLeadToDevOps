using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartController : MonoBehaviour
{

    public Button start_button;
    public Button how_to_play_button;
    public Button credits_button;

    public Image how_to_play_window;
    public Button how_to_play_window_close_button;

    public Image credits_window;
    public Button credits_window_close_button;
    
    void Start()
    {
        start_button.onClick.AddListener(StartButtonController);
        how_to_play_button.onClick.AddListener(HowToPlayButtonController);
        credits_button.onClick.AddListener(CreditsButtonController);
        how_to_play_window_close_button.onClick.AddListener(HowToPlayWindowCloseButtonController);
        credits_window_close_button.onClick.AddListener(CreditsWindowCloseButtonController);

        how_to_play_window.gameObject.SetActive(false);
        credits_window.gameObject.SetActive(false);
    }

    void StartButtonController(){
        SceneManager.LoadScene(1);
    }

    void HowToPlayButtonController(){
        how_to_play_window.gameObject.SetActive(true);
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
}
