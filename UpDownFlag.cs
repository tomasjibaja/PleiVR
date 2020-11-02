using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownFlag : MonoBehaviour {

    public float previousY;
    public float currentY;
    public string direction;

	// Use this for initialization
	void Start () {
    
        previousY = gameObject.transform.position.y;
        currentY = gameObject.transform.position.y;

    }
	
	// Update is called once per frame
	void Update () {

        previousY = currentY;
        currentY = gameObject.transform.position.y;

        if (previousY <= currentY)
        {
            direction = "up";
        }
        else direction = "down";

        //print(direction);

    }
}
