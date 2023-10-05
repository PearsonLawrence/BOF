using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    public bool isSlowMo;

    [SerializeField]
    private TimeController timeController;

    public float minSlow = .1f;
    public float speed = 100;
    // Start is called before the first frame update
    void Start()
    {
        timeController = this.GetComponent<TimeController>();
    }



    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            timeController.currentScale = Mathf.Lerp(timeController.currentScale, minSlow, Time.unscaledDeltaTime * speed);
            timeController.fixedTime = Mathf.Lerp(timeController.fixedTime, minSlow, Time.unscaledDeltaTime * speed);
        }
        else
        {
            timeController.currentScale = Mathf.Lerp(timeController.currentScale, 1, Time.unscaledDeltaTime * speed);
            timeController.fixedTime = Mathf.Lerp(timeController.fixedTime, .02f, Time.unscaledDeltaTime * speed);
        }
        Debug.Log(timeController.currentScale);
        if(timeController.currentScale > .5f && timeController.currentScale < 1)
            timeController.ChangeTimeScale();
    }
}
