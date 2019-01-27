using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource efxSource;                   //Drag a reference to the audio source which will play the sound effects.
    public AudioSource musicSource;                 //Drag a reference to the audio source which will play the music.
    public AudioSource ambientSource1;
    public AudioSource ambientSource2; 
    public static AudioManager instance = null;     //Allows other scripts to call functions from SoundManager.             
    public float lowPitchRange = .95f;              //The lowest a sound effect will be randomly pitched.
    public float highPitchRange = 1.05f;            //The highest a sound effect will be randomly pitched.
    [SerializeField] List<AudioClip> audioClips;


    void Awake()
    {
        //Check if there is already an instance of SoundManager
        if (instance == null)
            //if not, set it to this.
            instance = this;
        //If instance already exists:
        else if (instance != this)
            //Destroy this, this enforces our singleton pattern so there can only be one instance of SoundManager.
            Destroy(gameObject);

        //Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
        DontDestroyOnLoad(gameObject);
    }

    //public void Quack()
    //{
    //    PlaySingle(Quacks[Random.Range(0, Quacks.Count)]);
    //}
    //Used to play single sound clips.
    public void PlaySingle(AudioClip clip)
    {
        //Set the clip of our efxSource audio source to the clip passed in as a parameter.
        efxSource.clip = clip;

        //Play the clip.
        efxSource.PlayOneShot(clip);
    }

    public void PlayAmbiance(string biome)
    {
        AudioSource sourceAvailable;
        if (ambientSource1.isPlaying)
        {
            Debug.Log("fade out audiosource 1");
            StartCoroutine(FadeOut(ambientSource1, 0.9f));
            sourceAvailable = ambientSource2;
        }
        else
        {
            Debug.Log("fade out audiosource 2");
            StartCoroutine(FadeOut(ambientSource2, 0.9f));
            sourceAvailable = ambientSource1;
        }
        
        switch (biome)
        {
            case "Forest":
                return;
                break;
            case "Ocean":
                sourceAvailable.clip = GetAudioClip("OceanWaves");
                break;
            case "Mountain":
                sourceAvailable.clip = GetAudioClip("wind loop mountain");
                break;
            case "Desert":
                sourceAvailable.clip = GetAudioClip("DesertSound");
                break;
            case "Lava":
                sourceAvailable.clip = GetAudioClip("Lava Bubbles");
                break;
        }
        Debug.Log("Playing Audio");
        sourceAvailable.Play(); 
    }

    public void PlayLooped(AudioClip clip)
    {
        efxSource.clip = clip;
        efxSource.Play();
        efxSource.loop = true;
    }

    public void PlaySingleAtSource(AudioSource src)
    {
        //Play the clip.
        src.PlayOneShot(src.clip);
    }

    public void PlaySingleAtSource(AudioSource src, string clip)
    {
        var ac = GetAudioClip(clip);
        //Set the clip of our efxSource audio source to the clip passed in as a parameter.
        src.clip = ac;
        //Play the clip.
        src.PlayOneShot(src.clip);
    }

    public void PlaySingleAtSource(AudioSource src, AudioClip clip)
    {
        //Set the clip of our efxSource audio source to the clip passed in as a parameter.
        src.clip = clip;
        //Play the clip.
        src.PlayOneShot(src.clip);
    }

    public void PlaySingle(string clip)
    {
        var ac = GetAudioClip(clip);
        //Set the clip of our efxSource audio source to the clip passed in as a parameter.
        efxSource.clip = ac;

        //Play the clip.
        efxSource.PlayOneShot(ac);
    }

    public IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }



    public AudioClip GetAudioClip(string name)
    {
        return audioClips.Find(item => item.name.Substring(0) == name);
    }

    public void PlaySingleDelayed(AudioClip clip, float delay)
    {
        StartCoroutine(PlaySingleDelayedCoroutine(clip, delay));
    }

    public void PlaySingleDelayed(string clip, float delay)
    {
        StartCoroutine(PlaySingleDelayedCoroutine(GetAudioClip(clip), delay));
    }

    IEnumerator PlaySingleDelayedCoroutine(AudioClip clip, float delay)
    {
        yield return new WaitForSeconds(delay);
        PlaySingle(clip);
    }

    //RandomizeSfx chooses randomly between various audio clips and slightly changes their pitch.
    public void RandomizeSfx(params AudioClip[] clips)
    {
        //Generate a random number between 0 and the length of our array of clips passed in.
        int randomIndex = Random.Range(0, clips.Length);

        //Choose a random pitch to play back our clip at between our high and low pitch ranges.
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        //Set the pitch of the audio source to the randomly chosen pitch.
        efxSource.pitch = randomPitch;

        //Set the clip to the clip at our randomly chosen index.
        efxSource.clip = clips[randomIndex];

        //Play the clip.
        efxSource.Play();
    }
}
