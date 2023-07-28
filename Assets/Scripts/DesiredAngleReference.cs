using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesiredAngleReference : MonoBehaviour
{
    public GameObject actualAngle;
    public GameObject settings;
    public int type;
    public GameObject referenceLine;
    public GameObject zeroLine;

    private float gain;
    private float pt;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void SetFeedback() {
        gain = settings.GetComponent<ControlSettings>().gain_a;
        pt = settings.GetComponent<ControlSettings>().referencePoint;
        transform.localPosition = new Vector3(0f, gain * pt + 2f, 0f);

        actualAngle.GetComponent<ActualAngleReference>().ready = true;
        actualAngle.GetComponent<ActualAngleReference>().referencePoint = pt;
        zeroLine.SetActive(true);
        referenceLine.SetActive(false);
    }
}