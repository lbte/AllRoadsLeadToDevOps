using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildDeckController : MonoBehaviour
{
    // buttons for each deck of each part
    public Button deck_button;
    public Button up_arrow_button;
    public Button down_arrow_button;
    public Text deck_button_text;
    public Animator warning_categorize_window_animator;
    public Image warning_categorize_window;
    public Text warning_categorize_window_text;

    public GameObject carousel;
    public GameObject landscape;
    public GameObject air_trap;
    public GameObject ground_trap;
    public PlayerController player_controller_script;
    public StageController stage_controller_script;
    public CodeCarouselController code_carousel_script;
    public BuildCarouselController build_carousel_script;

    // list with the decks names
    private List<string> plan_parts = new List<string>() { "Categorize", "Building" };
    public int word_index;

    void Start()
    {   

        // to show views when the arrow buttons are clicked
        up_arrow_button.onClick.AddListener(UpButton);
        down_arrow_button.onClick.AddListener(DownButton);

        // default index for the project window
        word_index = plan_parts.IndexOf("Categorize"); // default screen
        UpdateButtonText();

    }

    // Update the text that appears on the deck button
    public void UpdateButtonText()
    {
        // change the button text
        deck_button_text.text = plan_parts[word_index];
        if (deck_button_text.text == "Categorize")
        {
            carousel.SetActive(true);
            landscape.SetActive(false);
        }
        else
        {
            carousel.SetActive(false);
            landscape.SetActive(true);
            stage_controller_script.next_stage_button.gameObject.SetActive(true);
            // deactivate the buttons so the player can't go back to the categorize
            up_arrow_button.gameObject.SetActive(false);
            down_arrow_button.gameObject.SetActive(false);

            if (player_controller_script.selected_architecture.id == "architecture_2")
            {
                air_trap.SetActive(false);
                ground_trap.SetActive(true);
            }
            else if(player_controller_script.selected_architecture.id == "architecture_1")
            {
                air_trap.SetActive(true);
                ground_trap.SetActive(false);
            }
            else if(player_controller_script.selected_architecture.id == "architecture_3")
            {
                air_trap.SetActive(false);
                ground_trap.SetActive(false);
                ///////////////// POP-UP bonito ("It looks like the desert is not an appropiate place for that architecture")
            }
        }
    }

    // move up on the arrows and show different views according to that
    public void UpButton()
    {   
        int impact = player_controller_script.impact_categorize_correctness;
        int hold = player_controller_script.hold_categorize_correctness;
        int bait = player_controller_script.bait_categorize_correctness;
        int mechanism = player_controller_script.mechanism_categorize_correctness;
        if((impact + hold + bait + mechanism) != 4){ // Categorize fails
            // Returns to code (Alguna habilidad/herramienta podria evitar esto)
            // Pop-up bonito (Mensaje="It looks like something went wrong with your materials ...")
            StartCoroutine(WarningWrongCategorizeDisplay("It looks like something went wrong with your materials...\nYou must code again.", 2f));
        }
        else{
            // Pop-up bonito (Mensaje="It looks you did right by categorizing your materials ...")
            StartCoroutine(WarningUpRightCategorizeDisplay("It looks you did right by categorizing your materials...", 2f));

        }
    }

    // move down on the arrows and show different views according to that
    public void DownButton()
    {
        int impact = player_controller_script.impact_categorize_correctness;
        int hold = player_controller_script.hold_categorize_correctness;
        int bait = player_controller_script.bait_categorize_correctness;
        int mechanism = player_controller_script.mechanism_categorize_correctness;
        if ((impact + hold + bait + mechanism) != 4)
        { // Categorize fails
            // Returns to code (Alguna habilidad/herramienta podria evitar esto)
            // Pop-up bonito (Mensaje="It looks like something went wrong with your materials ...")
            StartCoroutine(WarningWrongCategorizeDisplay("It looks like something went wrong with your materials...\nYou must code again.", 2f));
        }
        else
        {
            // Pop-up bonito (Mensaje="It looks you did right by categorizing your materials ...")
            StartCoroutine(WarningDownRightCategorizeDisplay("It looks you did right by categorizing your materials...", 2f));
        }
    }

    // pop up window for the wrong categorize message when clicking an arrow button
    IEnumerator WarningWrongCategorizeDisplay(string text, float delay)
    {
        warning_categorize_window.gameObject.SetActive(true);
        warning_categorize_window_text.text = text;
        warning_categorize_window_animator.SetBool("IsWarningCategorizeOpen", true);
        yield return new WaitForSeconds(delay);
        warning_categorize_window_animator.SetBool("IsWarningCategorizeOpen", false);

        // move to the code stage
        stage_controller_script.stage_title_text.text = "PLAN";
        stage_controller_script.NextStageButton();
        code_carousel_script.UpdateSelectedIcon();
    }

    // pop up window when clicking the up button and having a right answer
    IEnumerator WarningUpRightCategorizeDisplay(string text, float delay)
    {
        warning_categorize_window.gameObject.SetActive(true);
        warning_categorize_window_text.text = text;
        warning_categorize_window_animator.SetBool("IsWarningCategorizeOpen", true);
        yield return new WaitForSeconds(delay);
        warning_categorize_window_animator.SetBool("IsWarningCategorizeOpen", false);

        // up arrow button
        word_index++;
        if (word_index >= plan_parts.Count) word_index = 0;

        UpdateButtonText();
    }

    // pop up window when clicking the down button and having a right answer
    IEnumerator WarningDownRightCategorizeDisplay(string text, float delay)
    {
        warning_categorize_window.gameObject.SetActive(true);
        warning_categorize_window_text.text = text;
        warning_categorize_window_animator.SetBool("IsWarningCategorizeOpen", true);
        yield return new WaitForSeconds(delay);
        warning_categorize_window_animator.SetBool("IsWarningCategorizeOpen", false);

        // Down arrow button
        word_index--;
        if (word_index < 0) word_index = plan_parts.Count - 1;

        UpdateButtonText();
    }
}
