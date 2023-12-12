using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAbilityEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    public MonoBehaviour[] abilites;
    
    void Start()
    {
        StartCoroutine(ChooseRandomAbility());
    }

    IEnumerator ChooseRandomAbility()
    {
        yield return new WaitForSeconds(1f);

        int randomIndex = Random.Range(0, abilites.Length);
        MonoBehaviour selectedAbility = abilites[randomIndex];

        ApplyAbility(selectedAbility);
    }

    void ApplyAbility(MonoBehaviour ability)
    {
        if (ability != null)
        {
            ability.enabled = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
