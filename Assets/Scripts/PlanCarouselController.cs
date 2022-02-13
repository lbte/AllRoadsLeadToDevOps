using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanCarouselController : MonoBehaviour
{
    public List<Card> cards = new List<Card>();
    public int left_index = 0;
    public int center_index = 1;
    public int right_index = 2;

    public Image left_image;
    public Image center_image;
    public Image right_image;

    public Button left_card_button;
    public Button center_card_button;
    public Button right_card_button;

    public GameObject plan;
    private PlanDeckController plan_deck_controller;

    void Start()
    {
        left_card_button.onClick.AddListener(CarouselRotationLeft);
        right_card_button.onClick.AddListener(CarouselRotationRight);

        plan_deck_controller = plan.GetComponent<PlanDeckController>();

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

        plan_deck_controller.DisableLevelIcon();
        plan_deck_controller.UpdateLevelIcon();

        plan_deck_controller.DisableSelectedIcon();
        plan_deck_controller.UpdateSelectedIcon();
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

        plan_deck_controller.DisableLevelIcon();
        plan_deck_controller.UpdateLevelIcon();

        plan_deck_controller.DisableSelectedIcon();
        plan_deck_controller.UpdateSelectedIcon();
    }
}