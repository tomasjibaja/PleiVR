using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveformAnim : MonoBehaviour {

    private float offset = 0;
    private int waveform = 1;
    private int prevWaveform = 1;

    public OSC osc;

    public Material sinWave;
    public Material triangleWave;
    public Material sawWave;
    public Material squareWave;

    void OnCollisionEnter(Collision collision)
    {
        if (waveform == 4)
        {
            waveform = 1;
        }
        else waveform++;

        switch (waveform)
        {
            case 1:
                GetComponent<Renderer>().material = sinWave;
                break;
            case 2:
                GetComponent<Renderer>().material = triangleWave;
                break;
            case 3:
                GetComponent<Renderer>().material = sawWave;
                break;
            case 4:
                GetComponent<Renderer>().material = squareWave;
                break;
        }

        OscMessage waveMsg = new OscMessage();
        waveMsg.address = "/Esferas/Waveform";
        waveMsg.values.Add(waveform);
        osc.Send(waveMsg);

    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (prevWaveform != waveform)
        {

        }


        if (offset > -0.97f)
        {
            offset = offset - 0.01f;
        }
        else offset = 0;

        GetComponent<Renderer>().material.mainTextureOffset = new Vector2(offset, 1);

        prevWaveform = waveform;
        //GetComponent<Renderer>().material.mainTextureOffset = new Vector2(offset, 1); // Asigna valor correspondiente al offset del material
    }
}
