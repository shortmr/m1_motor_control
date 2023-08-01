using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeSine: MonoBehaviour
{
    public GameObject frequencySliders;
    public GameObject normalizedSliders;
    public GameObject speedSlider;
    public GameObject desiredEffortSine;
    public GameObject desiredAngleSine;
    public GameObject actualEffortControl;
    public GameObject actualAngleControl;

    private int sine;
    private string sineText;
    private TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        // Set type of sinusoidal waveform (frequencies: 1, normalized max power/speed: 2)
        sine = 1;
        desiredEffortSine.GetComponent<DesiredEffortSine>().sine = sine;
        desiredAngleSine.GetComponent<DesiredAngleSine>().sine = sine;
        sineText = "Frequency";

        text = GetComponentInChildren<TMP_Text>();
        text.text = sineText;

        frequencySliders.SetActive(true);
        normalizedSliders.SetActive(false);
    }

    public void FN()
    {
        // Check current sine
        if (sine == 1) {
            sine = 2;
        } else if (sine == 2) {
            sine = 1;
        }

        // redraw target
        actualEffortControl.GetComponent<LineRenderer>().positionCount = 0;
        actualAngleControl.GetComponent<LineRenderer>().positionCount = 0;
        desiredEffortSine.GetComponent<DesiredEffortSine>().sine = sine;
        desiredAngleSine.GetComponent<DesiredAngleSine>().sine = sine;

        if (sine == 1) {
            sineText = "Frequency";
            frequencySliders.SetActive(true);
            normalizedSliders.SetActive(false);
        } else if (sine == 2) {
            sineText = "Normalized";
            frequencySliders.SetActive(false);
            normalizedSliders.SetActive(true);
        }
        text = GetComponentInChildren<TMP_Text>();
        text.text = sineText;
    }
}
