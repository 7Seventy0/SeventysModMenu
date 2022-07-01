
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPLoader;
using TMPro;
using UnityEngine.UI;
public class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public string filepath;
    public List<AudioClip> musicList;
    public AudioSource audioSource;
    public int musicIndex;
    public bool isInShuffleMode = true;
    public bool autoPlaymode = true;
    public float volume = 0.1f;
    public float pitch = 1;
    public float time;
    public float musicLength;
    TextMeshPro mediaText;
    float lengthsec;
    float lengthmin;
    float lengthhrs;
    Image bar;
    string convertedLength;
    string convertedTime;
   public  bool isMessingWithPitch;
    void Start()
    {
        filepath =BepInEx.Paths.BepInExRootPath +"/SeventysModMenuMusic/";
        var folder = Directory.CreateDirectory(BepInEx.Paths.BepInExRootPath + "/SeventysModMenuMusic");
        Debug.Log("Made a new Music Folder");
        audioSource = GorillaTagger.Instance.gameObject.AddComponent<AudioSource>();
        mediaText = GameObject.Find("modMenuMediaTitle").GetComponent<TextMeshPro>();
        
        InvokeRepeating("SlowUpdate", 0, 0.1f);
        audioSource.clip = null;
    }
    float _pitch;
    public void FF()
    {
        _pitch += 0.01f;
        pitch = Mathf.Clamp(_pitch, -8, 8);
        audioSource.pitch = _pitch;
        
    }
    public void RW()
    {
        _pitch -= 0.01f;
        pitch = Mathf.Clamp(_pitch, -8, 8);
        audioSource.pitch = _pitch;
        
    }
    void Update()
    {
        if (musicList == null)
        {
            musicList = new List<AudioClip>();
        }
        if (musicIndex > musicList.Count)
        {
            musicIndex = 0;
        }
        if(musicIndex < 0)
        {
            musicIndex = musicList.Count;
        }
        volume = Mathf.Clamp(volume, 0f, 1f);
        if (bar == null)
        {
            bar = GameObject.Find("MusicBar").GetComponent<Image>();
        }
        if (audioSource.clip != null)
        {
            bar.fillAmount = audioSource.time / audioSource.clip.length;
            if (Mathf.Abs(audioSource.time - audioSource.clip.length) < 2)
            {
                if (isInShuffleMode)
                {
                    PlayRandomSong();
                }
                else
                {
                    PlayNextSong();
                }
            }
            float lastLength = musicLength;
            musicLength = audioSource.clip.length;
            TimeSpan t = TimeSpan.FromSeconds(musicLength);

            convertedLength = string.Format("{0:D2}h:{1:D2}m:{2:D2}s:{3:D3}ms",
                            t.Hours,
                            t.Minutes,
                            t.Seconds,
                            t.Milliseconds);
           
            
                time = audioSource.time;
            
            TimeSpan a = TimeSpan.FromSeconds(time);

            convertedTime = string.Format("{0:D2}h:{1:D2}m:{2:D2}s:{3:D3}ms",
                            a.Hours,
                            a.Minutes,
                            a.Seconds,
                            a.Milliseconds);


        }
        if (audioSource.clip != null && bar != null)
        {
            bar.fillAmount = audioSource.time / audioSource.clip.length;
        }
        if(mediaText == null)
        {
            mediaText = GameObject.Find("modMenuMediaTitle").GetComponent<TextMeshPro>();
        }
        if (mediaText != null && audioSource.isPlaying)
        {
            mediaText.text = "NOW PLAYING\n<#3600FF><size=12>" + audioSource.clip.name+ "<#FFFFFF><size=8>\n"+convertedTime+" / " + convertedLength;
        }
        else if(mediaText != null)
        {
            mediaText.text = "Playing Nothing\n Found <#00FFF7>"+musicList.Count+ " <#FFFFFF>Audio Files! ";
        }
        if (!isMessingWithPitch)
        {
            audioSource.pitch = 1;
            _pitch = 1;

        }

    }

    void SlowUpdate()
    {
        audioSource.volume = volume;
    }
    public void PlayRandomSong()
    {
        audioSource.Stop();
        audioSource.time = 0;
            musicIndex = UnityEngine.Random.Range(0, musicList.Count);
            audioSource.clip = musicList[musicIndex];
        audioSource.Play();
    }

    public void PreviousSong()
    {
        audioSource.Stop();
        audioSource.time = 0;
        musicIndex--;
        audioSource.clip = musicList[musicIndex];
        audioSource.Play();
    }
    public void PlayNextSong()
    {
        audioSource.Stop();
        audioSource.time = 0;
        musicIndex++;
        audioSource.clip = musicList[musicIndex];
        audioSource.Play();
    }
    public void Reload()
    {
        if(musicList == null)
        {
            musicList = new List<AudioClip>();
        }
        musicList.Clear();
        DirectoryInfo d = new DirectoryInfo(filepath);
        FileInfo[] Files = d.GetFiles("*.ogg");
        foreach(FileInfo file in Files)
        {
            StartCoroutine(LoadMusic(file.Name));
           
        }
        FileInfo[] Fileswav = d.GetFiles("*.wav");
        foreach (FileInfo file in Fileswav)
        {
            StartCoroutine(LoadMusic(file.Name));
            
        }
    }
    IEnumerator LoadMusic(string audioName)
    {
        WWW request = GetAudioFromFile(filepath, audioName);
        yield return request;
       AudioClip music = request.GetAudioClip();
        music.name = audioName;
        musicList.Add(music);
    }
    WWW GetAudioFromFile(string path, string fileName)
    {
        string musicToLoad = path + fileName;
        WWW request = new WWW(musicToLoad);
        return request;
    }
}
