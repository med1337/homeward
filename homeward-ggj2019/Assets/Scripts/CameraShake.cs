using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // How long the object should shake for.
    private float shakeDuration = 0.0f;

    // Amplitude of the shake. A larger value shakes the camera harder.
    private float shakeAmount = 0.7f;
    private float decreaseFactor = 1.0f;

    Vector3 originalPos;

    void Update()
    {
        if (shakeDuration > 0)
        {
            originalPos.z = transform.position.z;

            transform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
    }

    public void ShakeCam(float _duration, float _intensity)
    {
        originalPos = transform.localPosition;
        shakeDuration = _duration;
        shakeAmount = _intensity;
    }
}
