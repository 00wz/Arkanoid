using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Threading;
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
    private CancellationTokenSource _tokenSource;
    private const int BALL_LAYER_NUMBER = 3;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnValidate()
    {
        _sqrStoppingDistance = stoppingDistance * stoppingDistance;
    }

    public async UniTask GoTo(Vector3 target, Action<Animal> OnRaching)
    {
        StopAllTasks();
        _tokenSource = new();
        await LookTo(target, _tokenSource.Token);
        await MoveTo(target, _tokenSource.Token);
        OnRaching?.Invoke(this);
    }

    private async UniTask MoveTo(Vector3 target, CancellationToken cancellationToken)
    {
        while ((target - _rigidbody.position).sqrMagnitude > _sqrStoppingDistance &&
            !cancellationToken.IsCancellationRequested)
        {
            //await UniTask.WaitForFixedUpdate();//throw exeption: "MissingReferenceException: The object of type 'Rigidbody' has been destroyed..."
            await UniTask.Yield(cancellationToken);
            var nextFramePosition =
                Vector3.MoveTowards(_rigidbody.position, target, walkSpeed * Time.deltaTime);//Time.fixedDeltaTime);
            _rigidbody.MovePosition(nextFramePosition);
        }
    }

    private async UniTask LookTo(Vector3 target, CancellationToken cancellationToken)
    {
        var targetRotation = Quaternion.LookRotation(target - _rigidbody.position, Vector3.up);
        while (_rigidbody.rotation != targetRotation &&
            !cancellationToken.IsCancellationRequested)
        {
            //await UniTask.WaitForFixedUpdate();//throw exeption: "MissingReferenceException: The object of type 'Rigidbody' has been destroyed..."
            await UniTask.Yield(cancellationToken);
            var nexFrameRotation =
                Quaternion.RotateTowards(_rigidbody.rotation, targetRotation,
                rotationSpeedDgr * Time.deltaTime);//Time.fixedDeltaTime);
            _rigidbody.MoveRotation(nexFrameRotation);
        }
    }

    private void StopAllTasks()
    {
        if (_tokenSource != null && !_tokenSource.Token.IsCancellationRequested)
        {
            _tokenSource.Cancel();
            _tokenSource = null;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == BALL_LAYER_NUMBER)
        {
            Die();
        }
    }

    private void Die()
    {
        StopAllTasks();
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        StopAllTasks();
    }
}
