using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// it goes on every stage, for plan it goes on every section of the stage
public class TutorialTextTrigger : MonoBehaviour
{
    public Tutorial tutorial_dialogue;

    public void TriggerTutorial()
    {
        FindObjectOfType<TutorialController>().StartTutorial(tutorial_dialogue);
    }
}
