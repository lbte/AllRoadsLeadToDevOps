using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TurnOverCard : MonoBehaviour, IPointerDownHandler
{
    public GameObject cardBack;

    private bool card_back_active = false;

    void Update()
    {
        
        /*if (Input.GetMouseButtonDown(0))
        {
            // convert the click position to world space in order to properly compare against the position of the GameObjects.
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // to drop the z-coordinate, and use it as the starting point. 
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            // Vector2.zero as the direction of the Raycast to ensure only objects located directly at the point of the click are detected
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            // to determine if anything was hit by the click
            if (hit.collider != null)
            {
                Debug.Log(hit.collider.gameObject.name);
                hit.collider.attachedRigidbody.AddForce(Vector2.up);
            }
            StartFlip();
        }*/
    }

    public void OnPointerDown(PointerEventData event_data)
    {
        StartFlip();
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
