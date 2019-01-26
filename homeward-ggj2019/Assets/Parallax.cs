using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class Parallax : MonoBehaviour
{
    public Transform[] backgrounds;
    private float[] parallaxScales;
    public float smoothing;
   

    private Vector3 previousCamPos;

    // Use this for initialization
    private void Start()
    {
        previousCamPos = transform.position;
        parallaxScales = new float[backgrounds.Length];
        for (int i = 0; i < parallaxScales.Length; i++)
        {
            parallaxScales[i] = backgrounds[i].position.z * 1;
        }
    }

    // Update is called once per frame
    private void LateUpdate()
    { 
        for (int i = 0; i < backgrounds.Length; i++)
        {
            Vector3 parallax = (previousCamPos - transform.position) * (parallaxScales[i] / smoothing);
            backgrounds[i].position = new Vector3(backgrounds[i].position.x + parallax.x, backgrounds[i].position.y, backgrounds[i].position.z);
        }

        previousCamPos = transform.position;
    }

    
}