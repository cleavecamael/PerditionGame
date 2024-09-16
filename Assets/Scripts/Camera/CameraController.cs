using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform player;
    private float shakeDuration;
    private float shakeAmount;
    public bool isShaking = false;
    private Vector3 originalPosition;

    public void ShakeCamera(Vector2 v)
    {
        if (!isShaking)
        {
            isShaking = true;
            originalPosition = transform.position;
            shakeAmount = v.x;
            shakeDuration = v.y;
            StartCoroutine(Shake());
        }
    }

    IEnumerator Shake()
    {
        float duration = shakeDuration;
        while (duration > 0)
        {
            float x = originalPosition.x + Random.Range(-1f, 1f) * shakeAmount;
            float y = originalPosition.y + Random.Range(-1f, 1f) * shakeAmount;

            transform.localPosition = new Vector3(x, y, originalPosition.z);
            duration -= Time.deltaTime;
            yield return null;
            Vector3 cameraPosition = player.position;
            cameraPosition.z = transform.position.z;
            originalPosition = cameraPosition;
            transform.position = cameraPosition;
        }
        isShaking = false;
        transform.localPosition = originalPosition;
    }
    void Awake()
    {
        player = GameObject.Find("Player").transform;
    }

    void LateUpdate()
    {
        if (!isShaking)
        {
            Vector3 cameraPosition = player.position;
            cameraPosition.z = transform.position.z;
            transform.position = cameraPosition;
        }
    }
}