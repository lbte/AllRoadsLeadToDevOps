using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDescriptionController : MonoBehaviour
{

    private Button description_button;
    private Button close_description_button;
    public string description_text;
    public Image description_window;
    public Text text_description_window;
    public Animator animator_description_window;

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
        text_description_window.text = description_text;

    }

    void CloseDescriptionWindow()
    {
        animator_description_window.SetBool("IsDescriptionOpen", false);
        //description_window.gameObject.SetActive(false);
        //close_description_button.gameObject.SetActive(false);
        
    }
}
