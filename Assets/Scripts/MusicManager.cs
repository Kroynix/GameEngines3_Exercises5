using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{



    private MusicManager() {}
    private static MusicManager instance = null;

    public static MusicManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<MusicManager>();
                DontDestroyOnLoad(instance.transform.root);
            }
            return instance;
        }

        private set{ instance = value; }
    }

    [SerializeField]
    List<AudioClip> musicTracks;

    [SerializeField]
    AudioSource audioSrc;

    public void PlayTrack(TrackID id)
    {
        audioSrc.clip = musicTracks[(int)id];
        audioSrc.Play();
        StartCoroutine(FadeInTrackOverDuration(3.0f));
    }


    void DestroyAllClones()
    {
        MusicManager[] clones = FindObjectsOfType<MusicManager>();
        foreach (MusicManager clone in clones)
        {
            if (clone != Instance)
            {
                Destroy(clone.gameObject);
            }
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        DestroyAllClones();
    }


    IEnumerator FadeInTrackOverDuration(float duration)
    {
        float timer = 0;
        while(timer < duration)
        {
            timer += Time.deltaTime;
            float fadeValue = timer / duration;
            audioSrc.volume = Mathf.SmoothStep(0.0f, 0.5f, fadeValue);
            yield return new WaitForEndOfFrame();
        }

    }


}

    public enum TrackID
    {
        Overworld = 0,
        Battle = 1
    }