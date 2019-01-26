using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallexMenu : MonoBehaviour
{
    private RectTransform recTransform;
    private float yVelocity = 0.0F;
    private Quaternion calibrationQuaternion;

    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        recTransform = transform.Find("Frame").GetComponent<RectTransform>();
        CalibrateAccellerometer();
    }

    void FixedUpdate()
    {
        Vector3 accelarationRaw = Input.acceleration;
        Vector3 accelaration = FixAccelleration(accelarationRaw);
        float newPos = Mathf.SmoothDamp(0.0f, accelaration.x * 200, ref yVelocity, 0.1f);
        recTransform.rotation = Quaternion.Euler(0.0f, newPos, 0.0f);
    }

    void CalibrateAccellerometer()
    {
        Vector3 accelerationSnapshot = Input.acceleration;
        Quaternion rotateQuaternion = Quaternion.FromToRotation(new Vector3(0.0f, 0.0f, -1.0f), accelerationSnapshot);
        calibrationQuaternion = Quaternion.Inverse(rotateQuaternion);
    }

    Vector3 FixAccelleration(Vector3 accelaration)
    {
        Vector3 fixedAcceleration = calibrationQuaternion * accelaration;
        return fixedAcceleration;
    }

}
