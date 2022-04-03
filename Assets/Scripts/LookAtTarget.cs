using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    Transform target;

    void Start()
    {
        if (target == null)
            target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPostition = new Vector3(target.position.x,
                                       transform.position.y,
                                       target.position.z);
        transform.LookAt(targetPostition);
    }
}
