using System.Collections;
using UnityEngine;

public class CameraPath : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 3f;
    public float waitTime = 2f;

    private void Start()
    {
        StartCoroutine(FollowPath());
    }

    IEnumerator FollowPath()
    {
        foreach (Transform point in waypoints)
        {
            while (Vector3.Distance(transform.position, point.position) > 0.05f)
            {
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    point.position,
                    speed * Time.deltaTime);

                yield return null;
            }

            transform.position = point.position;

            yield return new WaitForSeconds(waitTime);
        }
    }
}