using UnityEngine;
using UnityEngine.SceneManagement;

public class FallDetection : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        GameManager.WineCollectedThisLife = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
