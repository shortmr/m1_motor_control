using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using JointState = RosMessageTypes.Geometry.Point32Msg;

public class JointSubscriber : MonoBehaviour
{
    public GameObject settings;
    public string topic;
    public float tau_s;
    public float q;

    private string m1;

    void Start() {
        m1 = settings.GetComponent<ControlSettings>().m1;
        ROSConnection.GetOrCreateInstance().Subscribe<JointState>(m1 + "/" + topic + "/", StreamData);
    }

    void StreamData(JointState d) {
//         tau_s = (float)d.effort[0]; // scaled interaction torque
//         q = (float)d.position[0]; // scaled M1 angle
        tau_s = (float)d.x; // scaled interaction torque
        q = (float)d.y; // scaled M1 angle
    }

    private void Update()
    {

    }
}
