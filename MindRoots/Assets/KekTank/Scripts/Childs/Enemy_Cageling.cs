using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Cageling : P_Enemy
{
    [Header("Cageling Stats")]
    public float AttackRange = 10;
    public float AttackBetween = 1;

    float waitingtoAttack = 0;
    public override void FollowTarget()
    {
        base.FollowTarget();

        float dis = Vector3.Distance(TargetToFollow.position, transform.position);
        if (dis < AttackRange)
        {
            State = AIstates.Attack;
            agent.ResetPath();
            agent.destination = transform.position;

            waitingtoAttack = 0;
        }
    }

    public override void Attacking()
    {
        if (waitingtoAttack == 0)
        {
            base.Attacking();
            waitingtoAttack = AttackBetween;
        }
        else
        {
            waitingtoAttack -= Time.deltaTime;
            if (waitingtoAttack < 0) waitingtoAttack = 0;
        }
        //Debug.Log("new attack");
    }
}
