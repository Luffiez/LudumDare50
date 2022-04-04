using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamiteBehavior : MonoBehaviour
{
    [SerializeField]
    GameObject ExplosionObject;

    [SerializeField]
    AudioSource audioSrc;

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
        {
            Instantiate(ExplosionObject,transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
