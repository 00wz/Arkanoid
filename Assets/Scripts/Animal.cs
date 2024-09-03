using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Animal : MonoBehaviour
{
    [SerializeField]
    private float stoppingDistance;
    [HideInInspector, SerializeField]
    private float _sqrStoppingDistance;
    [SerializeField]
    private float walkSpeed = 5;
    [SerializeField]
    private float rotationSpeedDgr = 180f;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnValidate()
    {
        _sqrStoppingDistance = stoppingDistance * stoppingDistance;
    }

    public async UniTask GoTo(Vector3 target, Action OnRaching)
    {
        await LookTo(target);
        await MoveTo(target);
        OnRaching?.Invoke();
    }

    private async UniTask MoveTo(Vector3 target)
    {
        while((target - _rigidbody.position).sqrMagnitude > _sqrStoppingDistance)
        {
            await UniTask.WaitForFixedUpdate();
            var nextFramePosition = 
                Vector3.MoveTowards(_rigidbody.position, target, walkSpeed * Time.fixedDeltaTime);
            _rigidbody.MovePosition(nextFramePosition);
        }
    }

    private async UniTask LookTo(Vector3 target)
    {
        var targetRotation = Quaternion.LookRotation(target - transform.position, Vector3.up);
        while (_rigidbody.rotation != targetRotation)
        {
            await UniTask.WaitForFixedUpdate();
            var nexFrameRotation =
                Quaternion.RotateTowards(_rigidbody.rotation, targetRotation, 
                rotationSpeedDgr * Time.fixedDeltaTime);
            _rigidbody.MoveRotation(nexFrameRotation);
        }
    }
}
