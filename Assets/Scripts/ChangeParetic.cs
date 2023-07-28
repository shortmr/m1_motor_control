using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeParetic: MonoBehaviour
{
    public GameObject deviceButton;
    public GameObject JointStatesR;
    public GameObject JointStatesL;
    public GameObject actualAngle;
    public GameObject actualEffort;
    public int paretic = 1;

    private string pareticText;
    private TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        // Set which device is paretic leg (R: 1, L: 2)
        paretic = 1;
        pareticText = "Paretic: " + JointStatesR.GetComponent<JointSubscriber>().m1_device;

        text = GetComponentInChildren<TMP_Text>();
        text.text = pareticText;

        // Check if paretic limb matches device
        if (deviceButton.GetComponent<ChangeDevice>().device == paretic) {
            actualAngle.GetComponent<ActualAngleReference>().pareticDevice = true;
            actualEffort.GetComponent<ActualEffortReference>().pareticDevice = true;
        }
        else
        {
            actualAngle.GetComponent<ActualAngleReference>().pareticDevice = false;
            actualEffort.GetComponent<ActualEffortReference>().pareticDevice = false;
        }
    }

    public void RL()
    {
        // Check current paretic limb
        if (paretic == 1) {
            paretic = 2;
        } else if (paretic == 2) {
            paretic = 1;
        }

        if (paretic == 1) {
            pareticText = "Paretic: " + JointStatesR.GetComponent<JointSubscriber>().m1_device;
        } else if (paretic == 2) {
            pareticText = "Paretic: " + JointStatesL.GetComponent<JointSubscriber>().m1_device;
        }
        text = GetComponentInChildren<TMP_Text>();
        text.text = pareticText;

        // Check if paretic limb matches device
        if (deviceButton.GetComponent<ChangeDevice>().device == paretic) {
            actualAngle.GetComponent<ActualAngleReference>().pareticDevice = true;
            actualEffort.GetComponent<ActualEffortReference>().pareticDevice = true;
        } else {
            actualAngle.GetComponent<ActualAngleReference>().pareticDevice = false;
            actualEffort.GetComponent<ActualEffortReference>().pareticDevice = false;
        }
    }
}
