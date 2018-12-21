using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class ObjectController : MonoBehaviour
{
    [Tooltip("MapManager"),SerializeField]
    GameObject _mapManager;

    [SerializeField]
    GameObject _target = null;

    NavMeshAgent _navAgent = null;
    bool Running = false;

    public AudioSource ZombiePlayer;
    public AudioClip[] ZombieClip;
    void Awake()
    {
        _navAgent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        ZombiePlayer = GetComponent<AudioSource>();
        _target = GameObject.FindGameObjectWithTag("Tower");
        if (_target != null)
        {
            _navAgent.destination = _target.transform.position;
        }
        StartCoroutine(Yawn());
    }

    void Update()
    {
        /*if(_mapManager.***********){
        NaviUpdate();
        _mapManager.******* = false;
        }*/
        if (GetComponent<Animator>().GetBool("Die") && !Running){
            Running = true;
            GetComponent<NavMeshAgent>().isStopped = true;
            StartCoroutine(Dying());
        }
    }

    void NaviUpdate()
    {
        
        // 次の位置への方向を求める
        var dir = _navAgent.nextPosition - transform.position;

        // 方向と現在の前方との角度を計算（スムーズに回転するように係数を掛ける）
        float smooth = Mathf.Min(1.0f, Time.deltaTime / 0.15f);
        var angle = Mathf.Acos(Vector3.Dot(transform.forward, dir.normalized)) * Mathf.Rad2Deg * smooth;

        // 回転軸を計算
        var axis = Vector3.Cross(transform.forward, dir);

        // 回転の更新
        var rot = Quaternion.AngleAxis(angle, axis);
        transform.forward = rot * transform.forward;

        // 位置の更新
        transform.position = _navAgent.nextPosition;
       
    }
    IEnumerator Dying()
    {
        int index = Random.Range(1, 2);
        ZombiePlayer.clip = ZombieClip[index];
        ZombiePlayer.Play();
        EnemyMnger.EnemyIns.EnemyCount--;
            yield return new WaitForSeconds(2.0f);
            Destroy(gameObject);
    }
    IEnumerator Yawn()
    {
        while (!Running)
        {
            if (!ZombiePlayer.isPlaying)
            {
                int temp = Random.Range(0, 1);
                if (temp == 0)
                {
                    ZombiePlayer.clip = ZombieClip[0];
                    ZombiePlayer.Play();
                }
                yield return new WaitForSeconds(22f);
            }

        }
    }
    void OnCollisionEnter(Collision Collide)
    {
        if(Collide.gameObject.tag == "Player")
        {
            GetComponent<Collider>().isTrigger = true;
        }
    }
    void OnCollisionExit(Collision Collide)
    {
        if (Collide.gameObject.tag == "Player")
        {
            GetComponent<Collider>().isTrigger = false;
        }
    }
}
