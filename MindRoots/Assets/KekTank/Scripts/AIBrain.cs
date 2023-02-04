using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AIBrain : MonoBehaviour
{
    static public void StateMachine<Par0, Par1, Par2, Par3>(AIstates State, P_Enemy AI, Par0 arg0 = null, Par1 arg1 = null, Par2 arg2 = null, Par3 arg3 = null)
        where Par0 : class
        where Par1 : class
        where Par2 : class
        where Par3 : class
    {
        switch(State)
        {
            case AIstates.None: break;
            case AIstates.Roam:
                AI.RandomRoam();
                break;
            case AIstates.Follow:
                AI.FollowTarget(arg0 as Transform);
                break;
            case AIstates.Attack:
                (arg0 as UnityEvent).Invoke();
                break;
            case AIstates.Wait:

                break;
            case AIstates.Patrol:

                break;
            case AIstates.Flee:

                break;
            case AIstates.Harvest:

                break;
        }
    }
}
