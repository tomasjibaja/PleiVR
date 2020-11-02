using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawRectangle : MonoBehaviour {

    // Variables publicas las fichas de volumen, filtro, material y script OSC del modulo
    public GameObject levelToken;
    public GameObject filterToken;
    public Material material;
    public OSC osc;

    // El objeto rectangulo a dibujar
    private GameObject rectangulo;

    // Variables de filtro, volumen, variables previas, el offset para el material y el Vector3 del punto medio entre las fichas
    private float level;
    private float filter;
    private float prevLevel;
    private float prevFilter;
    private float offset;
    private Vector3 puntoMedio;


    // Nro de cuerpo que controla, esto se especifica en el Inspector de Unity
    [SerializeField]
    private float cuerpo;

    // Funcion que dibuja el rectangulo
    // Si no es null el objeto, lo destruye, calcula el punto medio, crea el cubo y lo posicion
    // Le da el tamaño correcto, lo rota y le asigna el material correspondiente
    void DrawShape()
    {
        if (rectangulo != null)
        {
            Destroy(rectangulo);
        }

        puntoMedio = levelToken.transform.position + (filterToken.transform.position - levelToken.transform.position) / 2;

        rectangulo = GameObject.CreatePrimitive(PrimitiveType.Cube);
        rectangulo.transform.position = puntoMedio;
        rectangulo.transform.localScale = new Vector3(0.01f, level, filter);
        rectangulo.transform.eulerAngles = new Vector3(0, transform.parent.transform.parent.eulerAngles.y - 180, 51);
        rectangulo.GetComponent<Renderer>().material = material;
    }


    // Funcion que envia los mensajes correspondientes segun la posicion de las fichas
    void SendOSCMessages()
    {
        OscMessage levelMessage = new OscMessage();
        levelMessage.address = "/BataBall/" + cuerpo + "/Level";
        levelMessage.values.Add(level * 3);
        osc.Send(levelMessage);

        OscMessage filterMessage = new OscMessage();
        filterMessage.address = "/BataBall/" + cuerpo + "/Filter";
        filterMessage.values.Add(filter * 3.8f);
        osc.Send(filterMessage);
    }

    // Use this for initialization
    void Start () {

        // Asigna offset inicial, calcula level y filter segun la posicion de las fichas (distancia absoluta)
        // Dibuja el primer rectangulo y envia los primeros mensajes
        offset = 0;
        level = Mathf.Abs(filterToken.transform.position.x - transform.position.x);
        filter = Mathf.Abs(levelToken.transform.position.z - transform.position.z);
        prevLevel = Mathf.Abs(filterToken.transform.position.x - transform.position.x);
        prevFilter = Mathf.Abs(levelToken.transform.position.z - transform.position.z);
        DrawShape();
        SendOSCMessages();

    }

    // Update is called once per frame
    void Update () {

        // Calcula valores de level al inicio del ciclo
        level = Mathf.Abs(filterToken.transform.position.x - transform.position.x);
        filter = Mathf.Abs(levelToken.transform.position.z - transform.position.z);

        // Asigna valores de rotacion del material
        if (offset > -0.97f)
        {
            offset = offset - 0.01f;
        }
        else offset = 0;

        // Chequea si se movió alguna ficha para llamar la funcion de dibujar
        if (prevLevel != level || prevFilter != filter)
        {
            DrawShape();
            SendOSCMessages();
        }

        // Aplica el offset al material
        rectangulo.GetComponent<Renderer>().material.mainTextureOffset = new Vector2(offset, 1);

        // Asigna los valores de variables de chequeo previas al final de ciclo
        prevLevel = Mathf.Abs(filterToken.transform.position.x - transform.position.x);
        prevFilter = Mathf.Abs(levelToken.transform.position.z - transform.position.z);

    }
}
