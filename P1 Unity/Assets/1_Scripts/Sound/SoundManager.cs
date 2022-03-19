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
                        _backgroundEffectSource;

    #endregion


    #region methods

    public void PlaySound(AudioClip clip)
    {
        _effectSource.PlayOneShot(clip);
    }

    public void PlayOnBackground(AudioClip clip)
    {
        _backgroundEffectSource.PlayOneShot(clip);
    }

    #endregion


    private void Awake()
    {
        _instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        _musicSource.Play();
        _backgroundSource.Play();

    }
}
