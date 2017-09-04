using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public bool musicEnabled = true;
    public bool fxEnabled = true;

    [Range(0,1)]
    public float musicVolume = 1.0f;

    [Range(0,1)]
    public float fxVolume = 1.0f;

    public AudioClip clearRowSound;
    public AudioClip moveSound;
    public AudioClip dropSound;
    public AudioClip gameOverSound;
    public AudioClip backgroundMusic;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
