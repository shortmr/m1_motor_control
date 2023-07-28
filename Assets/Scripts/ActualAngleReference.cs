using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityEngine;
using System.Linq;
using TMPro;
using UnityEngine.UI; // Required when Using UI elements.

public class ActualAngleReference : MonoBehaviour
{
    public GameObject settings;
    public GameObject jointState;
    public GameObject jointStateMatch;
    public GameObject dataField;
    public GameObject stageText;
    public GameObject logText;
    public GameObject controlStartButton;
    public string deviceLR;
    public bool pareticDevice;

    public float positionAngle_f;
    public float positionAngle_f_m;
    public GameObject desiredAngle;
    public bool ready;
    public bool begin;

    public float referencePoint;
    public int trigger;

    public float max_df;
    public float max_pf;
    public float max_df_m;
    public float max_pf_m;

    private float gain;
    private float trialIndex = 0f;
    private List<float> counterList = new List<float>();
    private List<float> triggerList = new List<float>();
    private List<float> actualAngleList = new List<float>();
    private List<float> actualAngleMatchList = new List<float>();
    private List<float> desiredAngleList = new List<float>();

    private bool pass;

    private float trialDuration;
    private float referenceDuration;
    private bool running;
    private float counter;
    private float frameRate;

    // Start is called before the first frame update
    void Start()
    {
        gain = settings.GetComponent<ControlSettings>().gain_a;
        referenceDuration = settings.GetComponent<ControlSettings>().referenceDuration;
        frameRate = settings.GetComponent<ControlSettings>().frameRate;

        running = true;
        ready = false;
        pass = false;
        begin = false;
        counter = 0f;
        trigger = 0;

        ready = false;
        pass = true;
        counter = 0f;
        trialIndex = counter/referenceDuration;
        counterList.Clear();
        triggerList.Clear();
        actualAngleList.Clear();
        actualAngleMatchList.Clear();
        desiredAngleList.Clear();
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
        // get filtered position from subscriber
        positionAngle_f = jointState.GetComponent<JointSubscriber>().q;
        positionAngle_f_m = jointStateMatch.GetComponent<JointSubscriber>().q;

        float pA = positionAngle_f;
        float pA_m = positionAngle_f_m;

        // rescale based on paretic limb
        if (pareticDevice) {
            pA_m = 0.5f*positionAngle_f_m*(max_df_m - max_pf_m)+0.5f*(max_df_m + max_pf_m);
            pA_m = 2f*(pA_m - 0.5f*(max_df + max_pf))/(max_df - max_pf);
        }
        else {
            pA = 0.5f*positionAngle_f*(max_df - max_pf)+0.5f*(max_df + max_pf);
            pA = 2f*(pA - 0.5f*(max_df_m + max_pf_m))/(max_df_m - max_pf_m);
        }

        if (running) {
            if (ready) {
                ready = false;
                pass = true;
                counter = 0f;
                trialIndex = counter/referenceDuration;
                counterList.Clear();
                triggerList.Clear();
                actualAngleList.Clear();
                actualAngleMatchList.Clear();
                desiredAngleList.Clear();
            }
            if (pass & begin) {
                counter += Time.deltaTime; //1f/(float) Application.targetFrameRate;
                trialIndex = counter/referenceDuration;

                if (trialIndex > 1f) {
                    ready = true;
                    pass = false;
                    begin = false;
                    controlStartButton.GetComponent<StartControlTrial>().StartTrial();
                    trialIndex = 0f;
                    WriteFeedback();
                    int stageCount;
                    int.TryParse(stageText.GetComponent<TextMeshProUGUI>().text, out stageCount);
                    stageCount += 1;
                    stageText.GetComponent<TextMeshProUGUI>().text = stageCount.ToString();
                } else {
                    counterList.Add(counter);
                    triggerList.Add(trigger);
                    actualAngleList.Add(pA);
                    actualAngleMatchList.Add(pA_m);
                    desiredAngleList.Add(referencePoint/100f);
                }
            }
            transform.localPosition = new Vector3(0f, 100f*gain*pA+2f, 0.0f); // angle from -1 to 1
        }
    }

    public void WriteFeedback() {
        int positionCount = (int) (actualAngleList.Count);
        float avg_ang = actualAngleList.Average();

        // prepare csv string
        var sb = new StringBuilder("Time,Desired,Actual,DF,PF,ActualM,DFM,PFM,Trigger");

        for (int i = 0; i < positionCount; i++)
        {
            float time = counterList[i];
            float y = actualAngleList[i];
            float target = desiredAngleList[i];
            float trig = triggerList[i];
            float ym = actualAngleMatchList[i];
            sb.Append('\n').Append(time.ToString()).Append(',').Append(target.ToString()).Append(',').Append(y.ToString()).Append(',').Append(max_df.ToString()).Append(',').Append(max_pf.ToString()).Append(',').Append(ym.ToString()).Append(',').Append(max_df_m.ToString()).Append(',').Append(max_pf_m.ToString()).Append(',').Append(trig.ToString());
        }

        SaveToCSV(sb.ToString());
    }


    public void SaveToCSV (string str)
    {
        // Specify target file path
        var folder = Application.persistentDataPath;
        int trialNumber;
        int.TryParse(stageText.GetComponent<TextMeshProUGUI>().text, out trialNumber);
        string dataText = dataField.GetComponent<InputField>().text;
        var filePath = Path.Combine(folder, dataText + "_" + deviceLR + "_position_match_" + trialNumber.ToString("00") + ".csv");

        using(var writer = new StreamWriter(filePath, false))
        {
            writer.Write(str);
        }

        Debug.Log($"CSV file written to \"{filePath}\"");
        logText.GetComponent<TextMeshProUGUI>().text = $"CSV file written to \"{filePath}\"";
    }
}
