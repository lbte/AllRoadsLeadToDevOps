using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class FlipCardController : MonoBehaviour
{  
    public Image card_image;
    public Card card;

    public Button card_button;

    private bool isFlip = false;   // Tells whether the card is flipped or not

    // Start is called before the first frame update
    void Start()
    {
        card_button.onClick.AddListener(FlipCard);
        card_image.sprite = card.card_image;
    }

    // Shows the reverse of the center card
    void FlipCard()
    {
        if (isFlip)
        {
            card_image.sprite = card.card_image;
            isFlip = false;
        }
        else
        {
            card_image.sprite = card.card_description;
            isFlip = true;
        }
    }
}
