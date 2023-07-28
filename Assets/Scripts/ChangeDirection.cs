using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeDirection : MonoBehaviour
{
    public GameObject desiredEffortSine;
    public GameObject desiredAngleSine;
    public GameObject actualEffortControl;
    public GameObject actualAngleControl;
    public GameObject stageText;
    public GameObject trialCombined;
    public GameObject trial;
    public GameObject performanceDisplay;
    public int direction;

    private string directionText;
    private TMP_Text text;
    private float angleUp = 0f;
    private float angleDown = 180f;
    private TMP_Text feedbackText;

    // Start is called before the first frame update
    void Start()
    {
        // Set direction to dorsiflexion on start up (up: 1, down: 2, combined: 3)
        direction = 3;
        directionText = "DF/PF";

        text = GetComponentInChildren<TMP_Text>();
        text.text = directionText;

        trialCombined.SetActive(true);
    }

    public void Flip()
    {
        // Check current direction
        if (direction == 1) {
            direction = 2;
        } else if (direction == 2) {
            direction = 3;
        } else if (direction == 3) {
            direction = 1;
        }
         if (direction == 1) {
            directionText = "DF";
            desiredEffortSine.GetComponent<DesiredEffortSine>().type = 1;
            desiredEffortSine.GetComponent<DesiredEffortSine>().DrawFeedback(desiredEffortSine.GetComponent<LineRenderer>());
            actualEffortControl.GetComponent<ActualEffortControl>().type = 1;
            desiredAngleSine.GetComponent<DesiredAngleSine>().type = 1;
            desiredAngleSine.GetComponent<DesiredAngleSine>().DrawFeedback(desiredAngleSine.GetComponent<LineRenderer>());
            actualAngleControl.GetComponent<ActualAngleControl>().type = 1;
            trial.GetComponent<Transform>().localRotation = Quaternion.Euler(new Vector3(0f,0f,angleUp));
            trialCombined.SetActive(false);
        } else if (direction == 2) {
            directionText = "PF";
            desiredEffortSine.GetComponent<DesiredEffortSine>().type = 2;
            desiredEffortSine.GetComponent<DesiredEffortSine>().DrawFeedback(desiredEffortSine.GetComponent<LineRenderer>());
            actualEffortControl.GetComponent<ActualEffortControl>().type = 2;
            desiredAngleSine.GetComponent<DesiredAngleSine>().type = 2;
            desiredAngleSine.GetComponent<DesiredAngleSine>().DrawFeedback(desiredAngleSine.GetComponent<LineRenderer>());
            actualAngleControl.GetComponent<ActualAngleControl>().type = 2;
            trial.GetComponent<Transform>().localRotation = Quaternion.Euler(new Vector3(0f,0f,angleDown));
            trialCombined.SetActive(false);
        } else if (direction == 3) {
            directionText = "DF/PF";
            desiredEffortSine.GetComponent<DesiredEffortSine>().type = 3;
            desiredEffortSine.GetComponent<DesiredEffortSine>().DrawFeedback(desiredEffortSine.GetComponent<LineRenderer>());
            actualEffortControl.GetComponent<ActualEffortControl>().type = 3;
            desiredAngleSine.GetComponent<DesiredAngleSine>().type = 3;
            desiredAngleSine.GetComponent<DesiredAngleSine>().DrawFeedback(desiredAngleSine.GetComponent<LineRenderer>());
            actualAngleControl.GetComponent<ActualAngleControl>().type = 3;
            trial.GetComponent<Transform>().localRotation = Quaternion.Euler(new Vector3(0f,0f,angleUp));
            trialCombined.SetActive(true);
        }

        stageText.GetComponent<TextMeshProUGUI>().text = "0";
        actualEffortControl.GetComponent<LineRenderer>().positionCount = 0;
        actualAngleControl.GetComponent<LineRenderer>().positionCount = 0;

        feedbackText = performanceDisplay.GetComponentInChildren<TMP_Text>();
        feedbackText.text = "";

        text = GetComponentInChildren<TMP_Text>();
        text.text = directionText;
    }
}
