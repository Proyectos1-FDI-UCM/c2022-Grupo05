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
                        _backgroundEffectSource,
                        _cinematicSource,
                        _dialogueSource;

    [SerializeField]
    private AudioClip _normalClip,
                      _bossClip;

   

    #endregion


    #region methods

    public void PlayEffectSound(AudioClip clip)
    {
        _effectSource.PlayOneShot( clip);
     
    }

    public void PlayOnBackground(AudioClip clip)
    {
        _backgroundEffectSource.PlayOneShot(clip);
    }

    public void PlayCinematicSound(AudioClip clip)
    {
        _cinematicSource.PlayOneShot(clip);
    }
    public void PlayDialogueSound(AudioClip clip)
    {
       _dialogueSource.PlayOneShot(clip);
    }



    public void Boss()
    {
        _musicSource.Stop();

        _musicSource.clip = _bossClip;
        _musicSource.Play();

    }
    #endregion


    private void Awake()
    {
        _instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        _musicSource.clip = _normalClip;
        _musicSource.Play();
        _backgroundSource.Play();

    }
}
