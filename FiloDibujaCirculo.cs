using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class FiloDibujaCirculo : MonoBehaviour {

    [SerializeField]
    private KeyCode teclaCrear = KeyCode.L;

    [SerializeField]
    private Material materialPreview;

    [SerializeField]
    private Material materialSwordBlue;

    [SerializeField]
    private Material materialSwordGreen;

    [SerializeField]
    private GameObject OrbeAzulPrefab;

    [SerializeField]
    private GameObject OrbeVerdePrefab;

    // Crea una variable de accion booleana llamada gripBut y se la asigna a la accion GripButton, que previamente fue creada en el menu de SteamVRInput
    public SteamVR_Action_Boolean gripBut = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GripButton");

    // Crea una variable de tipo Input Source llamada hand
    public SteamVR_Input_Sources grabHand;

    private float tiling;
    private float tileInc;

    private Vector3 puntoInicial;
    private Vector3 puntoFinal;
    private Vector3 puntoMedio;

    // Variables de la burbuja
    private GameObject previewSphere;
    private Collider previewCollider;
    private float diametro = 0;

    private bool isGrabbed = false;

    //Variables de los orbes
    private GameObject orbeActual;
    private GameObject orbe1;
    private GameObject orbe2;
    private GameObject orbe3;
    private float freq1 = 0;
    private float freq2 = 0;
    private float freq3 = 0;
    private int proxOrbe = 1;
    public int esferaActivada;

    public OSC osc;

    // Funcion que actualiza el siguiente orbe a crear.
    // Son tres en total, si el siguiente es 1 crea uno nuevo en 1 (si el 1 ya existe, lo destruye)
    // asigna la frecuencia a ese orbe, la envía por OSC y asigna como siguiente orbe el 2, etc.
    // Tambien envia las frecuencias correspondientes por OSC
    void UpdateProxOrbe()
    {

        switch (proxOrbe)
        {
            case 1:
                if (orbe1 != null)
                {
                    Destroy(orbe1);
                }
                orbe1 = orbeActual;
                freq1 = 300 - (diametro * 100);

                OscMessage message1 = new OscMessage();
                message1.values.Add(freq1);
                message1.address = "/Drones/Orbe1freq";
                osc.Send(message1);

                proxOrbe = 2;
                break;
            case 2:
                if (orbe2 != null)
                {
                    Destroy(orbe2);
                }
                orbe2 = orbeActual;
                freq2 = 300 - (diametro * 100);

                OscMessage message2 = new OscMessage();
                message2.values.Add(freq2);
                message2.address = "/Drones/Orbe2freq";
                osc.Send(message2);

                proxOrbe = 3;
                break;
            case 3:
                if (orbe3 != null)
                {
                    Destroy(orbe3);
                }
                orbe3 = orbeActual;
                freq3 = 300 - (diametro * 100);

                OscMessage message3 = new OscMessage();
                message3.values.Add(freq3);
                message3.address = "/Drones/Orbe3freq";
                osc.Send(message3);

                proxOrbe = 1;
                break;
        }
    }

    //Funcion que al salir del runtime, le manda frecuencia 0 por OSC a PD para que se silencie
    void OnApplicationQuit()
    {
        OscMessage message1 = new OscMessage();
        message1.values.Add(0);
        message1.address = "/Drones/Orbe1freq";
        osc.Send(message1);

        message1.address = "/Drones/Orbe2freq";
        osc.Send(message1);

        message1.address = "/Drones/Orbe3freq";
        osc.Send(message1);

        message1.address = "/Drones/Orbe1level";
        osc.Send(message1);

        message1.address = "/Drones/Orbe2level";
        osc.Send(message1);

        message1.address = "/Drones/Orbe3level";
        osc.Send(message1);

        message1.address = "/Drones/Level";
        osc.Send(message1);

        message1.address = "/Drones/PreviewLevel";
        osc.Send(message1);
    }


    // Use this for initialization
    void Start()
    {
        tiling = 0;
        tileInc = 0.01f;
    }

    // Update is called once per frame
    void Update()
    {

        // Si ya existe una esfera de preview, la destruye
        if (previewSphere != null)
        {
            Destroy(previewSphere);
        }

        // Si no hay ningún orbe activo, silencia el módulo
        if (orbe1 == null && orbe2 == null && orbe3 == null && previewSphere == null)
        {
            OscMessage messageLevelInit = new OscMessage(); // Manda 0.7 level global a los drones
            messageLevelInit.values.Add(0);
            messageLevelInit.address = "/Drones/Level";
            osc.Send(messageLevelInit);
        }

        isGrabbed = transform.parent.GetComponent<SwordGrab>().swordGrab; // le asigna true a isGrabbed segun si el objeto padre, que es quien tiene el interactable, esta siendo agarrado

        // Si se presiona la tecla, asigna punto inicial y levanta la bandera de que se creó una preview
        if ((Input.GetKeyDown(teclaCrear) || gripBut.GetStateDown(grabHand)) && isGrabbed)
        {
            puntoInicial = transform.position;

            OscMessage messageInit = new OscMessage();
            messageInit.values.Add(puntoInicial.y / 20); // Le asigna como volumen la altura del punto inicial dividida por 10
            messageInit.address = "/Drones/Orbe" + proxOrbe + "level"; // Concatena el nro de orbe siguiente con la string usada como direccion para enviar el volumen al drone adecuado
            osc.Send(messageInit);

            OscMessage messageLevelInit = new OscMessage(); // Manda 0.7 level global a los drones
            messageLevelInit.values.Add(0.7f);
            messageLevelInit.address = "/Drones/Level";
            osc.Send(messageLevelInit);

            OscMessage previewLevelMsg = new OscMessage(); // Manda 0.7 level a la preview
            previewLevelMsg.values.Add(0.6f);
            previewLevelMsg.address = "/Drones/PreviewLevel";
            osc.Send(previewLevelMsg);

        }

        // Si se mantiene la tecla, asigna punto final, calcula punto medio y muestra burbuja de preview
        if ((Input.GetKey(teclaCrear) || gripBut.GetState(grabHand)) && isGrabbed)
        {
            puntoFinal = transform.position; //asigna punto final a la posicion actual

            puntoMedio.x = puntoInicial.x + (puntoFinal.x - puntoInicial.x) / 2; //calcula y asigna punto medio
            puntoMedio.y = puntoInicial.y + (puntoFinal.y - puntoInicial.y) / 2;
            puntoMedio.z = puntoInicial.z + (puntoFinal.z - puntoInicial.z) / 2;

            diametro = Vector3.Distance(puntoInicial, puntoFinal); // calcula y asigna diametro

            if (diametro > 2)
            {
                diametro = 2; // verifica que no supere el valor dado
            }

            previewSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere); // crea la esfera
            previewSphere.transform.position = puntoMedio; // la ubica en punto medio
            previewSphere.transform.localScale = new Vector3(diametro + Random.Range(0, 0.02f), diametro + Random.Range(0, 0.02f), diametro + Random.Range(0, 0.02f)); // le da tamaño de diametro aleatorio para que parezca una burbuja

            previewCollider = previewSphere.GetComponent<Collider>(); // obtiene el collider y lo deshabilita para que la esfera no choque con otros objetos
            previewCollider.enabled = false;

            previewSphere.GetComponent<MeshRenderer>().material = materialPreview; // le asigna el material que le hayamos dado por variable en inspector

            OscMessage previewFreqMsg = new OscMessage(); // Manda freq a la preview en cada frame
            previewFreqMsg.values.Add(300 - (diametro * 100));
            previewFreqMsg.address = "/Drones/PreviewFreq";
            osc.Send(previewFreqMsg);



        }

        // Si se suelta la tecla y ya se habia creado la preview, crea el orbe
        if ((Input.GetKeyUp(teclaCrear) || gripBut.GetStateUp(grabHand)) && isGrabbed)
        {
            if (proxOrbe == 1)
            {
                orbeActual = Instantiate(OrbeAzulPrefab); // Si proxOrbe es 1, instancia un orbe a partir del prefab orbe azul y lo asigna al puntero orbeactual
            } else {
                orbeActual = Instantiate(OrbeVerdePrefab); // Sino, hace lo mismo pero con el prefab de orbe verde
                }

            orbeActual.SetActive(true); // Lo activa, porque el objeto original está desactivado para que no se vea ni consuma recursos
            orbeActual.transform.position = puntoMedio; // Lo ubica en el punto medio
            orbeActual.transform.localScale = new Vector3(diametro, diametro, diametro); // Lo escala segun diametro

            UpdateProxOrbe(); // Actualiza cual es el siguiente orbe a crear y asigna freq

            OscMessage previewLevelMsg = new OscMessage(); // Manda 0 level a la preview (la silencia)
            previewLevelMsg.values.Add(0);
            previewLevelMsg.address = "/Drones/PreviewLevel";
            osc.Send(previewLevelMsg);

        }

        if (esferaActivada != 0)
        {
            proxOrbe = esferaActivada;
            switch (esferaActivada)
            {
                case 1:
                    if (orbe1 != null)
                    {
                        Destroy(orbe1);
                    }
                    OscMessage mute1Message = new OscMessage();
                    mute1Message.values.Add(0);
                    mute1Message.address = "/Drones/Orbe1freq";
                    osc.Send(mute1Message);
                    mute1Message.address = "/Drones/Orbe1level";
                    osc.Send(mute1Message);
                    break;
                case 2:
                    if (orbe2 != null)
                    {
                        Destroy(orbe2);
                    }
                    OscMessage mute2Message = new OscMessage();
                    mute2Message.values.Add(0);
                    mute2Message.address = "/Drones/Orbe2freq";
                    osc.Send(mute2Message);
                    mute2Message.address = "/Drones/Orbe2level";
                    osc.Send(mute2Message);
                    break;
                case 3:
                    if (orbe3 != null)
                    {
                        Destroy(orbe3);
                    }
                    OscMessage mute3Message = new OscMessage();
                    mute3Message.values.Add(0);
                    mute3Message.address = "/Drones/Orbe3freq";
                    osc.Send(mute3Message);
                    mute3Message.address = "/Drones/Orbe3level";
                    osc.Send(mute3Message);
                    break;
            }
            esferaActivada = 0;
        }

        if (proxOrbe == 1)
        {
            GetComponent<MeshRenderer>().material = materialSwordBlue; // le asigna a la hoja el color correspondiente al orbe que va a crear
        }
        else GetComponent<MeshRenderer>().material = materialSwordGreen;

        GetComponent<Renderer>().material.mainTextureScale = new Vector2(1, tiling);
        tiling = tiling + tileInc;

        if (tiling > 0.7f)
        {
            tileInc = -0.01f;
        }

        if (tiling < 0.1f)
        {
            tileInc = 0.01f;
        }

    }
}

