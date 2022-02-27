using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Selected architecture
    public Card selected_architecture = null;

    // Cards selected during the code phase
    public List<Card> selected_code_cards = new List<Card>();

    // Levels of the abilities
    public Dictionary<string, int> abilities_levels = new Dictionary<string, int>(){
        {"plan_level", 0}, {"code_level", 2}, {"build_level", 3}, {"test_level", 3},
        {"release_level", 3}, {"deploy_level", 0}, {"operate_level", 3}, {"monitor_level", 0}
    };

    // Tool cards selected
    public Dictionary<string, Card> tool_cards = new Dictionary<string, Card>(){
        {"plan", null}, {"code", null}, {"build", null}, {"test", null},
        {"release", null}, {"deploy", null}, {"operate", null}, {"monitor", null}
    };

    // Verify if the player can use a tool
    public bool can_use_plan_tool = false;
    public bool can_use_code_tool = false;
    public bool can_use_build_tool = false;
    public bool can_use_test_tool = false;
    public bool can_use_release_tool = false;
    public bool can_use_deploy_tool = false;
    public bool can_use_operate_tool = false;
    public bool can_use_monitor_tool = false;


    public int impact_categorize_correctness = 0;  // 0 -> Bad,  1 -> Good
    public int hold_categorize_correctness = 0;
    public int bait_categorize_correctness = 0;
    public int mechanism_categorize_correctness= 0;

    public int impact_build_correctness = 0;  // 0 -> Bad,  1 -> Good
    public int hold_build_correctness = 0;
    public int bait_build_correctness = 0;
    public int mechanism_build_correctness= 0;

    //reset bulid choices
    public bool build_categorize = false;

}
