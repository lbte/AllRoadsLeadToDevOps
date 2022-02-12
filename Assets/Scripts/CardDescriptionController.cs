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

    private GameObject plan_project;
    private GameObject plan_architecture;
    private GameObject plan_abilities;
    private GameObject plan_tools;
    //private GameObject code_stage;
    //private GameObject build_stage;

    // TODO: Crear GameObjects para cada sección que tenga carousel para acceder a su componenete planCarouselController y de ahí tomar el atributo cards
    // para saber cuál es la carta del centro y acceder a su descripción

    void Start()
    {
        description_button = GameObject.Find("CardDescriptionButton").GetComponent<Button>();
        close_description_button = GameObject.Find("CardDescriptionCloseButton").GetComponent<Button>();
        plan_project = GameObject.Find("PlanProject").GetComponent<GameObject>();
        plan_architecture = GameObject.Find("PlanArchitectures").GetComponent<GameObject>();
        plan_abilities = GameObject.Find("PlanAbilities").GetComponent<GameObject>();
        plan_tools = GameObject.Find("PlanTools").GetComponent<GameObject>();
        //code_stage = GameObject.Find("CodeItems").GetComponent<GameObject>();
        //build_stage = GameObject.Find("Build...").GetComponent<GameObject>();
        description_button.onClick.AddListener(DescriptionWindow);
        close_description_button.onClick.AddListener(CloseDescriptionWindow);
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
