using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Clock : MonoBehaviour
{

    public float MaxExpectedTime;
    public float CurrentTime;
    public float BossTimeValue = 10;
    public List<GameObject> BossList;

    public void updateTimeTrialInfo(GameObject Boss)
    {
        BossList.Add(Boss);
        MaxExpectedTime += BossTimeValue;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
