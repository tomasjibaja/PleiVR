using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;
using Valve.VR.InteractionSystem;


public class Texter : MonoBehaviour {

    public TextMesh TextoNota;
    public GameObject rotor;
    private float freq;
    private float freqColor;
    private float opacity = 0.3f;
    private string tune;
    private float note;
    private float compensadorRotacionY;
    public CircularDrive circularDrive;


    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {

        compensadorRotacionY = transform.parent.gameObject.transform.parent.gameObject.transform.eulerAngles.y;

        freq = rotor.transform.eulerAngles.y + 81 - compensadorRotacionY;

        if (circularDrive.driving && opacity < 1)
        {
            opacity = opacity + 0.1f;
        }
        else if (opacity > 0.3f) {
            opacity = opacity - 0.1f;
        }

        TextoNota.color = new Vector4(1, 0, 0, opacity);

        tune = null;

        // nota E
        if (Mathf.Abs(freq - 84) < 4 || Mathf.Abs(freq - 168) < 8 || Mathf.Abs(freq - 336) < 16)
        {

            tune = "(E)";
            note = 82.4f;

            if ((freq % note) < 2)
            {
                freqColor = (freq % note) / 2;
            }
            else freqColor = (note - (freq % note)) / 2;
            TextoNota.color = new Vector4(freqColor, (1 - freqColor), 0, opacity);

        }


        // nota F
        if (Mathf.Abs(freq - 89) < 4 || Mathf.Abs(freq - 178) < 8 || Mathf.Abs(freq - 356) < 16)
        {

            tune = "(F)";
            note = 87.3f;

            if ((freq % note) < 2)
            {
                freqColor = (freq % note) / 2;
            }
            else freqColor = (note - (freq % note)) / 2;
            TextoNota.color = new Vector4(freqColor, (1 - freqColor), 0, opacity);

        }


        // nota F#
        if (Mathf.Abs(freq - 94) < 4 || Mathf.Abs(freq - 188) < 8 || Mathf.Abs(freq - 376) < 16)
        {

            tune = "(F#)";
            note = 92.5f;

            if ((freq % note) < 2)
            {
                freqColor = (freq % note) / 2;
            }
            else freqColor = (note - (freq % note)) / 2;
            TextoNota.color = new Vector4(freqColor, (1 - freqColor), 0, opacity);

        }


        // nota G
        if (Mathf.Abs(freq - 100) < 4 || Mathf.Abs(freq - 200) < 8 || Mathf.Abs(freq - 400) < 16)
        {

            tune = "(G)";
            note = 98f;

            if ((freq % note) < 2)
            {
                freqColor = (freq % note) / 2;
            }
            else freqColor = (note - (freq % note)) / 2;
            TextoNota.color = new Vector4(freqColor, (1 - freqColor), 0, opacity);

        }


        // nota G#
        if (Mathf.Abs(freq - 106) < 4 || Mathf.Abs(freq - 212) < 8 || Mathf.Abs(freq - 424) < 16)
        {

            tune = "(G#)";
            note = 103.83f;

            if ((freq % note) < 2)
            {
                freqColor = (freq % note) / 2;
            }
            else freqColor = (note - (freq % note)) / 2;
            TextoNota.color = new Vector4(freqColor, (1 - freqColor), 0, opacity);

        }


        // nota A
        if (Mathf.Abs(freq - 112) < 4 || Mathf.Abs(freq - 224) < 8 || Mathf.Abs(freq - 448) < 16)
        {

            tune = "(A)";
            note = 110;

            if ((freq % note) < 2)
            {
                freqColor = (freq % note) / 2;
            }
            else freqColor = (note - (freq % note)) / 2;
            TextoNota.color = new Vector4(freqColor, (1 - freqColor), 0, opacity);

        }


        // nota A#
        if (Mathf.Abs(freq - 118) < 4 || Mathf.Abs(freq - 236) < 8)
        {

            tune = "(A#)";
            note = 116.54f;

            if ((freq % note) < 2)
            {
                freqColor = (freq % note) / 2;
            }
            else freqColor = (note - (freq % note)) / 2;
            TextoNota.color = new Vector4(freqColor, (1 - freqColor), 0, opacity);

        }


        // nota B
        if (Mathf.Abs(freq - 125) < 4 || Mathf.Abs(freq - 250) < 8)
        {

            tune = "(B)";
            note = 123.47f;

            if ((freq % note) < 2)
            {
                freqColor = (freq % note) / 2;
            }
            else freqColor = (note - (freq % note)) / 2;
            TextoNota.color = new Vector4(freqColor, (1 - freqColor), 0, opacity);

        }

        // nota C
        if (Mathf.Abs(freq - 132) < 4 || Mathf.Abs(freq - 264) < 8)
        { 

            tune = "(C)";
            note = 130.81f;

            if ((freq % note) < 2) {
                freqColor = (freq % note) / 2;
            } else freqColor = (note - (freq % note)) / 2;
            TextoNota.color = new Vector4(freqColor, (1 - freqColor), 0, opacity);

        }


        // nota C#
        if (Mathf.Abs(freq - 141) < 4 || Mathf.Abs(freq - 282) < 8)
        {

            tune = "(C#)";
            note = 138.59f;

            if ((freq % note) < 2)
            {
                freqColor = (freq % note) / 2;
            }
            else freqColor = (note - (freq % note)) / 2;
            TextoNota.color = new Vector4(freqColor, (1 - freqColor), 0, opacity);

        }


        // nota D
        if (Mathf.Abs(freq - 149) < 4 || Mathf.Abs(freq - 298) < 8)
        {

            tune = "(D)";
            note = 146.83f;

            if ((freq % note) < 2)
            {
                freqColor = (freq % note) / 2;
            }
            else freqColor = (note - (freq % note)) / 2;
            TextoNota.color = new Vector4(freqColor, (1 - freqColor), 0, opacity);

        }


        // nota D#
        if (Mathf.Abs(freq - 158) < 4 || Mathf.Abs(freq - 316) < 8)
        {

            tune = "(D#)";
            note = 155.56f;

            if ((freq % note) < 2)
            {
                freqColor = (freq % note) / 2;
            }
            else freqColor = (note - (freq % note)) / 2;
            TextoNota.color = new Vector4(freqColor, (1 - freqColor), 0, opacity);

        }



        TextoNota.text = freq.ToString("F2") + "Hz\n" + tune;

    }
}
