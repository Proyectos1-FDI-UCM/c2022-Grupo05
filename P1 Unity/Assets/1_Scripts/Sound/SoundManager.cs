using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    #region references

    static private SoundManager _instance;
    static public SoundManager Instance
    { get { return _instance; } }


    [SerializeField]
    private AudioSource _musicSource,
                        _backgroundSource,
                        _effectSource,
                        _cinematicSource,
                        _dialogueSource;

    [SerializeField]
    private AudioClip _normalClip,
                      _bossClip,
                      _mainMenuClip;
    [SerializeField]
    private VolumeControl _volumeController;


    #endregion


    #region methods

    public void PlayEffectSound(AudioClip clip)
    {
        _effectSource.PlayOneShot( clip);
     
    }

    public void PlayCinematicSound(AudioClip clip)
    {
        _cinematicSource.PlayOneShot(clip);
    }

    public void PlayDialogueSound(AudioClip clip)
    {
       _dialogueSource.PlayOneShot(clip);
    }


    public void FadeAudio()
    {
        _volumeController.FadeAudio();
    }


    public void MainMenu()
    {
        if (_mainMenuClip != _musicSource.clip)
        {
            _musicSource.Stop();
            _musicSource.clip = _mainMenuClip;
            _musicSource.Play();
        }
        
    }


    public void StopMusic()
    {
        _musicSource.Stop();
    }

    public void Level()
    {
        if (_musicSource.clip != _normalClip)
        {
            _musicSource.Stop();
            _musicSource.clip = _normalClip;
            _musicSource.Play();
        }
    }


    public void Boss()
    {
        if (_musicSource.clip != _bossClip)
        {
            _musicSource.Stop();
            _musicSource.clip = _bossClip;
            _musicSource.Play();
        }
    }
    #endregion


    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }


    // Start is called before the first frame update
    void Start()
    {
        _volumeController = _volumeController.GetComponent<VolumeControl>();

        _backgroundSource.Play();
    }
}
