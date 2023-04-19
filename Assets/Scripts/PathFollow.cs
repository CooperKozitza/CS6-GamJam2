using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollow : MonoBehaviour
{
    [Range(0, 10)]
    public float speed = 1.0f;
    public Transform pathParent;

    private Vector3[] path { get; set; }

    void Awake()
    {
        path = new Vector3[pathParent.childCount];
        for (int i = 0; i < path.Length; i++)
        {
            path[i] = pathParent.GetChild(i).position;
        }

        StartCoroutine(FollowPath());
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
}
