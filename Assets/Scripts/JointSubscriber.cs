using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using JointScaled = RosMessageTypes.CORC.JointScaled32Msg;

public class JointSubscriber : MonoBehaviour
{
    public string m1_device;
    public string topic;
    public float tau_s;
    public float q;
    public float tau_df;
    public float tau_pf;
    public float q_df;
    public float q_pf;

    void Start() {
        ROSConnection.GetOrCreateInstance().Subscribe<JointScaled>(m1_device + "/" + topic + "/", StreamData);
    }

    void StreamData(JointScaled d) {
        tau_s = (float)d.tau_s; // scaled interaction torque
        q = (float)d.q; // scaled M1 angle
        tau_df = (float)d.tau_df; // maximum torque in dorsiflexion (Nm)
        tau_pf = (float)d.tau_pf; // maximum torque in plantarflexion (Nm)
        q_df = (float)d.q_df; // maximum angle in dorsiflexion (deg)
        q_pf = (float)d.q_pf; // maximum angle in plantarflexion (deg)
    }

    private void Update()
    {

    }
}
