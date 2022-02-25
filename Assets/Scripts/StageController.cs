using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageController : MonoBehaviour
{
    public Button next_stage_button;
    public Text stage_title_text;

    private Button checklist_button;
    private Button checklist_close_button;
    public Button use_ability_button;
    public Button use_tool_button;
    private string checklist_text;
    private Image checklist_items_window;
    private Text checklist_items_text;
    public Animator checklist_window_animator;
    public Animator warning_checklist_window_animator;
    private Image warning_checklist_window;
    public Text warning_checklist_window_text;

    // abilities levels window
    public Image abilities_levels_window;
    public Animator abilities_levels_window_animator;
    public Button abilities_levels_window_close_button;
    public Button abilities_levels_button;

    public GameObject plan_stage;
    public GameObject code_stage;
    public GameObject build_stage;
    public GameObject test_stage;
    public GameObject release_stage;
    public GameObject deploy_stage;
    public GameObject operate_stage;
    public GameObject monitor_stage;

    // build change of phase popups
    public Animator warning_build_window_animator;
    public Image warning_build_window;
    public Text warning_build_window_text;

    private TutorialTextTrigger tutorial_trigger;

    private CodeCarouselController code_carousel_script;
    private PlayerController player_controller_script;
    private BuildCarouselController build_carousel_script;
    private BuildDeckController build_deck_controller_script;
    public PlanDeckController plan_deck_controller_script;


    public bool is_build_tool_used = false;
    public int is_build_ability_used = 0;  // min = 0, max = 2

    // Abilities summary
    private Image plan_ability_summary_image;
    private Image code_ability_summary_image;
    private Image build_ability_summary_image;
    private Image test_ability_summary_image;
    private Image release_ability_summary_image;
    private Image deploy_ability_summary_image;
    private Image operate_ability_summary_image;
    private Image monitor_ability_summary_image;

    public Sprite star_icon_level_1;
    public Sprite star_icon_level_2;
    public Sprite star_icon_level_3;

    // Start is called before the first frame update
    void Start()
    {
        plan_ability_summary_image = GameObject.Find("PlanLevelsImage").GetComponent<Image>();
        code_ability_summary_image = GameObject.Find("CodeLevelsImage").GetComponent<Image>();
        build_ability_summary_image = GameObject.Find("BuildLevelsImage").GetComponent<Image>();
        test_ability_summary_image = GameObject.Find("TestLevelsImage").GetComponent<Image>();
        release_ability_summary_image = GameObject.Find("ReleaseLevelsImage").GetComponent<Image>();
        deploy_ability_summary_image = GameObject.Find("DeployLevelsImage").GetComponent<Image>();
        operate_ability_summary_image = GameObject.Find("OperateLevelsImage").GetComponent<Image>();
        monitor_ability_summary_image = GameObject.Find("MonitorLevelsImage").GetComponent<Image>();

        checklist_button = GameObject.Find("ChecklistButton").GetComponent<Button>();
        checklist_close_button = GameObject.Find("ChecklistCloseButton").GetComponent<Button>();
        checklist_items_window = GameObject.Find("ChecklistItemsWindow").GetComponent<Image>();
        checklist_items_text = GameObject.Find("ChecklistItemsText").GetComponent<Text>();
        warning_checklist_window = GameObject.Find("WarningChecklistWindow").GetComponent<Image>();
        plan_deck_controller_script = GameObject.Find("Plan").GetComponent<PlanDeckController>();
        player_controller_script = GameObject.Find("Views").GetComponent<PlayerController>();

        warning_checklist_window.gameObject.SetActive(false);
        checklist_close_button.gameObject.SetActive(false);
        abilities_levels_window_animator.SetBool("IsAbilitiesLevelsWindowOpen", false);
        checklist_close_button.onClick.AddListener(ChecklistCloseButton);
        checklist_button.onClick.AddListener(ChecklistButton);
        abilities_levels_button.onClick.AddListener(AbilitiesLevelsWindowButton);
        abilities_levels_window_close_button.onClick.AddListener(AbilitiesLevelsWindowCloseButton);
        next_stage_button.onClick.AddListener(NextStageButton);
        use_ability_button.onClick.AddListener(UseAbilityButtonHandler);
        use_tool_button.onClick.AddListener(UseToolButtonHanlder);

        stage_title_text.text = "PLAN";
        plan_stage.SetActive(true);
        code_stage.SetActive(false);
        build_stage.SetActive(false);
        test_stage.SetActive(false);
        release_stage.SetActive(false);
        deploy_stage.SetActive(false);
        operate_stage.SetActive(false);
        monitor_stage.SetActive(false);
    }

    public void DeactivatedStages()
    {
        plan_stage.SetActive(false);
        code_stage.SetActive(false);
        build_stage.SetActive(false);
        test_stage.SetActive(false);
        release_stage.SetActive(false);
        deploy_stage.SetActive(false);
        operate_stage.SetActive(false);
        monitor_stage.SetActive(false);
    }

    public void NextStageButton()
    {
        if (stage_title_text.text == "PLAN")
        {
            // Check conditions required to complete the plan phase
            // - Select architecture (PlayerController)
            // - Assign tool (PlanDeckController)
            bool tool = plan_deck_controller_script.is_selected_random_card;
            Card architecture = player_controller_script.selected_architecture;
            if ((architecture.id == "architecture_1" || architecture.id == "architecture_2" || architecture.id == "architecture_3") && tool == true)
            {
                stage_title_text.text = "CODE";
                DeactivatedStages();
                code_stage.SetActive(true);
                tutorial_trigger = code_stage.GetComponent<TutorialTextTrigger>();
                tutorial_trigger.TriggerTutorial();
                if (checklist_window_animator.GetBool("IsOpen") == true) checklist_items_window.gameObject.SetActive(false);
            }
            else
            {
                // Pop-up: mensaje "Looks like something is missing, check your checklist"
                StartCoroutine(WarningWindowDisplay("Looks like there's something missing.\n\nCheck your checklist!", 2));
            }
        }
        else if (stage_title_text.text == "CODE")
        {
            // Save selected cards in PlayerController
            code_carousel_script = GameObject.Find("CodeItems").GetComponent<CodeCarouselController>();
            player_controller_script = GameObject.Find("Views").GetComponent<PlayerController>();

            // LIMPIAR CATEGORIZE Y LIMPIAR BUILDING PARA QUE NO APAREZCAN LAS CARTAS QUE ANTES SE HABIAN SELECCIONADO

            stage_title_text.text = "BUILD";
            DeactivatedStages();

            // Starts on carousel (Categorize)
            build_stage.SetActive(true);
            build_deck_controller_script = GameObject.Find("Build").GetComponent<BuildDeckController>();
            next_stage_button.gameObject.SetActive(false);
            warning_build_window.gameObject.SetActive(false);
            build_deck_controller_script.word_index = 0; // reset to go back to the categorize view and be able to go to the building
            build_deck_controller_script.deck_button_text.text = "Categorize";
            build_deck_controller_script.carousel.SetActive(true);
            build_deck_controller_script.up_arrow_button.gameObject.SetActive(true);
            build_deck_controller_script.down_arrow_button.gameObject.SetActive(true);
            build_deck_controller_script.landscape.SetActive(false);
            
            //is_build_tool_used = false;
            //is_build_ability_used = 0;

            // Update selected cards in BuildCarouselController (from PlayerController)
            build_carousel_script = GameObject.Find("BuildItems").GetComponent<BuildCarouselController>();
            build_carousel_script.AssignSelectedCodeCards();
            build_carousel_script.UpdateCardImages();


            tutorial_trigger = build_stage.GetComponent<TutorialTextTrigger>();
            tutorial_trigger.TriggerTutorial();
            if (checklist_window_animator.GetBool("IsOpen") == true) checklist_items_window.gameObject.SetActive(false);
        }
        else if (stage_title_text.text == "BUILD")
        {
            // Verify build correctness
            int impact = player_controller_script.impact_build_correctness;
            int hold = player_controller_script.hold_build_correctness;
            int bait = player_controller_script.bait_build_correctness;
            int mechanism = player_controller_script.mechanism_build_correctness;
            if ((impact + hold + bait + mechanism) != 4)
            { // Build fails -> Returns to Plan

                /////// POP-UP
                StartCoroutine(WarningBuildingToPlanDisplay("It looks like the building you made is not the right one. \nYou must plan again.", 3f));
            }
            else
            {   // Build is correct

                // Determinar carta de mecanismo elegida
                Card mechanism_card = null;
                foreach (Card card in player_controller_script.selected_code_cards)
                {
                    if (card.category == "mechanism")
                    {
                        mechanism_card = card;
                        break;
                    }
                }

                if (((mechanism_card.id == "balloon_fair" || mechanism_card.id == "turkey_balloon") && (player_controller_script.selected_architecture.id == "architecture_2"))
                || ((mechanism_card.id == "alpinism_pulley" || mechanism_card.id == "well_pulley") && player_controller_script.selected_architecture.id == "architecture_1"))
                {
                    /////// POP-UP
                    StartCoroutine(WarningRightBuildingToTestDisplay("You finished the build stage successfully! Great Job!!", 2f));
                }
                else
                {
                    /////// POP-UP (DEPENDIENDO DE ALGUNA HERRAMIENTA O HABILIDAD DECIR M�S O MENOS COSAS RESPECTO AL FALLO)
                    StartCoroutine(WarningBuildingToPlanDisplay("It seems that the mechanism you used is not the best suit for the architecture you selected. \nYou must plan again.", 3f));
                }
            }
        }
        else if (stage_title_text.text == "TEST")
        {
            stage_title_text.text = "RELEASE";
            DeactivatedStages();
            release_stage.SetActive(true);
            tutorial_trigger = release_stage.GetComponent<TutorialTextTrigger>();
            tutorial_trigger.TriggerTutorial();
            if (checklist_window_animator.GetBool("IsOpen") == true) checklist_items_window.gameObject.SetActive(false);
        }
        else if (stage_title_text.text == "RELEASE")
        {
            stage_title_text.text = "DEPLOY";
            DeactivatedStages();
            deploy_stage.SetActive(true);
            tutorial_trigger = deploy_stage.GetComponent<TutorialTextTrigger>();
            tutorial_trigger.TriggerTutorial();
            if (checklist_window_animator.GetBool("IsOpen") == true) checklist_items_window.gameObject.SetActive(false);
        }
        else if (stage_title_text.text == "DEPLOY")
        {
            stage_title_text.text = "OPERATE";
            DeactivatedStages();
            operate_stage.SetActive(true);
            tutorial_trigger = operate_stage.GetComponent<TutorialTextTrigger>();
            tutorial_trigger.TriggerTutorial();
            if (checklist_window_animator.GetBool("IsOpen") == true) checklist_items_window.gameObject.SetActive(false);
        }
        else if (stage_title_text.text == "OPERATE")
        {
            stage_title_text.text = "MONITOR";
            DeactivatedStages();
            monitor_stage.SetActive(true);
            tutorial_trigger = monitor_stage.GetComponent<TutorialTextTrigger>();
            tutorial_trigger.TriggerTutorial();
            if (checklist_window_animator.GetBool("IsOpen") == true) checklist_items_window.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("End of the stages.");
        }
    }

    void ChecklistButton()
    {
        checklist_items_window.gameObject.SetActive(true);
        checklist_close_button.gameObject.SetActive(true);
        checklist_window_animator.SetBool("IsOpen", true);

        if (stage_title_text.text == "PLAN")
        {
            checklist_text = "Select architecture. \n\n(Optional) Select any ability you want to level up. \n\nSelect a stage for the tool shown.";
            checklist_items_text.text = checklist_text;
        }
        else if (stage_title_text.text == "CODE")
        {
            checklist_text = "Choose the elements that compose the project solution.";
            checklist_items_text.text = checklist_text;
        }
        else if (stage_title_text.text == "BUILD")
        {
            checklist_text = "Place each element in a category.";
            checklist_items_text.text = checklist_text;
        }
        else if (stage_title_text.text == "TEST")
        {
            checklist_text = "";
            checklist_items_text.text = checklist_text;
        }
        else if (stage_title_text.text == "RELEASE")
        {
            checklist_text = "";
            checklist_items_text.text = checklist_text;
        }
        else if (stage_title_text.text == "DEPLOY")
        {
            checklist_text = "";
            checklist_items_text.text = checklist_text;
        }
        else if (stage_title_text.text == "OPERATE")
        {
            checklist_text = "";
            checklist_items_text.text = checklist_text;
        }
        else
        {
            Debug.Log("End of the stages.");
        }
    }

    void ChecklistCloseButton()
    {
        checklist_window_animator.SetBool("IsOpen", false);
        checklist_close_button.gameObject.SetActive(false);
    }

    void AbilitiesLevelsWindowButton()
    {
        abilities_levels_window_animator.SetBool("IsAbilitiesLevelsWindowOpen", true);
        DeactivateAbilitiesLevelsWindow();
        UpdateAbilitiesLevelWindow();
    }

    void DeactivateAbilitiesLevelsWindow(){
        plan_ability_summary_image.gameObject.SetActive(false);
        code_ability_summary_image.gameObject.SetActive(false);
        build_ability_summary_image.gameObject.SetActive(false);
        test_ability_summary_image.gameObject.SetActive(false);
        release_ability_summary_image.gameObject.SetActive(false);
        deploy_ability_summary_image.gameObject.SetActive(false);
        operate_ability_summary_image.gameObject.SetActive(false);
        monitor_ability_summary_image.gameObject.SetActive(false);
    }

    void UpdateAbilitiesLevelWindow(){
        int plan_level = player_controller_script.abilities_levels["plan_level"];
        if(plan_level >= 1) plan_ability_summary_image.gameObject.SetActive(true);
        if(plan_level == 1) plan_ability_summary_image.sprite = star_icon_level_1;
        else if(plan_level == 2) plan_ability_summary_image.sprite = star_icon_level_2;
        else if(plan_level == 3) plan_ability_summary_image.sprite = star_icon_level_3;

        int code_level = player_controller_script.abilities_levels["code_level"];
        if(code_level >= 1) code_ability_summary_image.gameObject.SetActive(true);
        if(code_level == 1) code_ability_summary_image.sprite = star_icon_level_1;
        else if(code_level == 2) code_ability_summary_image.sprite = star_icon_level_2;
        else if(code_level == 3) code_ability_summary_image.sprite = star_icon_level_3;

        int build_level = player_controller_script.abilities_levels["build_level"];
        if(build_level >= 1) build_ability_summary_image.gameObject.SetActive(true);
        if(build_level == 1) build_ability_summary_image.sprite = star_icon_level_1;
        else if(build_level == 2) build_ability_summary_image.sprite = star_icon_level_2;
        else if(build_level == 3) build_ability_summary_image.sprite = star_icon_level_3;

        int test_level = player_controller_script.abilities_levels["test_level"];
        if(test_level >= 1) test_ability_summary_image.gameObject.SetActive(true);
        if(test_level == 1) test_ability_summary_image.sprite = star_icon_level_1;
        else if(test_level == 2) test_ability_summary_image.sprite = star_icon_level_2;
        else if(test_level == 3) test_ability_summary_image.sprite = star_icon_level_3;

        int release_level = player_controller_script.abilities_levels["release_level"];
        if(release_level >= 1) release_ability_summary_image.gameObject.SetActive(true);
        if(release_level == 1) release_ability_summary_image.sprite = star_icon_level_1;
        else if(release_level == 2) release_ability_summary_image.sprite = star_icon_level_2;
        else if(release_level == 3) release_ability_summary_image.sprite = star_icon_level_3;

        int deploy_level = player_controller_script.abilities_levels["deploy_level"];
        if(release_level >= 1) release_ability_summary_image.gameObject.SetActive(true);
        if(deploy_level == 1) deploy_ability_summary_image.sprite = star_icon_level_1;
        else if(deploy_level == 2) deploy_ability_summary_image.sprite = star_icon_level_2;
        else if(deploy_level == 3) deploy_ability_summary_image.sprite = star_icon_level_3;

        int operate_level = player_controller_script.abilities_levels["operate_level"];
        if(operate_level >= 1) operate_ability_summary_image.gameObject.SetActive(true);
        if(operate_level == 1) operate_ability_summary_image.sprite = star_icon_level_1;
        else if(operate_level == 2) operate_ability_summary_image.sprite = star_icon_level_2;
        else if(operate_level == 3) operate_ability_summary_image.sprite = star_icon_level_3;

        int monitor_level = player_controller_script.abilities_levels["monitor_level"];
        if(monitor_level >= 1) monitor_ability_summary_image.gameObject.SetActive(true);
        if(monitor_level == 1) monitor_ability_summary_image.sprite = star_icon_level_1;
        else if(monitor_level == 2) monitor_ability_summary_image.sprite = star_icon_level_2;
        else if(monitor_level == 3) monitor_ability_summary_image.sprite = star_icon_level_3;
    }

    void AbilitiesLevelsWindowCloseButton()
    {
        abilities_levels_window_animator.SetBool("IsAbilitiesLevelsWindowOpen", false);
    }

    IEnumerator WarningWindowDisplay(string text, float delay)
    {
        warning_checklist_window.gameObject.SetActive(true);
        warning_checklist_window_text.text = text;
        warning_checklist_window_animator.SetBool("WarningChecklistIsOpen", true);
        yield return new WaitForSeconds(delay);
        warning_checklist_window_animator.SetBool("WarningChecklistIsOpen", false);
    }

    IEnumerator WarningRightBuildingToTestDisplay(string text, float delay)
    {
        warning_build_window.gameObject.SetActive(true);
        warning_build_window_text.text = text;
        warning_build_window_animator.SetBool("IsWarningCategorizeOpen", true);
        yield return new WaitForSeconds(delay);
        warning_build_window_animator.SetBool("IsWarningCategorizeOpen", false);

        // go to test stage
        stage_title_text.text = "TEST";
        DeactivatedStages();
        test_stage.SetActive(true);
        tutorial_trigger = test_stage.GetComponent<TutorialTextTrigger>();
        tutorial_trigger.TriggerTutorial();
        if (checklist_window_animator.GetBool("IsOpen") == true) checklist_items_window.gameObject.SetActive(false);
    }

    public IEnumerator WarningBuildingToPlanDisplay(string text, float delay)
    {
        warning_build_window.gameObject.SetActive(true);
        warning_build_window_text.text = text;
        warning_build_window_animator.SetBool("IsWarningCategorizeOpen", true);
        yield return new WaitForSeconds(delay);
        warning_build_window_animator.SetBool("IsWarningCategorizeOpen", false);

        // go to the plan stage
        DeactivatedStages();
        plan_stage.SetActive(true);
        stage_title_text.text = "PLAN";
        plan_deck_controller_script.DeactivateParts();
        plan_deck_controller_script.plan_project.SetActive(true);
        plan_deck_controller_script.word_index = 0;
        plan_deck_controller_script.deck_button_text.text = "Project";
        plan_deck_controller_script.UpdateButtonText();

        // SETEAR VARIABLES Y DEMAS (Tool y Abilities)
        plan_deck_controller_script.leveled_up_card = false;
        plan_deck_controller_script.is_selected_random_card = false;
        plan_deck_controller_script.is_generated_random_card = false;
    }

    void UseAbilityButtonHandler(){
        if (stage_title_text.text == "PLAN") {
            int level = player_controller_script.abilities_levels["plan_level"];
            if(level == 3){
                string message = "";
                string name = plan_deck_controller_script.random_tool_card.id; 
                
                if(name == "bitbucket") message = " "; // code
                else if(name == "docker") message = " "; // release, deploy, build
                else if(name == "puppet") message = " "; // operate, build
                else if(name == "github") message = " "; // code
                else if(name == "junit") message = " "; // test
                else if(name == "gradle") message = " "; // build
                else if(name == "chef") message = " "; // operate, release, build
                else if(name == "new_relic") message = " "; // monitor
                else if(name == "vagrant") message = " "; // test
                else if(name == "jira") message = " "; // plan, release
                else if(name == "powershell") message = " "; // operate
                else if(name == "selenium") message = " "; // test
                else if(name == "datadog") message = " "; // monitor
                else if(name == "aws") message = " "; // deploy
                else if(name == "jenkins") message = " ";  // release
                else if(name == "git") message = " "; // plan
                else if(name == "grafana") message = " "; // monitor
                else if(name == "ansible") message = " "; // operate, release, build

                // POP-UP (message)
                StartCoroutine(WarningWindowDisplay(message, 2));
            }
            if(level == 2){
                // POP-UP (GUATA is not the most appropiate architecture)
                StartCoroutine(WarningWindowDisplay("Hint: The Water oriented architecture is not the most appropriate architecture.", 2));
            }
            else{
                // POP-UP (You can't do anything with this ability level, level it up)
                StartCoroutine(WarningWindowDisplay("Hint: You can't do anything with this ability level, level it up.", 2));
            }
        }
        else if(stage_title_text.text == "CODE"){
            int level = player_controller_script.abilities_levels["code_level"];
            Debug.Log(level);
            if(level == 1){
                // POP-UP (Feather is not an appropiate component)
                StartCoroutine(WarningWindowDisplay("Hint: Feather is not an appropiate component for this project.", 2));
            }
            else if(level == 2){
                // POP-UP (Feather and burger are not appropiate compoments)
                StartCoroutine(WarningWindowDisplay("Hint: Feather and burger are not appropiate components for this project.", 3));
            }
            else if(level == 3){
                // POP-UP (Feather, burger and elastic are not appropiate components)
                StartCoroutine(WarningWindowDisplay("Hint: Feather, burger and elastic are not appropiate components for this project.", 3));
            }
            else{
                // POP-UP (You can't do anything with this ability level, level it up)
                StartCoroutine(WarningWindowDisplay("Hint: You can't do anything with this ability level, level it up.", 2));
            }
        }
        else if(stage_title_text.text == "BUILD"){
            int level = player_controller_script.abilities_levels["build_level"];
            if(level <= 1){
                // POP-UP (You can't do anything with this ability level, level it up)
                StartCoroutine(WarningWindowDisplay("Hint: You can't do anything with this ability level, level it up.", 2));
            }
            else if(level == 2){
                // Position of the element in the center (once)
                if(is_build_ability_used < 1){
                    is_build_ability_used += 1;
                    Card card = build_carousel_script.cards[build_carousel_script.center_index];
                    if(card.category == "hold") {
                        // POP-UP ()
                        StartCoroutine(WarningWindowDisplay("Hint: Place the "+card.card_title+" card in the second black box from top to bottom.", 3));
                    }
                    else if(card.category == "mechanism"){
                        // POP-UP ()
                        StartCoroutine(WarningWindowDisplay("Hint: Place the " + card.card_title + " card in the first black box from top to bottom.", 3));
                    }
                    else if(card.category == "impact"){
                        // POP-UP ()
                        StartCoroutine(WarningWindowDisplay("Hint: Place the " + card.card_title + " card in the third black box from top to bottom.", 3));
                    }
                    else if(card.category == "bait"){
                        // POP-UP ()
                        StartCoroutine(WarningWindowDisplay("Hint: Place the " + card.card_title + " card in the fourth black box from top to bottom.", 3));
                    }
                }
                else{
                    // POP-UP (You have already used this ability)
                    StartCoroutine(WarningWindowDisplay("You have already used this ability.", 2));
                }
            }
            else if(level == 3){
                if(is_build_ability_used < 2){
                    is_build_ability_used += 1;
                    Card card_1 = build_carousel_script.cards[build_carousel_script.center_index];
                    if(card_1.category == "hold") {
                        // POP-UP ()
                        StartCoroutine(WarningWindowDisplay("Hint: Place the " + card_1.card_title + " card in the second black box from top to bottom.", 3));
                    }
                    else if(card_1.category == "mechanism"){
                        // POP-UP ()
                        StartCoroutine(WarningWindowDisplay("Hint: Place the " + card_1.card_title + " card in the first black box from top to bottom.", 3));
                    }
                    else if(card_1.category == "impact"){
                        // POP-UP ()
                        StartCoroutine(WarningWindowDisplay("Hint: Place the " + card_1.card_title + " card in the third black box from top to bottom.", 3));
                    }
                    else if(card_1.category == "bait"){
                        // POP-UP ()
                        StartCoroutine(WarningWindowDisplay("Hint: Place the " + card_1.card_title + " card in the fourth black box from top to bottom.", 3));
                    }
                }
                else{
                    // POP-UP (You have already used this ability)
                    StartCoroutine(WarningWindowDisplay("You have already used this ability.", 2));
                }
            }
        }
    }

    void UseToolButtonHanlder(){
        if (stage_title_text.text == "PLAN") {
            if(player_controller_script.can_use_plan_tool == true){
                // POP-UP (Blueprint)
            }
            else{
                // POP-UP (You can't use this tool)
                StartCoroutine(WarningWindowDisplay("You can't use this tool.", 2));
            }
        }
        else if(stage_title_text.text == "CODE"){
            if(player_controller_script.can_use_code_tool == true){
                if(player_controller_script.selected_architecture.id == "architecture_1"){
                    // POP-UP (Arquitectura terrestre -> Pulley)
                    StartCoroutine(WarningWindowDisplay("Hint: The mechanism you should use is the Pulley.", 4));
                }
                else{
                    // POP-UP (Arquitectura area -> Ballon)
                    StartCoroutine(WarningWindowDisplay("Hint: The mechanism you should use is the Balloon.", 4));
                }
            }
            else{
                StartCoroutine(WarningWindowDisplay("You can't use this tool.", 2));
            }
        }
        else if(stage_title_text.text == "BUILD"){
            // Category of the element in the center (once)
            if(player_controller_script.can_use_build_tool == true){
                if(is_build_tool_used == false){
                    is_build_tool_used = true;
                    Card card = build_carousel_script.cards[build_carousel_script.center_index];

                    // POP-UP (card.category)
                    StartCoroutine(WarningWindowDisplay("Hint: You should place the "+ card.card_title + ", in the " + card.category + " category.", 4));
                }
                else{
                    // POP-UP (You have already used this tool)
                    StartCoroutine(WarningWindowDisplay("You have already used this tool.", 2));
                }
            }
            else{
                StartCoroutine(WarningWindowDisplay("You can't use this tool.", 2));
            }
        }
    }
}
