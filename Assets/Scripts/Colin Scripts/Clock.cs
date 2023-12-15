using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Clock : MonoBehaviour
{

    public float MaxExpectedTime;
    public float CurrentTime;
    public float BossTimeValue = 10;
    public List<GameObject> BossList;
    public TMP_Text timer;
    public SpriteRenderer render;
    public GameManagerComponent manager;
    public void updateTimeTrialInfo(GameObject Boss)
    {
        BossList.Add(Boss);
        MaxExpectedTime += BossTimeValue;
        CurrentTime = MaxExpectedTime;
    }
    public void removeBoss(GameObject Boss)
    {
        BossList.Remove(Boss);
    }
    public void updateTime()
    {
        float minutes = Mathf.FloorToInt(CurrentTime / 60);
        float seconds = Mathf.FloorToInt(CurrentTime % 60);


        timer.text = minutes + ":" + seconds;
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.isOver) return;



        if (manager.playerCanMove)
        {
            if (CurrentTime >= -.01f) CurrentTime -= Time.deltaTime;

            if (CurrentTime > 0)
            {
                updateTime();
            }
            else if (CurrentTime <= 0)
            {
                CurrentTime = 0;
                manager.updateScore();
            }

        }
    }
}
