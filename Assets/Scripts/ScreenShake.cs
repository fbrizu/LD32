using UnityEngine;
using System.Collections;

public class ScreenShake : MonoBehaviour
{
    public Vector3 initialPosition;

    float shakeTime;
    float shakeIntensity;
    
    // Update is called once per frame
    void Update()
    {
        if (shakeTime == -100)
        {
            Vector3 tmp = Random.insideUnitSphere * shakeIntensity;
            tmp.z = initialPosition.z;
            transform.position = tmp;
        }
        else if (shakeTime > 0)
        {
            Vector3 tmp = initialPosition + Random.insideUnitSphere * shakeIntensity;
            tmp.z = initialPosition.z;
            transform.position = tmp;
            shakeTime -= Time.deltaTime;
        }
        else
        {
            transform.position = initialPosition;
        }
    }

    public void StartShake(float timer, float intensity)
    {
        shakeTime = timer;
        shakeIntensity = intensity;
    }
}
