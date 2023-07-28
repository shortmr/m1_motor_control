using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeTask: MonoBehaviour
{
    public GameObject trackingAngleGroup;
    public GameObject matchingAngleGroup;
    public GameObject trackingEffortGroup;
    public GameObject matchingEffortGroup;
    public GameObject actualEffortControl;
    public GameObject actualAngleControl;
    public GameObject stageText;
    public GameObject controlStartButton;
    public GameObject performanceDisplay;
    public int task = 1;

    private string taskText;
    private TMP_Text text;
    private TMP_Text feedbackText;

    // Start is called before the first frame update
    void Start()
    {
        // Set current task (tracking: 1, matching: 2)
        task = 1;
        taskText = "Tracking";

        text = GetComponentInChildren<TMP_Text>();
        text.text = taskText;

        trackingAngleGroup.SetActive(true);
        matchingAngleGroup.SetActive(false);
        trackingEffortGroup.SetActive(true);
        matchingEffortGroup.SetActive(false);

        controlStartButton.GetComponent<StartControlTrial>().task = task;
    }

    public void TM()
    {
        // Check current task
        if (task == 1) {
            task = 2;
        } else if (task == 2) {
            task = 1;
        }

        if (task == 1) {
            taskText = "Tracking";
            trackingAngleGroup.SetActive(true);
            matchingAngleGroup.SetActive(false);
            trackingEffortGroup.SetActive(true);
            matchingEffortGroup.SetActive(false);
        } else if (task == 2) {
            taskText = "Matching";
            trackingAngleGroup.SetActive(false);
            matchingAngleGroup.SetActive(true);
            trackingEffortGroup.SetActive(false);
            matchingEffortGroup.SetActive(true);
        }
        text = GetComponentInChildren<TMP_Text>();
        text.text = taskText;
        controlStartButton.GetComponent<StartControlTrial>().task = task;

        stageText.GetComponent<TextMeshProUGUI>().text = "0";
        actualEffortControl.GetComponent<LineRenderer>().positionCount = 0;
        actualAngleControl.GetComponent<LineRenderer>().positionCount = 0;

        feedbackText = performanceDisplay.GetComponentInChildren<TMP_Text>();
        feedbackText.text = "";
    }
}