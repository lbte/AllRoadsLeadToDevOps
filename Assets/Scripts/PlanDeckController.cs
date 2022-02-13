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
    public Button level_up_button;
    private Image plan_tools_card_image;

    // each section objects
    public GameObject plan_project;
    public GameObject plan_architecture;
    public GameObject plan_abilities;
    public GameObject plan_tools;

    // list with the decks names
    private List<string> plan_parts = new List<string>() { "Project", "Architecture", "Abilities", "Tools" };
    private int word_index;
    private List<Card> deck;
    public Card random_card;

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
    public SingleCardController single_card_script_project;
    public SingleCardController single_card_script_tools;

    // to trigger the tutorials for each section
    private TutorialTextTrigger tutorial_trigger;

    private PlayerController player_controller_script;
    private PlanCarouselController architecture_cards;

    public bool selected_architecture_card = false;
    private List<Card> cards;

    // Selected cards icons
    public Image left_card_selected_icon;
    public Image center_card_selected_icon;
    public Image right_card_selected_icon;
    public Sprite card_selected_icon;

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

        select_button.onClick.AddListener(SelectButtonHandler);
        level_up_button.onClick.AddListener(LevelUpButtonHandler);

        plan_abilities_script = plan_abilities.GetComponent<PlanCarouselController>();

        player_controller_script = GameObject.Find("Views").GetComponent<PlayerController>();
        architecture_cards = plan_architecture.GetComponent<PlanCarouselController>();
        cards = architecture_cards.cards;

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

            // Change level icons
            DisableLevelIcon();
            UpdateLevelIcon();
        }
        else if (deck_button_text.text == "Tools")
        {
            DeactivateParts();
            plan_tools.SetActive(true);
            plan_tools_card_image = GameObject.Find("ToolCardImage").GetComponent<Image>();
            tutorial_trigger = plan_tools.GetComponent<TutorialTextTrigger>();
            tutorial_trigger.TriggerTutorial();
            select_button.gameObject.SetActive(false);

            random_card = single_card_script_tools.cards[(int)Random.Range(0, single_card_script_tools.cards.Count - 1)];

            plan_tools_card_image.sprite = random_card.card_image;
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
        deck = plan_abilities_script.cards;
        int left_card_level = deck[plan_abilities_script.left_index].level;
        int center_card_level = deck[plan_abilities_script.center_index].level;
        int right_card_level = deck[plan_abilities_script.right_index].level;

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

    void SelectButtonHandler(){
        int center_index = architecture_cards.center_index;

        if(deck_button_text.text == "Architecture"){
            if(cards[center_index].selected == true) {
                Debug.Log("Carta esta seleccionada");
                cards[center_index].selected = false;
                player_controller_script.selected_architecture = null;
                selected_architecture_card = false;
            }
            else if(cards[center_index].selected == false && selected_architecture_card == false) {
                cards[center_index].selected = true;
                player_controller_script.selected_architecture = cards[center_index];
                selected_architecture_card = true;
            }
        }

        DisableSelectedIcon();
        UpdateSelectedIcon();
    }

    void LevelUpButtonHandler(){
        
    }

    public void DisableSelectedIcon(){
        left_card_selected_icon.gameObject.SetActive(false);
        center_card_selected_icon.gameObject.SetActive(false);
        right_card_selected_icon.gameObject.SetActive(false);
    }

    public void UpdateSelectedIcon(){
        int left_index = architecture_cards.left_index;
        int center_index = architecture_cards.center_index;
        int right_index = architecture_cards.right_index;

        if(cards[left_index].selected == true) left_card_selected_icon.gameObject.SetActive(true);
        if(cards[center_index].selected == true) center_card_selected_icon.gameObject.SetActive(true);
        if(cards[right_index].selected == true) right_card_selected_icon.gameObject.SetActive(true);
    }
}
