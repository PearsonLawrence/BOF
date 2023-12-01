using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerScoreComponent : MonoBehaviour
{
    [SerializeField] private TMP_Text score;
    private int currentPlayerScore = 0;
    
    public void increaseScore(int amount)
    {
        currentPlayerScore += amount;
        score.text = "Score: " + currentPlayerScore;

    }
}
