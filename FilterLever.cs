using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class FilterLever : MonoBehaviour {

    public OSC osc;
    public LinearMapping handleMapping;
    public string parameter;
    public float valueAdjust;
    private float value;
    private float prevValue;


    // Use this for initialization
    void Start () {
		
        value = handleMapping.value * valueAdjust;
        prevValue = value;

    }
	
	// Update is called once per frame
	void Update () {

        value = handleMapping.value * valueAdjust;

        if (prevValue != value)
        {
           
            //Envia el mensaje por OSC
            OscMessage valueMessage = new OscMessage();
            valueMessage.address = "/GyrosFiltro/" + parameter;
            valueMessage.values.Add(value);
            osc.Send(valueMessage);
        }
    
    }
}
