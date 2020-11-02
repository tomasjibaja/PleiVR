using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BataBallToken : MonoBehaviour {

    [SerializeField]
    private int cuerpo;

    public string parametro;



    // Use this for initialization
    void Start () {
        switch (cuerpo)
        {
            case 1:
                GetComponent<Renderer>().material.color = new Color(5, 0, 0, 1);
                break;
            case 2:
                GetComponent<Renderer>().material.color = new Color(0, 2, 0, 1);
                break;
            case 3:
                GetComponent<Renderer>().material.color = new Color(0, 0, 3, 1);
                break;
            case 4:
                GetComponent<Renderer>().material.color = new Color(3, 0, 3, 1);
                break;
        }
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
