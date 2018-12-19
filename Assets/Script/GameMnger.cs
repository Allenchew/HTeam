﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public bool OnPause = false;

    
    private TimeState GameState = new TimeState(true,true);
    void Awake()
    {
        if(MngerIns == null)
        {
            MngerIns = this;
            
        }else if(MngerIns !=this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
	void Start () {
        
        StartCoroutine(I_timer(PhaseTime));
        GameState.PhaseEnd = false;
	}
    void Update () {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            MapControl.Mapmnger.GetCreateMap();
        }
	}
    IEnumerator I_timer(int InputTime)
    {
        for(int i = InputTime; i > 0; i--)
        {
            yield return new WaitForSeconds(1.0f);
        }
        _enterInterval();
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
           // EnemyManager.EnemyIns.GetSpawnEnemy(60, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
        }
    }
    void _gameOver()
    {

    }
    void _gameClear()
    {

    }
}
