using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BataBallCubeFace : MonoBehaviour {

    [SerializeField]
    private BataBallSpeed globalSpeed;

    private int prevCuerpo;
    private int actCuerpo;
    private float offset = 0;

	// Use this for initialization
	void Start () {

        prevCuerpo = transform.parent.GetComponent<BataBallCube>().cuerpo;
        GetComponent<Renderer>().material.color = new Color(5, 0, 0, 1);

    }

    // Update is called once per frame
    void Update () {

        // Asigna valores de rotacion del material
        if (offset > -0.97f)
        {
            offset = offset - 0.01f;
        }
        else offset = 0;

        GetComponent<Renderer>().material.mainTextureOffset = new Vector2(offset, 1);

        // Asigna valor de cuerpo actual
        actCuerpo = transform.parent.GetComponent<BataBallCube>().cuerpo;

        // Chequea valor de cuerpo del padre (el cubo) para asignarse un color
        if (prevCuerpo != actCuerpo)
        {
            switch (actCuerpo)
            {
                case 1:
                    GetComponent<Renderer>().material.color = new Color(5, 0, 0, 1);
                    break;
                case 2:
                    GetComponent<Renderer>().material.color = new Color(0, 2, 0, 1);
                    break;
                case 3:
                    GetComponent<Renderer>().material.color = new Color(0, 0, 3, 1);
                    break;
                case 4:
                    GetComponent<Renderer>().material.color = new Color(3, 0, 3, 1);
                    break;
            }
        }

        // Actualiza valor de cuerpo anterior para el siguiente frame
        prevCuerpo = actCuerpo;

	}
}
