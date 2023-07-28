using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeDevice: MonoBehaviour
{
    public GameObject actualEffortControl;
    public GameObject actualAngleControl;
    public GameObject actualEffortReference;
    public GameObject actualAngleReference;
    public GameObject JointStatesR;
    public GameObject JointStatesL;
    public GameObject pareticButton;
    public GameObject stageText;
    public GameObject m1Text;
    public GameObject performanceDisplay;
    public int device = 1;

    private string deviceText;
    private TMP_Text text;
    private TMP_Text feedbackText;

    // Start is called before the first frame update
    void Start()
    {
        // Set device for reference leg (R: 1, L: 2)
        device = 1;
        deviceText = JointStatesR.GetComponent<JointSubscriber>().m1_device;

        text = GetComponentInChildren<TMP_Text>();
        text.text = deviceText;
        m1Text.GetComponent<TextMeshProUGUI>().text = deviceText;

        actualEffortControl.GetComponent<ActualEffortControl>().deviceLR = "R";
        actualAngleControl.GetComponent<ActualAngleControl>().deviceLR = "R";

        actualEffortReference.GetComponent<ActualEffortReference>().deviceLR = "R";
        actualEffortReference.GetComponent<ActualEffortReference>().max_df = JointStatesR.GetComponent<JointSubscriber>().tau_df;
        actualEffortReference.GetComponent<ActualEffortReference>().max_pf = JointStatesR.GetComponent<JointSubscriber>().tau_pf;
        actualEffortReference.GetComponent<ActualEffortReference>().max_df_m = JointStatesL.GetComponent<JointSubscriber>().tau_df;
        actualEffortReference.GetComponent<ActualEffortReference>().max_pf_m = JointStatesL.GetComponent<JointSubscriber>().tau_pf;

        actualAngleReference.GetComponent<ActualAngleReference>().deviceLR = "R";
        actualAngleReference.GetComponent<ActualAngleReference>().max_df = JointStatesR.GetComponent<JointSubscriber>().q_df;
        actualAngleReference.GetComponent<ActualAngleReference>().max_pf = JointStatesR.GetComponent<JointSubscriber>().q_pf;
        actualAngleReference.GetComponent<ActualAngleReference>().max_df_m = JointStatesL.GetComponent<JointSubscriber>().q_df;
        actualAngleReference.GetComponent<ActualAngleReference>().max_pf_m = JointStatesL.GetComponent<JointSubscriber>().q_pf;

        // Check if paretic limb matches device
        if (pareticButton.GetComponent<ChangeParetic>().paretic == device) {
            actualAngleReference.GetComponent<ActualAngleReference>().pareticDevice = true;
            actualEffortReference.GetComponent<ActualEffortReference>().pareticDevice = true;
        }
        else
        {
            actualAngleReference.GetComponent<ActualAngleReference>().pareticDevice = false;
            actualEffortReference.GetComponent<ActualEffortReference>().pareticDevice = false;
        }
    }

    public void AB()
    {
        // Check current device
        if (device == 1) {
            device = 2;
        } else if (device == 2) {
            device = 1;
        }

        // Update matching and tracking joint states and parameters
        if (device == 1) {
            deviceText = JointStatesR.GetComponent<JointSubscriber>().m1_device;
            actualEffortControl.GetComponent<ActualEffortControl>().jointState = JointStatesR;
            actualEffortControl.GetComponent<ActualEffortControl>().deviceLR = "R";
            actualAngleControl.GetComponent<ActualAngleControl>().jointState = JointStatesR;
            actualAngleControl.GetComponent<ActualAngleControl>().deviceLR = "R";

            actualEffortReference.GetComponent<ActualEffortReference>().deviceLR = "R";
            actualEffortReference.GetComponent<ActualEffortReference>().jointState = JointStatesR;
            actualEffortReference.GetComponent<ActualEffortReference>().jointStateMatch = JointStatesL;
            actualEffortReference.GetComponent<ActualEffortReference>().max_df = JointStatesR.GetComponent<JointSubscriber>().tau_df;
            actualEffortReference.GetComponent<ActualEffortReference>().max_pf = JointStatesR.GetComponent<JointSubscriber>().tau_pf;
            actualEffortReference.GetComponent<ActualEffortReference>().max_df_m = JointStatesL.GetComponent<JointSubscriber>().tau_df;
            actualEffortReference.GetComponent<ActualEffortReference>().max_pf_m = JointStatesL.GetComponent<JointSubscriber>().tau_pf;

            actualAngleReference.GetComponent<ActualAngleReference>().deviceLR = "R";
            actualAngleReference.GetComponent<ActualAngleReference>().jointState = JointStatesR;
            actualAngleReference.GetComponent<ActualAngleReference>().jointStateMatch = JointStatesL;
            actualAngleReference.GetComponent<ActualAngleReference>().max_df = JointStatesR.GetComponent<JointSubscriber>().q_df;
            actualAngleReference.GetComponent<ActualAngleReference>().max_pf = JointStatesR.GetComponent<JointSubscriber>().q_pf;
            actualAngleReference.GetComponent<ActualAngleReference>().max_df_m = JointStatesL.GetComponent<JointSubscriber>().q_df;
            actualAngleReference.GetComponent<ActualAngleReference>().max_pf_m = JointStatesL.GetComponent<JointSubscriber>().q_pf;
        }
        else if (device == 2) {
            deviceText = JointStatesL.GetComponent<JointSubscriber>().m1_device;
            actualEffortControl.GetComponent<ActualEffortControl>().jointState = JointStatesL;
            actualEffortControl.GetComponent<ActualEffortControl>().deviceLR = "L";
            actualAngleControl.GetComponent<ActualAngleControl>().jointState = JointStatesL;
            actualAngleControl.GetComponent<ActualAngleControl>().deviceLR = "L";

            actualEffortReference.GetComponent<ActualEffortReference>().deviceLR = "L";
            actualEffortReference.GetComponent<ActualEffortReference>().jointState = JointStatesL;
            actualEffortReference.GetComponent<ActualEffortReference>().jointStateMatch = JointStatesR;
            actualEffortReference.GetComponent<ActualEffortReference>().max_df = JointStatesL.GetComponent<JointSubscriber>().tau_df;
            actualEffortReference.GetComponent<ActualEffortReference>().max_pf = JointStatesL.GetComponent<JointSubscriber>().tau_pf;
            actualEffortReference.GetComponent<ActualEffortReference>().max_df_m = JointStatesR.GetComponent<JointSubscriber>().tau_df;
            actualEffortReference.GetComponent<ActualEffortReference>().max_pf_m = JointStatesR.GetComponent<JointSubscriber>().tau_pf;

            actualAngleReference.GetComponent<ActualAngleReference>().deviceLR = "L";
            actualAngleReference.GetComponent<ActualAngleReference>().jointState = JointStatesL;
            actualAngleReference.GetComponent<ActualAngleReference>().jointStateMatch = JointStatesR;
            actualAngleReference.GetComponent<ActualAngleReference>().max_df = JointStatesL.GetComponent<JointSubscriber>().q_df;
            actualAngleReference.GetComponent<ActualAngleReference>().max_pf = JointStatesL.GetComponent<JointSubscriber>().q_pf;
            actualAngleReference.GetComponent<ActualAngleReference>().max_df_m = JointStatesR.GetComponent<JointSubscriber>().q_df;
            actualAngleReference.GetComponent<ActualAngleReference>().max_pf_m = JointStatesR.GetComponent<JointSubscriber>().q_pf;
        }

        stageText.GetComponent<TextMeshProUGUI>().text = "0";
        actualEffortControl.GetComponent<LineRenderer>().positionCount = 0;
        actualAngleControl.GetComponent<LineRenderer>().positionCount = 0;

        feedbackText = performanceDisplay.GetComponentInChildren<TMP_Text>();
        feedbackText.text = "";

        text = GetComponentInChildren<TMP_Text>();
        text.text = deviceText;
        m1Text.GetComponent<TextMeshProUGUI>().text = deviceText;

        // Check if paretic limb matches device
        if (pareticButton.GetComponent<ChangeParetic>().paretic == device) {
            actualAngleReference.GetComponent<ActualAngleReference>().pareticDevice = true;
            actualEffortReference.GetComponent<ActualEffortReference>().pareticDevice = true;
        } else {
            actualAngleReference.GetComponent<ActualAngleReference>().pareticDevice = false;
            actualEffortReference.GetComponent<ActualEffortReference>().pareticDevice = false;
        }
    }
}

