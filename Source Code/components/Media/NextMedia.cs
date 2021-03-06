using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
public class NextMedia : MonoBehaviour
{
    void Start()
    {
        gameObject.layer = 18;
    }
    void OnTriggerEnter(Collider collider)
    {
        if (MusicPlayer.instance.isInShuffleMode)
        {
            MusicPlayer.instance.PlayRandomSong();
        }
        else
        {
        MusicPlayer.instance.PlayNextSong();

        }
        gameObject.GetComponent<AudioSource>().pitch = UnityEngine.Random.Range(0.8f, 1.5f);
        gameObject.GetComponent<AudioSource>().PlayOneShot(gameObject.GetComponent<AudioSource>().clip);
    }
}

