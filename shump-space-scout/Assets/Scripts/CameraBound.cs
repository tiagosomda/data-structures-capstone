using UnityEngine;

public class CameraBound : MonoBehaviour
{
    private Camera gameCamera;
    private float cameraOrthographicSize;
    private Vector3 cameraPosition;

    public float MinY { get; private set; }
    public float MaxY { get; private set; }
    public float MinX { get; private set; }
    public float MaxX { get; private set; }

    void Start()
    {
        gameCamera = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        if(cameraOrthographicSize != gameCamera.orthographicSize || transform.position != cameraPosition)
        {
            SetBoundaries();
            cameraPosition = transform.position;
            cameraOrthographicSize = gameCamera.orthographicSize;
        }
    }
    private void SetBoundaries()
    {       
        var vertExtent = gameCamera.orthographicSize;    
        var horzExtent = vertExtent * Screen.width / Screen.height;
 
        MinX = -horzExtent + transform.position.x;
        MaxX = horzExtent  + transform.position.x;
        MinY = -vertExtent + transform.position.y;
        MaxY = vertExtent  + transform.position.y;
    }
}
