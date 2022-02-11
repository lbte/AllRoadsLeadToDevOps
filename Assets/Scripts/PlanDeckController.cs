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

    // Level icons
    public Sprite levelIcon;

    public Image left_card_level_icon_1;
    public Image left_card_level_icon_2;
    public Image left_card_level_icon_3;

    public Image center_card_level_icon_1;
    public Image center_card_level_icon_2;
    public Image center_card_level_icon_3;
    
    public Image right_card_level_icon_1;
    public Image right_card_level_icon_2;
    public Image right_card_level_icon_3;

    // Abilities cards
    private PlanCarouselController plan_abilities_script;

    // to trigger the tutorials for each section
    private TutorialTextTrigger tutorial_trigger;

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
        DisableLevelIcon();
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
            tutorial_trigger = plan_project.GetComponent<TutorialTextTrigger>();
            tutorial_trigger.TriggerTutorial();
            select_button.gameObject.SetActive(false);
        }
        else if (deck_button_text.text == "Architecture")
        {
            DeactivateParts();
            plan_architecture.SetActive(true);
            tutorial_trigger = plan_architecture.GetComponent<TutorialTextTrigger>();
            tutorial_trigger.TriggerTutorial();
            select_button.gameObject.SetActive(true);
        }
        else if (deck_button_text.text == "Abilities")
        {
            DeactivateParts();
            plan_abilities.SetActive(true);
            tutorial_trigger = plan_abilities.GetComponent<TutorialTextTrigger>();
            tutorial_trigger.TriggerTutorial();
            select_button.gameObject.SetActive(true);

            plan_abilities_script = plan_abilities.GetComponent<PlanCarouselController>();

            // Change level icons
            DisableLevelIcon();
            UpdateLevelIcon();
        }
        else if (deck_button_text.text == "Tools")
        {
            DeactivateParts();
            plan_tools.SetActive(true);
            tutorial_trigger = plan_tools.GetComponent<TutorialTextTrigger>();
            tutorial_trigger.TriggerTutorial();
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

    public void DisableLevelIcon()
    {
        left_card_level_icon_1.gameObject.SetActive(false);
        left_card_level_icon_2.gameObject.SetActive(false);
        left_card_level_icon_3.gameObject.SetActive(false);

        center_card_level_icon_1.gameObject.SetActive(false);
        center_card_level_icon_2.gameObject.SetActive(false);
        center_card_level_icon_3.gameObject.SetActive(false);

        right_card_level_icon_1.gameObject.SetActive(false);
        right_card_level_icon_2.gameObject.SetActive(false);
        right_card_level_icon_3.gameObject.SetActive(false);
    }

    public void UpdateLevelIcon()
    {
        List<Card> deck = plan_abilities_script.cards;
        int left_card_level = deck[plan_abilities_script.left_index].level;
        int center_card_level = deck[plan_abilities_script.center_index].level;
        int right_card_level = deck[plan_abilities_script.right_index].level;

        /* foreach(Image level in plan_abilities.transform.Find("LeftCardLevels")){
            level.gameObject.SetActive(false);
        } */
        // Left card
        if(left_card_level >= 1){
            left_card_level_icon_1.gameObject.SetActive(true);
        }
        if(left_card_level >= 2){
            left_card_level_icon_2.gameObject.SetActive(true);
        }
        if(left_card_level >= 3){
            left_card_level_icon_3.gameObject.SetActive(true);
        }

        // Center card
        if(center_card_level >= 1){
            center_card_level_icon_1.gameObject.SetActive(true);
        }
        if(center_card_level >= 2){
            center_card_level_icon_2.gameObject.SetActive(true);
        }
        if(center_card_level >= 3){
            center_card_level_icon_3.gameObject.SetActive(true);
        }

        // Right card
        if(right_card_level >= 1){
            right_card_level_icon_1.gameObject.SetActive(true);
        }
        if(right_card_level >= 2){
            right_card_level_icon_2.gameObject.SetActive(true);
        }
        if(right_card_level >= 3){
            right_card_level_icon_3.gameObject.SetActive(true);
        }
    }
}
