using UnityEngine;

public class BossSpawner : MonoBehaviour
{
   public GameObject bossPrefab;
   public GameObject bossHealthBar;
   
   private void OnTriggerEnter2D(Collider2D collision)
   {
      if (!collision.gameObject.CompareTag("Player")) return;
      bossPrefab.SetActive(true);
      bossHealthBar.SetActive(true);
      Destroy(gameObject);
   }
}
