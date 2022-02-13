using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDescriptionController : MonoBehaviour
{
    public Button description_button;
    public Button close_description_button;
    public Image description_window;
    public Text text_description_window;
    public Animator animator_description_window;

    private GameObject plan_stage;

    private GameObject plan_architecture;
    private PlanCarouselController plan_carousel_script;
    public PlanDeckController plan_deck_script;

    private GameObject plan_abilities;

    private StageController stage_controller_script;
    private GameObject code_stage;

    private CodeCarouselController code_carousel_script;
    private GameObject build_stage;
    private BuildCarouselController build_carousel_script;

    void Start()
    {
        // Assignment of each game object
        stage_controller_script = GameObject.Find("Views").GetComponent<StageController>();
        description_button.onClick.AddListener(DescriptionWindow);
        close_description_button.onClick.AddListener(CloseDescriptionWindow);
    }

    void StageDescriptionAssignment()
    {
        Debug.Log("a");
        if (stage_controller_script.stage_title_text.text == "PLAN") 
        {
            // Depending on which deck we are in, assign the description of the center card accordingly
            
            if (plan_deck_script.deck_button_text.text == "Architecture")
            {
                plan_architecture = GameObject.Find("PlanArchitectures");
                plan_carousel_script = plan_architecture.GetComponent<PlanCarouselController>();
                text_description_window.text = plan_carousel_script.cards[plan_carousel_script.center_index].card_description;
            }
            else if (plan_deck_script.deck_button_text.text == "Project")
            {
                text_description_window.text = plan_deck_script.single_card_script_project.cards[0].card_description;
            }
            else if (plan_deck_script.deck_button_text.text == "Abilities")
            {
                plan_abilities = GameObject.Find("PlanAbilities");
                plan_carousel_script = plan_abilities.GetComponent<PlanCarouselController>();
                text_description_window.text = plan_carousel_script.cards[plan_carousel_script.center_index].card_description;
            }
            else if (plan_deck_script.deck_button_text.text == "Tools")
            {
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
        StageDescriptionAssignment();
        animator_description_window.SetBool("IsDescriptionOpen", true);
    }

    void CloseDescriptionWindow()
    {
        animator_description_window.SetBool("IsDescriptionOpen", false);
    }
}
