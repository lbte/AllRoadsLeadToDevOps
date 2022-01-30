using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CodeDeckController : MonoBehaviour
{
     // buttons for each deck of each part
    public Button deck_button;
    public Button up_arrow_button;
    public Button down_arrow_button;
    public Text deck_button_text;
    public Button select_button;

    // 
    public GameObject code_items;

    public CodeCarouselController carousel_script;

    // list with the decks names
    private List<string> plan_parts = new List<string>() { "Impact", "Holding", "Mechanism", "Bait" };
    private int word_index;

    void Start()
    {
        carousel_script = GameObject.Find("CodeItems").GetComponent<CodeCarouselController>();
        // to show views when the arrow buttons are clicked
        up_arrow_button.onClick.AddListener(UpButton);
        down_arrow_button.onClick.AddListener(DownButton);

        // default index for the project window
        word_index = plan_parts.IndexOf("Impact"); // default screen
        UpdateButtonText();

    }

    // Update the text that appears on the deck button
    public void UpdateButtonText()
    {
        // change the button text
        deck_button_text.text = plan_parts[word_index];
    }

    // move up on the arrows and show different views according to that
    public void UpButton()
    {
        word_index++;
        if (word_index >= plan_parts.Count) word_index = 0;
        carousel_script.current_deck_index++;
        if (carousel_script.current_deck_index >= carousel_script.decks.Count) carousel_script.current_deck_index = 0;

        UpdateButtonText();
        carousel_script.UpdateCardImages();
    }

    // move down on the arrows and show different views according to that
    public void DownButton()
    {
        word_index--;
        if (word_index < 0) word_index = plan_parts.Count - 1;
        carousel_script.current_deck_index--;
        if (carousel_script.current_deck_index < 0) carousel_script.current_deck_index = carousel_script.decks.Count - 1;

        UpdateButtonText();
        carousel_script.UpdateCardImages();
    }
}
