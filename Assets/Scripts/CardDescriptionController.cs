using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDescriptionController : MonoBehaviour
{
    private Button description_button;
    private Button close_description_button;
    public Image description_window;
    public Text text_description_window;
    public Animator animator_description_window;
    public Image plan_tools_card_image;

    private GameObject plan_stage;
    private GameObject plan_project;

    private GameObject plan_architecture;
    private PlanCarouselController plan_carousel_script;
    private PlanDeckController plan_deck_script;

    private GameObject plan_abilities;

    private GameObject plan_tools;

    private StageController stage_controller_script;
    private GameObject code_stage;

    private CodeCarouselController code_carousel_script;
    private GameObject build_stage;
    private BuildCarouselController build_carousel_script;

    public SingleCardController single_card_script;

    void Start()
    {
        // Assignment of each game object
        description_button = GameObject.Find("CardDescriptionButton").GetComponent<Button>();
        close_description_button = GameObject.Find("CardDescriptionCloseButton").GetComponent<Button>();
        stage_controller_script = GameObject.Find("Views").GetComponent<StageController>();
        plan_deck_script = plan_stage.GetComponent<PlanDeckController>();
        description_button.onClick.AddListener(DescriptionWindow);
        close_description_button.onClick.AddListener(CloseDescriptionWindow);
    }

    void StageDescriptionAssignment()
    {
        if (stage_controller_script.stage_title_text.text == "PLAN") 
        {
            plan_stage = GameObject.Find("Plan");
            // Depending on which deck we are in, assign the description of the center card accordingly

            if (plan_deck_script.deck_button_text.text == "Project")
            {
                Debug.Log("PROJECT");
                plan_project = GameObject.Find("ProjectCardButton");
                single_card_script = plan_project.GetComponent<SingleCardController>();
                text_description_window.text = single_card_script.cards[0].card_description; 
            }
            else if (plan_deck_script.deck_button_text.text == "Architecture")
            {
                plan_architecture = GameObject.Find("PlanArchitectures");
                plan_carousel_script = plan_architecture.GetComponent<PlanCarouselController>();
                text_description_window.text = plan_carousel_script.cards[plan_carousel_script.center_index].card_description;
            }
            else if (plan_deck_script.deck_button_text.text == "Abilities")
            {
                plan_abilities = GameObject.Find("PlanAbilities");
                plan_carousel_script = plan_abilities.GetComponent<PlanCarouselController>();
                text_description_window.text = plan_carousel_script.cards[plan_carousel_script.center_index].card_description;
            }
            else if (plan_deck_script.deck_button_text.text == "Tools")
            {
                plan_tools = GameObject.Find("ToolCardButton");
                plan_tools_card_image = GameObject.Find("ToolCardImage").GetComponent<Image>();
                single_card_script = plan_tools.GetComponent<SingleCardController>();
                text_description_window.text = plan_deck_script.random_card.card_description; 
            }
        }
        else if (stage_controller_script.stage_title_text.text == "CODE")
        {
            code_stage = GameObject.Find("CodeItems");
            code_carousel_script = code_stage.GetComponent<CodeCarouselController>();
            text_description_window.text = code_carousel_script.deck[code_carousel_script.center_index].card_description;
        }
        else if (stage_controller_script.stage_title_text.text == "BUILD")
        {
            build_stage = GameObject.Find("BuildItems");
            build_carousel_script = build_stage.GetComponent<BuildCarouselController>();
            text_description_window.text = build_carousel_script.cards[build_carousel_script.center_index].card_description;
        }
    }

    void DescriptionWindow()
    {
        description_window.gameObject.SetActive(true);
        close_description_button.gameObject.SetActive(true);
        animator_description_window.SetBool("IsDescriptionOpen", true);
        StageDescriptionAssignment();

    }

    void CloseDescriptionWindow()
    {
        animator_description_window.SetBool("IsDescriptionOpen", false);
    }
}
