using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ShakingCamera : MonoBehaviour
{
    #region parameters

    float _timer = 0;       // Tiempo que dura la cámara agitándose

    #endregion


    #region references

    private CinemachineVirtualCamera _camera;
    CinemachineBasicMultiChannelPerlin _cameraBasicMultichanel;

    static private ShakingCamera _instance;
    static public ShakingCamera Instance
    { get { return _instance; } }

    #endregion


    private void Awake()
    {
        _instance = this;

        _camera = GetComponent<CinemachineVirtualCamera>();

        _cameraBasicMultichanel = _camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        _cameraBasicMultichanel.m_AmplitudeGain = 0;

    }


    #region methods

    public void ShakeCamera(float amount, float time)
    {
        
        _cameraBasicMultichanel.m_AmplitudeGain = amount;
        _timer = time;
    }


    public void StopCinematicShaking()
    {
        PlayerAccess.Instance.Input.enabled = true;

        _cameraBasicMultichanel.m_AmplitudeGain = 0;
    }

    #endregion



    // Update is called once per frame
    void Update()
    {
        if (_timer > 0)
        {
            _timer -= Time.deltaTime;

            if (_timer <= 0)
            {
                StopCinematicShaking();
            }
        }
    }
}
