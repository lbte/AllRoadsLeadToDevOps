using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseHoverOnButtonsController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool mouse_over = false;
    public Image legend_image;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        mouse_over = true;
        legend_image.gameObject.SetActive(true);
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        mouse_over = false;
        legend_image.gameObject.SetActive(false);
    }
}
