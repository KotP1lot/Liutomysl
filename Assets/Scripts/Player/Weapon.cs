using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Player player;
    public Collider2D weaponCollider;

    private Animator animator;
    private Dictionary<string, int> damage = new Dictionary<string, int>();

    private void Start()
    {
        animator = GetComponent<Animator>();

        var data = player.playerData;
        damage.Add("LightAttack_1", data.LightDamage1);
        damage.Add("LightAttack_2", data.LightDamage2);
        damage.Add("LightAttack_3", data.LightDamage3);
        damage.Add("StrongAttack_1", data.StrongDamage1);
        damage.Add("StrongAttack_2", data.StrongDamage2);
        damage.Add("StrongAttack_3", data.StrongDamage3);
    }

    private void Update()
    {
        animator.ResetTrigger("lightAttack");
        animator.ResetTrigger("strongAttack");

        if (player.CheckIfGrounded())
        {
            if (player.InputHandler.LightAttackInput) animator.SetTrigger("lightAttack");
            else if (player.InputHandler.StrongAttackInput) animator.SetTrigger("strongAttack");
        }
        
    }

    public void canContinue(int binaryBool)
    {
        animator.SetBool("canContinue",binaryBool==1? true:false);

        player.InputHandler.ResetAttackInput();
    }

    public delegate void onAnimStarted();
    public event onAnimStarted OnAnimStarted;
    private void onAnimationStarted()
    {
        OnAnimStarted.Invoke();
    }

    private void onAnimationFinished()
    {
        canContinue(0);
    }

    private void enableCollider(int binaryBool)
    {
        weaponCollider.enabled = binaryBool == 1 ? true : false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Enemy")
        {
            //Debug.Log("enemy hit");
            

            var animatorInfo = animator.GetCurrentAnimatorClipInfo(0);
            var currentAnim =animatorInfo[0].clip.name;
            Debug.Log(damage[currentAnim]);

            //damage enemy by damage[currentAnim]

            weaponCollider.enabled = false; 
        }
    }
}
