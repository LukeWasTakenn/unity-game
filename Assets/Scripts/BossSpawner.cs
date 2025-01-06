using System;
using SimpleAudioManager;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
   public GameObject bossPrefab;
   public GameObject bossHealthBar;
   private GameObject songPlayerObject;

   private void Awake()
   {
      songPlayerObject = GameObject.FindGameObjectWithTag("AudioManager");
   }


   private void OnTriggerEnter2D(Collider2D collision)
   {
      if (!collision.gameObject.CompareTag("Player")) return;
      bossPrefab.SetActive(true);
      bossHealthBar.SetActive(true);
      
      var songPlayer = songPlayerObject.GetComponent<Manager>();
      songPlayer.SetIntensity(2, 3f, 3f);
      
      Destroy(gameObject);
   }
}
