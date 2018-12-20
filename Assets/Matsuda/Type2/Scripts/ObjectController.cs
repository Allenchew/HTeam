using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class ObjectController : MonoBehaviour
{
    
    [SerializeField]
    GameObject _target = null;

    NavMeshAgent _navAgent = null;

    public bool _pathUpdate = false;


    void Awake()
    {
        _navAgent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        _target = GameObject.FindGameObjectWithTag("Tower");
        if (_target != null)
        {
            _navAgent.destination = _target.transform.position;
        }
    }


    void Update()
    {
        if (_pathUpdate)
        {
            _pathUpdate = false;
            PathUpdate();
        }
    }

    void PathUpdate()
    {
        //再計算
        //_navAgent.ResetPath();
        //_navAgent.CalculatePath(_target.transform.position, _navAgent.path);
        //_navAgent.SetPath(_navAgent.path);
    }




}
