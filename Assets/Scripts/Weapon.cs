using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private Animator animator;
    private string[][] animName;
    private Dictionary<int, AttackType> attackInputs;
    private bool canStartAnim;
    private int countAttack;

    private void Start()
    {
        animator = GetComponent<Animator>();
        animName = new string[2][];
        animName[0] = new string[3] { "LightAttack_1", "LightAttack_2", "LightAttack_3" };
        animName[1] = new string[3] { "StrongAttack_1", "StrongAttack_2", "StrongAttack_3" };
        attackInputs = new Dictionary<int, AttackType>();
        attackInputs.Add(0, AttackType.Light);
        attackInputs.Add(1, AttackType.Light);
        attackInputs.Add(2, AttackType.Light);

        canStartAnim = true;
        countAttack = 0;
        StartAnimPlay();
    }

    public void ChangeAttackInputs(Dictionary<int, AttackType> attackInputs)
    {
        this.attackInputs = attackInputs;

        if (canStartAnim)
        {
            StartAnimPlay();
        }
    }

    public delegate void onAnimFinished();
    public event onAnimFinished OnAnimFinished;
    private void onAnimationFinished()
    {
        canStartAnim = true;
        // OnAnimFinished.Invoke();
        StartAnimPlay(); 
    }
    private void StartAnimPlay()
    {
        if (attackInputs.ContainsKey(countAttack))
        {
            string[] typeAnimAttack;
            switch (attackInputs[countAttack]) {
                case AttackType.Light:
                    typeAnimAttack = animName[0];
                    break;
                case AttackType.Strong:
                    typeAnimAttack = animName[1];
                    break;
                default:
                    typeAnimAttack = new string[1] { "Idle" };
                    break;
            }

            animator.CrossFade(typeAnimAttack[countAttack], 0f, 0);
            if (countAttack + 1 > 2) countAttack = 0;  else countAttack++;
            canStartAnim = false;
        }
        else
        {
            animator.CrossFade("Idle", 0f, 0);
            countAttack = 0;
            canStartAnim = true;
        }
    }
}
