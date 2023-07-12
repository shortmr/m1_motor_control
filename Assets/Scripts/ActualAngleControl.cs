using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityEngine;
using System.Linq;
using TMPro;
using UnityEngine.UI; // Required when Using UI elements.

public class ActualAngleControl : MonoBehaviour
{
    public GameObject settings;
    public GameObject jointState;
    public GameObject performanceDisplay;
    public GameObject dataField;
    public GameObject stageText;
    public GameObject logText;

    public float positionAngle_f;
    public GameObject desiredAngle;
    public bool ready;
    public bool begin;
    public int type;

    private GameObject data;
    private TMP_Text feedbackText;
    private float gain;
    private float trialIndex = 0f;
    private List<float> counterList = new List<float>();
    private List<float> actualAngleList = new List<float>();
    private List<float> desiredAngleList = new List<float>();
    private int screenWidth = 25;

    private bool pass;

    private float trialDuration;
    private bool running;
    private float counter;
    private LineRenderer desiredAngleComponent;
    private Vector3[] angleVec;
    private float frameRate;

    // Start is called before the first frame update
    void Start()
    {
        data = jointState;
        gain = settings.GetComponent<ControlSettings>().gain_a;
        trialDuration = settings.GetComponent<ControlSettings>().trialDuration;
        frameRate = settings.GetComponent<ControlSettings>().frameRate;

        running = true;
        ready = false;
        pass = false;
        begin = false;
        type = 1;
        counter = 0f;

        angleVec = new Vector3[1];
        angleVec[0] = new Vector3(0f,0f,0f);

        feedbackText = performanceDisplay.GetComponentInChildren<TMP_Text>();
        feedbackText.text = "";

        desiredAngleComponent = desiredAngle.GetComponent<LineRenderer>();
        angleVec = new Vector3[desiredAngleComponent.positionCount];
        desiredAngleComponent.GetPositions(angleVec);
        ready = false;
        pass = true;
        counter = 0f;
        trialIndex = (float) desiredAngleComponent.positionCount * counter/trialDuration;
        counterList.Clear();
        actualAngleList.Clear();
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
        positionAngle_f = data.GetComponent<JointSubscriber>().q;

        if (running) {
            if (ready) {
                desiredAngleComponent = desiredAngle.GetComponent<LineRenderer>();
                angleVec = new Vector3[desiredAngleComponent.positionCount];
                desiredAngleComponent.GetPositions(angleVec);
                ready = false;
                pass = true;
                counter = 0f;
                trialIndex = (float) desiredAngleComponent.positionCount * counter/trialDuration;
                counterList.Clear();
                actualAngleList.Clear();
                desiredAngleList.Clear();
            }
            if (pass & begin) {
                GetComponent<LineRenderer>().positionCount = 0;
                counter += Time.deltaTime; //1f/(float) Application.targetFrameRate;
                trialIndex = (float) desiredAngleComponent.positionCount * counter/trialDuration;

                if ((int) trialIndex >= desiredAngleComponent.positionCount-1) {
                    ready = true;
                    pass = false;
                    begin = false;
                    trialIndex = 0f;
                    DrawFeedback(GetComponent<LineRenderer>());
                    int stageCount;
                    int.TryParse(stageText.GetComponent<TextMeshProUGUI>().text, out stageCount);
                    stageCount += 1;
                    stageText.GetComponent<TextMeshProUGUI>().text = stageCount.ToString();
                } else {
                    counterList.Add(counter);
                    actualAngleList.Add(positionAngle_f);
                    desiredAngleList.Add(angleVec[(int)trialIndex].y);
                }
            }
            transform.localPosition = new Vector3(angleVec[(int)trialIndex].x, 2*100f*gain*positionAngle_f+2f, 0.0f); // angle from -0.5 to 0.5
        }
    }

    public void DrawFeedback(LineRenderer renderer) {
        renderer.positionCount = (int) (actualAngleList.Count);
        float total_err = 0f; // compute total error for rmse
        float total_ang = 0f;
        float avg_ang = actualAngleList.Average();
        var sb = new StringBuilder("Time,Desired,Actual");
        for (int i = 0; i < renderer.positionCount; i++)
        {
            float time = counterList[i];
            float x = screenWidth * ((float) i / (float) renderer.positionCount - 0.5f);
            float y = actualAngleList[i];
            float target = desiredAngleList[i];

            total_err += Mathf.Pow(y-target, 2f);
            total_ang += Mathf.Pow(y-avg_ang, 2f);
            renderer.SetPosition(i, new Vector3(x, 2*100f*gain*y+2f, 0f));

            sb.Append('\n').Append(time.ToString()).Append(',').Append(target.ToString()).Append(',').Append(y.ToString());
        }
        float rmse = Mathf.Sqrt(total_err/(float) renderer.positionCount);
        float sd = Mathf.Sqrt(total_ang/(float) renderer.positionCount); // TODO: filter out movement frequency (Lodha, 2022) or use SPARC
        // update text feedback
        feedbackText.text = "Accuracy = " + rmse.ToString("0.00") + "\nSteadiness = " + sd.ToString("0.00"); // TODO: normalize score values to non-paretic limb
        SaveToCSV(sb.ToString());
    }


    public void SaveToCSV (string data)
    {
        // Specify target file path
        var folder = Application.persistentDataPath;
        int trialNumber;
        int.TryParse(stageText.GetComponent<TextMeshProUGUI>().text, out trialNumber);
        string dataText = dataField.GetComponent<InputField>().text;
        string trialType = "";
        if (type == 1) {
            trialType = "df";
        } else if (type == 2) {
            trialType = "pf";
        } else if (type == 3) {
            trialType = "dfpf";
        }
        var filePath = Path.Combine(folder, dataText + "_position_" + trialType + "_" + trialNumber.ToString("00") + ".csv");

        using(var writer = new StreamWriter(filePath, false))
        {
            writer.Write(data);
        }

        Debug.Log($"CSV file written to \"{filePath}\"");
        logText.GetComponent<TextMeshProUGUI>().text = $"CSV file written to \"{filePath}\"";
    }
}
