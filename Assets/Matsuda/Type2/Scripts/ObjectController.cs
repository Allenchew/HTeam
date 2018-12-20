using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class ObjectController : MonoBehaviour
{
    [SerializeField]
    Transform _target = null;

    NavMeshAgent _navAgent = null;

    NavMeshPath _naviPath;

    public bool _naviUpdate = false;

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

    private void Update()
    {
        if(_naviUpdate)
        {
            //_naviUpdate = false;
            _navAgent.ResetPath();
            _naviPath = null;
            _navAgent.CalculatePath(_target.transform.position,_naviPath);
            _navAgent.SetPath(_naviPath);
        }
    }

}
