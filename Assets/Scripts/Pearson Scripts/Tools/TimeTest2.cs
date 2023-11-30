using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class TimeTest2 : MonoBehaviour
{
    public Slider slide;
    public float maxFixedTime;
    public float currentScale, fixedTime = .02f;
    public float start, end;
    public TMP_Text val;
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
        Time.timeScale = slide.value;
        val.SetText(slide.value.ToString());
        Time.fixedDeltaTime = Mathf.Clamp(fixedTime * Time.timeScale, 0f, maxFixedTime);

        // audioMixer.SetFloat("Pitch", currentScale);
    }
}
