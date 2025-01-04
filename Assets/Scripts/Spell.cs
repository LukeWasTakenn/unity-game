using System.Dynamic;
using NUnit.Framework.Constraints;
using UnityEngine;

public class Spell : MonoBehaviour
{
    public LayerMask playerLayer;
    public float rayDistance = 3.2f;
    public float rayWidth = 2.5f;
    public float rayHeight = 2.8f;
    public int damage = 20;
    
    private Collider2D hit;

    // Update is called once per frame
    void Update()
    {
        hit = Physics2D.OverlapBox(new Vector2(transform.position.x, transform.position.y), new Vector3(rayWidth, rayHeight, 0), 0, playerLayer);
    }

    public void DoDamage()
    {
        if (!hit) return;
        
        var player = hit.gameObject.GetComponent<Player>();
        player.TakeDamage(damage);
    }
    
    public void OnAnimationFinish()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        // Gizmos.DrawRay(new Vector2(transform.position.x, transform.position.y), Vector2.down * rayDistance);
        Gizmos.DrawWireCube(new Vector2(transform.position.x, transform.position.y - 1.2f), new Vector3(rayWidth, rayHeight, 0));
    }
}
