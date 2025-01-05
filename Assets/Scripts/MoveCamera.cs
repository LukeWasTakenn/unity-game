using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public float cameraSpeed = 0.05f;
    
    void FixedUpdate()
    {
        transform.Translate(cameraSpeed, 0, 0);
    }
}
