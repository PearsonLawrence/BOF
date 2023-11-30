using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour
{

    public string nextGameScene;
    public string returnGameScene;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectLevel()
    {
        SceneManager.LoadScene(nextGameScene);
    }

    public void ReturnLevel()
    {
        SceneManager.LoadScene(returnGameScene);
    }
}
