using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class DiscoRotopad : MonoBehaviour {

    public float OscilatorID;
    public float Parameter;
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

            OscMessage messageV = new OscMessage();
            messageV.address = "/RotoPadInterface/Value";
            messageV.values.Add((1 - linearMapping.value) * valueAdjust);
            osc.Send(messageV);

            OscMessage messageP = new OscMessage();
            messageP.address = "/RotoPadInterface/Parameter";
            messageP.values.Add(Parameter);
            osc.Send(messageP);

            OscMessage messageO = new OscMessage();
            messageO.address = "/RotoPadInterface/OscilatorID";
            messageO.values.Add(OscilatorID);
            osc.Send(messageO);



            prevValue = linearMapping.value;
        }
    }
}
