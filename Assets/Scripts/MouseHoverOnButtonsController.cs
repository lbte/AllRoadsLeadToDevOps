using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseHoverOnButtonsController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image legend_image;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        legend_image.gameObject.SetActive(true);
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        legend_image.gameObject.SetActive(false);
    }
}
