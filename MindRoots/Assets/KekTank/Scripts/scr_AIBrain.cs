using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[System.Serializable]
public class StateStruct
{
    public string name = "";

    [Header("Positioning")]
    public Vector3 WanderPosition = new Vector3();
    public Vector3 WanderDirection = new Vector3();
    public bool StopMove = false;
    public bool RandomMove = false;

    [Header("Action Calls")]
    public UnityEvent AtStart = null;
    public UnityEvent DuringAll = null;
    public UnityEvent DuringMove = null;
    public UnityEvent DuringStable = null;
    public UnityEvent Reaction = null;
    public UnityEvent AtEnd = null;
}

[System.Serializable]
public class scr_AIBrain : MonoBehaviour
{
    public NavMeshAgent agent = null;
    NavMeshPath path = null;

    public int StateIndex = 0;
    public StateStruct[] States = new StateStruct[1];

    private void FixedUpdate()
    {
        StateStruct st = States[StateIndex];

        if (!st.StopMove)
        {
            agent.SamplePathPosition(0, 10, out NavMeshHit hit);
            

            agent.SetDestination(st.WanderPosition);
        }

    }

}
