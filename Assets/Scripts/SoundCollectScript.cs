using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundCollectScript : MonoBehaviour
{
    public AudioSource audioShot;
    //[SerializeField]
    public AudioClip collectAudio;
    private void OnTriggerEnter(Collider other)
    {
        audioShot = GetComponent<AudioSource>();
        audioShot.Play(delay: 10);
    }
}
