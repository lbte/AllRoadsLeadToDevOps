using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Card
{   
    public Sprite card_image;
    public string card_description;
    public bool selected = false;
    public string category = "";
    public int level = 0;
    public string id = "";
    public string card_title = "";
}
