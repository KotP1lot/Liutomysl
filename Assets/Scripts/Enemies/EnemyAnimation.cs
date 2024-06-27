using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private Enemy parentObject;

    private void Start()
    {
        parentObject = transform.parent.GetComponent<Enemy>();
    }

    public void AnimationTrigger() => parentObject.AnimationTrigger();
    public void EnableAttackCollider(int binaryBool) { parentObject.EnableAttackCollider(binaryBool); }
    public void AnimationFinishTrigger() => parentObject.AnimationFinishTrigger();
}
