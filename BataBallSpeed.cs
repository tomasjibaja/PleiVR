using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class BataBallSpeed : MonoBehaviour {

    public LinearMapping linearMapping;
    public float speed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        speed = linearMapping.value * 0.1f;

	}
}
