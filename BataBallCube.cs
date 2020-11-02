using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BataBallCube : MonoBehaviour {

    public Material upMaterial;
    public Material downMaterial;

    public string orientacion;
    public int cuerpo;
    public float lado;

    private float angulo;
    private float spark;

    public void Spark()
    {
        spark = 3;
    }

	// Use this for initialization
	void Start () {
        cuerpo = 1;
	}
	
	// Update is called once per frame
	void Update () {

        // Actualiza angulo del cubo con respecto al eje vertical superior
        angulo = Vector3.Angle(transform.up, Vector3.up);

        // Si es menor a 90 grados, el cubo mira hacia arriba, calcula lado, asigna up y material de up
        if (angulo < 90)
        {
            GetComponent<Renderer>().material = upMaterial;
            lado = (angulo / 1000) + 0.05f;
            orientacion = "up";
        }

        // Si es mayor a 90 grados, el cubo mira hacia abajo, calcula lado, asigna down y material de down
        if (angulo > 90)
        {
            GetComponent<Renderer>().material = downMaterial;
            lado = ((180 - angulo) / 1000) + 0.05f;
            orientacion = "down";
        }

        transform.localScale = new Vector3(lado, lado, lado); // Actualiza valor de scale

        // Chequea si se activó la funcion Spark() segun el valor que tenga la variable spark y aplica el comportamiento
        if (spark > 1)
        {
            GetComponent<Renderer>().material.color = new Color(spark, spark, spark, 1);
            spark = spark - 0.1f;
        }
    }
}
