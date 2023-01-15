using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Player player;
    public Collider2D weaponCollider;

    public Animator animator { get; private set; }
    private Dictionary<string, int> damage = new Dictionary<string, int>();
    private Dictionary<string, int> staminaCost = new Dictionary<string, int>();

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

        staminaCost.Add("LightAttack_1", data.LightCost1);
        staminaCost.Add("LightAttack_2", data.LightCost2);
        staminaCost.Add("LightAttack_3", data.LightCost3);
        staminaCost.Add("StrongAttack_1", data.StrongCost1);
        staminaCost.Add("StrongAttack_2", data.StrongCost2);
        staminaCost.Add("StrongAttack_3", data.StrongCost3);
    }

    private void Update()
    {
        animator.ResetTrigger("lightAttack");
        animator.ResetTrigger("strongAttack");

        if (player.CheckIfGrounded())
        {
            if (!player.isDamaged && player.InputHandler.LightAttackInput && player.playerData.SP > 0) animator.SetTrigger("lightAttack");
            else if (!player.isDamaged && player.InputHandler.StrongAttackInput && player.playerData.SP > 0) animator.SetTrigger("strongAttack");
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

        var animatorInfo = animator.GetCurrentAnimatorClipInfo(0);
        var currentAnim = animatorInfo[0].clip.name;
        player.SpendStamina(staminaCost[currentAnim]);
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
            var animatorInfo = animator.GetCurrentAnimatorClipInfo(0);
            var currentAnim =animatorInfo[0].clip.name;

            var enemy = collision.GetComponent<Enemy>();
            enemy.GetDamaged(damage[currentAnim]+player.playerData.damage, player.PlayerCollider);

            weaponCollider.enabled = false; 
        }
        if(collision.tag=="Breakable")
        {
            var obj = collision.GetComponent<Breakable>();
            obj.GetDamaged();

            weaponCollider.enabled = false;
        }
    }
}
