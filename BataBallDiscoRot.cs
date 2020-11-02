using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BataBallDiscoRot : MonoBehaviour {

    private float offsetX;
    private float offsetY;

    [SerializeField]
    private BataBallSpeed globalSpeed;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (offsetX > -1)
        {
            offsetX = offsetX - (globalSpeed.speed * globalSpeed.speed * 2);
        }
        else
        {
            offsetX = 0;
        }

        if (offsetY > -1)
        {
            offsetY = offsetY - 0.001f;
        }
        else
        {
            offsetY = 0;
        }

        GetComponent<Renderer>().material.mainTextureOffset = new Vector2(offsetX, offsetY);

    }
}
