using UnityEngine;
using UnityEngine.AI;

public class ClickToMove : MonoBehaviour
{
    private NavMeshAgent navAgent;

    private void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        Debug.Log("NavMeshAgent found: " + (navAgent != null));
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse clicked");

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Ray hit: " + hit.collider.name + " at " + hit.point);
                if (navAgent != null)
                {
                    navAgent.SetDestination(hit.point);
                    Debug.Log("Destination set");
                }
            }
            else
            {
                Debug.Log("Ray hit nothing");
            }
            NavMeshHit navHit;
            if (NavMesh.SamplePosition(hit.point, out navHit, 1.0f, NavMesh.AllAreas))
            {
                navAgent.SetDestination(navHit.position);
                Debug.Log("Moving to " + navHit.position);
            }
            else
            {
                Debug.Log("Clicked point not on NavMesh");
            }

        }
    }
}
