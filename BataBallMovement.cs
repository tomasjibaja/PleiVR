using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BataBallMovement : MonoBehaviour {

    [SerializeField]
    private float movementSpeed = 5f;
    private float direction = 1;
    private float newSpeed;
    private string colName;
    public OSC osc;
    public GameObject velChanger;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


       
        newSpeed = 1 + (velChanger.transform.position.y * velChanger.transform.position.y);

        movementSpeed = newSpeed;

        if(transform.position.y > 8.5) 
        {
            direction = -1;
        }

        if (transform.position.y < 1.5)
        {
            direction = 1;
        }

        movementSpeed = movementSpeed * direction;
        transform.position = transform.position + new Vector3(0, movementSpeed * Time.deltaTime, 0);
        print(movementSpeed);

    }

    private void OnTriggerEnter(Collider other)
    {

        colName = other.name;
        OscMessage message = new OscMessage();
        message.values.Add(1);

        if (movementSpeed > 0){

            switch(colName)
            {
                case "BDUP":
                    message.address = "/BataBall/BDUP";
                    osc.Send(message);
                    print("BDUP");
                    break;
                case "SNUP":
                    message.address = "/BataBall/SNUP";
                    osc.Send(message);
                    print("SNUP");
                    break;
                case "HHUP":
                    message.address = "/BataBall/HHUP";
                    osc.Send(message);
                    print("HHUP");
                    break;
                case "CYUP":
                    message.address = "/BataBall/CYUP";
                    osc.Send(message);
                    print("CYUP");
                    break;
            }

        }

        if (movementSpeed < 0)
        {

            switch (colName)
            {
                case "BDDO":
                    message.address = "/BataBall/BDDO";
                    osc.Send(message);
                    print("BDDO");
                    break;
                case "SNDO":
                    message.address = "/BataBall/SNDO";
                    osc.Send(message);
                    print("SNDO");
                    break;
                case "HHDO":
                    message.address = "/BataBall/HHDO";
                    osc.Send(message);
                    print("HHDO");
                    break;
                case "CYDO":
                    message.address = "/BataBall/CYDO";
                    osc.Send(message);
                    print("CYDO");
                    break;
            }

        }



    }

}
