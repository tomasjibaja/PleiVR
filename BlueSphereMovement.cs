using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueSphereMovement : MonoBehaviour
{

    private float counter = 0;
    private bool isGrabbed = false;
    private float sphereTiling;
    private float sphereOffset;
    private Vector3 originalScale;
    private float shakeFactor;

    public void grabSphere()
    {
        isGrabbed = true;
    }

    public void dropSphere()
    {
        isGrabbed = false;
        transform.localScale = originalScale;
    }

    void Float()
    {
        if (counter < 50)
        {
            transform.position += Vector3.up * (Time.deltaTime / 10);
            counter++;
        }

        if (counter > 49 & counter < 75)
        {
            transform.position += Vector3.up * (Time.deltaTime / 15);
            counter++;
        }

        if (counter > 74 & counter < 100)
        {
            transform.position += Vector3.up * (Time.deltaTime / 20);
            counter++;
        }

        if (counter > 99 & counter < 150)
        {
            transform.position += Vector3.down * (Time.deltaTime / 10);
            counter++;
        }

        if (counter > 149 & counter < 175)
        {
            transform.position += Vector3.down * (Time.deltaTime / 15);
            counter++;
        }

        if (counter > 174)
        {
            transform.position += Vector3.down * (Time.deltaTime / 20);
            counter++;
        }

        if (counter > 200)
        {
            counter = 0;
        }

    }

    void Shake()
    {

        transform.localScale = originalScale;
        shakeFactor = Random.Range(-10, 10);
        shakeFactor = shakeFactor / 1000;
        print(shakeFactor);
        transform.localScale = new Vector3(transform.localScale.x + shakeFactor, transform.localScale.y + shakeFactor, transform.localScale.z + shakeFactor);

    }


    // Use this for initialization
    void Start()
    {

        originalScale = transform.localScale;

    }

    // Update is called once per frame
    void Update()
    {

        if (isGrabbed == false)
        {
            Float();
        }

        if (isGrabbed == true)
        {
            Shake();
        }

        sphereTiling = (counter * 0.004f) - 0.3f; // Calcula el valor de tiling para el material en este frame
        GetComponent<Renderer>().material.mainTextureScale = new Vector2(1, sphereTiling); // Asigna valor al tiling del material

    }
}
