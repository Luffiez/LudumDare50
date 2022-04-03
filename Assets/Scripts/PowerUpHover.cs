using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpHover : MonoBehaviour
{
    // Start is called before the first frame update
    float startY;
    [SerializeField]
    float height;
    [SerializeField]
    float howerSpeed;
    void Start()
    {
        startY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, startY + Mathf.Sin(Time.time * howerSpeed) * height, transform.position.z);
    }
}
