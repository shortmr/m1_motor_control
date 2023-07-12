using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ForcePositionSwitch : MonoBehaviour
{
    public GameObject forceControl;
    public GameObject positionControl;
    public GameObject startTrial;
    public GameObject stageText;
    public GameObject performanceDisplay;

    private string modeText;
    private TMP_Text text;
    private TMP_Text feedbackText;

    private int mode;

    // Start is called before the first frame update
    void Start()
    {
        // Set mode (1) to force control on startup
        modeText = "Force Control";
        forceControl.SetActive(true);
        positionControl.SetActive(false);
        mode = 1;

        startTrial.GetComponent<StartControlTrial>().mode = mode;
        text = GetComponentInChildren<TMP_Text>();
        text.text = modeText;
    }

    public void Switch()
    {
        stageText.GetComponent<TextMeshProUGUI>().text = "0";
        feedbackText = performanceDisplay.GetComponentInChildren<TMP_Text>();
        feedbackText.text = "";

        stageText.GetComponent<TextMeshProUGUI>().text = "0";
        if (mode == 2) {
            mode = 1;
            modeText = "Force Control";
            forceControl.SetActive(true);
            positionControl.SetActive(false);
        }
        else if (mode == 1)  {
            mode = 2;
            modeText = "Position Control";
            positionControl.SetActive(true);
            forceControl.SetActive(false);
        }
        startTrial.GetComponent<StartControlTrial>().mode = mode;
        text = GetComponentInChildren<TMP_Text>();
        text.text = modeText;
    }
}

