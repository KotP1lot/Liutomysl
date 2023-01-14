using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAlerts : MonoBehaviour
{
    public Sprite detected;
    public Sprite lost;

    private SpriteRenderer spriteRenderer;
    private Animator animator;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    public void DetectedAlert()
    {
        spriteRenderer.sprite = detected;
        animator.SetTrigger("StartAnim");
    }

    public void LostAlert()
    {
        spriteRenderer.sprite = lost;
        animator.SetTrigger("StartAnim");
    }

    public void Flip(int direction)
    {
        transform.localScale = new Vector3(0.7f * direction, 0.7f, 1f);
    }
}
