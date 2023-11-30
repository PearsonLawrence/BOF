using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public enum ShakeType
{
    playerDamage,
    playerDeath,
    enemyDamage,
    enemyDeath,
    projectileImpact,
    playerImpact
}


public class CameraShake : MonoBehaviour
{
    [SerializeField]
    private GameObject CamParent;
    public AnimationCurve PlayerDamageCurve;
    public AnimationCurve PlayerDeathCurve;
    public AnimationCurve EnemyDamageCurve;
    public AnimationCurve EnemyDeathCurve;
    public AnimationCurve ProjectileImpactCurve;
    public AnimationCurve PlayerImpactCurve;
    private AnimationCurve currentCurve;

    public float currentShakeTime = 1f;
    public float damageShakeTime = 1f;
    public float enemyDeathShakeTime = 1f;
    public float playerDeathShakeTime = 1f;
    public float projectileImpactShakeTime = 1f;
    public float playerImpactShakeTime = 1f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            doShake(ShakeType.playerImpact);
        }
    }
    public void doShake(ShakeType type)
    {
        switch(type)
        {
            case ShakeType.playerDamage:
                currentCurve = PlayerDamageCurve;
                currentShakeTime = damageShakeTime;
                break;
            case ShakeType.playerDeath:
                currentCurve = PlayerDeathCurve;
                currentShakeTime = playerDeathShakeTime;
                break;
            case ShakeType.enemyDamage:
                currentCurve = EnemyDamageCurve;
                currentShakeTime = damageShakeTime;
                break;
            case ShakeType.enemyDeath:
                currentCurve = EnemyDeathCurve;
                currentShakeTime = enemyDeathShakeTime;
                break;
            case ShakeType.projectileImpact:
                currentCurve = ProjectileImpactCurve;
                currentShakeTime = projectileImpactShakeTime;
                break;
            case ShakeType.playerImpact:
                currentCurve = PlayerImpactCurve;
                currentShakeTime = playerImpactShakeTime;
                break;
        }
        StartCoroutine(Shake(type));
    }

    public IEnumerator Shake(ShakeType type)
    {
        float TimeUsed = 0f;

        while(TimeUsed < currentShakeTime)
        {
            TimeUsed += Time.deltaTime;
            float strength = currentCurve.Evaluate(TimeUsed / currentShakeTime);
            transform.position = CamParent.transform.position + Random.insideUnitSphere * strength;
            yield return null;

        }
        transform.position = CamParent.transform.position;
    }
}
