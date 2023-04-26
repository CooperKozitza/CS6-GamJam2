using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathFollow : MonoBehaviour
{
    [Range(0, 10)]
    public float speed = 1.0f;
    public Transform pathParent;

    public Transform playerTransform;

    private Vector3[] path { get; set; }
    public LayerMask mask;

    private Coroutine followPathCoroutine;

    private NavMeshAgent agent;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = false;

        path = new Vector3[pathParent.childCount];
        for (int i = 0; i < path.Length; i++)
        {
            path[i] = pathParent.GetChild(i).position;
        }

        followPathCoroutine = StartCoroutine(FollowPath());
    }

    private IEnumerator FollowPath()
    {
        int targetIndex = 0;

        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, path[targetIndex], speed * Time.deltaTime);
            transform.LookAt(path[targetIndex]);
            if (transform.position == path[targetIndex])
            {
                targetIndex = (targetIndex + 1) % path.Length;
            }
            yield return null;
        }
    }

    void OnDrawGizmos()
    {
        Vector3 startPosition = pathParent.GetChild(0).position;
        Vector3 previousPosition = startPosition;

        foreach (Transform waypoint in pathParent)
        {
            Gizmos.DrawSphere(waypoint.position, .3f);
            Gizmos.DrawLine(previousPosition, waypoint.position);
            previousPosition = waypoint.position;
        }

        Gizmos.DrawLine(previousPosition, startPosition);
    }

    void Update()
    {

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, 100, mask, QueryTriggerInteraction.Ignore))
        {
            print(hitInfo.collider.gameObject.name);
            if (hitInfo.collider.gameObject.CompareTag("Player"))
            {
                Debug.DrawLine(ray.origin, hitInfo.point, Color.red);
                StopCoroutine(followPathCoroutine);

                agent.enabled = true;
                InvokeRepeating("followPlayer", 0f, 0.5f);
            }
            else
            {
                Debug.DrawLine(ray.origin, hitInfo.point, Color.yellow);
            }
        }
        else
        {
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * 100, Color.green);
        }

    }

    void followPlayer()
    {
        agent.destination = playerTransform.position;
    }
}
