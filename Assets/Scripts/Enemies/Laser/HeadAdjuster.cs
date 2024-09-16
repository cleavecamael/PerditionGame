using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HeadAdjuster : MonoBehaviour
{
    [SerializeField] private Transform leftHead;
    [SerializeField] private Transform rightHead;
    [SerializeField] private Transform main;
    // Start is called before the first frame update;

    // Update is called once per frame

    private void Update()
    {
        adjustHead();
    }
    public void adjustHead()
    {
        if (main.GetComponent<SpriteRenderer>().flipX)
        {
            moveAll(computeDisplacement(leftHead.position));
    
            transform.position = leftHead.position;
 

        }
        else
        {
            moveAll(computeDisplacement(rightHead.position));
            transform.position = rightHead.position;
        }
    }
    Vector2 computeDisplacement(Vector3 position)
    {
        return position - transform.position;
    }

    private void moveAll(Vector3 direction)
    {
        foreach (Transform child in transform)
        {
            child.position += direction;
        }
    }
}
