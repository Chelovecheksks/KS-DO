using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _mapSizeX, _mapSizeZ;
    [SerializeField] private float _speed;
    [SerializeField] private float _timeBetweenChangeDestination;

    private NavMeshAgent _agent;
    private Vector3 _destination;
    public bool _haveTarget;

    public void SetHaveTarget(bool haveTarget)
    {
        _haveTarget = haveTarget;
    }

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = _speed;

        StartCoroutine("WaitBeetwenChangeDestination");
    }

    public void SetDestination(Vector3 destination)
    {
        _agent.destination = destination;
    }

    private void ChangeDestination()
    {
        if (_haveTarget)
            return;

        _destination = new Vector3(Random.Range(-(_mapSizeX / 2), _mapSizeX / 2), 0, Random.Range(-(_mapSizeZ / 2), _mapSizeZ / 2));
        _agent.destination = _destination;
    }

    IEnumerator WaitBeetwenChangeDestination()
    {
        while (true)
        {
            ChangeDestination();
            yield return new WaitForSeconds(_timeBetweenChangeDestination);
        }
    }
}
