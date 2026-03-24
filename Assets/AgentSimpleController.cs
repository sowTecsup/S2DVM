using UnityEngine;
using UnityEngine.AI;
public class AgentSimpleController : MonoBehaviour
{
    public Transform Target;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();  
    }

    // Update is called once per frame
    void Update()
    {
        if(Target != null)
        {
            agent.SetDestination(Target.position);
        }
    }
}
