using UnityEngine;
using System;

[RequireComponent(typeof(Movement))]
public class Unit : MonoBehaviour
{
    [SerializeField] private Resource _resource;
    [SerializeField] private Vector3 _initialPosition;
    [SerializeField] private Vector2 _moveDirection;

    private Movement _movement;

    public event Action<Resource> CollectedResource;

    public bool IsResourceCollected { get; private set; } = false;
    public bool IsStanding { get; private set; } = true;

    private void Start()
    {
        _movement = GetComponent<Movement>();
        _initialPosition = transform.position;
    }

    private void Update()
    {
        if (_resource != null)
        {
            _movement.Move(_moveDirection);
            IsStanding = false;
        }

        if (IsResourceCollected & Vector2.Distance(transform.position, _initialPosition) < 0.2)
        {
            CollectedResource?.Invoke(_resource);
            _movement.Stop();
            IsResourceCollected = false;
            IsStanding = true;
            _resource = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Resource resource))
        {
            IsResourceCollected = true;
            ChangeMoveDirection();
            resource.gameObject.SetActive(false);
        }
    }

    public void StartMovingToResource(Resource resource)
    {
        _resource = resource;
        _moveDirection = _resource.transform.position - transform.position;
    }

    public void ChangeMoveDirection()
    {
        _moveDirection = _initialPosition - transform.position;
    }
}
