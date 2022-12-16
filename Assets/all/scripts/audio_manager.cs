using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audio_manager : MonoBehaviour
{
    public AudioSource bgm;
    public AudioSource fire;
    public AudioSource tankExplosion_music;
    public AudioSource shellExplosion_music;
    public static audio_manager audioManager;

    private void Awake()
    {
        audioManager = this;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void bgm_play()
    {
        bgm.loop = true;
        bgm.Play();
    }

    public void Fire_play()
    {
        fire.Play();
    }

    public void tankExplosion_play()
    {
        tankExplosion_music.Play();
    }

    public void shellExplosion_play()
    {
        shellExplosion_music.Play();
    }
}
