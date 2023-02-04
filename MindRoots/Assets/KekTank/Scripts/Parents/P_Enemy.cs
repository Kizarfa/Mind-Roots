using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public enum AIstates
{
    None,       //Hiçbir þey yapmaz
    Roam,       //Rasgele dolaþýr
    Follow,     //Bizi yakalamaya çalýþýr
    Attack,     //Bize vurmaya çalýþýr
    Wait,       //Bizi ya da kendi bekleme süresinin gelmesini bekleyebilir
    Patrol,     //Belirli bir patikayý takip edebilir
    Flee,       //Bizden uzaða kaçýp saldýrabilir
    Harvest     //Elektrik toplamaya gidebilir
}
public abstract class P_Enemy : MonoBehaviour
{
    [Header("Stats")]
    public int Health = 10;
    public int Power = 1;
    public int Speed = 1;

    [Header("State")]
    public AIstates State = AIstates.None;
    [Tooltip("State is considered as None while 'This' > 0. Decreases in time.")]
    public float Mindlessness = 0;
    public Transform TargetToFollow = null;

    [Header("Machine Parts")]
    public Collider HitBox = null;
    public Collider RoamArea = null;
    NavMeshAgent agent = null;
    Rigidbody rig = null;

    private void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        rig = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        //AIBrain.StateMachine<Transform, UnityEvent, Transform, Transform>(State, this);
        if (Mindlessness > 0)
        {
            None();
        }
        else
        {
            switch (State)
            {
                case AIstates.None: break;
                case AIstates.Roam:
                    RandomRoam();
                    break;
                case AIstates.Follow:
                    FollowTarget(TargetToFollow);
                    break;
                case AIstates.Attack:
                    Attacking();
                    break;
                case AIstates.Wait:
                    Waiting();
                    break;
                case AIstates.Patrol:
                    Patrolling();
                    break;
                case AIstates.Flee:
                    Fleeing();
                    break;
                case AIstates.Harvest:
                    Harvesting();
                    break;
            }
        }

    }

    public virtual void None()
    {
        Mindlessness -= Time.deltaTime;
        if (Mindlessness < 0)
        {
            Mindlessness = 0;
        }
    }

    public virtual void RandomRoam()
    {
        if (Vector3.Distance(agent.destination, transform.position) <= 5)
        {
            Bounds ra = RoamArea.bounds;

            Vector3 RPos = new Vector3(0, 0, 0);
            RPos.x = ra.center.x + Random.Range(-ra.extents.x, ra.extents.x);
            RPos.y = ra.center.y + Random.Range(-ra.extents.y, ra.extents.y);
            RPos.z = ra.center.z + Random.Range(-ra.extents.z, ra.extents.z);

            NavMeshPath p = new NavMeshPath();
            if (agent.CalculatePath(RPos, p)) agent.path = p;
        }
    }

    public virtual void FollowTarget(Transform Target)
    {
        NavMeshPath p = new NavMeshPath();
        agent.CalculatePath(Target.position, p);
    }

    public virtual void TakeHit(int Damage, Vector3 HitPos, int pushStrength = 10, int confuse = 1)
    {
        Health -= Damage;
        Mindlessness = confuse;

        rig.AddExplosionForce(pushStrength, transform.position + (HitPos - transform.position).normalized, 5);
    }

    public virtual void Attacking()
    {

    }

    public virtual void Waiting()
    {

    }

    public virtual void Patrolling()
    {

    }

    public virtual void Fleeing()
    {

    }

    public virtual void Harvesting()
    {

    }

    public virtual void Death()
    {

    }

}
