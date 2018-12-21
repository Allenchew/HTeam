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
    public int PhaseTime = 10;
    public int IntervalTime = 3;
    public int PlayLife = 5;
    public int[][] EnemyToSpawn = new int[][] { new int[] { 2, 3 } ,new int[] { 2,2,3},new int[] {2,1,3,5 } };
    public int PhaseCount = 3;
    public bool ShowGameOver = false;
    public bool GameClear = false;
    public bool OnPause = false;
    public bool Started = false;

    public GameObject enemyholder;
    public GameObject Wall;
    public GameObject MapManager;

    private bool init = false;
    private TimeState GameState = new TimeState(true,true);
    void Awake()
    {
        MngerIns = this;
    }
	void Start () {
        
        MapManager.GetComponent<MapManagerControl>().CreateMap(3-PhaseCount +1);
        
        PhaseCount--;
        
        GameState.PhaseEnd = false;
	}
    void Update () {
        if (Input.anyKey)
        {
            Started = true;
            if(Started && !init)
            {
                init = true;
                StartCoroutine(I_timer(PhaseTime));
                SoundMnger.SoundIns.PlayBgm(SoundMnger.SoundIns.Bgm[1]);
                EnemyMnger.EnemyIns.GetSpawnEnemy(EnemyToSpawn[0]);
                
            }
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
            if (EnemyMnger.EnemyIns.EnemyCount == 0 && GameState.IntervelEnd) { break; }
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
            MapManager.GetComponent<MapManagerControl>().ReCreate(3 - PhaseCount +1);
            GameState.PhaseEnd = false;
            GameState.IntervelEnd = true;
            StartCoroutine(I_timer(PhaseTime));
            PhaseCount--;
            EnemyMnger.EnemyIns.GetSpawnEnemy(EnemyToSpawn[3-PhaseCount]);
        }
    }
}
