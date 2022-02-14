using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Selected architecture
    public Card selected_architecture;  

    // Cards selected during the code phase
    public List<Card> selected_code_cards = new List<Card>();

    // Levels of the abilities
    public Dictionary<string, int> abilities_levels = new Dictionary<string, int>(){
        {"plan_level", 0}, {"code_level", 0}, {"build_level", 0}, {"test_level", 0},
        {"release_level", 0}, {"deploy_level", 0}, {"operate_level", 0}, {"monitor_level", 0}
    };

    // Tool cards selected
    public Dictionary<string, Card> tool_cards = new Dictionary<string, Card>(){
        {"plan", null}, {"code", null}, {"build", null}, {"test", null},
        {"release", null}, {"deploy", null}, {"operate", null}, {"monitor", null}
    };
}
