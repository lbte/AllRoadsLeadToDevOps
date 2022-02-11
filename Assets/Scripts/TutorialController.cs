using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    public Text tutorial_text;
    public Animator animator;

    // for all the sentences we want to display
    private Queue<string> sentences = new Queue<string>();

    public void StartTutorial(Tutorial tutorial)
    {
        animator.SetBool("tutorialIsOpen", true);

        sentences.Clear();

        // loop through all the sentences in our dialogue
        foreach (string sentence in tutorial.sentences)
        {
            // placing them in a queue
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        // check if there are sentences in the queue
        if (sentences.Count == 0)
        {
            EndTutorial();
            return; // to return out of the rest of the function
        }

        // get the next sentence in the queue
        string sentence = sentences.Dequeue();
        StopAllCoroutines(); //in case any sentence is already being displayed
        StartCoroutine(TypeSentence(sentence));
    }

    // animate each character
    IEnumerator TypeSentence (string sentence)
    {
        tutorial_text.text = "";

        foreach(char letter in sentence.ToCharArray())
        {
            tutorial_text.text += letter;
            // to wait a single frame
            yield return null;
        }
    }

    void EndTutorial()
    {
        animator.SetBool("tutorialIsOpen", false);
    }
}
