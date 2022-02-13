using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Card selected_architecture;  
    public List<Card> selected_code_cards = new List<Card>();
    public Dictionary<string, int> abilities_levels = new Dictionary<string, int>(){
        {"plan_level", 0}, {"code_level", 0}, {"build_level", 0}, {"test_level", 0},
        {"release_level", 0}, {"deploy_level", 0}, {"operate_level", 0}, {"monitor_level", 0}
    };

    /* public int plan_level = 0;
    public int code_level = 0;
    public int build_level = 0;
    public int test_level = 0;
    public int release_level = 0;
    public int deploy_level = 0;
    public int operate_level = 0;
    public int monitor_level = 0; */
}
