using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Player player;

    private Animator animator;
    private string[][] animName;
    private Dictionary<int, AttackType> attackInputs;
    private bool canStartAnim;
    private int countAttack;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animator.ResetTrigger("lightAttack");
        animator.ResetTrigger("strongAttack");

        if (player.InputHandler.LightAttackInput) animator.SetTrigger("lightAttack");
        else if (player.InputHandler.StrongAttackInput) animator.SetTrigger("strongAttack");

        
    }

    public void ChangeAttackInputs(Dictionary<int, AttackType> attackInputs)
    {
        this.attackInputs = attackInputs;

        if (canStartAnim)
        {
            StartAnimPlay();
        }
    }

    public void canContinue(int binaryBool)
    {
        animator.SetBool("canContinue",binaryBool==1? true:false);

        player.InputHandler.ResetAttackInput();
    }

    public delegate void onAnimFinished();
    public event onAnimFinished OnAnimFinished;
    private void onAnimationFinished()
    {
        canContinue(0);

        //canStartAnim = true;
        // OnAnimFinished.Invoke();
        //StartAnimPlay(); 
    }
    private void StartAnimPlay()
    {
        //if (attackInputs.ContainsKey(countAttack))
        //{
        //    string[] typeAnimAttack;
        //    switch (attackInputs[countAttack]) {
        //        case AttackType.Light:
        //            typeAnimAttack = animName[0];
        //            break;
        //        case AttackType.Strong:
        //            typeAnimAttack = animName[1];
        //            break;
        //        default:
        //            typeAnimAttack = new string[1] { "Idle" };
        //            break;
        //    }

        //    animator.CrossFade(typeAnimAttack[countAttack], 0f, 0);
        //    if (countAttack + 1 > 2) countAttack = 0;  else countAttack++;
        //    canStartAnim = false;
        //}
        //else
        //{
        //    animator.CrossFade("Idle", 0f, 0);
        //    countAttack = 0;
        //    canStartAnim = true;
        //}
    }
}
