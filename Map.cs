using UnityEngine;
using System.Collections.Generic;

public class Map : MonoBehaviour
{
    [SerializeField] private List<Resource> _resources = new List<Resource>();
    [SerializeField] private Unit _unit;

    [SerializeField] private List<Resource> _collectedResources = new List<Resource>();

    private void Awake()
    {
        _unit.CollectedResource += CollectResource;
    }

    private void OnDisable()
    {
        _unit.CollectedResource += CollectResource;
    }

    private void Update()
    {
        if (_resources.Count > 0)
        {
            if (_unit.IsStanding)
            {
                _unit.StartMovingToResource(_resources[0]);
            }
        }
    }   

    public void CollectResource(Resource resource)
    {
        _collectedResources.Add(resource);
        _resources.Remove(resource);
    }

    public void AddResource(Resource resource)
    {
        _resources.Add(resource);
    }
}
