using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMusic()
    {
        if (!GetComponent<AudioSource>().isPlaying)
            GetComponent<AudioSource>().Play();
    }

    public void StopMusic()
    {
        GetComponent<AudioSource>().Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
