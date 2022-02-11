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

    // TODO: Crear GameObjects para cada sección que tenga carousel para acceder a su componenete planCarouselController y de ahí tomar el atributo cards
    // para saber cuál es la carta del centro y acceder a su descripción

    void Start()
    {
        description_button = GameObject.Find("CardDescriptionButton").GetComponent<Button>();
        close_description_button = GameObject.Find("CardDescriptionCloseButton").GetComponent<Button>();
        description_button.onClick.AddListener(DescriptionWindow);
        close_description_button.onClick.AddListener(CloseDescriptionWindow);
    }

    void DescriptionWindow()
    {
        description_window.gameObject.SetActive(true);
        close_description_button.gameObject.SetActive(true);
        animator_description_window.SetBool("IsDescriptionOpen", true);
        text_description_window.text = GetComponent<Carta>().card_description;

    }

    void CloseDescriptionWindow()
    {
        animator_description_window.SetBool("IsDescriptionOpen", false);
    }
}
