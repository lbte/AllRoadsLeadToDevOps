using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildCarouselController : MonoBehaviour
{
    public List<Card> cards = new List<Card>();             
    public List<Card> cards_materials = new List<Card>();
    // 0: Anvil, 1: piano, 2: feather, 
    // 3: rope, 4: elastic, 5: braided cable, 
    // 6: ballon, 7: pulley, 8: handwork
    // 9: carrot, 10: blackberry, 11: burger

    public int left_index = 0;
    public int center_index = 1;
    public int right_index = 2;

    public Image left_image;
    public Image center_image;
    public Image right_image;
    public Text left_card_title_text;
    public Text center_card_title_text;
    public Text right_card_title_text;

    public Button left_card_button;
    public Button center_card_button;
    public Button right_card_button;

    [SerializeField] private BuildDeckController deck;

    private PlayerController player_controller_script;

    void Start()
    {
        deck = GameObject.Find("Build").GetComponent<BuildDeckController>();

        left_card_button.onClick.AddListener(CarouselRotationLeft);
        right_card_button.onClick.AddListener(CarouselRotationRight);

    }

    // Updates cards images
    public void UpdateCardImages()
    {
        left_image.sprite = cards[left_index].card_image;
        left_card_title_text.text = cards[left_index].card_title;
        center_image.sprite = cards[center_index].card_image;
        center_card_title_text.text = cards[center_index].card_title;
        right_image.sprite = cards[right_index].card_image;
        right_card_title_text.text = cards[right_index].card_title;
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

    }

    public void AssignSelectedCodeCards(){
        player_controller_script = GameObject.Find("Views").GetComponent<PlayerController>();
        cards.Clear();
        foreach(Card card in player_controller_script.selected_code_cards){
            if(card.id == "blacksmith_anvil" || card.id == "anvil_auction") cards.Add(cards_materials[0]);
            else if(card.id == "fight_piano" || card.id == "old_piano") cards.Add(cards_materials[1]);
            else if(card.id == "eagle_feather" || card.id == "pillow_feather") cards.Add(cards_materials[2]);

            else if(card.id == "swings_rope" || card.id == "cowboy_rope") cards.Add(cards_materials[3]);
            else if(card.id == "elastic_rope" || card.id == "pants_elastic") cards.Add(cards_materials[4]);
            else if(card.id == "electricity_cable" || card.id == "charger_cable") cards.Add(cards_materials[5]);

            else if(card.id == "balloon_fair" || card.id == "turkey_balloon") cards.Add(cards_materials[6]);
            else if(card.id == "alpinism_pulley" || card.id == "well_pulley") cards.Add(cards_materials[7]);
            else if(card.id == "gym_handwork" || card.id == "hire_handwork") cards.Add(cards_materials[8]);

            else if(card.id == "compost_carrots" || card.id == "buy_carrots") cards.Add(cards_materials[9]);
            else if(card.id == "jam_blackberry" || card.id == "gather_blackberry") cards.Add(cards_materials[10]);
            else if(card.id == "own_cooked_food" || card.id == "food_delivery") cards.Add(cards_materials[11]);
        }
    }
}