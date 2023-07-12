using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesiredAngleControl : MonoBehaviour
{
    public GameObject settings;
    public float positionAngle;
    public GameObject desiredAngle;
    public GameObject controlStartButton;
    public bool ready;
    public bool begin;

    private float gain;
    private float trialDuration;
    private bool running;
    private float counter;
    private LineRenderer desiredAngleComponent;
    private Vector3[] angleVec;
    private bool pass;

    // Start is called before the first frame update
    void Start()
    {
        gain = settings.GetComponent<ControlSettings>().gain_a;
        trialDuration = settings.GetComponent<ControlSettings>().trialDuration;
        running = true;
        ready = false;
        pass = false;
        begin = false;
        counter = 0f;
    }

    void OnEnable() {
        running = true;
    }

    void OnDisable() {
        running = false;
        begin = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (running) {
            if (ready) {
                desiredAngleComponent = desiredAngle.GetComponent<LineRenderer>();
                angleVec = new Vector3[desiredAngleComponent.positionCount];
                desiredAngleComponent.GetPositions(angleVec);
                ready = false;
                pass = true;
                counter = 0f;
                float trialIndex = (float) desiredAngleComponent.positionCount * counter/trialDuration;
                transform.localPosition = angleVec[(int)trialIndex];
            }
            if (pass & begin) {
                counter += Time.deltaTime; //1f/(float) Application.targetFrameRate;
                float trialIndex = (float) desiredAngleComponent.positionCount * counter/trialDuration;
                if ((int) trialIndex >= desiredAngleComponent.positionCount) {
                    ready = true;
                    pass = false;
                    begin = false;
                    controlStartButton.GetComponent<StartControlTrial>().StartTrial();
                } else {
                    transform.localPosition = angleVec[(int)trialIndex];
                }
            }
        }
    }

}
