using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float shakeDuration = 5f;
    public float shakeAmount = 1f;
    public bool start = false;
    private Vector3 originalPosition;
    void Update()
    {
        if (start)
        {
            ShakeCamera();
        }
    }

    public void ShakeCamera()
    {
        StartCoroutine(Shake());
    }

    IEnumerator Shake()
    {
        Debug.Log("Shake Camera");
        float duration = shakeDuration;
        while (duration > 0)
        {
            originalPosition = transform.position;

            float x = originalPosition.x + Random.Range(-1f, 1f) * shakeAmount;
            float y = originalPosition.y + Random.Range(-1f, 1f) * shakeAmount;

            transform.localPosition = new Vector3(x, y, originalPosition.z);
            duration -= Time.deltaTime;

            yield return null;

        }
        start = false;
        transform.localPosition = originalPosition;
    }
}
