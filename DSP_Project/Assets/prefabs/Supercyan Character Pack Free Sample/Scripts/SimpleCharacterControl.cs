using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;

public class SimpleCharacterControl : MonoBehaviour {



    [SerializeField] private float m_moveSpeed = 2;
    [SerializeField] private Animator m_animator;
    private NavMeshAgent agent;
    private int destPoint;
    public Transform[] goals;

    private void Start()
    {
        
        destPoint = 0;
        agent = GetComponent<NavMeshAgent>();
        Debug.Log(agent.name);
        agent.autoBraking = false;
        m_animator.SetFloat("MoveSpeed", m_moveSpeed);
        GotoNextPoint();
    }
    void Update() {
        if (!agent.pathPending && agent.remainingDistance < .5f)
        {
            GotoNextPoint();
        }
    }

    void GotoNextPoint()
    {

        // Returns if no points have been set up
        if (goals.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        agent.destination = goals[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % goals.Length;
    }

}

   




