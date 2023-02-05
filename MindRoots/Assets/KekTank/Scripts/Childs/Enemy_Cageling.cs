using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Enemy_Cageling : P_Enemy
{
    public GameObject bullet;

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
            Vector3 dir = FindObjectOfType<scr_PlayerMachine>().gameObject.transform.position - transform.position;

            GameObject b = Instantiate(bullet);
            b.transform.position = transform.position;

            P_Bullet pb = b.GetComponent<P_Bullet>();
            pb.Direction = dir;

            b.GetComponent<Rigidbody>().velocity = dir * pb.Speed;

            b.transform.LookAt(FindObjectOfType<scr_PlayerMachine>().Aim.position);
            b.transform.Rotate(new Vector3(-90, 0, 0));

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
