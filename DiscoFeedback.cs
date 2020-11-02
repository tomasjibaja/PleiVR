using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class DiscoFeedback : MonoBehaviour {

    public float valueAdjust;
    public OSC osc;
    public LinearMapping linearMapping;
    private float prevValue;

    // Use this for initialization
    void Start () {

        prevValue = linearMapping.value;

    }

    // Update is called once per frame
    void Update () {

        if (prevValue != linearMapping.value)
        {

            OscMessage feedbackMsg = new OscMessage();
            feedbackMsg.address = "/RotoPadInterface/Feedback";
            feedbackMsg.values.Add((1 - linearMapping.value) * valueAdjust);
            osc.Send(feedbackMsg);

            prevValue = linearMapping.value;
        }

    }
}
