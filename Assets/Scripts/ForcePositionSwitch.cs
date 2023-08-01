using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ForcePositionSwitch : MonoBehaviour
{
    public GameObject forceControl;
    public GameObject positionControl;
    public GameObject actualEffortControl;
    public GameObject actualAngleControl;
    public GameObject stageText;
    public GameObject startTrial;
    public GameObject performanceDisplay;
    public GameObject matchUnitText;
    public GameObject powerSlider;
    public GameObject speedSlider;

    private string modeText;
    private TMP_Text text;
    private TMP_Text feedbackText;

    private int mode;

    // Start is called before the first frame update
    void Start()
    {
        // Set mode (1) to force control on startup
        modeText = "Force";
        forceControl.SetActive(true);
        positionControl.SetActive(false);
        mode = 1;

        startTrial.GetComponent<StartControlTrial>().mode = mode;
        text = GetComponentInChildren<TMP_Text>();
        text.text = modeText;
        matchUnitText.GetComponent<TextMeshProUGUI>().text = "% MVC";
        powerSlider.SetActive(true);
        speedSlider.SetActive(false);
    }

    public void Switch()
    {
        stageText.GetComponent<TextMeshProUGUI>().text = "0";
        actualEffortControl.GetComponent<LineRenderer>().positionCount = 0;
        actualAngleControl.GetComponent<LineRenderer>().positionCount = 0;

        feedbackText = performanceDisplay.GetComponentInChildren<TMP_Text>();
        feedbackText.text = "";

        if (mode == 2) {
            mode = 1;
        } else if (mode == 1)  {
            mode = 2;
        }

        if (mode == 1) {
            modeText = "Force";
            forceControl.SetActive(true);
            positionControl.SetActive(false);
            matchUnitText.GetComponent<TextMeshProUGUI>().text = "% MVC";
            powerSlider.SetActive(true);
            speedSlider.SetActive(false);
        }
        else if (mode == 2)  {
            modeText = "Position";
            positionControl.SetActive(true);
            forceControl.SetActive(false);
            matchUnitText.GetComponent<TextMeshProUGUI>().text = "% ROM";
            powerSlider.SetActive(false);
            speedSlider.SetActive(true);
        }
        startTrial.GetComponent<StartControlTrial>().mode = mode;
        text = GetComponentInChildren<TMP_Text>();
        text.text = modeText;
    }
}

