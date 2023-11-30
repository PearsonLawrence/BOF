using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class TimeController : MonoBehaviour
{
    public float maxFixedTime;
    public float currentScale = 1, fixedTime = .02f;
    public float start, end;
    [SerializeField]
    private AudioMixer audioMixer;
    // Start is called before the first frame update
    void Start()
    {
        maxFixedTime = Time.fixedDeltaTime;
        start = Time.timeScale;
    }

    public void ChangeTimeScale()
    {
        Time.timeScale = currentScale;
        Time.fixedDeltaTime = Math.Clamp(fixedTime * Time.timeScale,0f, maxFixedTime);

       // audioMixer.SetFloat("Pitch", currentScale);
    }

}
