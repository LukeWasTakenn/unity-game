using SmallHedge.SoundManager;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Teleporter : Interactable
{
    public Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public override void Interact()
    {
        animator.Play("Activate");
        GameManager.WineCollected += GameManager.WineCollectedThisLife;
        GameManager.WineCollectedThisLife = 0;
        SoundManager.PlaySound(SoundType.TeleporterActivate, null, 0.4f);
    }

    public void OnAnimationEnd()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
