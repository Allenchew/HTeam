using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMnger : MonoBehaviour {

    public GameObject holdEnemy;
    public GameObject Enemy;
    public GameObject[] SpawnPoint;
    public int EnemyCount = 0;
    public float spawndelay;
    
    private bool Spawning = false;
    private int spawnPointCount = 0;

    public static EnemyMnger EnemyIns = null;
	
    private void Awake()
    {
       EnemyIns = this;
    }

	void Start () {
        spawnPointCount = GameMnger.MngerIns.PhaseCount + 2;
	}
    
	void Update () {
		
	}

    public void GetSpawnEnemy(int[] number)
    {
        if (!Spawning)
        {
            StartCoroutine(I_spawnEnemy(number, SpawnPoint,spawnPointCount - GameMnger.MngerIns.PhaseCount));
            Spawning = true;
        }
    }
    IEnumerator I_spawnEnemy(int[] number,GameObject[] spawnPoint, int GetPhase)
    {
        int[] temp = number;
        int Total = 0;
        foreach (int count in temp) Total += count;
        EnemyCount = Total;
        for(int i = 0; i < Total; i++)
        {
            for(int j = 0; j < GetPhase; j++)
            {
                if (temp[j] > 0)
                {
                    var tempEnemy = GameObject.Instantiate(Enemy);
                    tempEnemy.transform.position = spawnPoint[j].transform.position;
                    yield return new WaitForSeconds(spawndelay);
                    temp[j]--;
                }
                
            }
        }
        Spawning = false;
    }

}
