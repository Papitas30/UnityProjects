using UnityEngine;
using UnityEngine.AI;

public class GhostAI : MonoBehaviour
{
    public Transform Player;
    NavMeshAgent agent;
    public int ghostSpeed;
    public bool isTargeting;
    public bool DeathRange;
    public LayerMask whatIsPlayer;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {

        DeathRange = Physics.CheckSphere(transform.position, 1.125f, whatIsPlayer);
        if (DeathRange)
        {
            agent.SetDestination(transform.position);
            agent.speed = 1;
            agent.isStopped = true;
        }
        else if (!DeathRange)
        {

            agent.isStopped = false;
            agent.speed = ghostSpeed;
            agent.SetDestination(Player.position);
        }
        

    }
}
