using UnityEngine;

public class MainMenuCamera : MonoBehaviour
{
    void FixedUpdate()
    {
        transform.Translate(0.05f, 0, 0);
    }
}
