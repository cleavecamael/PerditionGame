using UnityEngine;

public class CameraBoundingBox : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject mainCamera;
    new Camera camera;
    Bounds bounds;
    void Awake()
    {
        mainCamera = GameObject.Find("Main Camera");
        camera = mainCamera.GetComponent<Camera>();
        BoxCollider2D  boxCollider = GetComponent<BoxCollider2D>();
        bounds = boxCollider.bounds;
        bounds.Encapsulate(CameraExtensions.OrthographicBounds(camera));
        boxCollider.size =  bounds.size - new Vector3(3f, 5f, 0) ;
        boxCollider.offset = bounds.center - boxCollider.transform.position;
    }


}
