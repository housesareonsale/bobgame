using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    [Range(0f, 2f)]
    public float Intensity;

    Transform target;
    Vector3 intialPosition;
    bool isShaking = false;
    float pendingShakeDuration = 0f;

    void Start()
    {
        target = GetComponent<Transform>();
        intialPosition = target.localPosition;
    }


    public void Shake(float duration)
    {
        if(duration > 0)
        {
            pendingShakeDuration += duration;
        }
    }

    void Update()
    {
        if(pendingShakeDuration > 0 && !isShaking)
        {
            intialPosition = target.localPosition;
            StartCoroutine(DoShake());
        }
    }

    IEnumerator DoShake()
    {
        isShaking = true;

        var startTime = Time.realtimeSinceStartup;

        while(Time.realtimeSinceStartup < startTime + pendingShakeDuration)
        {
            var randomPoint = new Vector3(Random.Range(-1f,1f) * Intensity, Random.Range(-1f,1f) * Intensity, 0);
            target.localPosition = intialPosition + randomPoint;
            yield return null;
        }

        pendingShakeDuration = 0f;
        target.localPosition = intialPosition;
        isShaking = false;
    }
}