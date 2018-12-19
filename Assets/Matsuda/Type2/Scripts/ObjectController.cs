using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class ObjectController : MonoBehaviour
{
    [SerializeField]
    Transform _target = null;

    NavMeshAgent _navAgent = null;

    void Awake()
    {
        _navAgent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        if (_target != null)
        {
            _navAgent.destination = _target.position;
        }
    }

}
