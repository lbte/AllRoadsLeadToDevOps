using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildCarouselController : MonoBehaviour
{
    public List<Card> cards = new List<Card>();
    private int left_index = 0;
    private int center_index = 1;
    private int right_index = 2;

    public Button select_button;

    public Image left_image;
    public Image center_image;
    public Image right_image;

    public Image left_card_selected_icon;
    public Image center_card_selected_icon;
    public Image right_card_selected_icon;
    public Sprite card_selected_icon_impact;
    public Sprite card_selected_icon_holding;
    public Sprite card_selected_icon_mechanism;
    public Sprite card_selected_icon_bait;

    public Button left_card_button;
    public Button center_card_button;
    public Button right_card_button;

    [SerializeField] private BuildDeckController deck;

    void Start()
    {
        deck = GameObject.Find("Build").GetComponent<BuildDeckController>();

        select_button.onClick.AddListener(SelectButtonHandler);
        left_card_button.onClick.AddListener(CarouselRotationLeft);
        right_card_button.onClick.AddListener(CarouselRotationRight);

        DisableSelectedIcon();
        UpdateCardImages();
    }

    // Updates cards images
    void UpdateCardImages()
    {
        left_image.sprite = cards[left_index].card_image;
        center_image.sprite = cards[center_index].card_image;
        right_image.sprite = cards[right_index].card_image;
    }

    // Rotates the carousel of cards to the right
    void CarouselRotationRight()
    {
        left_index++;
        center_index++;
        right_index++;

        if (right_index >= cards.Count) right_index = 0;
        if (center_index >= cards.Count) center_index = 0;
        if (left_index >= cards.Count) left_index = 0;

        UpdateCardImages();
        DisableSelectedIcon();
        UpdateSelectedIcon();
    }

    // Rotates the carousel of cards to the left
    void CarouselRotationLeft()
    {
        left_index--;
        center_index--;
        right_index--;

        if (right_index < 0) right_index = cards.Count - 1;
        if (center_index < 0) center_index = cards.Count - 1;
        if (left_index < 0) left_index = cards.Count - 1;

        UpdateCardImages();
        DisableSelectedIcon();
        UpdateSelectedIcon();

    }

    void SelectButtonHandler(){
        string deck_button_text = deck.deck_button_text.text;
        if(cards[center_index].selected == true){
            cards[center_index].selected = false;
            cards[center_index].category = "";
        }
        else{
            cards[center_index].selected = true;
            cards[center_index].category = deck_button_text;
        }
        DisableSelectedIcon();
        UpdateSelectedIcon();
    }

    void DisableSelectedIcon(){
        left_card_selected_icon.gameObject.SetActive(false);
        center_card_selected_icon.gameObject.SetActive(false);
        right_card_selected_icon.gameObject.SetActive(false);
    }

    void UpdateSelectedIcon(){
        if(cards[left_index].selected == true){
            left_card_selected_icon.gameObject.SetActive(true);

            if(cards[left_index].category == "Impact") left_card_selected_icon.sprite = card_selected_icon_impact;
            else if(cards[left_index].category == "Holding") left_card_selected_icon.sprite = card_selected_icon_holding;
            else if(cards[left_index].category == "Mechanism") left_card_selected_icon.sprite = card_selected_icon_mechanism;
            else if(cards[left_index].category == "Bait") left_card_selected_icon.sprite = card_selected_icon_bait;
        }
        if(cards[center_index].selected == true){
            center_card_selected_icon.gameObject.SetActive(true);

            if(cards[center_index].category == "Impact") center_card_selected_icon.sprite = card_selected_icon_impact;
            else if(cards[center_index].category == "Holding") center_card_selected_icon.sprite = card_selected_icon_holding;
            else if(cards[center_index].category == "Mechanism") center_card_selected_icon.sprite = card_selected_icon_mechanism;
            else if(cards[center_index].category == "Bait") center_card_selected_icon.sprite = card_selected_icon_bait;
        }
        if(cards[right_index].selected == true){
            right_card_selected_icon.gameObject.SetActive(true);

            if(cards[right_index].category == "Impact") right_card_selected_icon.sprite = card_selected_icon_impact;
            else if(cards[right_index].category == "Holding") right_card_selected_icon.sprite = card_selected_icon_holding;
            else if(cards[right_index].category == "Mechanism") right_card_selected_icon.sprite = card_selected_icon_mechanism;
            else if(cards[right_index].category == "Bait") right_card_selected_icon.sprite = card_selected_icon_bait;
        }
    }
}