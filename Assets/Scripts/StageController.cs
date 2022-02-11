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

    public GameObject plan_stage;
    public GameObject code_stage;
    public GameObject build_stage;
    public GameObject test_stage;
    public GameObject release_stage;
    public GameObject deploy_stage;
    public GameObject operate_stage;
    public GameObject monitor_stage;

    private TutorialTextTrigger tutorial_trigger;

    // Start is called before the first frame update
    void Start()
    {
        checklist_button = GameObject.Find("ChecklistButton").GetComponent<Button>();
        checklist_close_button = GameObject.Find("ChecklistCloseButton").GetComponent<Button>();
        checklist_items_window = GameObject.Find("ChecklistItemsWindow").GetComponent<Image>();
        checklist_items_text = GameObject.Find("ChecklistItemsText").GetComponent<Text>();

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

    void NextStageButton()
    {
        if (stage_title_text.text == "PLAN")
        {
            stage_title_text.text = "CODE";
            DeactivatedStages();
            code_stage.SetActive(true);
            tutorial_trigger = code_stage.GetComponent<TutorialTextTrigger>();
            tutorial_trigger.TriggerTutorial();
            if (checklist_window_animator.GetBool("IsOpen") == true) checklist_items_window.gameObject.SetActive(false);
        }
        else if (stage_title_text.text == "CODE")
        {
            stage_title_text.text = "BUILD";
            DeactivatedStages();
            build_stage.SetActive(true);
            tutorial_trigger = build_stage.GetComponent<TutorialTextTrigger>();
            tutorial_trigger.TriggerTutorial();
            if (checklist_window_animator.GetBool("IsOpen") == true) checklist_items_window.gameObject.SetActive(false);
        }
        else if (stage_title_text.text == "BUILD")
        {
            stage_title_text.text = "TEST";
            DeactivatedStages();
            test_stage.SetActive(true);
            tutorial_trigger = test_stage.GetComponent<TutorialTextTrigger>();
            tutorial_trigger.TriggerTutorial();
            if (checklist_window_animator.GetBool("IsOpen") == true) checklist_items_window.gameObject.SetActive(false);
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

}
