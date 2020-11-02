using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetButtonBang : MonoBehaviour {

    public OSC osc;
    private bool sent = false;

    public void SendBang()
    {

        if (!sent) {
            OscMessage resetBang = new OscMessage();
            resetBang.address = "/ResetBang";
            resetBang.values.Add(1);
            osc.Send(resetBang);
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
