using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageController : MonoBehaviour
{
    public Button next_stage_button;
    public Text stage_title_text;

    public GameObject plan_stage;
    public GameObject code_stage;
    public GameObject build_stage;
    public GameObject test_stage;
    public GameObject release_stage;
    public GameObject deploy_stage;
    public GameObject operate_stage;
    public GameObject monitor_stage;

    // Start is called before the first frame update
    void Start()
    {
        next_stage_button.onClick.AddListener(NextStageButton);
        stage_title_text.text = "PLAN";
        plan_stage.SetActive(true);
        code_stage.SetActive(false);
        build_stage.SetActive(false);
        test_stage.SetActive(false);
        release_stage.SetActive(false);
        deploy_stage.SetActive(false);
        operate_stage.SetActive(false);
        monitor_stage.SetActive(false);
    }

    void DeactivatedStages()
    {
        plan_stage.SetActive(false);
        code_stage.SetActive(false);
        build_stage.SetActive(false);
        test_stage.SetActive(false);
        release_stage.SetActive(false);
        deploy_stage.SetActive(false);
        operate_stage.SetActive(false);
        monitor_stage.SetActive(false);
    }

    void NextStageButton()
    {
        if (stage_title_text.text == "PLAN")
        {
            stage_title_text.text = "CODE";
            DeactivatedStages();
            code_stage.SetActive(true);         
        }
        else if (stage_title_text.text == "CODE")
        {
            stage_title_text.text = "BUILD";
            DeactivatedStages();
            build_stage.SetActive(true);
        }
        else if (stage_title_text.text == "BUILD")
        {
            stage_title_text.text = "TEST";
            DeactivatedStages();
            test_stage.SetActive(true);
        }
        else if (stage_title_text.text == "TEST")
        {
            stage_title_text.text = "RELEASE";
            DeactivatedStages();
            release_stage.SetActive(true);
        }
        else if (stage_title_text.text == "RELEASE")
        {
            stage_title_text.text = "DEPLOY";
            DeactivatedStages();
            deploy_stage.SetActive(true);
        }
        else if (stage_title_text.text == "DEPLOY")
        {
            stage_title_text.text = "OPERATE";
            DeactivatedStages();
            operate_stage.SetActive(true);
        }
        else if (stage_title_text.text == "OPERATE")
        {
            stage_title_text.text = "MONITOR";
            DeactivatedStages();
            monitor_stage.SetActive(true);
        }
        else
        {
            Debug.Log("End of the stages.");
        }
    }
}
