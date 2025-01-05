using System.Collections;
using NUnit.Framework.Internal;
using SmallHedge.SoundManager;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Wine : MonoBehaviour, ICollectible
{
    public float bobSpeed = 5f;
    public float bobHeight = 0.5f;

    private Vector2 pos;
    
    private void Start()
    {
        pos = transform.position;
    }
    
    private void Update()
    {
        float newY = Mathf.Sin(Time.time * bobSpeed) * bobHeight + pos.y;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z) ;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;
        Collect();
    }

    public void Collect()
    {
        ScoreManager.Instance.CollectWine();
        GameManager.WineCollected++;
        SoundManager.PlaySound(SoundType.WinePickup);
        Destroy(gameObject);
    }
}
