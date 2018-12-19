using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMnger : MonoBehaviour {

    public AudioSource SEPlayer;
    public AudioSource BgmPlayer;

    public static SoundMnger SoundIns = null;

    private void Awake()
    {
        if(SoundIns == null)
        {
            SoundIns = this;
        }else if( SoundIns !=this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
	void Start () {
		
	}
	
	void Update () {
		
	}

    public void playSe(AudioClip clip)
    {
        SEPlayer.clip = clip;
        SEPlayer.Play();
    }

    public void PlayBgm(AudioClip clip)
    {
        BgmPlayer.clip = clip;
        BgmPlayer.Play();
    }
}
