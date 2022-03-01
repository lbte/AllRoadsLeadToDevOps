using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class StageController : MonoBehaviour
{
    public Button next_stage_button;
    public Text stage_title_text;
    public Image devops_cycle_image;

    private Button checklist_button;
    private Button checklist_close_button;
    public Button use_ability_button;
    public Button use_tool_button;
    public Button how_to_play_button;
    public Button how_to_play_close_button;
    private string checklist_text;
    private Image checklist_items_window;
    private Text checklist_items_text;
    public Animator checklist_window_animator;
    public Animator warning_checklist_window_animator;
    private Image warning_checklist_window;
    public Text warning_checklist_window_text;
    public Image how_to_play_image;
    public Image how_to_play_background_image;
    public Button next_button_tools_howto;

    // abilities levels window
    public Image abilities_levels_window;
    public Animator abilities_levels_window_animator;
    public Button abilities_levels_window_close_button;
    public Button abilities_levels_button;

    // stage fade transition
    public Animator stage_transition;
    public Image stage_transition_image;

    public GameObject plan_stage;
    public GameObject code_stage;
    public GameObject build_stage;
    public GameObject test_stage;
    public GameObject release_stage;
    public GameObject deploy_stage;
    public GameObject operate_stage;
    public GameObject monitor_stage;

    private TutorialTextTrigger tutorial_trigger;

    private CodeCarouselController code_carousel_script;
    private PlayerController player_controller_script;
    private BuildCarouselController build_carousel_script;
    private BuildDeckController build_deck_controller_script;
    public PlanDeckController plan_deck_controller_script;
    private TimeController time_controller_script;


    public bool is_build_tool_used = false;
    public int is_build_ability_used = 0;  // min = 0, max = 2
    public int is_release_ability_used = 0;  // min = 0, max = 1
    public int is_deploy_ability_used = 0; // min = 0, max = 1
    public int is_deploy_tool_used = 0; // min = 0, max = 1
    public int is_operate_ability_used = 0;  // min = 0, max = 2
    public int is_monitor_ability_used = 0; // min = 0, max = 2
    public int is_release_tool_used = 0; // min = 0, max = 1
    public bool is_operate_failed = false;
    public bool is_test_tool_used = false;
    public bool is_test_ability_used = false;
    public bool is_test_failed = false;
    public float fail_operate_probability = 0.4f;
    public bool is_operate_tool_used = false;
    public bool is_plan_tool_used = false;
    public bool is_monitor_failed = false;

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

    // sprites for the devops cycle phase displayed on the top of the screen.
    public Sprite plan_devops_cycle;
    public Sprite code_devops_cycle;
    public Sprite build_devops_cycle;
    public Sprite test_devops_cycle;
    public Sprite release_devops_cycle;
    public Sprite deploy_devops_cycle;
    public Sprite operate_devops_cycle;
    public Sprite monitor_devops_cycle;

    // TEST
    public Button left_test_card;
    public Button right_test_card;

    public Image left_test_card_image;
    public Image right_test_card_image;

    public Text left_test_card_title;
    public Text right_test_card_title;

    public Text left_test_card_description;
    public Text right_test_card_description;

    public Image test_result_image_wolf;
    public Sprite test_result_wolf_success;
    public Sprite test_result_wolf_failure;

    public Image operate_result_image_bunny;
    public Sprite operate_result_bunny_success;
    public Sprite operate_result_bunny_failure;


    // video player for release
    public VideoPlayer videoRelease;


    // Blueprints for architectures tool
    public Image blueprint_ground_architecture;
    public Image blueprint_air_architecture;
    public Button close_button_blueprint_ground_architecture;
    public Button close_button_blueprint_air_architecture;
    public Text text_blueprint_ground_architecture;
    public Text text_blueprint_air_architecture;


    // Monitor Images
    public Image monitor_result_image_success;
    public Image monitor_result_image_failure;

    // for deploy animation
    public Button airtrap_deploy_animation_button;
    public Animator airtrap_deploy_animation_animator;
    public Button groundtrap_deploy_animation_button;
    public Animator groundtrap_deploy_animation_animator;

    public Image balloon_image;
    public Sprite sprite_balloon_nudo;
    public Sprite sprite_balloon_suelto;
    public Image pulley_image;
    public Sprite sprite_pulley_nudo;
    public Sprite sprite_pulley_suelta;
    public GameObject deploy_air_trap;
    public GameObject deploy_ground_trap;

    // KPI Score variables
    public bool is_plan_visited = false;
    public bool is_code_visited = false;
    public bool is_build_visited = false;
    public bool is_test_visited = false;
    public bool is_release_visited = false;
    public bool is_deploy_visited = false;
    public bool is_operate_visited = false;
    public bool is_monitor_visited = false;

    // for the load results part
    public Image last_game_records_background_image;
    public Button exit_game_button;

    // sprites for how to play
    public Sprite plan_project_howto_sprite;
    public Sprite plan_architecture_howto_sprite;
    public Sprite plan_abilities_howto_sprite;
    public Sprite plan_tools_howto_sprite;
    public Sprite plan_tools_howto_sprite2;
    public Sprite code_howto_sprite;
    public Sprite build_categorize_howto_sprite;
    public Sprite build_building_howto_sprite;
    public Sprite test_howto_sprite;
    public Sprite release_howto_sprite;
    public Sprite deploy_howto_sprite;
    public Sprite operate_howto_sprite;
    public Sprite monitor_howto_sprite;

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
        code_carousel_script = GameObject.Find("CodeItems").GetComponent<CodeCarouselController>();
        time_controller_script = GameObject.Find("Views").GetComponent<TimeController>();

        warning_checklist_window.gameObject.SetActive(false);
        checklist_close_button.gameObject.SetActive(false);
        abilities_levels_window_animator.SetBool("IsAbilitiesLevelsWindowOpen", false);
        how_to_play_image.gameObject.SetActive(false);
        how_to_play_close_button.gameObject.SetActive(false);
        how_to_play_background_image.gameObject.SetActive(false);
        next_button_tools_howto.gameObject.SetActive(false);

        checklist_close_button.onClick.AddListener(ChecklistCloseButton);
        checklist_button.onClick.AddListener(ChecklistButton);
        abilities_levels_button.onClick.AddListener(AbilitiesLevelsWindowButton);
        abilities_levels_window_close_button.onClick.AddListener(AbilitiesLevelsWindowCloseButton);
        next_stage_button.onClick.AddListener(NextStageButton);
        use_ability_button.onClick.AddListener(UseAbilityButtonHandler);
        use_tool_button.onClick.AddListener(UseToolButtonHanlder);
        close_button_blueprint_ground_architecture.onClick.AddListener(BlueprintGroundCloseButton);
        close_button_blueprint_air_architecture.onClick.AddListener(BlueprintAirCloseButton);
        exit_game_button.onClick.AddListener(ExitGame);
        how_to_play_button.onClick.AddListener(HowToPlayDisplay);
        how_to_play_close_button.onClick.AddListener(HowToPlayClose);
        next_button_tools_howto.onClick.AddListener(HowToPlayToolsNext);

        airtrap_deploy_animation_button.onClick.AddListener(AirTrapDeployAnimation);
        groundtrap_deploy_animation_button.onClick.AddListener(GroundTrapDeployAnimation);

        videoRelease = GameObject.Find("VideoRelease").GetComponent<VideoPlayer>();
        videoRelease.playOnAwake = false;

        stage_title_text.text = "PLAN";
        devops_cycle_image.sprite = plan_devops_cycle;
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
                if(is_plan_visited == false) player_controller_script.player_final_score += 20;
                is_plan_visited = true;
                StartCoroutine(LoadCodeStage(1));
            }
            else
            {
                // Pop-up: mensaje "Looks like something is missing, check your checklist"
                StartCoroutine(WarningWindowDisplay("Looks like there's something missing.\n\nCheck your checklist!", 2));
            }
        }
        else if (stage_title_text.text == "CODE")
        {   
            if(code_carousel_script.selected_cards_count == 4){
                // Save selected cards in PlayerController
                code_carousel_script = GameObject.Find("CodeItems").GetComponent<CodeCarouselController>();
                player_controller_script = GameObject.Find("Views").GetComponent<PlayerController>();

                if(is_code_visited == false) player_controller_script.player_final_score += 20;
                is_code_visited = true;

                StartCoroutine(LoadBuildStage(1)); 
            }
            else{
                StartCoroutine(WarningWindowDisplay("You have to select 4 cards.", 2));
            }

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

                player_controller_script.player_final_score -= 5;

                /////// POP-UP
                StartCoroutine(WarningBuildingToPlanDisplay("It looks like the building you made is not the right one. \nYou must plan again.", 4f));

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

                // Mechanism is correct
                if (((mechanism_card.id == "balloon_fair" || mechanism_card.id == "turkey_balloon") && (player_controller_script.selected_architecture.id == "architecture_2"))
                || ((mechanism_card.id == "alpinism_pulley" || mechanism_card.id == "well_pulley") && player_controller_script.selected_architecture.id == "architecture_1"))
                {   
                    if(is_build_visited == false) player_controller_script.player_final_score += 20;
                    is_build_visited = true;

                    /////// POP-UP
                    is_test_failed = false;
                    is_test_tool_used = false;
                    StartCoroutine(WarningRightBuildingToTestDisplay("You finished the build stage successfully! Great Job!!", 3f));
                }
                else  // Mechanism is not correct
                {
                    player_controller_script.player_final_score -= 5;

                    StartCoroutine(WarningBuildingToPlanDisplay("It seems that the mechanism you used is not the best suit for the architecture you selected. \nYou must plan again.", 4f));
                }
            }
        }
        else if (stage_title_text.text == "TEST")
        {   
            if (is_test_failed == false){

                if(is_test_visited == false) player_controller_script.player_final_score += 20;
                is_test_visited = true;

                //warning_checklist_window_text.gameObject.SetActive(false);
                warning_checklist_window.gameObject.SetActive(false);
                StartCoroutine(LoadReleaseStage(1));

                // play the video on the release stage
                videoRelease.Play();
                tutorial_trigger = release_stage.GetComponent<TutorialTextTrigger>();
                tutorial_trigger.TriggerTutorial();
            }
            else{
                player_controller_script.player_final_score -= 5;

                StartCoroutine(WarningToPlanDisplay("As you failed the testing phase, you have to plan again.", 4f));
            }
        }
        else if (stage_title_text.text == "RELEASE")
        {
            if(is_release_visited == false) player_controller_script.player_final_score += 20;
            is_release_visited = true;

            StartCoroutine(LoadDeployStage(1));

            if(player_controller_script.selected_architecture.id == "architecture_2")
            {
                deploy_air_trap.SetActive(true);
                deploy_ground_trap.SetActive(false);
                balloon_image.sprite = sprite_balloon_nudo;
            }
            else if(player_controller_script.selected_architecture.id == "architecture_1")
            {
                deploy_ground_trap.SetActive(true);
                deploy_air_trap.SetActive(false);
                pulley_image.sprite = sprite_pulley_nudo;
            }
        }
        else if (stage_title_text.text == "DEPLOY")
        {
            is_operate_failed = false;

            if(is_deploy_visited == false) player_controller_script.player_final_score += 20;
            is_deploy_visited = true;

            StartCoroutine(LoadOperateStage(1));
        }
        else if (stage_title_text.text == "OPERATE")
        {   
            if(is_operate_failed == false) {
                is_monitor_failed = false;
                is_monitor_ability_used = 0;

                StartCoroutine(LoadMonitorStage(1));

                // Checks if monitor fails
                Card blacksmith = code_carousel_script.deck[0];
                Card piano_fight = code_carousel_script.deck[11];
                Card piano_old = code_carousel_script.deck[6];
                Card cowboy = code_carousel_script.deck[1];
                Card pants = code_carousel_script.deck[5];
                Card charger = code_carousel_script.deck[17];
                Card turkey = code_carousel_script.deck[23];
                Card well = code_carousel_script.deck[4];
                Card compost = code_carousel_script.deck[18];
                Card jam = code_carousel_script.deck[3];

                if(piano_fight.selected == true || piano_old.selected == true) is_monitor_failed = true;
                else if(blacksmith.selected == true) is_monitor_failed = true;
                else if(cowboy.selected == true) is_monitor_failed = true;
                else if(pants.selected == true) is_monitor_failed = true;
                else if(charger.selected == true) is_monitor_failed = true;
                else if(turkey.selected == true) is_monitor_failed = true;
                else if(well.selected == true) is_monitor_failed = true;
                else if(compost.selected == true) is_monitor_failed = true;
                else if(jam.selected == true) is_monitor_failed = true;

                if(is_monitor_failed == true){
                    // Lobo triste -> Returns to plan
                    monitor_result_image_failure.gameObject.SetActive(true);
                    monitor_result_image_success.gameObject.SetActive(false);
                }
                else{
                    monitor_result_image_failure.gameObject.SetActive(false);
                    monitor_result_image_success.gameObject.SetActive(true);
                    // Lobo feliz -> Wins the game
                }
            }
            else{
                StartCoroutine(WarningToPlanDisplay("Your trap failed the operate phase.", 3f));
            }
        }
        else if (stage_title_text.text == "MONITOR"){
            if(is_monitor_failed == true){
                player_controller_script.player_final_score -= 5;

                // Lobo triste -> Returns to plan
                StartCoroutine(WarningToPlanDisplay("Your materials didn't last long enough in order to be used again, check the way you gathered your elements and try to make it more durable this time.", 5f));
            }
            else{
                if(is_monitor_visited == false) player_controller_script.player_final_score += 20;
                is_monitor_visited = true;

                time_controller_script.is_timer_active = false;
                player_controller_script.player_final_time = time_controller_script.timer_text.text;
                // Lobo feliz -> Wins the game

                StartCoroutine(MonitorLoadLastGameData());
            }
        }
    }

    void ChecklistButton()
    {
        checklist_items_window.gameObject.SetActive(true);
        checklist_close_button.gameObject.SetActive(true);
        checklist_window_animator.SetBool("IsOpen", true);

        if (stage_title_text.text == "PLAN")
        {
            checklist_text = "Select an architecture. \n\n(Optional) Select any ability you want to level up. \n\nSelect a stage for the tool shown.";
            checklist_items_text.text = checklist_text;
        }
        else if (stage_title_text.text == "CODE")
        {
            checklist_text = "Choose the elements that compose the project solution.";
            checklist_items_text.text = checklist_text;
        }
        else if (stage_title_text.text == "BUILD")
        {
            checklist_text = "Place each element in a category in the categorize part. \n\nPlace each element in the scenario of the trap.";
            checklist_items_text.text = checklist_text;
        }
        else if (stage_title_text.text == "TEST")
        {
            checklist_text = "No items to check here.";
            checklist_items_text.text = checklist_text;
        }
        else if (stage_title_text.text == "RELEASE")
        {
            checklist_text = "No items to check here.";
            checklist_items_text.text = checklist_text;
        }
        else if (stage_title_text.text == "DEPLOY")
        {
            checklist_text = "Select the deploy button to deploy the trap.";
            checklist_items_text.text = checklist_text;
        }
        else if (stage_title_text.text == "OPERATE")
        {
            checklist_text = "No items to check here.";
            checklist_items_text.text = checklist_text;
        }
        else
        {
            checklist_text = "No items to check here.";
            checklist_items_text.text = checklist_text;
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
        if(deploy_level >= 1) deploy_ability_summary_image.gameObject.SetActive(true);
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

    // loads the code stage
    IEnumerator LoadCodeStage(float delay)
    {
        stage_transition.SetTrigger("StartStageFade");
        yield return new WaitForSeconds(delay);
        stage_transition.SetTrigger("EndStageFade");

        stage_title_text.text = "CODE";
        DeactivatedStages();
        code_stage.SetActive(true);

        devops_cycle_image.sprite = code_devops_cycle;

        tutorial_trigger = code_stage.GetComponent<TutorialTextTrigger>();
        tutorial_trigger.TriggerTutorial();
        if (checklist_window_animator.GetBool("IsOpen") == true) checklist_items_window.gameObject.SetActive(false);
    }

    // loads the build stage
    IEnumerator LoadBuildStage(float delay)
    {   
        stage_transition.SetTrigger("StartStageFade");
        yield return new WaitForSeconds(delay);
        stage_transition.SetTrigger("EndStageFade");

        stage_title_text.text = "BUILD";
        DeactivatedStages();

        //Starts on carousel (Categorize)
        build_stage.SetActive(true);
        devops_cycle_image.sprite = build_devops_cycle;

        /////// reset build categorize and landscape
        player_controller_script.build_categorize = true;

        build_deck_controller_script = GameObject.Find("Build").GetComponent<BuildDeckController>();
        next_stage_button.gameObject.SetActive(false);
        build_deck_controller_script.word_index = 0; // reset to go back to the categorize view and be able to go to the building
        build_deck_controller_script.deck_button_text.text = "Categorize";
        build_deck_controller_script.carousel.SetActive(true);
        build_deck_controller_script.up_arrow_button.gameObject.SetActive(true);
        build_deck_controller_script.down_arrow_button.gameObject.SetActive(true);
        build_deck_controller_script.landscape.SetActive(false);
        build_deck_controller_script.air_trap.SetActive(false);
        build_deck_controller_script.ground_trap.SetActive(false);

        // Update selected cards in BuildCarouselController (from PlayerController)
        build_carousel_script = GameObject.Find("BuildItems").GetComponent<BuildCarouselController>();
        build_carousel_script.AssignSelectedCodeCards();
        build_carousel_script.UpdateCardImages();


        tutorial_trigger = build_stage.GetComponent<TutorialTextTrigger>();
        tutorial_trigger.TriggerTutorial();
        if (checklist_window_animator.GetBool("IsOpen") == true) checklist_items_window.gameObject.SetActive(false);
    }

    IEnumerator LoadReleaseStage(float delay)
    {
        // transition to the release stage
        stage_transition.SetTrigger("StartStageFade");
        yield return new WaitForSeconds(delay);
        stage_transition.SetTrigger("EndStageFade");

        stage_title_text.text = "RELEASE";
        DeactivatedStages();
        release_stage.SetActive(true);
        devops_cycle_image.sprite = release_devops_cycle;
    }

    IEnumerator LoadDeployStage(float delay)
    {
        // transition to the deploy stage
        stage_transition.SetTrigger("StartStageFade");
        yield return new WaitForSeconds(delay);
        stage_transition.SetTrigger("EndStageFade");

        if (videoRelease.isPlaying)
        {
            videoRelease.Stop();
        }

        stage_title_text.text = "DEPLOY";
        DeactivatedStages();
        deploy_stage.SetActive(true);
        devops_cycle_image.sprite = deploy_devops_cycle;
        tutorial_trigger = deploy_stage.GetComponent<TutorialTextTrigger>();
        tutorial_trigger.TriggerTutorial();
        if (checklist_window_animator.GetBool("IsOpen") == true) checklist_items_window.gameObject.SetActive(false);
    }

    IEnumerator LoadOperateStage(float delay)
    {
        // transition to the operate stage
        stage_transition.SetTrigger("StartStageFade");
        yield return new WaitForSeconds(delay);
        stage_transition.SetTrigger("EndStageFade");

        stage_title_text.text = "OPERATE";
        DeactivatedStages();
        operate_stage.SetActive(true);
        devops_cycle_image.sprite = operate_devops_cycle;
        tutorial_trigger = operate_stage.GetComponent<TutorialTextTrigger>();
        tutorial_trigger.TriggerTutorial();
        if (checklist_window_animator.GetBool("IsOpen") == true) checklist_items_window.gameObject.SetActive(false);

        fail_operate_probability = 0.4f;

        // Check if fails operate
        Card blacksmith = code_carousel_script.deck[0];
        Card piano = code_carousel_script.deck[11];
        Card cowboy = code_carousel_script.deck[1];
        Card pants = code_carousel_script.deck[5];
        Card charger = code_carousel_script.deck[17];
        Card turkey = code_carousel_script.deck[23];
        Card well = code_carousel_script.deck[4];
        Card compost = code_carousel_script.deck[18];
        Card jam = code_carousel_script.deck[3];

        if (blacksmith.selected == true) fail_operate_probability += 0.1f;
        if (piano.selected == true) fail_operate_probability += 0.1f;
        if (cowboy.selected == true) fail_operate_probability += 0.1f;
        if (pants.selected == true) fail_operate_probability += 0.1f;
        if (charger.selected == true) fail_operate_probability += 0.1f;
        if (turkey.selected == true) fail_operate_probability += 0.1f;
        if (well.selected == true) fail_operate_probability += 0.1f;
        if (compost.selected == true) fail_operate_probability += 0.1f;
        if (jam.selected == true) fail_operate_probability += 0.1f;

        // Tool
        if (player_controller_script.can_use_operate_tool == true && is_operate_tool_used == false)
        {
            is_operate_tool_used = true;
            fail_operate_probability -= 0.1f;
        }

        // Ability
        int level = player_controller_script.abilities_levels["operate_level"];
        if (level == 2)
        {
            if (is_operate_ability_used < 1)
            {
                is_operate_ability_used += 1;
                fail_operate_probability -= 0.05f;
            }
        }
        else if (level == 3)
        {
            if (is_operate_ability_used < 2)
            {
                is_operate_ability_used += 1;
                fail_operate_probability -= 0.1f;
            }
        }

        // Show result -> Rabbit in a cage or not ...
        float random_probability = (float)Random.Range(0.0f, 1.0f);
        Debug.Log(random_probability);
        Debug.Log(fail_operate_probability);
        if (random_probability <= fail_operate_probability)
        {
            player_controller_script.player_final_score -= 5;
            is_operate_failed = true;

            // Operate fails -> Returns to plan
            // IMAGEN CONEJITO NO ATRAPADO
            operate_result_image_bunny.sprite = operate_result_bunny_failure;
        }
        else
        {   
            is_operate_failed = false;

            if (is_operate_visited == false) player_controller_script.player_final_score += 20;
            is_operate_visited = true;

            // Operate ok -> next
            // IMAGEN CONEJITO ATRAPADO
            operate_result_image_bunny.sprite = operate_result_bunny_success;
            StartCoroutine(WarningWindowDisplay("You finished the operate stage successfully! Great Job!!", 4f));
        }
    }

    IEnumerator LoadMonitorStage(float delay)
    {
        // transition to the monitor stage
        stage_transition.SetTrigger("StartStageFade");
        yield return new WaitForSeconds(delay);
        stage_transition.SetTrigger("EndStageFade");

        stage_title_text.text = "MONITOR";
        DeactivatedStages();
        monitor_stage.SetActive(true);
        devops_cycle_image.sprite = monitor_devops_cycle;
        tutorial_trigger = monitor_stage.GetComponent<TutorialTextTrigger>();
        tutorial_trigger.TriggerTutorial();
        if (checklist_window_animator.GetBool("IsOpen") == true) checklist_items_window.gameObject.SetActive(false);
    }

    IEnumerator MonitorLoadLastGameData()
    {
        // transition to the table 
        stage_transition.SetTrigger("StartStageFade");
        yield return new WaitForSeconds(1);
        stage_transition.SetTrigger("EndStageFade");

        // save game data
        SaveSystem.SaveGameplay(player_controller_script.name, player_controller_script.player_final_score, player_controller_script.player_final_time, player_controller_script.selected_architecture.id);

        // load last game data
        LastGameController.LastGameController_Instance.LoadLastGameData(SaveSystem.version, player_controller_script.name, player_controller_script.player_final_score, player_controller_script.player_final_time, player_controller_script.selected_architecture.id);
    }

    // general window display message
    IEnumerator WarningWindowDisplay(string text, float delay)
    {
        warning_checklist_window.gameObject.SetActive(true);
        warning_checklist_window_text.text = text;
        warning_checklist_window_animator.SetBool("WarningChecklistIsOpen", true);
        yield return new WaitForSeconds(delay);
        warning_checklist_window_animator.SetBool("WarningChecklistIsOpen", false);
    }

    // window to display a message and move to the plan stage
    IEnumerator WarningToPlanDisplay(string text, float delay)
    {
        warning_checklist_window.gameObject.SetActive(true);
        warning_checklist_window_text.text = text;
        warning_checklist_window_animator.SetBool("WarningChecklistIsOpen", true);
        yield return new WaitForSeconds(delay);
        warning_checklist_window_animator.SetBool("WarningChecklistIsOpen", false);

        // transition to the plan stage
        stage_transition.SetTrigger("StartStageFade");
        yield return new WaitForSeconds(1f);
        stage_transition.SetTrigger("EndStageFade");
        next_stage_button.gameObject.SetActive(true);

        // go to the plan stage
        DeactivatedStages();
        plan_stage.SetActive(true);
        stage_title_text.text = "PLAN";
        devops_cycle_image.sprite = plan_devops_cycle;
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

    IEnumerator WarningRightBuildingToTestDisplay(string text, float delay)
    {
        warning_checklist_window.gameObject.SetActive(true);
        warning_checklist_window_text.text = text;
        warning_checklist_window_animator.SetBool("WarningChecklistIsOpen", true);
        yield return new WaitForSeconds(delay);
        warning_checklist_window_animator.SetBool("WarningChecklistIsOpen", false);

        // reset build categorize and landscape
        player_controller_script.build_categorize = true;

        // transition to the test stage
        stage_transition.SetTrigger("StartStageFade");
        yield return new WaitForSeconds(1f);
        stage_transition.SetTrigger("EndStageFade");

        // go to test stage
        stage_title_text.text = "TEST";
        DeactivatedStages();
        test_stage.SetActive(true);
        devops_cycle_image.sprite = test_devops_cycle;

        is_test_ability_used = false;
        left_test_card.gameObject.SetActive(false);
        right_test_card.gameObject.SetActive(false);

        tutorial_trigger = test_stage.GetComponent<TutorialTextTrigger>();
        tutorial_trigger.TriggerTutorial();
        if (checklist_window_animator.GetBool("IsOpen") == true) checklist_items_window.gameObject.SetActive(false);

        Card feather = build_carousel_script.cards_materials[2];
        Card anvil = build_carousel_script.cards_materials[0];
        Card elastic = build_carousel_script.cards_materials[4];
        Card burger = build_carousel_script.cards_materials[11];
        Card cable = build_carousel_script.cards_materials[5];
        Card ballon = build_carousel_script.cards_materials[6];
        Card handwork = build_carousel_script.cards_materials[8];
        List<Card> check_cards = build_carousel_script.cards;

        while(true){
            if(check_cards.Contains(feather)){
                // Fails
                test_result_image_wolf.sprite = test_result_wolf_failure;
                StartCoroutine(WarningWindowDisplay("Your trap has failed the test.", 4));
                is_test_failed = true;
                break;
            }
            if(check_cards.Contains(anvil) && check_cards.Contains(elastic)){
                // Fails
                test_result_image_wolf.sprite = test_result_wolf_failure;
                StartCoroutine(WarningWindowDisplay("Your trap has failed the test.", 4));
                is_test_failed = true;
                break;
            }
            if(check_cards.Contains(burger)){
                // Fails
                test_result_image_wolf.sprite = test_result_wolf_failure;
                StartCoroutine(WarningWindowDisplay("Your trap has failed the test.", 4));
                is_test_failed = true;
                break;
            }
            if(check_cards.Contains(cable) && check_cards.Contains(ballon)){
                // Fails
                test_result_image_wolf.sprite = test_result_wolf_failure;
                StartCoroutine(WarningWindowDisplay("Your trap has failed the test.", 4));
                is_test_failed = true;
                break;
            }
            if(check_cards.Contains(handwork)){
                // Fails
                test_result_image_wolf.sprite = test_result_wolf_failure;
                StartCoroutine(WarningWindowDisplay("Your trap has failed the test.", 4));
                is_test_failed = true;
                break;
            }
            if(check_cards.Contains(elastic)){
                // Fails
                test_result_image_wolf.sprite = test_result_wolf_failure;
                StartCoroutine(WarningWindowDisplay("Your trap has failed the test.", 4));
                is_test_failed = true;
                break;
            }
            else{
                // Test is good
                test_result_image_wolf.sprite = test_result_wolf_success;
                StartCoroutine(WarningWindowDisplay("Your trap has passed the tests made! Great Job!!", 4));
                break;
            }
        }
    }

    public IEnumerator WarningBuildingToPlanDisplay(string text, float delay)
    {
        warning_checklist_window.gameObject.SetActive(true);
        warning_checklist_window_text.text = text;
        warning_checklist_window_animator.SetBool("WarningChecklistIsOpen", true);
        yield return new WaitForSeconds(delay);
        warning_checklist_window_animator.SetBool("WarningChecklistIsOpen", false);

        // reset build categorize and landscape
        player_controller_script.build_categorize = true;

        // transition to the plan stage
        stage_transition.SetTrigger("StartStageFade");
        yield return new WaitForSeconds(1f);
        stage_transition.SetTrigger("EndStageFade");

        // go to the plan stage
        DeactivatedStages();
        plan_stage.SetActive(true);
        stage_title_text.text = "PLAN";
        devops_cycle_image.sprite = plan_devops_cycle;
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
                
                if(name == "bitbucket") message = "Hint: You should place this tool in the code stage."; // code
                else if(name == "docker") message = "Hint: You should place this tool in the deploy, release or build stage"; // release, deploy, build
                else if(name == "puppet") message = "Hint: You should place this tool in the operate or build stage."; // operate, build
                else if(name == "github") message = "Hint: You should place this tool in the code stage."; // code
                else if(name == "junit") message = "Hint: You should place this tool in the test stage."; // test
                else if(name == "gradle") message = "Hint: You should place this tool in the build stage."; // build
                else if(name == "chef") message = "Hint: You should place this tool in the operate, release or build stage."; // operate, release, build
                else if(name == "new_relic") message = "Hint: You should place this tool in the monitor stage."; // monitor
                else if(name == "vagrant") message = "Hint: You should place this tool in the test stage."; // test
                else if(name == "jira") message = "Hint: You should place this tool in the plan or release stage."; // plan, release
                else if(name == "powershell") message = "Hint: You should place this tool in the operate stage."; // operate
                else if(name == "selenium") message = "Hint: You should place this tool in the test stage."; // test
                else if(name == "datadog") message = "Hint: You should place this tool in the monitor stage."; // monitor
                else if(name == "aws") message = "Hint: You should place this tool in the deploy stage."; // deploy
                else if(name == "jenkins") message = "Hint: You should place this tool in the release stage.";  // release
                else if(name == "git") message = "Hint: You should place this tool in the plan stage."; // plan
                else if(name == "grafana") message = "Hint: You should place this tool in the monitor stage."; // monitor
                else if(name == "ansible") message = "Hint: You should place this tool in the operate, release or build stage."; // operate, release, build

                // POP-UP (message)
                StartCoroutine(WarningWindowDisplay(message, 4));
            }
            if(level == 2){
                // POP-UP (GUATA is not the most appropiate architecture)
                StartCoroutine(WarningWindowDisplay("Hint: The Water oriented architecture is not the most appropriate architecture.", 4));
            }
            else{
                // POP-UP (You can't do anything with this ability level, level it up)
                StartCoroutine(WarningWindowDisplay("Hint: You can't do anything with this ability level, level it up.", 4));
            }
        }
        else if(stage_title_text.text == "CODE"){
            int level = player_controller_script.abilities_levels["code_level"];
            if(level == 1){
                // POP-UP (Feather is not an appropiate component)
                StartCoroutine(WarningWindowDisplay("Hint: Feather is not an appropiate component for this project.", 4));
            }
            else if(level == 2){
                // POP-UP (Feather and burger are not appropiate compoments)
                StartCoroutine(WarningWindowDisplay("Hint: Feather and burger are not appropiate components for this project.", 4));
            }
            else if(level == 3){
                // POP-UP (Feather, burger and elastic are not appropiate components)
                StartCoroutine(WarningWindowDisplay("Hint: Feather, burger and elastic are not appropiate components for this project.", 4));
            }
            else{
                // POP-UP (You can't do anything with this ability level, level it up)
                StartCoroutine(WarningWindowDisplay("Hint: You can't do anything with this ability level, level it up.", 4));
            }
        }
        else if(stage_title_text.text == "BUILD"){
            int level = player_controller_script.abilities_levels["build_level"];
            if(level <= 1){
                // POP-UP (You can't do anything with this ability level, level it up)
                StartCoroutine(WarningWindowDisplay("You can't do anything with this ability level, level it up.", 4));
            }
            else if(level == 2){
                // Position of the element in the center (once)
                if(is_build_ability_used < 1){
                    is_build_ability_used += 1;
                    Card card = build_carousel_script.cards[build_carousel_script.center_index];
                    if(card.category == "hold") {
                        // POP-UP ()
                        StartCoroutine(WarningWindowDisplay("Hint: Place the "+card.card_title+" card in the second black box from top to bottom.", 5));
                    }
                    else if(card.category == "mechanism"){
                        // POP-UP ()
                        StartCoroutine(WarningWindowDisplay("Hint: Place the " + card.card_title + " card in the first black box from top to bottom.", 5));
                    }
                    else if(card.category == "impact"){
                        // POP-UP ()
                        StartCoroutine(WarningWindowDisplay("Hint: Place the " + card.card_title + " card in the third black box from top to bottom.", 5));
                    }
                    else if(card.category == "bait"){
                        // POP-UP ()
                        StartCoroutine(WarningWindowDisplay("Hint: Place the " + card.card_title + " card in the fourth black box from top to bottom.", 5));
                    }
                }
                else{
                    // POP-UP (You have already used this ability)
                    StartCoroutine(WarningWindowDisplay("You have already used this ability.", 3));
                }
            }
            else if(level == 3){
                if(is_build_ability_used < 2){
                    is_build_ability_used += 1;
                    Card card_1 = build_carousel_script.cards[build_carousel_script.center_index];
                    if(card_1.category == "hold") {
                        // POP-UP ()
                        StartCoroutine(WarningWindowDisplay("Hint: Place the " + card_1.card_title + " card in the second black box from top to bottom.", 5));
                    }
                    else if(card_1.category == "mechanism"){
                        // POP-UP ()
                        StartCoroutine(WarningWindowDisplay("Hint: Place the " + card_1.card_title + " card in the first black box from top to bottom.", 5));
                    }
                    else if(card_1.category == "impact"){
                        // POP-UP ()
                        StartCoroutine(WarningWindowDisplay("Hint: Place the " + card_1.card_title + " card in the third black box from top to bottom.", 5));
                    }
                    else if(card_1.category == "bait"){
                        // POP-UP ()
                        StartCoroutine(WarningWindowDisplay("Hint: Place the " + card_1.card_title + " card in the fourth black box from top to bottom.", 5));
                    }
                }
                else{
                    // POP-UP (You have already used this ability)
                    StartCoroutine(WarningWindowDisplay("You have already used this ability.", 3));
                }
            }
        }
        else if(stage_title_text.text == "TEST"){
            Card feather = build_carousel_script.cards_materials[2];
            Card anvil = build_carousel_script.cards_materials[0];
            Card elastic = build_carousel_script.cards_materials[4];
            Card burger = build_carousel_script.cards_materials[11];
            Card cable = build_carousel_script.cards_materials[5];
            Card ballon = build_carousel_script.cards_materials[6];
            Card handwork = build_carousel_script.cards_materials[8];
            int level = player_controller_script.abilities_levels["test_level"];
            List<Card> check_cards = build_carousel_script.cards;

            if(level <= 2){
                // POP-UP (You can't do anything with this ability level, level it up)
                StartCoroutine(WarningWindowDisplay("You can't do anything with this ability level, level it up.", 4));
            }
            else if(level == 3){
                if(is_test_ability_used == false){
                    if(check_cards.Contains(feather)){
                        // POP-UP (The feather does not generate the desired impact, it only "tickles")
                        left_test_card.gameObject.SetActive(true);
                        left_test_card_image.sprite = feather.card_image;
                        left_test_card_title.text = feather.card_title;
                        left_test_card_description.text = "The feather does not generate the desired impact, it only 'tickles'";

                        is_test_ability_used = true;
                    }
                    else if(check_cards.Contains(anvil) && check_cards.Contains(elastic)){
                        // POP-UP (Do you remember Newton's third law? No? It was that the anvil fell too fast and the elastic 
                        //force of the spring returned it to you, let us know when the bruise is removed.)
                        left_test_card.gameObject.SetActive(true);
                        left_test_card_image.sprite = anvil.card_image;
                        left_test_card_title.text = anvil.card_title;
                        left_test_card_description.text = "Do you remember Newton's third law? No? It was that the anvil fell too fast and the elastic force of the spring returned it to you, let us know when the bruise is removed.";

                        right_test_card.gameObject.SetActive(true);
                        right_test_card_image.sprite = elastic.card_image;
                        right_test_card_title.text = elastic.card_title;
                        right_test_card_description.text = "Do you remember Newton's third law? No? It was that the anvil fell too fast and the elastic force of the spring returned it to you, let us know when the bruise is removed.";

                        is_test_ability_used = true;
                    }
                    else if(check_cards.Contains(burger)){
                        // POP-UP (It seems that our victim has a traumatic memory of his grandfather rabbitburger.)
                        left_test_card.gameObject.SetActive(true);
                        left_test_card_image.sprite = burger.card_image;
                        left_test_card_title.text = burger.card_title;
                        left_test_card_description.text = "It seems that our victim has a traumatic memory of his grandfather rabbitburger";

                        is_test_ability_used = true;
                    }
                    else if(check_cards.Contains(cable) && check_cards.Contains(ballon)){
                        // POP-UP (Our balloon could not hold the weight of the braided cable, do you think it is a fiction movie?)
                        left_test_card.gameObject.SetActive(true);
                        left_test_card_image.sprite = cable.card_image;
                        left_test_card_title.text = cable.card_title;
                        left_test_card_description.text = "Our balloon could not hold the weight of the braided cable, do you think it is a fiction movie?";

                        right_test_card.gameObject.SetActive(true);
                        right_test_card_image.sprite = ballon.card_image;
                        right_test_card_title.text = ballon.card_title;
                        right_test_card_description.text = "Our balloon could not hold the weight of the braided cable, do you think it is a fiction movie?";

                        is_test_ability_used = true;
                    }
                    else if(check_cards.Contains(handwork)){
                        // POP-UP (Have you ever wondered which is your favorite finger? I hope you did not lose it in the machucon.)
                        left_test_card.gameObject.SetActive(true);
                        left_test_card_image.sprite = handwork.card_image;
                        left_test_card_title.text = handwork.card_title;
                        left_test_card_description.text = "The feather does not generate the desired impact, it only 'tickles'";

                        is_test_ability_used = true;
                    }
                    else if(check_cards.Contains(elastic)){
                        // POP-UP (Have you seen how elastics stretch and then you can't really use them? That's exactly what 
                        // happened and it's not really helpful when holding a heavy weight.)
                        left_test_card.gameObject.SetActive(true);
                        left_test_card_image.sprite = elastic.card_image;
                        left_test_card_title.text = elastic.card_title;
                        left_test_card_description.text = "Have you seen how elastics stretch and then you can't really use them? That's exactly what happened and it's not really helpful when holding a heavy weight.'";

                        is_test_ability_used = true;
                    }
                    else{
                        StartCoroutine(WarningWindowDisplay("The ability is not going to give you any advantage because everything is ok.", 3));
                    }
                }
                else{
                    StartCoroutine(WarningWindowDisplay("You have already used this ability.", 3));
                }
            }
        }
        else if(stage_title_text.text == "RELEASE"){
            int level = player_controller_script.abilities_levels["release_level"];
                
            if(level <= 2){
                StartCoroutine(WarningWindowDisplay("You can't do anything with this ability level, level it up.", 4));
            }
            else if(level == 3){
                if(is_release_ability_used < 1){
                    is_release_ability_used += 1;
                    int random_index = (int)Random.Range(0, 7);
                    string random_ability = "";
                    string name_ability = "";
                    if(random_index == 0){
                        random_ability = "plan_level";
                        name_ability = "plan";
                    }
                    else if(random_index == 1){
                        random_ability = "code_level";
                        name_ability = "code";
                    }
                    else if(random_index == 2){
                        random_ability = "build_level";
                        name_ability = "build";
                    }
                    else if(random_index == 3){
                        random_ability = "test_level";
                        name_ability = "test";
                    }
                    else if(random_index == 4){
                        random_ability = "release_level";
                        name_ability = "release";
                    }
                    else if(random_index == 5){
                        random_ability = "deploy_level";
                        name_ability = "deploy";
                    }
                    else if(random_index == 6){
                        random_ability = "operate_level";
                        name_ability = "operate";
                    }
                    else if(random_index == 7){
                        random_ability = "monitor_level";
                        name_ability = "monitor";
                    }
                    while(true){
                        // Check if all abilities are at max level
                        int plan_level = player_controller_script.abilities_levels["plan_level"];
                        int code_level = player_controller_script.abilities_levels["code_level"];
                        int build_level = player_controller_script.abilities_levels["build_level"];
                        int test_level = player_controller_script.abilities_levels["test_level"];
                        int release_level = player_controller_script.abilities_levels["release_level"];
                        int deploy_level = player_controller_script.abilities_levels["deploy_level"];
                        int operate_level = player_controller_script.abilities_levels["operate_level"];
                        int monitor_level = player_controller_script.abilities_levels["monitor_level"];

                        if(plan_level == 3 && code_level == 3 && build_level == 3 && test_level == 3 && 
                        release_level == 3 && deploy_level == 3 && operate_level == 3 && monitor_level == 3){
                            break;
                        }

                        if(player_controller_script.abilities_levels[random_ability] == 3){
                            random_index = (int)Random.Range(0, 7);
                            if(random_index == 0){
                                random_ability = "plan_level";
                                name_ability = "plan";
                            }
                            else if(random_index == 1){
                                random_ability = "code_level";
                                name_ability = "code";
                            }
                            else if(random_index == 2){
                                random_ability = "build_level";
                                name_ability = "build";
                            }
                            else if(random_index == 3){
                                random_ability = "test_level";
                                name_ability = "test";
                            }
                            else if(random_index == 4){
                                random_ability = "release_level";
                                name_ability = "release";
                            }
                            else if(random_index == 5){
                                random_ability = "deploy_level";
                                name_ability = "deploy";
                            }
                            else if(random_index == 6){
                                random_ability = "operate_level";
                                name_ability = "operate";
                            }
                            else if(random_index == 7){
                                random_ability = "monitor_level";
                                name_ability = "monitor";
                            }
                        }
                        else{
                            player_controller_script.abilities_levels[random_ability] += 1;
                            StartCoroutine(WarningWindowDisplay("You have gained one level in the " + name_ability + " ability.", 4));

                            break;
                        }
                    }
                }
                else{
                    StartCoroutine(WarningWindowDisplay("You have already used this ability.", 3));
                }
            }
        }
        else if(stage_title_text.text == "DEPLOY"){
            int level = player_controller_script.abilities_levels["deploy_level"];
                
            if(level <= 2){
                StartCoroutine(WarningWindowDisplay("You can't do anything with this ability level, level it up.", 4));
            }
            else if(level == 3){
                if(is_deploy_ability_used < 1){
                    is_deploy_ability_used += 1;
                    int random_index = (int)Random.Range(0, 7);
                    string random_ability = "";
                    string name_ability = "";
                    if(random_index == 0){
                        random_ability = "plan_level";
                        name_ability = "plan";
                    }
                    else if(random_index == 1){
                        random_ability = "code_level";
                        name_ability = "code";
                    }
                    else if(random_index == 2){
                        random_ability = "build_level";
                        name_ability = "build";
                    }
                    else if(random_index == 3){
                        random_ability = "test_level";
                        name_ability = "test";
                    }
                    else if(random_index == 4){
                        random_ability = "release_level";
                        name_ability = "release";
                    }
                    else if(random_index == 5){
                        random_ability = "deploy_level";
                        name_ability = "deploy";
                    }
                    else if(random_index == 6){
                        random_ability = "operate_level";
                        name_ability = "operate";
                    }
                    else if(random_index == 7){
                        random_ability = "monitor_level";
                        name_ability = "monitor";
                    }
                    while(true){
                        // Check if all abilities are at max level
                        int plan_level = player_controller_script.abilities_levels["plan_level"];
                        int code_level = player_controller_script.abilities_levels["code_level"];
                        int build_level = player_controller_script.abilities_levels["build_level"];
                        int test_level = player_controller_script.abilities_levels["test_level"];
                        int release_level = player_controller_script.abilities_levels["release_level"];
                        int deploy_level = player_controller_script.abilities_levels["deploy_level"];
                        int operate_level = player_controller_script.abilities_levels["operate_level"];
                        int monitor_level = player_controller_script.abilities_levels["monitor_level"];

                        if(plan_level == 3 && code_level == 3 && build_level == 3 && test_level == 3 && 
                        release_level == 3 && deploy_level == 3 && operate_level == 3 && monitor_level == 3){
                            break;
                        }

                        if(player_controller_script.abilities_levels[random_ability] == 3){
                            random_index = (int)Random.Range(0, 7);
                            if(random_index == 0){
                                random_ability = "plan_level";
                                name_ability = "plan";
                            }
                            else if(random_index == 1){
                                random_ability = "code_level";
                                name_ability = "code";
                            }
                            else if(random_index == 2){
                                random_ability = "build_level";
                                name_ability = "build";
                            }
                            else if(random_index == 3){
                                random_ability = "test_level";
                                name_ability = "test";
                            }
                            else if(random_index == 4){
                                random_ability = "release_level";
                                name_ability = "release";
                            }
                            else if(random_index == 5){
                                random_ability = "deploy_level";
                                name_ability = "deploy";
                            }
                            else if(random_index == 6){
                                random_ability = "operate_level";
                                name_ability = "operate";
                            }
                            else if(random_index == 7){
                                random_ability = "monitor_level";
                                name_ability = "monitor";
                            }
                        }
                        else{
                            player_controller_script.abilities_levels[random_ability] += 1;
                            StartCoroutine(WarningWindowDisplay("You have gained one level in the " + name_ability + " ability.", 4));

                            break;
                        }
                    }
                }
                else{
                    StartCoroutine(WarningWindowDisplay("You have already used this ability.", 3));
                }
            }
        }
        else if(stage_title_text.text == "OPERATE"){
            int level = player_controller_script.abilities_levels["operate_level"];
            if(level <= 1){
                StartCoroutine(WarningWindowDisplay("You can't do anything with this ability level, level it up.", 4));
            }
            else{
                StartCoroutine(WarningWindowDisplay("This is a passive ability, the effect is already active", 4));
            }
        }
        else if(stage_title_text.text == "MONITOR"){
            int level = player_controller_script.abilities_levels["monitor_level"];
            Card blacksmith = code_carousel_script.deck[0];
            Card piano_fight = code_carousel_script.deck[11];
            Card piano_old = code_carousel_script.deck[6];
            Card cowboy = code_carousel_script.deck[1];
            Card pants = code_carousel_script.deck[5];
            Card charger = code_carousel_script.deck[17];
            Card turkey = code_carousel_script.deck[23];
            Card well = code_carousel_script.deck[4];
            Card compost = code_carousel_script.deck[18];
            Card jam = code_carousel_script.deck[3];

            if(level <= 1){
                StartCoroutine(WarningWindowDisplay("You can't do anything with this ability level, level it up.", 4));
            }
            else if(level == 2){
                if(is_monitor_ability_used < 1){
                    string message = "";
                    if(blacksmith.selected == true){
                        message = "Getting the anvil from the blacksmith is not the best choice.";
                        is_monitor_ability_used += 1;
                    }
                    else if(piano_fight.selected == true || piano_old.selected == true){
                        message = "A piano is no the best choice in terms of durability.";
                    }
                    else if(cowboy.selected == true){
                        message = "Getting the rope from the cowboy is not the best choice.";
                        is_monitor_ability_used += 1;
                    }
                    else if(pants.selected == true){
                        message = "Getting the elastic from the pants is not the best choice.";
                        is_monitor_ability_used += 1;
                    }
                    else if(charger.selected == true){
                        message = "Getting the cable from the charger is not the best choice.";
                        is_monitor_ability_used += 1;
                    }
                    else if(turkey.selected == true){
                        message = "Getting the balloon from the balloons from Turkey is not the best choice.";
                        is_monitor_ability_used += 1;
                    }
                    else if(well.selected == true){
                        message = "Getting the pulley from the well is not the best choice.";
                        is_monitor_ability_used += 1;
                    }
                    else if(compost.selected == true){
                        message = "Getting the carrot from the compost is not the best choice.";
                        is_monitor_ability_used += 1;
                    }
                    else if(jam.selected == true){
                        message = "Using the jam as blueberries is not the best choice.";
                        is_monitor_ability_used += 1;
                    }
                    else{
                        message = "Everything went smoothly.";
                    }
                    StartCoroutine(WarningWindowDisplay(message, 4));
                }
                else{
                    StartCoroutine(WarningWindowDisplay("You have already used this ability.", 3));
                }
            }
            else if(level == 3){
                if(is_monitor_ability_used < 2){
                    string message = "";
                    while(true){
                        if(blacksmith.selected == true){
                            if(message != "Getting the anvil from the blacksmith is not the best choice. \n"){
                                message += "Getting the anvil from the blacksmith is not the best choice. \n";
                            }
                            is_monitor_ability_used += 1;
                            if(is_monitor_ability_used == 2) break;
                        }
                        if(piano_fight.selected == true || piano_old.selected == true){
                            if(message != "A piano is no the best choice in terms of durability. \n"){
                                message += "A piano is no the best choice in terms of durability. \n";
                            }
                            is_monitor_ability_used += 1;
                            if(is_monitor_ability_used == 2) break;
                        }
                        if(cowboy.selected == true){
                            if(message != "Getting the rope from the cowboy is not the best choice. \n"){
                                message += "Getting the rope from the cowboy is not the best choice. \n";
                            }
                            is_monitor_ability_used += 1;
                            if(is_monitor_ability_used == 2) break;
                        }
                        if(pants.selected == true){
                            if(message != "Getting the elastic from the pants is not the best choice. \n"){
                                message += "Getting the elastic from the pants is not the best choice. \n";
                            }
                            is_monitor_ability_used += 1;
                            if(is_monitor_ability_used == 2) break;
                        }
                        if(charger.selected == true){
                            if(message != "Getting the cable from the charger is not the best choice. \n"){
                                message += "Getting the cable from the charger is not the best choice. \n";
                            }
                            is_monitor_ability_used += 1;
                            if(is_monitor_ability_used == 2) break;
                        }
                        if(turkey.selected == true){
                            if(message != "Getting the balloon from the balloons from Turkey is not the best choice. \n"){
                                message += "Getting the balloon from the balloons from Turkey is not the best choice. \n";
                            }
                            is_monitor_ability_used += 1;
                            if(is_monitor_ability_used == 2) break;
                        }
                        if(well.selected == true){
                            if(message != "Getting the pulley from the well is not the best choice. \n"){
                                message += "Getting the pulley from the well is not the best choice. \n";
                            }
                            is_monitor_ability_used += 1;
                            if(is_monitor_ability_used == 2) break;
                        }
                        if(compost.selected == true){
                            if(message != "Getting the carrot from the compost is not the best choice. \n"){
                                message += "Getting the carrot from the compost is not the best choice. \n";
                            }
                            is_monitor_ability_used += 1;
                            if(is_monitor_ability_used == 2) break;
                        }
                        if(jam.selected == true){
                            if(message != "Using the jam as blueberries is not the best choice. \n"){
                                message += "Using the jam as blueberries is not the best choice. \n";
                            }
                            is_monitor_ability_used += 1;
                            if(is_monitor_ability_used == 2) break;
                        }
                        if(is_monitor_ability_used == 0){
                            message = "Everything went smoothly.";
                            break;
                        }
                    }
                    StartCoroutine(WarningWindowDisplay(message, 6));
                }
                else{
                    StartCoroutine(WarningWindowDisplay("You have already used this ability.", 3));
                }
            }
        }
    }

    void BlueprintGroundCloseButton()
    {
        blueprint_ground_architecture.gameObject.SetActive(false);
        close_button_blueprint_ground_architecture.gameObject.SetActive(false);
        text_blueprint_ground_architecture.gameObject.SetActive(false);
    }

    void BlueprintAirCloseButton()
    {
        blueprint_air_architecture.gameObject.SetActive(false);
        close_button_blueprint_air_architecture.gameObject.SetActive(false);
        text_blueprint_air_architecture.gameObject.SetActive(false);
    }

    void UseToolButtonHanlder(){
        if (stage_title_text.text == "PLAN") {
            if(player_controller_script.can_use_plan_tool == true && is_plan_tool_used == false)
            {
                if(player_controller_script.selected_architecture.id == "")
                {
                    StartCoroutine(WarningWindowDisplay("You must select an architecture to be able to use this tool.", 3));
                }
                else
                {
                    is_plan_tool_used = true;
                    if (player_controller_script.selected_architecture.id == "architecture_1")
                    {
                        blueprint_ground_architecture.gameObject.SetActive(true);
                        text_blueprint_ground_architecture.gameObject.SetActive(true);
                        close_button_blueprint_ground_architecture.gameObject.SetActive(true);
                    }
                    else if (player_controller_script.selected_architecture.id == "architecture_2")
                    {
                        blueprint_air_architecture.gameObject.SetActive(true);
                        close_button_blueprint_air_architecture.gameObject.SetActive(true);
                        text_blueprint_air_architecture.gameObject.SetActive(true);
                    }
                }
            }
            else{
                // POP-UP (You can't use this tool)
                StartCoroutine(WarningWindowDisplay("You can't use this tool.", 3));
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
                StartCoroutine(WarningWindowDisplay("You can't use this tool.", 3));
            }
        }
        else if(stage_title_text.text == "BUILD"){
            // Category of the element in the center (once)
            if(player_controller_script.can_use_build_tool == true){
                if(is_build_tool_used == false){
                    is_build_tool_used = true;
                    Card card = build_carousel_script.cards[build_carousel_script.center_index];

                    // POP-UP (card.category)
                    StartCoroutine(WarningWindowDisplay("Hint: You should place the "+ card.card_title + ", in the " + card.category + " category.", 5));
                }
                else{
                    // POP-UP (You have already used this tool)
                    StartCoroutine(WarningWindowDisplay("You have already used this tool.", 3));
                }
            }
            else{
                StartCoroutine(WarningWindowDisplay("You can't use this tool.", 3));
            }
        }
        else if(stage_title_text.text == "TEST"){
            if(player_controller_script.can_use_test_tool == true){
                if(is_test_tool_used == false){
                    is_test_tool_used = true;
                    int count_wrong = 0;
                    foreach (KeyValuePair<string, Card> tool in player_controller_script.tool_cards) {
                        if(tool.Value != null){
                            string name = tool.Value.id;
                            string phase = tool.Key;
                            
                            Debug.Log(name + " " + phase);

                            if(name == "bitbucket"){
                                if(phase != "code") count_wrong += 1; // code
                            }
                            else if(name == "docker") {
                                if(phase != "release" && phase != "deploy" && phase != "build") count_wrong += 1; // release, deploy, build
                            }
                            else if(name == "puppet") {
                                if(phase != "operate" && phase != "build") count_wrong += 1; // operate, build
                            }
                            else if(name == "github") {
                                if(phase != "code") count_wrong += 1; // code
                            }
                            else if(name == "junit") {
                                if(phase != "test") count_wrong += 1; // test
                            }
                            else if(name == "gradle") {
                                if(phase != "build") count_wrong += 1; // build
                            }
                            else if(name == "chef") { 
                                if(phase != "operate" && phase != "release" && phase != "build") count_wrong += 1; // operate, release, build
                            }
                            else if(name == "new_relic") {
                                 if(phase != "monitor") count_wrong += 1; // monitor
                            }
                            else if(name == "vagrant") {
                                if(phase != "test") count_wrong += 1; // test
                            } 
                            else if(name == "jira") {
                                if(phase != "plan" && phase != "release") count_wrong += 1; // plan, release
                            }
                            else if(name == "powershell") {
                                if(phase != "operate") count_wrong += 1; // operat
                            }
                            else if(name == "selenium") {
                                if(phase != "test") count_wrong += 1; // test
                            }
                            else if(name == "datadog") {
                                if(phase != "monitor") count_wrong += 1; // monitor
                            }
                            else if(name == "aws") {
                                if(phase != "deploy") count_wrong += 1; // deploy
                            }
                            else if(name == "jenkins") {
                                if(phase != "release") count_wrong += 1;  // release
                            }
                            else if(name == "git") {
                                if(phase != "plan") count_wrong += 1; // plan
                            }
                            else if(name == "grafana") {
                                if(phase != "monitor") count_wrong += 1; // monitor
                            }
                            else if(name == "ansible") {
                                if(phase != "operate" && phase != "release" && phase != "build") count_wrong += 1; // operate, release, build
                            }

                            StartCoroutine(WarningWindowDisplay("There are " + count_wrong.ToString() + " tool(s) in the DevOps cycle that are not placed correctly.", 5));
                        }
                    }
                }
                else{
                    StartCoroutine(WarningWindowDisplay("You have already used this tool.", 3));
                }
            }
            else{
                StartCoroutine(WarningWindowDisplay("You can't use this tool.", 3));
            }
        }
        else if(stage_title_text.text == "RELEASE"){
            if(player_controller_script.can_use_release_tool == true){
                if(is_release_tool_used < 1){
                    is_release_tool_used += 1;
                    int random_index = (int)Random.Range(0, 7);
                    string random_ability = "";
                    string name_ability = "";
                    if(random_index == 0){
                        random_ability = "plan_level";
                        name_ability = "plan";
                    }
                    else if(random_index == 1){
                        random_ability = "code_level";
                        name_ability = "code";
                    }
                    else if(random_index == 2){
                        random_ability = "build_level";
                        name_ability = "build";
                    }
                    else if(random_index == 3){
                        random_ability = "test_level";
                        name_ability = "test";
                    }
                    else if(random_index == 4){
                        random_ability = "release_level";
                        name_ability = "release";
                    }
                    else if(random_index == 5){
                        random_ability = "deploy_level";
                        name_ability = "deploy";
                    }
                    else if(random_index == 6){
                        random_ability = "operate_level";
                        name_ability = "operate";
                    }
                    else if(random_index == 7){
                        random_ability = "monitor_level";
                        name_ability = "monitor";
                    }
                    while(true){
                        // Check if all abilities are at max level
                        int plan_level = player_controller_script.abilities_levels["plan_level"];
                        int code_level = player_controller_script.abilities_levels["code_level"];
                        int build_level = player_controller_script.abilities_levels["build_level"];
                        int test_level = player_controller_script.abilities_levels["test_level"];
                        int release_level = player_controller_script.abilities_levels["release_level"];
                        int deploy_level = player_controller_script.abilities_levels["deploy_level"];
                        int operate_level = player_controller_script.abilities_levels["operate_level"];
                        int monitor_level = player_controller_script.abilities_levels["monitor_level"];

                        if(plan_level == 3 && code_level == 3 && build_level == 3 && test_level == 3 && 
                        release_level == 3 && deploy_level == 3 && operate_level == 3 && monitor_level == 3){
                            break;
                        }

                        if(player_controller_script.abilities_levels[random_ability] == 3){
                            random_index = (int)Random.Range(0, 7);
                            if(random_index == 0){
                                random_ability = "plan_level";
                                name_ability = "plan";
                            }
                            else if(random_index == 1){
                                random_ability = "code_level";
                                name_ability = "code";
                            }
                            else if(random_index == 2){
                                random_ability = "build_level";
                                name_ability = "build";
                            }
                            else if(random_index == 3){
                                random_ability = "test_level";
                                name_ability = "test";
                            }
                            else if(random_index == 4){
                                random_ability = "release_level";
                                name_ability = "release";
                            }
                            else if(random_index == 5){
                                random_ability = "deploy_level";
                                name_ability = "deploy";
                            }
                            else if(random_index == 6){
                                random_ability = "operate_level";
                                name_ability = "operate";
                            }
                            else if(random_index == 7){
                                random_ability = "monitor_level";
                                name_ability = "monitor";
                            }
                        }
                        else{
                            player_controller_script.abilities_levels[random_ability] += 1;
                            StartCoroutine(WarningWindowDisplay("You have gained one level in the " + name_ability + " ability.", 4));

                            break;
                        }
                    }
                }
                else{
                    StartCoroutine(WarningWindowDisplay("You have already used this tool.", 3));
                }
            }
            else{
                StartCoroutine(WarningWindowDisplay("You can't use this tool.", 3));
            }
        }
        else if(stage_title_text.text == "DEPLOY"){
            if(player_controller_script.can_use_deploy_tool == true){
                if(is_deploy_tool_used < 1){
                    is_deploy_tool_used += 1;
                    int random_index = (int)Random.Range(0, 7);
                    string random_ability = "";
                    string name_ability = "";
                    if(random_index == 0){
                        random_ability = "plan_level";
                        name_ability = "plan";
                    }
                    else if(random_index == 1){
                        random_ability = "code_level";
                        name_ability = "code";
                    }
                    else if(random_index == 2){
                        random_ability = "build_level";
                        name_ability = "build";
                    }
                    else if(random_index == 3){
                        random_ability = "test_level";
                        name_ability = "test";
                    }
                    else if(random_index == 4){
                        random_ability = "release_level";
                        name_ability = "release";
                    }
                    else if(random_index == 5){
                        random_ability = "deploy_level";
                        name_ability = "deploy";
                    }
                    else if(random_index == 6){
                        random_ability = "operate_level";
                        name_ability = "operate";
                    }
                    else if(random_index == 7){
                        random_ability = "monitor_level";
                        name_ability = "monitor";
                    }
                    while(true){
                        // Check if all abilities are at max level
                        int plan_level = player_controller_script.abilities_levels["plan_level"];
                        int code_level = player_controller_script.abilities_levels["code_level"];
                        int build_level = player_controller_script.abilities_levels["build_level"];
                        int test_level = player_controller_script.abilities_levels["test_level"];
                        int release_level = player_controller_script.abilities_levels["release_level"];
                        int deploy_level = player_controller_script.abilities_levels["deploy_level"];
                        int operate_level = player_controller_script.abilities_levels["operate_level"];
                        int monitor_level = player_controller_script.abilities_levels["monitor_level"];

                        if(plan_level == 3 && code_level == 3 && build_level == 3 && test_level == 3 && 
                        release_level == 3 && deploy_level == 3 && operate_level == 3 && monitor_level == 3){
                            break;
                        }

                        if(player_controller_script.abilities_levels[random_ability] == 3){
                            random_index = (int)Random.Range(0, 7);
                            if(random_index == 0){
                                random_ability = "plan_level";
                                name_ability = "plan";
                            }
                            else if(random_index == 1){
                                random_ability = "code_level";
                                name_ability = "code";
                            }
                            else if(random_index == 2){
                                random_ability = "build_level";
                                name_ability = "build";
                            }
                            else if(random_index == 3){
                                random_ability = "test_level";
                                name_ability = "test";
                            }
                            else if(random_index == 4){
                                random_ability = "release_level";
                                name_ability = "release";
                            }
                            else if(random_index == 5){
                                random_ability = "deploy_level";
                                name_ability = "deploy";
                            }
                            else if(random_index == 6){
                                random_ability = "operate_level";
                                name_ability = "operate";
                            }
                            else if(random_index == 7){
                                random_ability = "monitor_level";
                                name_ability = "monitor";
                            }
                        }
                        else{
                            player_controller_script.abilities_levels[random_ability] += 1;
                            StartCoroutine(WarningWindowDisplay("You have gained one level in the " + name_ability + " ability.", 4));

                            break;
                        }
                    }
                }
                else{
                    StartCoroutine(WarningWindowDisplay("You have already used this tool.", 3));
                }
            }
            else{
                StartCoroutine(WarningWindowDisplay("You can't use this tool.", 3));
            }
        }
        else if(stage_title_text.text == "OPERATE"){
            if(player_controller_script.can_use_operate_tool == true){
                StartCoroutine(WarningWindowDisplay("This is a passive tool, the effect is already active", 4));
            }
            else{
                StartCoroutine(WarningWindowDisplay("You can't use this tool.", 3));
            }
        }
        else if(stage_title_text.text == "MONITOR"){
            if(player_controller_script.can_use_monitor_tool == true){
                int count = 0;

                Card blacksmith = code_carousel_script.deck[0];
                Card piano_fight = code_carousel_script.deck[11];
                Card piano_old = code_carousel_script.deck[6];
                Card cowboy = code_carousel_script.deck[1];
                Card pants = code_carousel_script.deck[5];
                Card charger = code_carousel_script.deck[17];
                Card turkey = code_carousel_script.deck[23];
                Card well = code_carousel_script.deck[4];
                Card compost = code_carousel_script.deck[18];
                Card jam = code_carousel_script.deck[3];

                if(blacksmith.selected == true) count += 1;
                if(piano_fight.selected == true || piano_old.selected == true) count += 1;
                if(cowboy.selected == true) count += 1;
                if(pants.selected == true) count += 1;
                if(charger.selected == true) count += 1;
                if(turkey.selected == true) count += 1;
                if(well.selected == true) count += 1;
                if(compost.selected == true) count += 1;
                if(jam.selected == true) count += 1;

                StartCoroutine(WarningWindowDisplay("There are " + count.ToString() + " elements that were not collected in the best way.", 4));
            }
            else{
                StartCoroutine(WarningWindowDisplay("You can't use this tool.", 3));
            }
        }
    }

    void AirTrapDeployAnimation()
    {
        airtrap_deploy_animation_animator.SetBool("IsOnStartPositionAir", true);
        balloon_image.sprite = sprite_balloon_suelto;

    }

    void GroundTrapDeployAnimation()
    {
        groundtrap_deploy_animation_animator.SetBool("IsOnStartPosition", true);
        pulley_image.sprite = sprite_pulley_suelta;

    }

    public void ExitGame()
    {
        Application.Quit();
    }

    // to display the how to play image for each stage
    void HowToPlayDisplay()
    {
        how_to_play_image.gameObject.SetActive(true);
        how_to_play_close_button.gameObject.SetActive(true);
        how_to_play_background_image.gameObject.SetActive(true);
        if (stage_title_text.text == "PLAN")
        {
            if (plan_deck_controller_script.deck_button_text.text == "Project") how_to_play_image.sprite = plan_project_howto_sprite;
            else if (plan_deck_controller_script.deck_button_text.text == "Architecture") how_to_play_image.sprite = plan_architecture_howto_sprite;
            else if (plan_deck_controller_script.deck_button_text.text == "Abilities") how_to_play_image.sprite = plan_abilities_howto_sprite;
            else if (plan_deck_controller_script.deck_button_text.text == "Tools")
            {
                next_button_tools_howto.gameObject.SetActive(true);
                how_to_play_image.sprite = plan_tools_howto_sprite;
            }
        }
        else if(stage_title_text.text == "CODE") how_to_play_image.sprite = code_howto_sprite;
        else if (stage_title_text.text == "BUILD")
        {
            if(build_deck_controller_script.deck_button_text.text == "Categorize") how_to_play_image.sprite = build_categorize_howto_sprite;
            else how_to_play_image.sprite = build_building_howto_sprite;
        }
        else if (stage_title_text.text == "TEST") how_to_play_image.sprite = test_howto_sprite;
        else if (stage_title_text.text == "RELEASE") how_to_play_image.sprite = release_howto_sprite;
        else if (stage_title_text.text == "DEPLOY") how_to_play_image.sprite = deploy_howto_sprite;
        else if (stage_title_text.text == "OPERATE") how_to_play_image.sprite = operate_howto_sprite;
        else if (stage_title_text.text == "MONITOR") how_to_play_image.sprite = monitor_howto_sprite;
    }

    void HowToPlayClose()
    {
        how_to_play_image.gameObject.SetActive(false);
        how_to_play_close_button.gameObject.SetActive(false);
        how_to_play_background_image.gameObject.SetActive(false);

        next_button_tools_howto.gameObject.SetActive(false);
    }

    void HowToPlayToolsNext()
    {
        how_to_play_image.sprite = plan_tools_howto_sprite2;
        next_button_tools_howto.gameObject.SetActive(false);
    }
}
