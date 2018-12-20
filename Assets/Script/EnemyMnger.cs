using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMnger : MonoBehaviour {

    public GameObject Enemy;
    public GameObject SpawnPoint;
    public float spawndelay;
    
    private bool Spawning = false;

    public static EnemyMnger EnemyIns = null;
	
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

    public void GetSpawnEnemy(int number)
    {
        if (!Spawning)
        {
            StartCoroutine(I_spawnEnemy(number, SpawnPoint.transform.position));
            Spawning = true;
        }
    }
    IEnumerator I_spawnEnemy(int number,Vector3 spawnPoint)
    {
        for(int i = 0; i < number; i++)
        {
            var tempEnemy = GameObject.Instantiate(Enemy);
            tempEnemy.transform.position = spawnPoint;
            yield return new WaitForSeconds(spawndelay);
        }
        Spawning = false;
    }

}
