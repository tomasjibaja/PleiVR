using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickTracker : MonoBehaviour {


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        gameObject.transform.position = transform.parent.gameObject.transform.position;
        gameObject.transform.eulerAngles = transform.parent.gameObject.transform.eulerAngles;


    }
}
