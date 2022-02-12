using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CodeCarouselController : MonoBehaviour
{
    public List<Card> deck = new List<Card>();
    public int left_index = 0;
    public int center_index = 1;
    public int right_index = 2;

    public Image left_image;
    public Image center_image;
    public Image right_image;

    public Image left_card_selected_icon;
    public Image center_card_selected_icon;
    public Image right_card_selected_icon;
    public Sprite card_selected_icon;

    public Button left_card_button;
    public Button center_card_button;
    public Button right_card_button;

    public Button select_button;

    void Start()
    {
        select_button.onClick.AddListener(SelectButtonHandler);
        left_card_button.onClick.AddListener(CarouselRotationLeft);
        right_card_button.onClick.AddListener(CarouselRotationRight);

        DisableSelectedIcon();
        UpdateCardImages();
    }

    // Updates cards images
    public void UpdateCardImages()
    {
        left_image.sprite = deck[left_index].card_image;
        center_image.sprite = deck[center_index].card_image;
        right_image.sprite = deck[right_index].card_image;
    }

    // Rotates the carousel of cards to the right
    void CarouselRotationRight()
    {
        left_index++;
        center_index++;
        right_index++;

        if (right_index >= deck.Count) right_index = 0;
        if (center_index >= deck.Count) center_index = 0;
        if (left_index >= deck.Count) left_index = 0;

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

        if (right_index < 0) right_index = deck.Count - 1;
        if (center_index < 0) center_index = deck.Count - 1;
        if (left_index < 0) left_index = deck.Count - 1;

        UpdateCardImages();
        DisableSelectedIcon();
        UpdateSelectedIcon();
    }

    void SelectButtonHandler(){
        if(deck[center_index].selected == true){
            deck[center_index].selected = false;
        }
        else{
            deck[center_index].selected = true;
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
        if(deck[left_index].selected == true){
            left_card_selected_icon.gameObject.SetActive(true);
            left_card_selected_icon.sprite = card_selected_icon;
        }
        if(deck[center_index].selected == true){
            center_card_selected_icon.gameObject.SetActive(true);
            center_card_selected_icon.sprite = card_selected_icon;
        }
        if(deck[right_index].selected == true){
            right_card_selected_icon.gameObject.SetActive(true);
            right_card_selected_icon.sprite = card_selected_icon;
        }
    }
}
