using UnityEngine;
using System.Collections;

public class bgMusicPlayer : MonoBehaviour {

    //背景音乐
    public AudioClip m_musicClip;
    //声音源
    protected AudioSource m_Audio;

	// Use this for initialization
	void Start () {
        m_Audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if ( !m_Audio.isPlaying )
        {
            m_Audio.clip = m_musicClip;
            m_Audio.Play();
        }
	}
}
