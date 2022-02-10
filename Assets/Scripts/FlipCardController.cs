using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class FlipCardController : MonoBehaviour
{  
    public Image card_image;
    public Card card;
    public float x, y, z;
    public int timer;

    public Button card_button;

    private bool isFlip = false;   // Tells whether the card is flipped or not

    // Start is called before the first frame update
    void Start()
    {
        card_button.onClick.AddListener(StartFlipCard);
        card_image.sprite = card.card_image;
    }

    // Shows the reverse of the center card
    void StartFlipCard()
    {
        StartCoroutine(CalculateFlip());

    }

    void Flip()
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

    IEnumerator CalculateFlip()
    {
        for (int i = 0; i < 180; i++)
        {
            yield return new WaitForSeconds(0.01f);
            transform.Rotate(new Vector3(x, y, z));
            timer++;

            if (timer == 90 || timer == -90)
            {
                Flip();
            }
        }
        timer = 0;
    }

}
