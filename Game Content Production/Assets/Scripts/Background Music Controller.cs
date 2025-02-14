using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicController : MonoBehaviour
{
    public bool stopMusicInThisScene = false;

    private void Start()
    {
        if (stopMusicInThisScene && MusicManager.Instance != null)
        {
            MusicManager.Instance.StopMusic();
        }
        else if (MusicManager.Instance != null)
        {
            MusicManager.Instance.PlayMusic();
        }
    }
}
