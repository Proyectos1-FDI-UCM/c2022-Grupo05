using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    #region references

    [SerializeField] string _volumeParameter = "MasterVolume";      // Master volume del Audio mixer
    [SerializeField] AudioMixer _mixer;                             // Audio mixer
    [SerializeField] Slider  _slider;                               // Deslizador
    [SerializeField] int _multiplier = 30;                          // Valor por el que se multiplica el valor del master volume
    #endregion

    #region methods

    private void Awake()
    {
        _slider.onValueChanged.AddListener(ChangeSlideValue);
    }


    // Cambia el valor del master volume al valor que reciba
    private void ChangeSlideValue (float value)
    {
        _mixer.SetFloat(_volumeParameter, Mathf.Log10(value) * _multiplier );
    }


    public void FadeAudio()
    {
        float currentTime = 0.5f;
        float start = _slider.value;

        while (currentTime > 0.5f)
        {
            currentTime -= Time.deltaTime;
            ChangeSlideValue(start * currentTime * 10);
        }

        currentTime = 0.5f;

        while (currentTime > 0.5f)
        {
            currentTime -= Time.deltaTime;
            ChangeSlideValue(start * Time.deltaTime * 10);
        }
    }



    // Guarda el último valor del volumen
    private void OnDisable()
    {
        PlayerPrefs.SetFloat(_volumeParameter, _slider.value);
    }

    #endregion


    // Start is called before the first frame update
    void Start()
    {
        // Pone el valor del volumen al último guardado
        _slider.value = PlayerPrefs.GetFloat(_volumeParameter, _slider.value);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
