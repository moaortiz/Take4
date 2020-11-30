using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : MonoBehaviour
{
    public List<GameObject> wayPoints;
    private NavMeshAgent agent;
    private const float WP_THRESHOLD = 5.0f;
    private GameObject currentWP;
    private int currentWPIndex = -1;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentWP = GetNextWaypoint();
        agent.SetDestination(currentWP.transform.position);
    }

    GameObject GetNextWaypoint()
    {
        currentWPIndex++;
        if (currentWPIndex == wayPoints.Count)
        {
            currentWPIndex = 0;
        }
        return wayPoints[currentWPIndex];
    }

    // Update is called once per frame
    public void PatrolWayPointes()
    {
        if(Vector3.Distance(transform.position, currentWP.transform.position) < WP_THRESHOLD)
        {
            currentWP = GetNextWaypoint();
            agent.SetDestination(currentWP.transform.position);
        }
    }
}
