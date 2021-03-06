﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metronome : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField]
    private AudioSource audioSource;
    private AudioSource ticSource;

    [Header("Song")]
    [SerializeField]
    private SongData selectSong;

    private Vector3Int rotateAngle;

    private double offset;
    private double bpm;

    private int split;

    private double oneBeatTime;
    private double nextSample;

    private void Awake(){
        rotateAngle = Vector3Int.zero;
        rotateAngle.z = 90;

        ticSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = selectSong.audioClip;
        
        offset = selectSong.offset;
        bpm = selectSong.bpm;

        double offsetForSample = offset * audioSource.clip.frequency;
        oneBeatTime = (60.0 / bpm);

        nextSample = offsetForSample;

        audioSource.clip.frequency.Log();
        audioSource.clip.samples.Log();
        
        (audioSource.clip.samples / audioSource.clip.frequency).Log();

        audioSource.Play();
    }

    private void Update(){
        ((float)audioSource.timeSamples / (float)audioSource.clip.frequency).Log();
        if(audioSource.timeSamples >= nextSample){
            StartCoroutine(TicSFX());
        }
    }

    private IEnumerator TicSFX(){
        ticSource.Play();
        nextSample += oneBeatTime * audioSource.clip.frequency;
        gameObject.transform.Rotate(rotateAngle);
        yield return null;
    }
}
