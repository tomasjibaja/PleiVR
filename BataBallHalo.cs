using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BataBallHalo : MonoBehaviour {

    private float offset;
    private float incremento = 0.01f;

    [SerializeField]
    private float direccion;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (offset > -0.97f)
        {
            offset = offset - (incremento * direccion);
        }
        else {
            offset = 0;
            incremento = Random.Range(0.0001f, 0.0020f);
        }

        GetComponent<Renderer>().material.mainTextureOffset = new Vector2(offset, 1);


    }
}
