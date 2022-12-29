using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActState : EnemyOnGroundState
{
    List<EnemyState> possibleActs;

    public EnemyActState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string aminBoolName) : base(enemy, stateMachine, enemyData, aminBoolName)
    {
        possibleActs = new List<EnemyState>();
        if (enemyData.canAttack) possibleActs.Add(enemy.AttackState);
        if (enemyData.canShoot) possibleActs.Add(enemy.ShootState);
        if (enemyData.canBlock) possibleActs.Add(enemy.BlockState);
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        enemy.SetVelocityX(0f);

        var rand = Random.Range(0,possibleActs.Count);
        Debug.Log(rand);

        stateMachine.ChangeState(possibleActs[rand]);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
