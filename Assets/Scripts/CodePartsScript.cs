using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodePartsScript : MonoBehaviour
{

    public GameObject CodePanel;
    public bool active;

    void Awake()
    {
        CodePanel.SetActive(false);
        active = false;
    }

    public void TaskOnClick()
    {
        active = !active;
    }

    void Update()
    {
        if (active)
        {
            CodePanel.SetActive(true);
        }
        else
        {
            CodePanel.SetActive(false);
        }
    }


}
