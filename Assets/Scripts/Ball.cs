using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour
{
    [SerializeField]
    private AudioClip CarriageHitAudioClip;

    public static float moveSpeed;

    private Vector3 _moveDirection;
    private Rigidbody _rigidbody;
    private const int CARRIAGE_LAYER_NUMBER = 6;

    private void Start()
    {
        _moveDirection = transform.forward;
        _rigidbody = GetComponent<Rigidbody>();
    }
    
    private void FixedUpdate()
    {
        _rigidbody.velocity = _moveDirection * moveSpeed;
    }
    
    
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == CARRIAGE_LAYER_NUMBER)
        {
            var afterHitDirection = collision.contacts[0].normal;
            _moveDirection = new Vector3(afterHitDirection.x, 0f, afterHitDirection.z).normalized;
            Utils.PlayClip2D(CarriageHitAudioClip);
        }
        else
        {
            var afterHitDirection = Vector3.Reflect(_moveDirection, collision.contacts[0].normal);
            _moveDirection = new Vector3(afterHitDirection.x, 0f, afterHitDirection.z).normalized;
        }
    }
    
}
