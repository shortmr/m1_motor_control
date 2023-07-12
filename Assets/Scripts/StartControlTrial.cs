using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StartControlTrial : MonoBehaviour
{
    public GameObject targetEffort;
    public GameObject actualEffort;
    public GameObject targetAngle;
    public GameObject actualAngle;
    public GameObject logText;
    public int mode;

    private int start;
    private string startText;
    private TMP_Text text;


    // Start is called before the first frame update
    void Start()
    {
        // TODO: Set mode to force control on startup
        start = 1;
        startText = "Start";
        GetComponent<Image>().color = new Color(255f*1f, 255f*0.89f, 255f*0f);
        text = GetComponentInChildren<TMP_Text>();
        text.text = startText;
    }

    public void StartTrial()
    {
        if (start == 1) {
            start = 0;
            startText = "...";
            GetComponent<Image>().color = new Color(255f*1f, 255f*0f, 255f*0f);
            if (mode == 1) {
                // Force Control
                targetEffort.GetComponent<DesiredEffortControl>().begin = true;
                actualEffort.GetComponent<ActualEffortControl>().begin = true;
            }
            else if (mode == 2) {
                // Position Control
                targetAngle.GetComponent<DesiredAngleControl>().begin = true;
                actualAngle.GetComponent<ActualAngleControl>().begin = true;
            }
            logText.GetComponent<TextMeshProUGUI>().text = "";
        }
        else if (start == 0)  {
            start = 1;
            startText = "Start";
            GetComponent<Image>().color = new Color(255f*1f, 255f*0.89f, 255f*0f);
        }
        text = GetComponentInChildren<TMP_Text>();
        text.text = startText;
    }
}
