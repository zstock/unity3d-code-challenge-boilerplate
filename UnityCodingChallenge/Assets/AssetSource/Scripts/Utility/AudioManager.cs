using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    private static AudioSource _source;

    private void Start()
    {
        _source = GetComponent<AudioSource>();
    }

    public static void PlayClip(AudioClip clip)
    {
        _source.PlayOneShot(clip);
    }
}
