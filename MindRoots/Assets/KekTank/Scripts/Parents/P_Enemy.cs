using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public enum AIstates
{
    None,       //Hi�bir �ey yapmaz
    Roam,       //Rasgele dola��r
    Follow,     //Bizi yakalamaya �al���r
    Attack,     //Bize vurmaya �al���r
    Wait,       //Bizi ya da kendi bekleme s�resinin gelmesini bekleyebilir
    Patrol,     //Belirli bir patikay� takip edebilir
    Flee,       //Bizden uza�a ka��p sald�rabilir
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
    public NavMeshAgent agent = null;
    public Rigidbody rig = null;

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
                    FollowTarget();
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
            rig.velocity = Vector3.zero;
        }
    }

    public virtual void RandomRoam()
    {
        if (transform.parent.GetComponent<scr_ArenaStarter>().PlayerIsInside)
        {
            State = AIstates.Follow;
            TargetToFollow = FindObjectOfType<scr_PlayerMachine>().gameObject.transform;
            return;
        }

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

    public virtual void FollowTarget()
    {
        if (Vector3.Distance(agent.destination, TargetToFollow.position) > 0)
        {
            NavMeshPath p = new NavMeshPath();
            if (agent.CalculatePath(TargetToFollow.position, p)) agent.path = p;
        }

    }

    public virtual void TakeHit(int Damage, Vector3 HitPos, int pushStrength = 200, int confuse = 1)
    {
        Health -= Damage;
        Mindlessness = confuse;

        rig.AddExplosionForce(pushStrength, transform.position + (HitPos - transform.position).normalized, 5);

        if (Health <= 0) Death();
    }

    public virtual void Attacking()
    {
        if (HitBox.gameObject.activeInHierarchy) 
        {
            HitBox.gameObject.SetActive(false);
            State = AIstates.Roam;
        }
        else
        {
            HitBox.gameObject.SetActive(true);
        }

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
        transform.parent.GetComponent<scr_ArenaStarter>().RotDecrease();

        Destroy(gameObject);
    }

}