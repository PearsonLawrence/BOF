using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableObject : MonoBehaviour
{
    public System.Action onDestroy;

    private void OnDestroy()
    {
        if (onDestroy != null)
        {
            onDestroy();
        }

    }
}
