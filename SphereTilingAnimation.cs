using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereTilingAnimation : MonoBehaviour {

    private float offset = 0;
    private float tiling = 3;


    [SerializeField]
    private int nroOrbe;

    [SerializeField]
    FiloDibujaCirculo scriptDeLaEspada;

    public void Pertu()
    {
        tiling = 30;
        scriptDeLaEspada.esferaActivada = nroOrbe;
    }

    // Use this for initialization
    void Start () {
    }

    // Update is called once per frame
    void Update () {


        // Calcula offset para el efecto de anillos luminicos que suben
        if (offset < 1)
        {
            offset = offset + Random.Range(0.01f, 0.03f);
        }
        else offset = 0;


        // Calcula valores de perturbación, si la esfera fue interactuada
        if (tiling > 3)
        {
            tiling = tiling - 0.5f;
        }

        GetComponent<Renderer>().material.mainTextureOffset = new Vector2(1, offset); // Asigna valor correspondiente al offset del material

        GetComponent<Renderer>().material.mainTextureScale = new Vector2(1, tiling); // Asigna valor al tiling del material


    }
}
