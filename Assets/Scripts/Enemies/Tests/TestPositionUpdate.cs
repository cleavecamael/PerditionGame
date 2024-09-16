using UnityEngine;

public class TestPositionUpdate : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3Position testPosition;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        testPosition.pos = this.transform.position;
    }
}
