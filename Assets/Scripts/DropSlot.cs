using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropSlot : MonoBehaviour, IDropHandler
{

    public GameObject item;
    public BuildCarouselController build_carousel;
    public GameObject card_prefab;

    public void OnDrop(PointerEventData eventData)
    {
        if(item)
        {
            Destroy(item);
        }

        item = Instantiate(card_prefab) as GameObject;
        item.transform.SetParent(transform);
        item.transform.position = transform.position;
        item.transform.GetChild(0).GetComponent<Image>().sprite = build_carousel.center_image.sprite;

        item.transform.name = build_carousel.cards[build_carousel.center_index].category;
        Debug.Log(item.transform.name);


        if (eventData.pointerDrag != null && DragHandler.itemDragging.CompareTag("CardClone"))
        {
            item.transform.GetChild(0).GetComponent<Image>().sprite = DragHandler.itemDragging.transform.GetChild(0).GetComponent<Image>().sprite;
            Destroy(DragHandler.itemDragging);

        }

    }

}
