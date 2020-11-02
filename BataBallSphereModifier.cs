using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BataBallSphereModifier : MonoBehaviour {

    private GameObject cuboActual;

    private float rotationX;
    private float rotationY;
    private float rotationZ;

    // Crea funcion de colision
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Cube") // Si la colision se llama Cube
        {
            if (collision.gameObject.GetComponent<BataBallCube>().cuerpo < 4) // Y si el nro de cuerpo de la colision es menor a 4
            {
                collision.gameObject.GetComponent<BataBallCube>().cuerpo++; // Incrementa el nro de cuerpo de la colision
            } else collision.gameObject.GetComponent<BataBallCube>().cuerpo = 1; // Sino (o sea, si es 4) le asigna 1
        }

        rotationX = Random.Range(-5.0f, 2.0f); // Rerandomiza los valores de rotacion
        rotationY = Random.Range(-2.0f, 7.0f);
        rotationZ = Random.Range(-3.0f, 6.0f);

        cuboActual = collision.gameObject; // Le asigna el gameObject de la colision a la variable cuboActual
        GetComponent<SphereCollider>().enabled = false; // Desactiva el collider de la esfera, pq si el cubo se suelta dentro y el collider de la esfera está activado, no se puede sacar
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
        // Si el cuboActual no es null
        if (cuboActual != null)
        {
            // Y si la distancia entre la esfera y el cubo es mayor al radio de la esfera dividido 4
            if (Vector3.Distance(cuboActual.transform.position, transform.position) > (transform.localScale.x / 4))
            {
                GetComponent<SphereCollider>().enabled = true; // Reactiva el collider de la esfera
            }
        }

        // Actualiza valores de rotacion
        transform.eulerAngles = transform.eulerAngles + new Vector3(rotationX, rotationY, rotationZ);

    }
}
