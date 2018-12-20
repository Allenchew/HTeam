using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class ObjectController : MonoBehaviour
{
    
    [SerializeField]
    GameObject _target = null;

    NavMeshAgent _navAgent = null;
    
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
  


}
