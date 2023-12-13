using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAbilityEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] enemyAbilites;
    
    void Start()
    {
        StartCoroutine(ChooseRandomAbility());
    }

    IEnumerator ChooseRandomAbility()
    {
        yield return new WaitForSeconds(1f);

        int randomIndex = Random.Range(0, enemyAbilites.Length);
        Debug.Log(randomIndex);
        GameObject selectedEnemy = enemyAbilites[randomIndex];
        

        ApplyAbility(selectedEnemy);
    }

    void ApplyAbility(GameObject enemyWithAbility)
    {
        if (enemyWithAbility != null)
        {
            MonoBehaviour[] allScripts = enemyWithAbility.GetComponents<MonoBehaviour>();
            foreach (var script in allScripts) 
            {
                script.enabled = false;
            }

            MonoBehaviour abilityScript = enemyWithAbility.GetComponent<MonoBehaviour>();
            if (abilityScript != null)
            {
                abilityScript.enabled = true;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
