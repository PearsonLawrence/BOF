using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class GameManagerComponent : MonoBehaviour
{
    public Image LoadingScreen;
    public Image WinScreen;
    public PlayerInputController PlayerInput;
    public LevelGeneration levelGen;
    public bool playerCanMove;
    public bool levelGenComplete;
    public float fadeTime;
    public float fadeSpeed;
    public float alpha;
    private bool playerReady = false;
    public Clock clock;
    public bool isTimeTrial;
    public bool isOver;
    public bool isWin;
    public TMP_Text resultText;
    public GameObject winScreen;
    public GameObject loseScreen;
    public AudioManager audioman;

    public void playerDeath()
    {
        isOver = true;
        isWin = false;
        playerCanMove = false;
        loseScreen.SetActive(true);
    }

    public void starDeath()
    {
        
        isOver = true;
        isWin = true;
        playerCanMove = false;
        winScreen.SetActive(true);
    }
    public void updateScore()
    {
        if(isTimeTrial)
        {
            if(clock.BossList.Count <= 0)
            {
                isOver = true;
                isWin = true;
                return;
            }
            if(clock.CurrentTime <= 0)
            {
                isOver = true;
                isWin = false;
                PlayerInput.GetComponent<HealthComponent>().takeDamage(100000);
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        alpha = LoadingScreen.color.a;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isOver)
        {
            if (levelGen.FirstRoom && !PlayerInput.playerIsPlaying)
            {
                Vector2 roomPos = levelGen.FirstRoom.transform.position;
                PlayerInput.gameObject.transform.position = new Vector2(roomPos.x, roomPos.y + 2);
                playerReady = true;
            }
            if (levelGen.stopGeneration && !PlayerInput.playerIsPlaying)
            {
                if (playerReady)
                {
                    alpha -= Time.deltaTime * fadeSpeed;
                    LoadingScreen.color = new Color(LoadingScreen.color.r, LoadingScreen.color.g, LoadingScreen.color.b, alpha);
                    if (alpha <= 0)
                    {
                        PlayerInput.playerIsPlaying = true;
                        LoadingScreen.enabled = false;
                    }
                }

            }
            playerCanMove = PlayerInput.playerIsPlaying;
        }
        else
        {
            if(isWin)
            {
                winScreen.SetActive(true);

                if (isTimeTrial)
                    resultText.text = clock.timer.text;
                else
                    resultText.text = Camera.main.GetComponent<PlayerScoreComponent>().currentPlayerScore.ToString();
            }
            else
            {
                loseScreen.SetActive(true);

                if(isTimeTrial)
                    resultText.text = "00:00";
                else
                    resultText.text = Camera.main.GetComponent<PlayerScoreComponent>().currentPlayerScore.ToString();

            }
        }
        
    }
}
