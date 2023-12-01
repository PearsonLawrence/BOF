using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    // Start is called before the first frame update
    void Start()
    {
        alpha = LoadingScreen.color.a;
    }

    // Update is called once per frame
    void Update()
    {
        if(levelGen.FirstRoom && !PlayerInput.playerIsPlaying)
        {
            Vector2 roomPos = levelGen.FirstRoom.transform.position;
            PlayerInput.gameObject.transform.position = new Vector2(roomPos.x, roomPos.y + 2);
            playerReady = true;
        }
        if (levelGen.stopGeneration && !PlayerInput.playerIsPlaying)
        {
            if(playerReady)
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
    }
}
