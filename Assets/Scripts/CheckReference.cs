using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CheckReference : MonoBehaviour
{
    public GameObject valueText;
    public GameObject desiredEffortReference;
    public GameObject desiredAngleReference;
    public GameObject settings;

    private Slider referenceSlider;
    private float t_;
    private float maxAmount = 100f;
    private float minAmount = -100f;
    private int numberOfSteps = 0;
    private float stepAmount = 1f;
    private string displayText;

    void Start()
    {
        referenceSlider = GetComponent<Slider>();
        referenceSlider.maxValue = maxAmount;
        referenceSlider.minValue = minAmount;
        numberOfSteps = (int) ((referenceSlider.maxValue - referenceSlider.minValue)/ stepAmount);

        referenceSlider.value = 20f; // initialize at 20% ROM/MVC
        settings.GetComponent<ControlSettings>().referencePoint = referenceSlider.value;
        valueText.GetComponent<TextMeshProUGUI>().text = referenceSlider.value.ToString("0.");

        desiredEffortReference.GetComponent<DesiredEffortReference>().SetFeedback();
        desiredAngleReference.GetComponent<DesiredAngleReference>().SetFeedback();
    }

    public void ChangeValue()
    {
        float range = (referenceSlider.value / (referenceSlider.maxValue - referenceSlider.minValue)) * numberOfSteps;
        int ceil = Mathf.CeilToInt(range);

        referenceSlider.value = ceil * stepAmount;
        settings.GetComponent<ControlSettings>().referencePoint = referenceSlider.value;
        displayText = referenceSlider.value.ToString("0.");
        valueText.GetComponent<TextMeshProUGUI>().text = displayText;

        // Update targets
        desiredEffortReference.GetComponent<DesiredEffortReference>().SetFeedback();
        desiredAngleReference.GetComponent<DesiredAngleReference>().SetFeedback();
    }
}
