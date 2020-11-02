using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class SwordGrab : MonoBehaviour {

    public bool swordGrab = false;
    public OSC osc;

    public Hand grabbingHand;


    public void onSwordGrab()
    {
        swordGrab = true;
    }

    public void onSwordDrop()
    {
        swordGrab = false;

        OscMessage previewLevelMsg = new OscMessage(); // Manda 0 level a la preview
        previewLevelMsg.values.Add(0);
        previewLevelMsg.address = "/Drones/PreviewLevel";
        osc.Send(previewLevelMsg);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
    
	}
}
