using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximitySound : MonoBehaviour
{
    #region references

    AudioSource _audioSource;
    [SerializeField] private AudioClip _clip;

    #endregion


    public void PlaySound()
    {
        _audioSource.PlayOneShot(_clip);
    }



    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();

    }

}
