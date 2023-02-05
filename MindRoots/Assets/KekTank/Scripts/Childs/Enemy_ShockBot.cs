using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_ShockBot : P_Enemy
{
    public Animator anor;

    [Header("ShockBot Stats")]
    public float AttackRange = 10;
    public float AttackBetween = 1;

    float waitingtoAttack = 0;
    public override void FollowTarget()
    {
        base.FollowTarget();
        anor.SetBool("Walking", true);

        float dis = Vector3.Distance(TargetToFollow.position, transform.position);
        if (dis < AttackRange)
        {
            State = AIstates.Attack;
            agent.ResetPath();
            agent.destination = transform.position;

            waitingtoAttack = 0;

            anor.SetBool("Walking", false);
        }
    }

    public override void Attacking()
    {
        if (waitingtoAttack == 0)
        {
            anor.SetBool("Attacking", true);
            base.Attacking();
            waitingtoAttack = AttackBetween;
        }
        else
        {
            waitingtoAttack -= Time.deltaTime;
            if (waitingtoAttack < 0) waitingtoAttack = 0;

            anor.SetBool("Attacking", false);
        }
        //Debug.Log("new attack");
    }

    public override void None()
    {
        base.None();

        anor.SetBool("Walking", false);
        anor.SetBool("Attacking", false);
    }

    public override void RandomRoam()
    {
        base.RandomRoam();

        anor.SetBool("Walking", true);
    }
}
