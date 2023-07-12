using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ControlSettings : MonoBehaviour
{
    public float frameRate;
    public string m1; // string (m1_x or m1_y)
    public float mvcRange; // % MVC (uni-directional)
    public float mvcOffset; // % MVC (uni-directional)
    public float mvcRangeCombined; // % MVC (bi-directional DF and PF)
    public float mvcOffsetCombined; // % MVC (bi-directional DF and PF)
    public float romRange; // % ROM (uni-directional)
    public float romOffset; // % ROM (uni-directional)
    public float romRangeCombined; // % ROM (bi-directional DF and PF)
    public float romOffsetCombined; // % ROM (bi-directional DF and PF)
    public float primaryFrequency; // Hz
    public float secondaryFrequency; // Hz
    public float trialDuration; // seconds
    public float gain_e; // scale factor for torque
    public float gain_a; // scale factor for angle

    public GameObject trial;
    public GameObject stageText;
    public GameObject m1Text;

    void Start()
    {
        Application.targetFrameRate = (int) frameRate;
        QualitySettings.vSyncCount = 0;

        stageText.GetComponent<TextMeshProUGUI>().text = "0";
        m1Text.GetComponent<TextMeshProUGUI>().text = m1;

        // Initialize pre-trial arrows
        trial.SetActive(true);
    }
}