using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicHandler : MonoBehaviour
{
    private AudioSource source;

    public List<AudioClip> songs = new List<AudioClip>();

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    void Start()
    {
        StartCoroutine(IEPlayRandomSong());
    }

    private IEnumerator IEPlayRandomSong()
    {
        int num = Random.Range(0, songs.Count);
        var clip = songs[num];
        source.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        yield return IEPlayRandomSong();
    }
}