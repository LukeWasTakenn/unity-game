using Unity.VisualScripting;
using UnityEngine;

public class Spike : MonoBehaviour
{
   private void OnTriggerEnter2D(Collider2D collision)
   {
      if (!collision.gameObject.CompareTag("Player")) return;

      var player = collision.gameObject.GetComponent<Player>();
      player.TakeDamage(20);
   }
}
