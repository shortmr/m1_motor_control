using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TriggerControl : MonoBehaviour
{
    public GameObject actualEffortReference;
    public GameObject actualAngleReference;

    private int start;
    private string triggerText;
    private TMP_Text text;
    private int trigger;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize variables
        trigger = 0;
        triggerText = "Trigger";
        text = GetComponentInChildren<TMP_Text>();
        text.text = triggerText;
        GetComponent<Button>().interactable = true;
    }

    public void SendTrigger(bool reset)
    {
        if (reset) {
            trigger = 0;
            triggerText = "Trigger";
            GetComponent<Button>().interactable = true;
        }
        else {
            if (trigger == 0) {
                trigger = 1;
                triggerText = "...";
                GetComponent<Button>().interactable = false;
            }
            else if (trigger == 1)  {
                trigger = 0;
                triggerText = "Trigger";
                GetComponent<Button>().interactable = true;
            }
        }

        actualEffortReference.GetComponent<ActualEffortReference>().trigger = trigger;
        actualAngleReference.GetComponent<ActualAngleReference>().trigger = trigger;
        text = GetComponentInChildren<TMP_Text>();
        text.text = triggerText;
    }
}
