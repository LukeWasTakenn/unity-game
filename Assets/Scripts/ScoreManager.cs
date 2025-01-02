using System;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public TMP_Text wineCountText;
    
    int wineCount = 0;
    
    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        wineCountText.text = wineCount.ToString();
    }

    public void CollectWine()
    {
        wineCount++;
        wineCountText.text = wineCount.ToString();
    }
}
