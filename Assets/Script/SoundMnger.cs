using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMnger : MonoBehaviour {
    public AudioClip[] Bgm;
    public AudioClip[] SE;
    

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
        BgmPlayer = GetComponent<AudioSource>();
        SEPlayer = transform.GetChild(0).GetComponent<AudioSource>();
            PlayBgm(Bgm[0]);
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
