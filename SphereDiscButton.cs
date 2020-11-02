using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereDiscButton : MonoBehaviour {

    public Material materialOn;
    public Material materialOff;
    public OSC osc;
    public BotonesEsfera esferaState;
    public float nota;

    private string prevEsferaState;
    private float offset = 0;

    private bool buttonIsOn = false;
    private bool prevButtonState = false;


    // Funcion de encender el modo discreto, se llama cuando el boton es presionado
    // Envia por OSC un 1 a PD para que prenda el modo discreto. Si no fue cambiado antes,
    // cambia el estado del objeto padre Botones a "discreto". Togglea el estado de
    // buttonIsOn
    public void prenderDiscreto()
    {
        if (esferaState.estado != "discreto")
        {
            OscMessage discMsg = new OscMessage();
            discMsg.address = "/Esferas/Disc";
            discMsg.values.Add(1);
            osc.Send(discMsg);

            esferaState.estado = "discreto";
        }

        buttonIsOn = !buttonIsOn;
    }


    // Funcion de apagar el modo discreto, se llama si detecta que el objeto padre Botones
    // tiene su estado como "continuo". 
    // Envía un 0 por OSC para que PD apague el modo discreto, solo si es el controlador
    // de la nota 1, para que no envíe 12 veces el mensaje a PD y ralentice.
    // Tambien vuelve el material a Off y desactiva cada nota en PD.
    void apagarDiscreto()
    {
        if (nota == 1)
        {
            OscMessage discMsg = new OscMessage();
            discMsg.address = "/Esferas/Disc";
            discMsg.values.Add(0);
            osc.Send(discMsg);
        }

        GetComponent<MeshRenderer>().material = materialOff;

        OscMessage estadoOff = new OscMessage();
        estadoOff.address = "/Esferas/Estado";
        estadoOff.values.Add(0);
        osc.Send(estadoOff);

        OscMessage notaOff = new OscMessage();
        notaOff.address = "/Esferas/Nota";
        notaOff.values.Add(nota);
        osc.Send(notaOff);

    }


    // Use this for initialization
    void Start () {

        GetComponent<MeshRenderer>().material = materialOff;

        OscMessage estadoOff = new OscMessage();
        estadoOff.address = "/Esferas/Estado";
        estadoOff.values.Add(0);
        osc.Send(estadoOff);

        OscMessage notaOff = new OscMessage();
        notaOff.address = "/Esferas/Nota";
        notaOff.values.Add(nota);
        osc.Send(notaOff);

    }

    // Update is called once per frame
    void Update () {

        // Chequea si el estado del modulo pasó a continuo recientemente
        if (esferaState.estado == "continuo" && prevEsferaState == "discreto")
        {
            apagarDiscreto(); // Si es así, llama la funcion de apagar el modo discreto
        }

        // Calcula el valor de offset del material para el frame actual
        if (offset < 1.01)
        {
            offset = offset + 0.01f;
        }
        else offset = 0;

        // Si el botón está encendido recientemente (o sea, si en el frame anterior no lo estaba)
        // envía los mensajes de qué nota encender y cambia el material del botón
        if (buttonIsOn && (prevButtonState != buttonIsOn))
        {
            GetComponent<MeshRenderer>().material = materialOn;

            OscMessage estadoOn = new OscMessage();
            estadoOn.address = "/Esferas/Estado";
            estadoOn.values.Add(1);
            osc.Send(estadoOn);

            OscMessage notaOn = new OscMessage();
            notaOn.address = "/Esferas/Nota";
            notaOn.values.Add(nota);
            osc.Send(notaOn);


        }

        //Si el boton fue apagado recientemente, envia mensajes de apagar la nota y cambia el material a apagado
        if (!buttonIsOn && (prevButtonState != buttonIsOn))
        {
            GetComponent<MeshRenderer>().material = materialOff;

            OscMessage estadoOff = new OscMessage();
            estadoOff.address = "/Esferas/Estado";
            estadoOff.values.Add(0);
            osc.Send(estadoOff);

            OscMessage notaOff = new OscMessage();
            notaOff.address = "/Esferas/Nota";
            notaOff.values.Add(nota);
            osc.Send(notaOff);

        }

        GetComponent<Renderer>().material.mainTextureOffset = new Vector2(1, offset); // Asigna valor correspondiente al offset del material
        prevButtonState = buttonIsOn; // Actualiza valores de estados previos
        prevEsferaState = esferaState.estado;
    }
}
