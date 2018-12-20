using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMnger : MonoBehaviour {
    struct TimeState
    {
        public bool PhaseEnd;
        public bool IntervelEnd;

        public TimeState(bool phase,bool interval)
        {
            PhaseEnd = phase;
            IntervelEnd = interval; 
        }
    }
    
    public static GameMnger MngerIns = null;
    public int PhaseTime = 60;
    public int IntervalTime = 10;
    public int PlayLife = 5;
    public int EnemyToSpawn = 10;
    public int PhaseCount = 3;
    public bool ShowGameOver = false;
    public bool GameClear = false;
    public bool OnPause = false;
    public bool Started = false;
    public GameObject Wall;
    public GameObject MapManager;

    
    private TimeState GameState = new TimeState(true,true);
    void Awake()
    {
        MngerIns = this;
    }
	void Start () {
        
        MapManager.GetComponent<MapManagerControl>().CreateMap();
        StartCoroutine(I_timer(PhaseTime));
        PhaseCount--;
        EnemyMnger.EnemyIns.GetSpawnEnemy(EnemyToSpawn);
        GameState.PhaseEnd = false;
	}
    void Update () {
        if (Input.anyKey)
        {
            Started = true;
            if (ShowGameOver)
            {
                SceneManager.LoadScene("main");
            }
            if (GameClear) // zombie count
            {
                SceneManager.LoadScene("main");
            }
        }
	}
    IEnumerator I_timer(int InputTime)
    {
        for(int i = InputTime; i > 0; i--)
        {
            yield return new WaitForSeconds(1.0f);
        }
        if (PhaseCount > 0)
            _enterInterval();
        else
            GameClear = true;
    }
    void _enterInterval()
    {
        if (!GameState.PhaseEnd)
        {
            GameState.PhaseEnd = true;
            GameState.IntervelEnd = false;
            StartCoroutine(I_timer(IntervalTime));
        }
        else if (!GameState.IntervelEnd)
        {
            GameState.PhaseEnd = false;
            GameState.IntervelEnd = true;
            StartCoroutine(I_timer(PhaseTime));
            PhaseCount--;
            EnemyMnger.EnemyIns.GetSpawnEnemy(EnemyToSpawn);
        }
    }
}
