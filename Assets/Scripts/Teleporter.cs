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
    }

    public void OnAnimationEnd()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
