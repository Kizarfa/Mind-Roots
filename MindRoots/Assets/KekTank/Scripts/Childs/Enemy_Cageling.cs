using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Cageling : P_Enemy
{
    [Header("Cageling Stats")]
    public float AttackRange = 10;

    public override void FollowTarget(Transform target)
    {
        base.FollowTarget(target);

        float dis = Vector3.Distance(target.position, transform.position);
        if (dis <= AttackRange) State = AIstates.Attack;
    }

    public override void Attacking()
    {
        base.Attacking();
        Debug.Log("new attack");
    }
}
