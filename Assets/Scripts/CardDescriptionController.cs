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

    private GameObject plan_stage;
    //private GameObject plan_project;
    private GameObject plan_architecture;
    private PlanCarouselController plan_carousel_script;
    private PlanDeckController plan_deck_script;
    private GameObject plan_abilities;
    //private GameObject plan_tools;
    private StageController stage_controller_script;
    private GameObject code_stage;

    private CodeCarouselController code_carousel_script;
    private GameObject build_stage;
    private BuildCarouselController build_carousel_script;

    void Start()
    {
        // Assignment of each game object
        description_button = GameObject.Find("CardDescriptionButton").GetComponent<Button>();
        close_description_button = GameObject.Find("CardDescriptionCloseButton").GetComponent<Button>();

        plan_stage = GameObject.Find("Plan");
        //plan_project = GameObject.Find("PlanProject").GetComponent<GameObject>();
        plan_architecture = GameObject.Find("PlanArchitectures");
        plan_abilities = GameObject.Find("PlanAbilities");
        //plan_tools = GameObject.Find("PlanTools").GetComponent<GameObject>();
        stage_controller_script = GameObject.Find("Views").GetComponent<StageController>();
        code_stage = GameObject.Find("CodeItems");
        build_stage = GameObject.Find("BuildItems");

        description_button.onClick.AddListener(DescriptionWindow);
        close_description_button.onClick.AddListener(CloseDescriptionWindow);
    }

    void Update()
    {
        StageDescriptionAssignment();
    }

    void StageDescriptionAssignment()
    {
        // CREO QUE ESTO TAL VEZ SE PODRÍA REALIZAR CON LOS SCRIPTS YA EXISTENTES, PERO NO QUIERO PENSAR. ASÍ ESTÁ FUNCIONANDO, SO NO SÉ. USTEDES DIRÁN.
        if(stage_controller_script.stage_title_text.text == "PLAN")
        {
            // Depending on which deck we are in, assign the description of the center card accordingly
            plan_deck_script = plan_stage.GetComponent<PlanDeckController>();

            if (plan_deck_script.deck_button_text.text == "Project")
            {
                text_description_window.text = "PROJEEECT"; //TODO: ACCEDER AL ATRIBUTO CARD_DESCRIPTION PARA ESTA CARTA QUE ESTÁ SOLA.
            }
            else if (plan_deck_script.deck_button_text.text == "Architecture")
            {
                plan_carousel_script = plan_architecture.GetComponent<PlanCarouselController>();
                text_description_window.text = plan_carousel_script.cards[plan_carousel_script.center_index].card_description;
            }
            else if (plan_deck_script.deck_button_text.text == "Abilities")
            {
                plan_carousel_script = plan_abilities.GetComponent<PlanCarouselController>();
                text_description_window.text = plan_carousel_script.cards[plan_carousel_script.center_index].card_description;
            }
            else if (plan_deck_script.deck_button_text.text == "Tools")
            {
                text_description_window.text = ""; //TODO: ACCEDER AL ATRIBUTO CARD_DESCRIPTION PARA ESTA CARTA QUE ESTÁ SOLA.
            }
        }
        else if (stage_controller_script.stage_title_text.text == "CODE")
        {
            code_carousel_script = code_stage.GetComponent<CodeCarouselController>();
            text_description_window.text = code_carousel_script.deck[code_carousel_script.center_index].card_description;
        }
        else if (stage_controller_script.stage_title_text.text == "BUILD")
        {
            build_carousel_script = build_stage.GetComponent<BuildCarouselController>();
            text_description_window.text = build_carousel_script.cards[build_carousel_script.center_index].card_description;
        }
        

        
    }

    void DescriptionWindow()
    {
        description_window.gameObject.SetActive(true);
        close_description_button.gameObject.SetActive(true);
        animator_description_window.SetBool("IsDescriptionOpen", true);
        //text_description_window.text = GetComponent<Carta>().card_description;

    }

    void CloseDescriptionWindow()
    {
        animator_description_window.SetBool("IsDescriptionOpen", false);
    }
}
