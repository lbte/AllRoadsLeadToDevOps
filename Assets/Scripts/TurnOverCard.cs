using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TurnOverCard : MonoBehaviour
{
    public GameObject cardBack;

    private bool card_back_active = false;

    void Update()
    {

        /*if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse clicked");
            // convert the click position to world space in order to properly compare against the position of the GameObjects.
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // to drop the z-coordinate, and use it as the starting point. 
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            // Vector2.zero as the direction of the Raycast to ensure only objects located directly at the point of the click are detected
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            // to determine if anything was hit by the click
            Debug.Log("You selected the " + hit.collider.gameObject.name);
            if (hit.collider.gameObject.name == "ProjectCardFront" || hit.collider.gameObject.name == "ProjectCardBack")
            {
                Debug.Log("You selected the " + hit.collider.gameObject.name);
                //hit.collider.attachedRigidbody.AddForce(Vector2.up);
                StartFlip();
            }
        }*/

        if (Input.GetMouseButtonDown(0))
        { // if left button pressed...
            // turning a screen point into a ray
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                

                if (hit.transform != null)
                {
                    Debug.Log("You selected the " + hit.transform.gameObject.name);
                }
                // the object identified by hit.transform was clicked
                // do whatever you want
                /*if (hit.transform.gameObject.name == "ProjectCardFront" || hit.transform.gameObject.name == "ProjectCardBack")
                {
                    Debug.Log("You selected the " + hit.transform.gameObject.name);
                    //hit.collider.attachedRigidbody.AddForce(Vector2.up);
                    StartFlip();
                }*/
            }
        }

        /*if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse is pressed down");
            Camera cam = Camera.main;

            //Raycast depends on camera projection mode
            Vector2 origin = Vector2.zero;
            Vector2 dir = Vector2.zero;

            if (cam.orthographic)
            {
                origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
            else
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                origin = ray.origin;
                dir = ray.direction;
            }

            RaycastHit2D hit = Physics2D.Raycast(origin, dir);

            //Check if we hit anything
            if (hit)
            {
                Debug.Log("We hit " + hit.collider.name);
            }
        }*/
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
