using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PhantomPlayer : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    
    public void SetStartPosition(Vector3 position)
    {
        gameObject.SetActive(true);
        transform.position = position;
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void SetTarget(Vector3 target)
    {
        agent.SetDestination(target);
    }

    public bool CanGetThroughTheMaze()
    {
        if (Vector3.Distance(agent.destination, transform.position) <= 0.25)
        {
            gameObject.SetActive(false);
            return true;
        }
           
        return false;
    }

}
