using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveAgentBehaviour : MonoBehaviour, IBehaviour
{
    [SerializeField] private Transform targetTransform;
    private NavMeshAgent navMeshAgent;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    public float Evaluate()
    {
        return 1 / (this.gameObject.transform.position - targetTransform.position).magnitude;
    }
    public void Behaviour()
    {
        if (Vector3.Distance(targetTransform.position, transform.position) > 1.5f)
        {
            navMeshAgent.destination = targetTransform.position;
        }
        else
        {
            navMeshAgent.destination = navMeshAgent.transform.position;
            Debug.Log("SHOOT!");
        }
    }
}
