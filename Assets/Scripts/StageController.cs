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
    private string checklist_text;
    private Image checklist_items_window;
    private Text checklist_items_text;
    public Animator checklist_window_animator;
    public Animator warning_checklist_window_animator;
    private Image warning_checklist_window;

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
    private PlanDeckController plan_deck_controller_script;

    // Start is called before the first frame update
    void Start()
    {
        checklist_button = GameObject.Find("ChecklistButton").GetComponent<Button>();
        checklist_close_button = GameObject.Find("ChecklistCloseButton").GetComponent<Button>();
        checklist_items_window = GameObject.Find("ChecklistItemsWindow").GetComponent<Image>();
        checklist_items_text = GameObject.Find("ChecklistItemsText").GetComponent<Text>();
        warning_checklist_window = GameObject.Find("WarningChecklistWindow").GetComponent<Image>();
        plan_deck_controller_script = GameObject.Find("Plan").GetComponent<PlanDeckController>();
        player_controller_script = GameObject.Find("Views").GetComponent<PlayerController>();

        warning_checklist_window.gameObject.SetActive(false);
        checklist_close_button.gameObject.SetActive(false);
        checklist_close_button.onClick.AddListener(ChecklistCloseButton);
        checklist_button.onClick.AddListener(ChecklistButton);
        next_stage_button.onClick.AddListener(NextStageButton);

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

    void DeactivatedStages()
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
            if((architecture.id == "architecture_1" || architecture.id == "architecture_2" || architecture.id == "architecture_3") && tool == true){
                stage_title_text.text = "CODE";
                DeactivatedStages();
                code_stage.SetActive(true);
                tutorial_trigger = code_stage.GetComponent<TutorialTextTrigger>();
                tutorial_trigger.TriggerTutorial();
                if (checklist_window_animator.GetBool("IsOpen") == true) checklist_items_window.gameObject.SetActive(false);
            }
            else{
                // Pop-up: mensaje "Looks like something is missing, check your checklist"
                StartCoroutine(WarningWindowDisplay(2));
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
            build_deck_controller_script.word_index = 0; // reset to go back to the categorize view and be able to go to the building
            build_deck_controller_script.deck_button_text.text = "Categorize";
            build_deck_controller_script.carousel.SetActive(true);
            build_deck_controller_script.up_arrow_button.gameObject.SetActive(true);
            build_deck_controller_script.down_arrow_button.gameObject.SetActive(true);
            build_deck_controller_script.landscape.SetActive(false);

            // Update selected cards in BuildCarouselController (from PllayerController)
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
            if((impact + hold + bait + mechanism) != 4){ // Build fails -> Returns to Plan

                /////// POP-UP

                DeactivatedStages();
                plan_stage.SetActive(true);
                stage_title_text.text = "PLAN";

                // SETEAR VARIABLES Y DEMAS (Tool y Abilities)
                plan_deck_controller_script.leveled_up_card = false;
                plan_deck_controller_script.is_selected_random_card = false;
                plan_deck_controller_script.is_generated_random_card = false;



            }
            else{   // Build is correct

                /////// POP-UP

                stage_title_text.text = "TEST";
                DeactivatedStages();
                test_stage.SetActive(true);
                tutorial_trigger = test_stage.GetComponent<TutorialTextTrigger>();
                tutorial_trigger.TriggerTutorial();
                if (checklist_window_animator.GetBool("IsOpen") == true) checklist_items_window.gameObject.SetActive(false);
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

    IEnumerator WarningWindowDisplay(float delay)
    {
        warning_checklist_window.gameObject.SetActive(true);
        warning_checklist_window_animator.SetBool("WarningChecklistIsOpen", true);
        yield return new WaitForSeconds(delay);
        warning_checklist_window_animator.SetBool("WarningChecklistIsOpen", false);
    }
}
