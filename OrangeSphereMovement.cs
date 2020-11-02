using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeSphereMovement : MonoBehaviour {

    private float counter = 0;
    private bool isGrabbed = false;
    private bool prevGrabbed = true;
    private float sphereTiling;
    private float sphereOffset;
    private Vector3 originalScale;
    private float shakeFactor;
    private float offset;

    private float altura;
    private float distancia;

    public GameObject blueSphere;
    public Material line1Material;
    public Material line2Material;
    public Material line3Material;
    public Material line4Material;


    public OSC osc;

    // Función ejecutada al tomar la esfera, activa la variable de chequeo.
    public void grabSphere()
    {
        isGrabbed = true;
    }

    // Función que retorna el tamaño de la esfera al original
    // cuando es soltada. Desactiva la variable de chequeo.
    public void dropSphere()
    {
        isGrabbed = false;
        transform.localScale = originalScale;
    }

    // Función que hace flotar a la esfera cuando no ha sido tomada
    // Flota a diferentes velocidades por medio del incremento diferencial
    // de la variable counter.
    void Float()
    {
        if (counter < 50)
        {
            transform.position += Vector3.up * (Time.deltaTime / 10);
            counter++;
        }

        if (counter > 49 & counter < 75)
        {
            transform.position += Vector3.up * (Time.deltaTime / 13);
            counter++;
        }

        if (counter > 74 & counter < 100)
        {
            transform.position += Vector3.up * (Time.deltaTime / 16);
            counter++;
        }

        if (counter > 99 & counter < 150)
        {
            transform.position += Vector3.down * (Time.deltaTime / 10);
            counter++;
        }

        if (counter > 149 & counter < 175)
        {
            transform.position += Vector3.down * (Time.deltaTime / 13);
            counter++;
        }

        if (counter > 174)
        {
            transform.position += Vector3.down * (Time.deltaTime / 16);
            counter++;
        }

        if (counter > 200)
        {
            counter = 0;
        }
    }

    // Función que sacude aleatoriamente la esfera cuando ha sido tomada
    // y está produciendo sonido.
    void Shake()
    {
        transform.localScale = originalScale;
        shakeFactor = Random.Range(-10, 10);
        shakeFactor = shakeFactor / 1000;
        transform.localScale = new Vector3(transform.localScale.x + shakeFactor, transform.localScale.y + shakeFactor, transform.localScale.z + shakeFactor);
    }

    // Función que envía mensaje a PD para que apague el sonido.
    void apagarEsferas()
    {
        OscMessage esferasOff = new OscMessage();
        esferasOff.address = "/Esferas/OnOff";
        esferasOff.values.Add(0);
        osc.Send(esferasOff);
    }

    // Función que envía mensaje a PD para que inicie el sonido.
    public void prenderEsferas()
    {
        OscMessage esferasOn = new OscMessage();
        esferasOn.address = "/Esferas/OnOff";
        esferasOn.values.Add(1);
        osc.Send(esferasOn);
    }

    void actualizarParametros()
    {
        // Calcula el promedio entre alturas de ambas esferas, el ultimo divisor es el ajuste para nivel de volumen en PD.
        // Envía los mensajes correspondientes a PD.
        altura = (transform.position.y + blueSphere.transform.position.y) * 0.7f; 
        OscMessage alturaMsg = new OscMessage();
        alturaMsg.address = "/Esferas/Volumen";
        alturaMsg.values.Add(altura);
        osc.Send(alturaMsg);

        distancia = (Vector3.Distance(transform.position, blueSphere.transform.position)) * 500;
        OscMessage distanciaMsg = new OscMessage();
        distanciaMsg.address = "/Esferas/Distancia";
        distanciaMsg.values.Add(distancia);
        osc.Send(distanciaMsg);
    }

    // Dibuja y destruye en cada frame las líneas de rayos entre las dos esferas 
    // mediante cuatro objetos que varían el offset de su material.
    void DrawPowerLine()
    {
        if (offset < 1.1)
        {
            offset = offset + 0.1f;
        }
        else offset = 0;

        GameObject line1 = new GameObject();
        line1.AddComponent<LineRenderer>();
        LineRenderer lr1 = line1.GetComponent<LineRenderer>();
        lr1.material = new Material(line1Material);
        lr1.material.mainTextureOffset = new Vector2(offset * (Random.Range(-0.3f, 0.3f)), 1);
        lr1.startWidth = Random.Range(0.010f, 0.030f);
        lr1.SetPosition(0, transform.position);
        lr1.SetPosition(1, blueSphere.transform.position);
        lr1.alignment = LineAlignment.TransformZ;
        line1.transform.eulerAngles = new Vector3(line1.transform.eulerAngles.x, Random.Range(0, 35), line1.transform.eulerAngles.z);
        lr1.numCapVertices = 0;
        GameObject.Destroy(line1, 0.05f);

        GameObject line2 = new GameObject();
        line2.AddComponent<LineRenderer>();
        LineRenderer lr2 = line2.GetComponent<LineRenderer>();
        lr2.material = new Material(line2Material);
        lr2.material.mainTextureOffset = new Vector2(offset * (Random.Range(-0.5f, 0.5f)), 1);
        lr2.startWidth = Random.Range(0.010f, 0.200f);
        lr2.SetPosition(0, transform.position);
        lr2.SetPosition(1, blueSphere.transform.position);
        lr2.alignment = LineAlignment.TransformZ;
        line2.transform.eulerAngles = new Vector3(line2.transform.eulerAngles.x, Random.Range(45, 80), line2.transform.eulerAngles.z);
        lr2.numCapVertices = 0;
        GameObject.Destroy(line2, 0.01f);

        GameObject line3 = new GameObject();
        line3.AddComponent<LineRenderer>();
        LineRenderer lr3 = line3.GetComponent<LineRenderer>();
        lr3.material = new Material(line3Material);
        lr3.material.mainTextureOffset = new Vector2(offset * (Random.Range(-0.4f, 0.4f)), 1);
        lr3.startWidth = Random.Range(0.010f, 0.050f);
        lr3.SetPosition(0, transform.position);
        lr3.SetPosition(1, blueSphere.transform.position);
        lr3.alignment = LineAlignment.TransformZ;
        line3.transform.eulerAngles = new Vector3(line3.transform.eulerAngles.x, Random.Range(90, 125), line3.transform.eulerAngles.z);
        lr3.numCapVertices = 0;
        GameObject.Destroy(line3, 0.01f);

        GameObject line4 = new GameObject();
        line4.AddComponent<LineRenderer>();
        LineRenderer lr4 = line4.GetComponent<LineRenderer>();
        lr4.material = new Material(line4Material);
        lr4.material.mainTextureOffset = new Vector2(offset * (Random.Range(-0.2f, 0.2f)), 1);
        lr4.startWidth = Random.Range(0.010f, 0.030f);
        lr4.SetPosition(0, transform.position);
        lr4.SetPosition(1, blueSphere.transform.position);
        lr4.alignment = LineAlignment.TransformZ;
        line4.transform.eulerAngles = new Vector3(line4.transform.eulerAngles.x, Random.Range(135, 170), line4.transform.eulerAngles.z);
        lr4.numCapVertices = 0;
        GameObject.Destroy(line4, 0.05f);
    }

    // Use this for initialization
    void Start () {

        originalScale = transform.localScale;

	}

    // Update is called once per frame
    void Update()
    {
        // Si la esfera no está tomada, flota y si fue soltada recientemente
        // llama a la función de apagarEsferas.
        if (isGrabbed == false)
        {
            Float();
            if (prevGrabbed == true)
            {
                apagarEsferas();
            }
        }

        // Si está tomada, llama a las funciones de sacudir, dibujar líneas y
        // y actualizar parámetros.
        if (isGrabbed == true)
        {
            Shake();
            DrawPowerLine();
            actualizarParametros();
        }

        sphereTiling = (counter * 0.004f) - 0.3f; // Calcula el valor de tiling para el material en este frame
        GetComponent<Renderer>().material.mainTextureScale = new Vector2(1, sphereTiling); // Asigna valor al tiling del material
        prevGrabbed = isGrabbed;

    }
}
