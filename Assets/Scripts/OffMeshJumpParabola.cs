using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class OffMeshJumpParabola : MonoBehaviour
{
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float jumpDuration = 0.5f;

    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoTraverseOffMeshLink = false;
    }

    private void Start()
    {
        StartCoroutine(TraverseOffMeshLinks());
    }

    private IEnumerator TraverseOffMeshLinks()
    {
        while (true)
        {
            if (agent.isOnOffMeshLink)
            {
                yield return StartCoroutine(DoParabolaJump());
                agent.CompleteOffMeshLink();
            }

            yield return null;
        }
    }

    private IEnumerator DoParabolaJump()
    {
        OffMeshLinkData data = agent.currentOffMeshLinkData;

        Vector3 startPos = transform.position;
        Vector3 endPos = data.endPos + Vector3.up * agent.baseOffset;

        float t = 0f;

        while (t < 1f)
        {
            Vector3 dir = (endPos - startPos).normalized;
            if (dir != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    Quaternion.LookRotation(dir),
                    10f * Time.deltaTime
                );
            }

            float yOffset = jumpHeight * 4f * (t - t * t);
            transform.position = Vector3.Lerp(startPos, endPos, t) + Vector3.up * yOffset;

            t += Time.deltaTime / jumpDuration;
            yield return null;
        }

        transform.position = endPos;
    }
}