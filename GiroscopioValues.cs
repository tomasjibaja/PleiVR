using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class GiroscopioValues : MonoBehaviour {

    public OSC osc;
    public LinearMapping handleMapping;
    private bool grabbed = false;
    private bool prevGrabbed = true;
    private float freq;
    private float cutoff;

    public float volAdjust;
    private float volume;


    public void isGrabbed()
    {
        grabbed = true;
    }

    public void isDropped()
    {
        grabbed = false;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
        if (grabbed) {

            //Calcula angulos de rotacion, los suma y arma valor de freq
            freq = transform.eulerAngles.x + transform.eulerAngles.y + transform.eulerAngles.z;
            freq = (freq / 1.55f) + 150;

            //Envia el mensaje por OSC
            OscMessage freqMessage = new OscMessage();
            freqMessage.address = "/GyrosFreq";
            freqMessage.values.Add(freq);
            osc.Send(freqMessage);

            //Calcula la freq de cutoff segun altura del padre de la esfera (la mano VR)
            cutoff = (gameObject.transform.parent.transform.position.y + 1) * 2;
            cutoff = Mathf.Max(Mathf.Pow(cutoff, 5), 20);

            //Envia el mensaje de cutoff por OSC
            OscMessage cutoffMessage = new OscMessage();
            cutoffMessage.address = "/GyrosCutoff";
            cutoffMessage.values.Add(cutoff);
            osc.Send(cutoffMessage);

            //Envia volumen
            volume = handleMapping.value;
            OscMessage volumeMessage = new OscMessage();
            volumeMessage.address = "/GyrosVol";
            volumeMessage.values.Add(volume * volAdjust);
            osc.Send(volumeMessage);

        }

        if (grabbed == false && prevGrabbed == true) {

            //Envia volumen 0
            OscMessage volumeMessage = new OscMessage();
            volumeMessage.address = "/GyrosVol";
            volumeMessage.values.Add(0);
            osc.Send(volumeMessage);

        }
     
        prevGrabbed = grabbed;

	}
}
