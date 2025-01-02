using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public int wineCollected = 0;
    public TMP_Text wineText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void CollectWine()
    {
        Instance.wineCollected++;
        wineText.text = Instance.wineCollected.ToString();
    }
}
