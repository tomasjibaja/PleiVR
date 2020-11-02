using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereContButton : MonoBehaviour {

    public Material materialOn;
    public Material materialOff;
    public OSC osc;
    public BotonesEsfera esferaState;
    private float offset = 0;

    public void prenderContinuo()
    {
        GetComponent<MeshRenderer>().material = materialOn;
        
        OscMessage contMsg = new OscMessage();
        contMsg.address = "/Esferas/Cont";
        contMsg.values.Add(1);
        osc.Send(contMsg);

        esferaState.estado = "continuo";
    }

    void apagarContinuo()
    {
        GetComponent<MeshRenderer>().material = materialOff;
        
        OscMessage contMsg = new OscMessage();
        contMsg.address = "/Esferas/Cont";
        contMsg.values.Add(0);
        osc.Send(contMsg);
    }

	// Use this for initialization
	void Start () {

        GetComponent<MeshRenderer>().material = materialOff;

    }

    // Update is called once per frame
    void Update () {

        if (esferaState.estado == "discreto")
        {
            apagarContinuo();
        }

        GetComponent<Renderer>().material.mainTextureOffset = new Vector2(1, offset); // Asigna valor correspondiente al offset del material
        if (offset < 1.01)
        {
            offset = offset + 0.01f;
        }
        else offset = 0;

    }
}
