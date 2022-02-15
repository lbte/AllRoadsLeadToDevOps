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
    private PlanCarouselController plan_architecture_carousel_script;
    private PlanCarouselController plan_abilities_carousel_script;

    private bool selected_architecture_card = false;
    private bool leveled_up_card = false;

    private List<Card> cards_architectures;             
    private List<Card> cards_abilities;

    // Selected cards icons
    public Image left_card_selected_icon;
    public Image center_card_selected_icon;
    public Image right_card_selected_icon;
    public Sprite card_selected_icon;

    // Select random card (Tool)
    public int random_index_tool_card;
    public Card random_tool_card;
    public bool is_selected_random_card = false;
    public bool is_generated_random_card = false;

    // Lifecycle buttons
    public Button plan_lifecycle_button;
    public Button code_lifecycle_button;
    public Button build_lifecycle_button;
    public Button test_lifecycle_button;
    public Button release_lifecycle_button;
    public Button deploy_lifecycle_button;
    public Button operate_lifecycle_button;
    public Button monitor_lifecycle_button;

    // Lifecycle icons
    public Image plan_lifecycle_icon;
    public Image code_lifecycle_icon;
    public Image build_lifecycle_icon;
    public Image test_lifecycle_icon;
    public Image release_lifecycle_icon;
    public Image deploy_lifecycle_icon;
    public Image operate_lifecycle_icon;
    public Image monitor_lifecycle_icon;

    void Start()
    {   
        // the default view is the project view
        plan_project.SetActive(true);
        plan_architecture.SetActive(false);
        plan_abilities.SetActive(false);
        plan_tools.SetActive(false);

        // Lifecycle button controllers
        plan_lifecycle_button.onClick.AddListener(PlanLifecycleButtonHandler);
        code_lifecycle_button.onClick.AddListener(CodeLifecycleButtonHandler);
        build_lifecycle_button.onClick.AddListener(BuildLifecycleButtonHandler);
        test_lifecycle_button.onClick.AddListener(TestLifecycleButtonHandler);
        release_lifecycle_button.onClick.AddListener(ReleaseLifecycleButtonHandler);
        deploy_lifecycle_button.onClick.AddListener(DeployLifecycleButtonHandler);
        operate_lifecycle_button.onClick.AddListener(OperateLifecycleButtonHandler);
        monitor_lifecycle_button.onClick.AddListener(MonitorLifecycleButtonHandler);

        // to show views when the arrow buttons are clicked
        up_arrow_button.onClick.AddListener(UpButton);
        down_arrow_button.onClick.AddListener(DownButton);

        select_button.onClick.AddListener(SelectButtonHandler);
        level_up_button.onClick.AddListener(LevelUpButtonHandler);

        plan_abilities_script = plan_abilities.GetComponent<PlanCarouselController>();

        player_controller_script = GameObject.Find("Views").GetComponent<PlayerController>();
        plan_architecture_carousel_script = plan_architecture.GetComponent<PlanCarouselController>();
        plan_abilities_carousel_script = plan_abilities.GetComponent<PlanCarouselController>();

        cards_architectures = plan_architecture_carousel_script.cards;
        cards_abilities = plan_abilities_carousel_script.cards;

        // default index for the project window
        word_index = plan_parts.IndexOf("Project"); // default screen
        UpdateButtonText();
        DisableLevelIcon();
        DisableLifecycleIcons();
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

            DisableSelectedIcon();
            UpdateSelectedIcon();
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

            // Generates random card
            if(is_generated_random_card == false){
                GenerateRandomToolCard();
                is_generated_random_card = true;
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
        int center_index = plan_architecture_carousel_script.center_index;

        if(deck_button_text.text == "Architecture"){
            if(cards_architectures[center_index].selected == true) {
                cards_architectures[center_index].selected = false;
                player_controller_script.selected_architecture = null;
                selected_architecture_card = false;
            }
            else if(cards_architectures[center_index].selected == false && selected_architecture_card == false) {
                cards_architectures[center_index].selected = true;
                player_controller_script.selected_architecture = cards_architectures[center_index];
                selected_architecture_card = true;
            }
        }

        DisableSelectedIcon();
        UpdateSelectedIcon();
    }

    void LevelUpButtonHandler(){
        int center_index = plan_abilities_carousel_script.center_index;
        if(deck_button_text.text == "Abilities"){
            if(leveled_up_card == false){
                leveled_up_card = true;
                int current_level = cards_abilities[center_index].level;
                if(current_level < 3){
                    cards_abilities[center_index].level += 1;
                    player_controller_script.abilities_levels[cards_abilities[center_index].id] += 1;
                    
                    // Pop-up imagen level-up con sonidito
                }
                else{
                    // Pop-up, mensaje: "Max level already reached" o algo asi :p
                }
            }
        }
        DisableLevelIcon();
        UpdateLevelIcon();
    }

    public void DisableSelectedIcon(){
        left_card_selected_icon.gameObject.SetActive(false);
        center_card_selected_icon.gameObject.SetActive(false);
        right_card_selected_icon.gameObject.SetActive(false);
    }

    public void UpdateSelectedIcon(){
        int left_index = plan_architecture_carousel_script.left_index;
        int center_index = plan_architecture_carousel_script.center_index;
        int right_index = plan_architecture_carousel_script.right_index;

        if(cards_architectures[left_index].selected == true) {
            left_card_selected_icon.gameObject.SetActive(true);
            left_card_selected_icon.sprite = card_selected_icon;
        }
        if(cards_architectures[center_index].selected == true) {
            center_card_selected_icon.gameObject.SetActive(true);
            center_card_selected_icon.sprite = card_selected_icon;
        }
        if(cards_architectures[right_index].selected == true) {
            right_card_selected_icon.gameObject.SetActive(true);
            right_card_selected_icon.sprite = card_selected_icon;
        }
    }

    void GenerateRandomToolCard(){
        random_index_tool_card = (int)Random.Range(0, single_card_script_tools.cards.Count - 1);
        while(true){
            random_tool_card = single_card_script_tools.cards[random_index_tool_card];
            if(random_tool_card.selected == false){
                random_tool_card.selected = true;
                break;
            }
            else random_index_tool_card = (int)Random.Range(0, single_card_script_tools.cards.Count - 1);
        }
        plan_tools_card_image.sprite = random_tool_card.card_image;
    }

    void DisableLifecycleIcons(){
        plan_lifecycle_icon.gameObject.SetActive(false);
        code_lifecycle_icon.gameObject.SetActive(false);
        build_lifecycle_icon.gameObject.SetActive(false);
        test_lifecycle_icon.gameObject.SetActive(false);
        release_lifecycle_icon.gameObject.SetActive(false);
        deploy_lifecycle_icon.gameObject.SetActive(false);
        operate_lifecycle_icon.gameObject.SetActive(false);
        monitor_lifecycle_icon.gameObject.SetActive(false);
    }

    void UpdateLifecycleIcons(){
        if(player_controller_script.tool_cards["plan"] != null){
            plan_lifecycle_icon.gameObject.SetActive(true);
            plan_lifecycle_icon.sprite = player_controller_script.tool_cards["plan"].card_image;
        }
        if(player_controller_script.tool_cards["code"] != null){
            code_lifecycle_icon.gameObject.SetActive(true);
            code_lifecycle_icon.sprite = player_controller_script.tool_cards["code"].card_image;
        }
        if(player_controller_script.tool_cards["build"] != null){
            build_lifecycle_icon.gameObject.SetActive(true);
            build_lifecycle_icon.sprite = player_controller_script.tool_cards["build"].card_image;
        }
        if(player_controller_script.tool_cards["test"] != null){
            test_lifecycle_icon.gameObject.SetActive(true);
            test_lifecycle_icon.sprite = player_controller_script.tool_cards["test"].card_image;
        }
        if(player_controller_script.tool_cards["release"] != null){
            release_lifecycle_icon.gameObject.SetActive(true);
            release_lifecycle_icon.sprite = player_controller_script.tool_cards["release"].card_image;
        }
        if(player_controller_script.tool_cards["deploy"] != null){
            deploy_lifecycle_icon.gameObject.SetActive(true);
            deploy_lifecycle_icon.sprite = player_controller_script.tool_cards["deploy"].card_image;
        }
        if(player_controller_script.tool_cards["operate"] != null){
            operate_lifecycle_icon.gameObject.SetActive(true);
            operate_lifecycle_icon.sprite = player_controller_script.tool_cards["operate"].card_image;
        }
        if(player_controller_script.tool_cards["monitor"] != null){
            monitor_lifecycle_icon.gameObject.SetActive(true);
            monitor_lifecycle_icon.sprite = player_controller_script.tool_cards["monitor"].card_image;
        }
    }

    void PlanLifecycleButtonHandler(){  
        /* string t = GameObject.GetComponent<Button>().GetComponent<Text>().text;
        Debug.Log(t); */

        if(is_selected_random_card == false){
            player_controller_script.tool_cards["plan"] = random_tool_card;
            is_selected_random_card = true;
        }
        DisableLifecycleIcons();
        UpdateLifecycleIcons();

        // Debug
        /* foreach(KeyValuePair<string, Card> entry in player_controller_script.tool_cards)
        {   
            if(entry.Value != null) Debug.Log(entry.Key + " " + entry.Value.card_description);
        } */
    }

    void CodeLifecycleButtonHandler(){
        if(is_selected_random_card == false){
            player_controller_script.tool_cards["code"] = random_tool_card;
            is_selected_random_card = true;
        }
        DisableLifecycleIcons();
        UpdateLifecycleIcons();
    }

    void BuildLifecycleButtonHandler(){
        if(is_selected_random_card == false){
            player_controller_script.tool_cards["build"] = random_tool_card;
            is_selected_random_card = true;
        }
        DisableLifecycleIcons();
        UpdateLifecycleIcons();
    }

    void TestLifecycleButtonHandler(){
        if(is_selected_random_card == false){
            player_controller_script.tool_cards["test"] = random_tool_card;
            is_selected_random_card = true;
        }
        DisableLifecycleIcons();
        UpdateLifecycleIcons();
    }

    void ReleaseLifecycleButtonHandler(){
        if(is_selected_random_card == false){
            player_controller_script.tool_cards["release"] = random_tool_card;
            is_selected_random_card = true;
        }
        DisableLifecycleIcons();
        UpdateLifecycleIcons();
    }

    void DeployLifecycleButtonHandler(){
        if(is_selected_random_card == false){
            player_controller_script.tool_cards["deploy"] = random_tool_card;
            is_selected_random_card = true;
        }
        DisableLifecycleIcons();
        UpdateLifecycleIcons();
    }

    void OperateLifecycleButtonHandler(){
        if(is_selected_random_card == false){
            player_controller_script.tool_cards["operate"] = random_tool_card;
            is_selected_random_card = true;
        }
        DisableLifecycleIcons();
        UpdateLifecycleIcons();
    }

    void MonitorLifecycleButtonHandler(){
        if(is_selected_random_card == false){
            player_controller_script.tool_cards["monitor"] = random_tool_card;
            is_selected_random_card = true;
        }
        DisableLifecycleIcons();
        UpdateLifecycleIcons();
    }
}
