using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour
{
    [SerializeField]
    public float moveSpeed = 2f;

    //private Vector3 _moveDirection;
    private Rigidbody _rigidbody;

    private void Start()
    {
        //_moveDirection = transform.forward;
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.AddForce(transform.forward * moveSpeed, ForceMode.VelocityChange);
    }
    /*
    private void FixedUpdate()
    {
        _rigidbody.MovePosition(_rigidbody.position + _moveDirection * Time.deltaTime * moveSpeed);
    }
    */
    /*
    private void OnCollisionEnter(Collision collision)
    {
        var afterHitDirection = Vector3.Reflect(_moveDirection, collision.contacts[0].normal);
        _moveDirection = new Vector3(afterHitDirection.x, 0f, afterHitDirection.z).normalized;
    }
    */
}
