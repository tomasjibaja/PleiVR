using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class algorithmNumber : MonoBehaviour {

    public OSC osc;
    public int algorithm;
    private bool sent;

    public void SendAlgNum()
    {

        if (!sent)
        {
            OscMessage algNum = new OscMessage();
            algNum.address = "/AlgNum";
            algNum.values.Add(algorithm - 1);
            osc.Send(algNum);
            sent = true;
        }
    }

    public void ResetSent()
    {
        sent = false;
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
