using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingScript : MonoBehaviour {

    private float a;
    private float inc;


    // Use this for initialization
    void Start () {

        // a = (transform.position.x * transform.position.y * transform.position.z) % 10000;
        a = 0;
        inc = Random.Range(5, 15);
        inc = inc / 10;
    }
	
	// Update is called once per frame
	void Update () {


        if (a < 50)
        {
            transform.position += Vector3.up * (Time.deltaTime / (4 + inc));
            a = a + inc;
        }

        if (a > 49 & a < 75)
        {
            transform.position += Vector3.up * (Time.deltaTime / (5 + inc));
            a = a + inc;
        }

        if (a > 74 & a < 100)
        {
            transform.position += Vector3.up * (Time.deltaTime / (6 + inc));
            a = a + inc;
        }

        if (a > 99 & a < 150)
        {
            transform.position += Vector3.down * (Time.deltaTime / (4 + inc));
            a = a + inc;
        }

        if (a > 149 & a < 175)
        {
            transform.position += Vector3.down * (Time.deltaTime / (5 + inc));
            a = a + inc;
        }

        if (a > 174)
        {
            transform.position += Vector3.down * (Time.deltaTime / (6 + inc));
            a = a + inc;
        }

        if (a > 200)
        {
            a = 0;
            inc = Random.Range(5, 15);
            inc = inc / 10;
        }


    }
}