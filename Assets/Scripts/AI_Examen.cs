using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Examen : MonoBehaviour
{

    enum State
    {
        Patrolling,
        Chasing,
        Attacking,
    }
       
    State currentState;
    UnityEngine.AI.NavMeshAgent agent;
    public int destinationIndex = 0;

    public Transform[] destinationPoints;
    public float visionRange;
    public float hitRange;
    public Transform player;

    void Awake()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }
    // Start is called before the first frame update
    void Start()
    {
        currentState = State.Patrolling;

        destinationIndex = (destinationIndex + 1) % destinationPoints.Length;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case State.Patrolling:
                Patrol();
            break;
            case State.Chasing:
                Chase();
            break;
            case State.Attacking:
                Attack();
            break;
        }
    }

    void Patrol()
    {
        agent.destination = destinationPoints[destinationIndex].position;
        
        
        if(Vector3.Distance(transform.position, player.position) < visionRange)
        {
            currentState = State.Chasing;
        }
    }

     void Chase()
    {
        agent.destination = player.position;

        if(Vector3.Distance(transform.position, player.position) > visionRange)
        {
            currentState = State.Patrolling;
        }

        if(Vector3.Distance(transform.position, player.position) < hitRange)
        {
            currentState = State.Attacking;
        }
    }

     void Attack()
    {
    agent.destination = player.position;
        
        Debug.Log("Attack");
        
        
        if(Vector3.Distance(transform.position, player.position) > hitRange)
        {
            currentState = State.Chasing;
        }
    }
}
