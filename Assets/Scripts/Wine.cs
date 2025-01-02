using UnityEngine;

public class Wine : MonoBehaviour, ICollectible
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;
        Collect();
    }

    public void Collect()
    {
        Destroy(gameObject);
        ScoreManager.Instance.CollectWine();
    }
}
