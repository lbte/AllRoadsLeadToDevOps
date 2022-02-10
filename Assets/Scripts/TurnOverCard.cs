using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOverCard : MonoBehaviour
{
    public GameObject cardBack;

    private bool card_back_active = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartFlip();
        }
    }

    public void StartFlip()
    {
        StartCoroutine(CalculateFlip());
    }

    public void Flip()
    {
        if (card_back_active)
        {
            cardBack.SetActive(false);
            card_back_active = false;
        }
        else
        {
            cardBack.SetActive(true);
            card_back_active = true;
        }
    }

    IEnumerator CalculateFlip()
    {
        for (float i = 0f; i < 180f; i += 10f)
        {

            transform.rotation = Quaternion.Euler(0f, i, 0f);

            if (i == 90f || i == -90f)
            {
                Flip();
            }
            yield return new WaitForSeconds(0.01f);
        }
    }
}
