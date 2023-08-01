using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CheckNormalized : MonoBehaviour
{
    public GameObject valueText;
    public GameObject settings;
    public GameObject desiredEffortSine;
    public GameObject desiredAngleSine;
    public bool effortFlag;
    public float max_df;
    public float max_pf;

    private float stepAmount;
    private float rangeAmplitude = 1f;
    private float rangeScale = 100f;
    private Slider normSlider;
    private int numberOfSteps = 0;
    private string displayText;
    private bool startup = true;

    void Start() {
    }

    void OnEnable() {
        if (startup) {
            normSlider = GetComponent<Slider>();
            startup = false;
        }

        if (effortFlag) {
            normSlider.maxValue = 10f;
            stepAmount = 1f;
            numberOfSteps = (int) (normSlider.maxValue / stepAmount);
            normSlider.value = 5f;
        }
        else {
            normSlider.maxValue = 100f;
            stepAmount = 10f;
            numberOfSteps = (int) (normSlider.maxValue / stepAmount);
            normSlider.value = 10f;
        }
        valueText.GetComponent<TextMeshProUGUI>().text = normSlider.value.ToString("0.");
        ChangeFrequency();
    }

    void OnDisable() {
        // Update targets
        desiredEffortSine.GetComponent<DesiredEffortSine>().DrawFeedback(desiredEffortSine.GetComponent<LineRenderer>());
        desiredAngleSine.GetComponent<DesiredAngleSine>().DrawFeedback(desiredAngleSine.GetComponent<LineRenderer>());
    }

    public void ChangeFrequency()
    {
        if (!startup) {
            rangeAmplitude = 0.5f*(max_df + max_pf);
            if (effortFlag) {
                rangeScale = settings.GetComponent<ControlSettings>().mvcRangeCombined/100f;
            }
            else {
                rangeScale = settings.GetComponent<ControlSettings>().romRangeCombined/100f;
            }
            float range = (normSlider.value / normSlider.maxValue) * numberOfSteps;

            int ceil = Mathf.CeilToInt(range);
            normSlider.value = ceil * stepAmount;
            displayText = normSlider.value.ToString("0.");
            valueText.GetComponent<TextMeshProUGUI>().text = displayText;

            if (effortFlag) {
                // Calculate normalized frequency based on MVC
                if (Mathf.Abs(rangeAmplitude) <= 0.0001f) {
                    Debug.Log("Undefined normalized frequency");
                    settings.GetComponent<ControlSettings>().normalizedEffortFrequency = 0.1f;
                }
                else {
                    settings.GetComponent<ControlSettings>().normalizedEffortFrequency = normSlider.value/(rangeScale*rangeAmplitude*2f*Mathf.PI);
                }
            }
            else {
                // Calculate normalized frequency based on ROM
                if (Mathf.Abs(rangeAmplitude) <= 0.0001f) {
                    Debug.Log("Undefined normalized frequency");
                    settings.GetComponent<ControlSettings>().normalizedAngleFrequency = 0.1f;
                }
                else {
                    settings.GetComponent<ControlSettings>().normalizedAngleFrequency = normSlider.value/(rangeScale*rangeAmplitude*2f*Mathf.PI);
                }
            }

            // Update targets
            desiredEffortSine.GetComponent<DesiredEffortSine>().DrawFeedback(desiredEffortSine.GetComponent<LineRenderer>());
            desiredAngleSine.GetComponent<DesiredAngleSine>().DrawFeedback(desiredAngleSine.GetComponent<LineRenderer>());
        }
    }
}
