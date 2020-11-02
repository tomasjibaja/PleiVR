using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;


public class PadGetPos : MonoBehaviour {

    public GameObject basePad;
   

    // Use this for initialization
    void Start () {
    
    }

    // Update is called once per frame
    void Update () {

        //Copia la posición del pad base
        transform.position = basePad.transform.position;
       
    }
}
