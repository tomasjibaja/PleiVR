using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class BeatDetector : MonoBehaviour {

    public OSC osc;
    public CircularDrive circularDrive;
    private string collisionName;
    private string parentDirection;
    Animator anim;

    private void OnCollisionEnter(Collision col)
    {
        collisionName = col.transform.gameObject.name;

        // accede al padre de col (que seria el objeto palillo)
        // y accede a la variable pública de direccion del script updownflag.
        // Primero chequea que no sea null el padre, pq con el temita ese
        // de que el throwable nos cambia al objeto que está siendo agarrado
        // si no chequeamos, tira nullexceptionerror.
        if (col.transform.parent.name == "PalilloEmisor") {

            parentDirection = col.transform.parent.gameObject.GetComponent<UpDownFlag>().direction;

            }


        if (collisionName == "Colisionador" && parentDirection == "down")
        {
            // Si colisiona con un objeto de nombre Colisionador
            // y el padre del mismo tiene dirección hacia abajo, entra al ciclo

            OscMessage freqMessage = new OscMessage();
            freqMessage.address = "/RotoPadFreq";
            freqMessage.values.Add(circularDrive.outAngle + 81);
            osc.Send(freqMessage);


            OscMessage bangMessage = new OscMessage();
            bangMessage.address = "/RotoPadBang";
            bangMessage.values.Add(1);
            osc.Send(bangMessage);

            anim = GetComponent<Animator>();
            anim.Play("golpePad1");

        }


    }


}
