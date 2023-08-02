using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StartControlTrial : MonoBehaviour
{
    public GameObject desiredEffortControl;
    public GameObject actualEffortControl;
    public GameObject desiredAngleControl;
    public GameObject actualAngleControl;

    public GameObject actualEffortReference;
    public GameObject actualAngleReference;
    public GameObject controlTriggerButton;

    public GameObject logText;
    public int mode;
    public int task;
    public GameObject zeroEffortLine;
    public GameObject referenceEffortLine;
    public GameObject zeroAngleLine;
    public GameObject referenceAngleLine;

    private int start;
    private string startText;
    private TMP_Text text;


    // Start is called before the first frame update
    void Start()
    {
        // Initialize variables
        task = 1;
        start = 1;
        startText = "Start";
        GetComponent<Image>().color = new Color(255f*1f, 255f*0.89f, 255f*0f);
        text = GetComponentInChildren<TMP_Text>();
        text.text = startText;
        GetComponent<Button>().interactable = true;
        controlTriggerButton.SetActive(false);
    }

    public void StartTrial()
    {
        if (start == 1) {
            start = 0;
            startText = "...";
            GetComponent<Image>().color = new Color(255f*1f, 255f*0f, 255f*0f);
            GetComponent<Button>().interactable = false;
            if (mode == 1) {
                // Force Control
                if (task == 1) {
                    desiredEffortControl.GetComponent<DesiredEffortControl>().begin = true;
                    actualEffortControl.GetComponent<ActualEffortControl>().begin = true;
                }
                else if (task == 2) {
                    zeroEffortLine.SetActive(false);
                    referenceEffortLine.SetActive(true);
                    actualEffortReference.GetComponent<ActualEffortReference>().begin = true;
                    controlTriggerButton.SetActive(true);
                    controlTriggerButton.GetComponent<Button>().interactable = true;
                }
            }
            else if (mode == 2) {
                // Position Control
                if (task == 1) {
                    desiredAngleControl.GetComponent<DesiredAngleControl>().begin = true;
                    actualAngleControl.GetComponent<ActualAngleControl>().begin = true;
                }
                else if (task == 2) {
                    zeroAngleLine.SetActive(false);
                    referenceAngleLine.SetActive(true);
                    actualAngleReference.GetComponent<ActualAngleReference>().begin = true;
                    controlTriggerButton.SetActive(true);
                    controlTriggerButton.GetComponent<Button>().interactable = true;
                }
            }
            logText.GetComponent<TextMeshProUGUI>().text = "";
        }
        else if (start == 0)  {
            start = 1;
            startText = "Start";
            GetComponent<Image>().color = new Color(255f*1f, 255f*0.89f, 255f*0f);
            GetComponent<Button>().interactable = true;
            if (task == 2) {
                if (mode == 1) {
                    zeroEffortLine.SetActive(true);
                    referenceEffortLine.SetActive(false);
                }
                else if (mode == 2) {
                    zeroAngleLine.SetActive(true);
                    referenceAngleLine.SetActive(false);
                }
                controlTriggerButton.GetComponent<TriggerControl>().SendTrigger(true);
                controlTriggerButton.SetActive(false);
            }
        }
        text = GetComponentInChildren<TMP_Text>();
        text.text = startText;
    }
}
