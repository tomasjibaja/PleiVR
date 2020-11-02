using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BataBallSphere : MonoBehaviour {

    private float direction = 1;
    private float velocity = 1;

    private float rotationX;
    private float rotationY;
    private float rotationZ;

    private string colisionOrientation;
    private int colisionCuerpo;
    private float colisionLado;
    private float upDown;

    public OSC osc;


    [SerializeField]
    private BataBallSpeed globalSpeed;

    [SerializeField]
    private GameObject discoUp;

    [SerializeField]
    private GameObject discoDown;

    // Si detecta colision
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Cube")
        {
            colisionOrientation = collision.gameObject.GetComponent<BataBallCube>().orientacion; // Almacena orientacion de la colision
            colisionCuerpo = collision.gameObject.GetComponent<BataBallCube>().cuerpo; // Almacena cuerpo que tiene asignado la colision
            colisionLado = collision.gameObject.GetComponent<BataBallCube>().lado; // Almacena el lado de la colision (tamaño del cubo)


            // Si la colision está hacia arriba y la esfera está yendo hacia abajo, o si la colision está hacia abajo y la esfera va hacia arriba
            if ((colisionOrientation == "up" && direction == -1) || (colisionOrientation == "down" && direction == 1))
            {
                if (colisionCuerpo == 4) // Si el cuerpo es el 4, que es el cuerpo pitcheado
                {
                    upDown = OrientationToNumber(colisionOrientation); // Crea una variable a la que la funcion le almacena 0 si es up y 1 si es otro valor

                    OscMessage ladoMessage = new OscMessage(); // Crea y envia mensaje de tamaño usando como path la suma de cuerpo y orientacion
                    ladoMessage.values.Add(colisionLado);
                    ladoMessage.address = "/BataBall/" + (colisionCuerpo + upDown) + "/Ratio";
                    osc.Send(ladoMessage);

                }
                else
                {
                    OscMessage ladoMessage = new OscMessage(); // Crea y envia mensaje de tamaño usando como path el nro de cuerpo 
                    ladoMessage.values.Add(colisionLado);
                    ladoMessage.address = "/BataBall/" + colisionCuerpo + "/Ratio";
                    osc.Send(ladoMessage);
                }

                // Hacer destellar el cubo que colisiona
                collision.gameObject.GetComponent<BataBallCube>().Spark();

            }
        }
    }

    // Funcion que toma la orientacion en string y devuelve un valor en float
    float OrientationToNumber(string orientation)
    {
        if (orientation == "up")
        {
            return 0;
        }
        else return 1;
    }

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

        velocity = globalSpeed.speed; // Almacena variable de velocidad

        // Si la posicion es mayor que la del disco superior, menos el radio de la esfera
        if (transform.position.y > (discoUp.transform.position.y - (transform.localScale.y / 2)))
        {
            // Asigna dirección hacia abajo y rerandomiza la rotacion de la esfera
            direction = -1;
            rotationX = Random.Range(-2.0f, 2.0f);
            rotationY = Random.Range(-2.0f, 2.0f);
            rotationZ = Random.Range(-2.0f, 2.0f);

        }

        // Si la posicion es menor que la del disco superior, menos el radio de la esfera
        if (transform.position.y < (discoDown.transform.position.y + (transform.localScale.y / 2)))
        {
            // Asigna dirección hacia arriba y rerandomiza la rotacion de la esfera
            direction = 1;
            rotationX = Random.Range(-2.0f, 2.0f);
            rotationY = Random.Range(-2.0f, 2.0f);
            rotationZ = Random.Range(-2.0f, 2.0f);
        }

        // Asigna los valores de incremento de posicion y rotacion
        transform.position = transform.position + new Vector3(0, direction * velocity, 0);
        transform.eulerAngles = transform.eulerAngles + new Vector3(rotationX, rotationY, rotationZ);

    }
}
