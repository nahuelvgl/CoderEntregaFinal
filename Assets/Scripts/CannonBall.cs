using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    public Vector3 direction;
    public float speed;
    public int damage;

    // Start is called before the first frame update
    void Start()
    {
    Destroy(gameObject,0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed;
    }
}
