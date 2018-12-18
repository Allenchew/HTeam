using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    public GameObject Enemy;
    public float spawndelay;

    private bool Spawning = false;

    public static EnemyManager EnemyIns = null;
	
    private void Awake()
    {
        if(EnemyIns == null)
        {
            EnemyIns = this;
        }else if(EnemyIns != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

	void Start () {
		
	}
    
	void Update () {
		
	}

    public void GetSpawnEnemy(int number, Vector3 spawnPoint, Quaternion facePoint)
    {
        if (!Spawning)
        {
            StartCoroutine(I_spawnEnemy(number, spawnPoint, facePoint));
            Spawning = true;
        }
    }
    IEnumerator I_spawnEnemy(int number,Vector3 spawnPoint,Quaternion facePoint)
    {
        for(int i = 0; i < number; i++)
        {
            var tempEnemy = GameObject.Instantiate(Enemy,spawnPoint,facePoint);
            yield return new WaitForSeconds(spawndelay);
        }
        Spawning = false;
    }
}
