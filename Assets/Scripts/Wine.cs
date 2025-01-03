using UnityEngine;

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
        Destroy(gameObject);
        ScoreManager.Instance.CollectWine();
    }
}
