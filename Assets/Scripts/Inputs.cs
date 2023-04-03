using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inputs : MonoBehaviour
{
    [SerializeField] private float speed;
    private Vector3 direction;
    public float health = 100;
    // Start is called before the first frame update
    void Start()
    {
        speed = 0.2f;
    }
    private void Move(Vector3 moveDirection)
    {
        transform.position += moveDirection * speed;
    }

    // Update is called once per frame
    void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        //var vertical = Input.GetAxis("Vertical");
        var direction = new Vector3(horizontal, 0, 0);
        transform.rotation= Quaternion.LookRotation(direction);
        Move(direction);

    }
    private void RotateLeft()
    {
        transform.Rotate(new Vector3(0, -0.5f, 0));
    }
    private void RotateRight()
    {
        transform.Rotate(new Vector3(0, 0.5f, 0));
    }
    public void RecieveDamage()
    {
        health -= 50f;
    }
  

    private void JAttack()
    {
        if (Input.GetKey(KeyCode.Q))
        {

            Debug.Log("succesful attack");
        }
    }
}
