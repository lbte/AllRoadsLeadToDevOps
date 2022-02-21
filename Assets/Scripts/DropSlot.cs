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
    public PlayerController player_controller_script;

    void Start(){
        player_controller_script = GameObject.Find("Views").GetComponent<PlayerController>();
    }

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
        //Debug.Log(item.transform.name);

        // Check categorize correctness
        // Fail categorize -> Returns to code
        CheckCategorizeCorrectness(item);

        // Building section
        if (eventData.pointerDrag != null && DragHandler.itemDragging.CompareTag("CardClone"))
        {
            item.transform.GetChild(0).GetComponent<Image>().sprite = DragHandler.itemDragging.transform.GetChild(0).GetComponent<Image>().sprite;
            item.transform.name = DragHandler.itemDragging.transform.name;
            //Debug.Log("a");
            //Debug.Log(item.transform.name);
            Destroy(DragHandler.itemDragging);

            // Check building correctness
            CheckBuildCorrectness(item);
        }
    }

    void CheckBuildCorrectness(GameObject item){
        if(item.transform.name == "impact"){
            if(item.transform.parent.name == "impact") player_controller_script.impact_build_correctness = 1;
            else if(item.transform.parent.name == "hold" ) player_controller_script.hold_build_correctness = 0;
            else if(item.transform.parent.name == "bait" ) player_controller_script.bait_build_correctness  = 0;
            else if(item.transform.parent.name == "mechanism" ) player_controller_script.mechanism_build_correctness = 0;
        }

        if(item.transform.name == "hold"){
            if(item.transform.parent.name == "impact") player_controller_script.impact_build_correctness = 0;
            else if(item.transform.parent.name == "hold" ) player_controller_script.hold_build_correctness = 1;
            else if(item.transform.parent.name == "bait" ) player_controller_script.bait_build_correctness  = 0;
            else if(item.transform.parent.name == "mechanism" ) player_controller_script.mechanism_build_correctness = 0;
        }

        if(item.transform.name == "bait"){
            if(item.transform.parent.name == "impact") player_controller_script.impact_build_correctness = 0;
            else if(item.transform.parent.name == "hold" ) player_controller_script.hold_build_correctness = 0;
            else if(item.transform.parent.name == "bait" ) player_controller_script.bait_build_correctness  = 1;
            else if(item.transform.parent.name == "mechanism" ) player_controller_script.mechanism_build_correctness = 0;
        }

        if(item.transform.name == "mechanism"){
            if(item.transform.parent.name == "impact") player_controller_script.impact_build_correctness = 0;
            else if(item.transform.parent.name == "hold" ) player_controller_script.hold_build_correctness = 0;
            else if(item.transform.parent.name == "bait" ) player_controller_script.bait_build_correctness  = 0;
            else if(item.transform.parent.name == "mechanism" ) player_controller_script.mechanism_build_correctness = 1;
        }
    }

    void CheckCategorizeCorrectness(GameObject item){
        if(item.transform.name == "impact"){
            if(item.transform.parent.name == "impact") player_controller_script.impact_categorize_correctness = 1;
            else if(item.transform.parent.name == "hold" ) player_controller_script.hold_categorize_correctness = 0;
            else if(item.transform.parent.name == "bait" ) player_controller_script.bait_categorize_correctness  = 0;
            else if(item.transform.parent.name == "mechanism" ) player_controller_script.mechanism_categorize_correctness = 0;
        }

        if(item.transform.name == "hold"){
            if(item.transform.parent.name == "impact") player_controller_script.impact_categorize_correctness = 0;
            else if(item.transform.parent.name == "hold" ) player_controller_script.hold_categorize_correctness = 1;
            else if(item.transform.parent.name == "bait" ) player_controller_script.bait_categorize_correctness  = 0;
            else if(item.transform.parent.name == "mechanism" ) player_controller_script.mechanism_categorize_correctness = 0;
        }

        if(item.transform.name == "bait"){
            if(item.transform.parent.name == "impact") player_controller_script.impact_categorize_correctness = 0;
            else if(item.transform.parent.name == "hold" ) player_controller_script.hold_categorize_correctness = 0;
            else if(item.transform.parent.name == "bait" ) player_controller_script.bait_categorize_correctness  = 1;
            else if(item.transform.parent.name == "mechanism" ) player_controller_script.mechanism_categorize_correctness = 0;
        }

        if(item.transform.name == "mechanism"){
            if(item.transform.parent.name == "impact") player_controller_script.impact_categorize_correctness = 0;
            else if(item.transform.parent.name == "hold" ) player_controller_script.hold_categorize_correctness = 0;
            else if(item.transform.parent.name == "bait" ) player_controller_script.bait_categorize_correctness  = 0;
            else if(item.transform.parent.name == "mechanism" ) player_controller_script.mechanism_categorize_correctness = 1;
        }
    }
}
