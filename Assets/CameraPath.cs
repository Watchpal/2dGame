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
            Vector3 target = new Vector3(
                    point.position.x,
                    point.position.y,
                    transform.position.z
                );
            while (Vector2.Distance(
                new Vector2(transform.position.x, transform.position.y),
                new Vector2(target.x, target.y)) > 0.05f)
            {
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    target,
                    speed * Time.deltaTime);

                yield return null;
            }

            transform.position = target;

            yield return new WaitForSeconds(waitTime);
        }
    }
}