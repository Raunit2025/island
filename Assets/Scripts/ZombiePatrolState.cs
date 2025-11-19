using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class ZombiePatrolingState : StateMachineBehaviour
{
    float timer;
    public float patrolingTime = 10f;

    Transform player;
    NavMeshAgent agent;

    public float detectionArea = 18f;
    public float patrolSpeed = 2f;

    List<Transform> waypointsList = new List<Transform>();

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = animator.GetComponentInParent<NavMeshAgent>();

        if (agent == null)
        {
            Debug.LogError("NavMeshAgent NOT found on zombie!");
            return;
        }

        if (!agent.isOnNavMesh)
        {
            Debug.LogError("Zombie is NOT placed on NavMesh!");
            return;
        }

        agent.speed = patrolSpeed;
        timer = 0;

        GameObject waypointCluster = GameObject.FindGameObjectWithTag("Waypoints");
        if (waypointCluster == null)
        {
            Debug.LogError("No Waypoints object found!");
            return;
        }

        foreach (Transform t in waypointCluster.transform)
            waypointsList.Add(t);

        if (waypointsList.Count == 0)
        {
            Debug.LogError("Waypoints list is EMPTY!");
            return;
        }

        agent.SetDestination(waypointsList[Random.Range(0, waypointsList.Count)].position);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!agent || !agent.isOnNavMesh) return;

        if (agent.remainingDistance <= agent.stoppingDistance)
            agent.SetDestination(waypointsList[Random.Range(0, waypointsList.Count)].position);

        timer += Time.deltaTime;
        if (timer > patrolingTime)
            animator.SetBool("isPatroling", false);

        float distanceFromPlayer = Vector3.Distance(player.position, animator.transform.position);
        if (distanceFromPlayer < detectionArea)
            animator.SetBool("isChasing", true);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (agent && agent.isOnNavMesh)
            agent.SetDestination(agent.transform.position);
    }
}
