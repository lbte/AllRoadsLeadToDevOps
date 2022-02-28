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
            if(card.id == "blacksmith_anvil"){
                Card anvil_card = cards_materials[0];
                anvil_card.card_description = "Falling speed: High\nNumber of uses: Some\nPrecision: High\nImpact: Deadly\nDurability: High\nEase of getting: Difficult";
                cards.Add(anvil_card);
            }
            else if(card.id == "anvil_auction") {
                Card anvil_card = cards_materials[0];
                anvil_card.card_description = "Falling speed: High\nNumber of uses: Several\nPrecision: High\nImpact: Deadly\nDurability: High\nEase of getting: Easy";
                cards.Add(anvil_card);
            }

            else if(card.id == "fight_piano"){
                Card piano_card = cards_materials[1];
                piano_card.card_description = "Falling speed: Medium\nNumber of uses: Once\nPrecision: High\nImpact: Deadly\nDurability: Low\nEase of getting: Difficult";
                cards.Add(piano_card);
            } 
            else if(card.id == "old_piano"){
                Card piano_card = cards_materials[1];
                piano_card.card_description = "Falling speed: Medium\nNumber of uses: Once\nPrecision: High\nImpact: Deadly\nDurability: Medium\nEase of getting: Easy";
                cards.Add(piano_card);
            } 

            else if(card.id == "eagle_feather"){
                Card feather_card = cards_materials[2];
                feather_card.card_description = "Falling speed: Low\nNumber of uses: Many\nPrecision: Low\nImpact: Mild\nDurability: Medium\nEase of getting: Difficult";
                cards.Add(feather_card);
            }
            else if(card.id == "pillow_feather"){
                Card feather_card = cards_materials[2];
                feather_card.card_description = "Falling speed: Low\nNumber of uses: Many\nPrecision: Low\nImpact: Mild\nDurability: Medium\nEase of getting: Easy";
                cards.Add(feather_card);
            } 

            else if(card.id == "swings_rope"){
                Card rope_card = cards_materials[3];
                rope_card.card_description = "Weight : Light\nStability: High\nDurability: Medium\nEase of getting: Easy";
                cards.Add(rope_card);
            }
            else if(card.id == "cowboy_rope"){
                Card rope_card = cards_materials[3];
                rope_card.card_description = "Weight : Light\nStability: High\nDurability: Medium\nEase of getting: Difficult";
                cards.Add(rope_card);
            } 

            else if(card.id == "elastic_rope"){
                Card elastic_card = cards_materials[4];
                elastic_card.card_description = "Weight : Light\nStability: Low\nDurability: High\nEase of getting: Easy";
                cards.Add(elastic_card);
            }
            else if(card.id == "pants_elastic"){
                Card elastic_card = cards_materials[4];
                elastic_card.card_description = "Weight : Light\nStability: Low\nDurability: Low.\nEase of getting: Easy";
                cards.Add(elastic_card);
            }

            else if(card.id == "electricity_cable"){
                Card cable_card = cards_materials[5];
                cable_card.card_description = "Weight : Heavy\nStability: High\nDurability: High\nEase of getting: Medium";
                cards.Add(cable_card);
            }
            else if(card.id == "charger_cable"){
                Card cable_card = cards_materials[5];
                cable_card.card_description = "Weight : Light\nStability: Medium\nDurability: Low\nEase of getting: Easy";
                cards.Add(cable_card);
            }

            else if(card.id == "balloon_fair"){
                Card ballon_card = cards_materials[6];
                ballon_card.card_description = "Number of uses: Some\nInstallation difficulty: High\nEffort level: Decreased\nStability: High\nDurability: Medium\nEase of getting: Easy";
                cards.Add(ballon_card);
            }
            else if(card.id == "turkey_balloon"){
                Card ballon_card = cards_materials[6];
                ballon_card.card_description = "Number of uses: Some\nInstallation difficulty: High\nEffort level: Decreased\nStability: High\nDurability: Medium\nEase of getting: Difficult";
                cards.Add(ballon_card);
            } 

            else if(card.id == "alpinism_pulley"){
                Card pulley_card = cards_materials[7];
                pulley_card.card_description = "Number of uses: Many\nInstallation difficulty: High\nEffort level: Decreased\nStability: Low\nDurability: High\nEase of getting: Easy";
                cards.Add(pulley_card);
            }
            else if(card.id == "well_pulley"){
                Card pulley_card = cards_materials[7];
                pulley_card.card_description = "Number of uses: Some\nInstallation difficulty: High\nEffort level: Decreased\nStability: Low\nDurability: Low\nEase of getting: Easy";
                cards.Add(pulley_card);
            }

            else if(card.id == "gym_handwork"){
                Card handwork_card = cards_materials[8];
                handwork_card.card_description = "Number of uses: Few\nInstallation difficulty: Low\nTesting difficulty: Low\nEffort level: Increased\nStability: High\nDurability: Medium\nEase of getting: Difficult";
                cards.Add(handwork_card);
            }
            else if(card.id == "hire_handwork"){
                Card handwork_card = cards_materials[8];
                handwork_card.card_description = "Number of uses: Several\nInstallation difficulty: Low\nTesting difficulty: Low\nEffort level: Increased\nStability: High\nDurability: High\nEase of getting: Easy";
                cards.Add(handwork_card);
            } 

            else if(card.id == "compost_carrots"){
                Card carrot_card = cards_materials[9];
                carrot_card.card_description = "Amount of time to eat: High\nEffectiveness: Medium\nDurability: Low\nEase of getting: Low";
                cards.Add(carrot_card);
            }
            else if(card.id == "buy_carrots"){
                Card carrot_card = cards_materials[9];
                carrot_card.card_description = "Amount of time to eat: High\nEffectiveness: High\nDurability: High\nEase of getting: Easy";
                cards.Add(carrot_card);
            }

            else if(card.id == "jam_blackberry"){
                Card blackberry_card = cards_materials[10];
                blackberry_card.card_description = "Amount of time to eat: Low\nEffectiveness: Low\nDurability: High\nEase of getting: Easy";
                cards.Add(blackberry_card);
            }
            else if(card.id == "gather_blackberry"){
                Card blackberry_card = cards_materials[10];
                blackberry_card.card_description = "Amount of time to eat: Low\nEffectiveness: High\nDurability: Medium\nEase of getting: Easy.";
                cards.Add(blackberry_card);
            }

            else if(card.id == "own_cooked_food"){
                Card burger_card = cards_materials[11];
                burger_card.card_description = "Amount of time to eat: High\nEffectiveness: Low\nDurability: Low\nEase of getting: Medium";
                cards.Add(burger_card);
            }
            else if(card.id == "food_delivery"){
                Card burger_card = cards_materials[11];
                burger_card.card_description = "Amount of time to eat: High\nEffectiveness: Low\nDurability: Low\nEase of getting: Easy";
                cards.Add(burger_card);
            }
        }
    }
}