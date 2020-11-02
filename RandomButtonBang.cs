using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomButtonBang : MonoBehaviour {

    public OSC osc;
    private bool sent = false;

    public void SendBang() {

        if (!sent)
        {
            OscMessage randomBang = new OscMessage();
            randomBang.address = "/RandomBang";
            randomBang.values.Add(1);
            osc.Send(randomBang);
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
