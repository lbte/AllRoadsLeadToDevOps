using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildDeckController : MonoBehaviour
{
    // buttons for each deck of each part
    public Button deck_button;
    public Button up_arrow_button;
    public Button down_arrow_button;
    public Text deck_button_text;

    public GameObject carousel;
    public GameObject landscape;
    public GameObject air_trap;
    public GameObject ground_trap;
    public PlayerController player_controller;

    // list with the decks names
    private List<string> plan_parts = new List<string>() { "Categorize", "Building" };
    private int word_index;

    void Start()
    {   

        // to show views when the arrow buttons are clicked
        up_arrow_button.onClick.AddListener(UpButton);
        down_arrow_button.onClick.AddListener(DownButton);

        // default index for the project window
        word_index = plan_parts.IndexOf("Categorize"); // default screen
        UpdateButtonText();

    }

    // Update the text that appears on the deck button
    public void UpdateButtonText()
    {
        // change the button text
        deck_button_text.text = plan_parts[word_index];

        if (deck_button_text.text == "Categorize")
        {
            carousel.SetActive(true);
            landscape.SetActive(false);
        }
        else
        {
            carousel.SetActive(false);
            landscape.SetActive(true);

            if (player_controller.selected_architecture.id == "architecture_2")
            {
                air_trap.SetActive(false);
            }
            else if(player_controller.selected_architecture.id == "architecture_1")
            {
                ground_trap.SetActive(false);
            }

        }
    }

    // move up on the arrows and show different views according to that
    public void UpButton()
    {
        word_index++;
        if (word_index >= plan_parts.Count) word_index = 0;

        UpdateButtonText();

    }

    // move down on the arrows and show different views according to that
    public void DownButton()
    {
        word_index--;
        if (word_index < 0) word_index = plan_parts.Count - 1;
        
        UpdateButtonText();
    }
}
