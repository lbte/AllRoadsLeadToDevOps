using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanDeckController : MonoBehaviour
{
    // buttons for each deck of each part
    public Button deck_button;
    public Button up_arrow_button;
    public Button down_arrow_button;
    public Text deck_button_text;
    public Button select_button;

    // each section objects
    public GameObject plan_project;
    public GameObject plan_architecture;
    public GameObject plan_abilities;
    public GameObject plan_tools;

    // list with the decks names
    private List<string> plan_parts = new List<string>() { "Project", "Architecture", "Abilities", "Tools" };
    private int word_index;

    void Start()
    {
        // the default view is the project view
        plan_project.SetActive(true);
        plan_architecture.SetActive(false);
        plan_abilities.SetActive(false);
        plan_tools.SetActive(false);

        // to show views when the arrow buttons are clicked
        up_arrow_button.onClick.AddListener(UpButton);
        down_arrow_button.onClick.AddListener(DownButton);

        // default index for the project window
        word_index = plan_parts.IndexOf("Project"); // default screen
        UpdateButtonText();

    }

    void DeactivateParts()
    {
        plan_project.SetActive(false);
        plan_architecture.SetActive(false);
        plan_abilities.SetActive(false);
        plan_tools.SetActive(false);
    }

    // Update the text that appears on the deck button
    public void UpdateButtonText()
    {
        // change the button text
        deck_button_text.text = plan_parts[word_index];

        if (deck_button_text.text == "Project")
        {
            DeactivateParts();
            plan_project.SetActive(true);
            select_button.gameObject.SetActive(false);
        }
        else if (deck_button_text.text == "Architecture")
        {
            DeactivateParts();
            plan_architecture.SetActive(true);
            select_button.gameObject.SetActive(false);
        }
        else if (deck_button_text.text == "Abilities")
        {
            DeactivateParts();
            plan_abilities.SetActive(true);
            select_button.gameObject.SetActive(true);
        }
        else
        {
            DeactivateParts();
            plan_tools.SetActive(true);
            select_button.gameObject.SetActive(false);
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
