using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class StarTeleport : MonoBehaviour
{

    public float teleportRange;
    public float teleportInterval;
    public float Bound = 10f;

    private Vector3 StartPos;
    [SerializeField] private StarBoss boss;
    // Start is called before the first frame update
    void Start()
    {
        StartPos = transform.position;
        boss = GetComponent<StarBoss>();

    }


    public IEnumerator TeleportRoutine()
    {
        bool temp = (boss != null && boss.isFighting);
        while (true)
        {
            yield return new WaitForSeconds(teleportInterval);

            boss.anim.SetBool("isTeleporting", true);
        }
    }

    public void Teleport()
    {
        Vector3 randomPosition = GetRandomPosition();

        transform.position = new Vector3(Random.Range(StartPos.x - Bound, StartPos.x + Bound), Random.Range(StartPos.y - Bound, StartPos.y + Bound), 0f);


        boss.anim.SetBool("isTeleporting", false);
    }

    Vector3 GetRandomPosition()
    {
        Vector2 randomDirection = Random.insideUnitCircle.normalized * teleportRange;
        Vector3 randomPosition = transform.position + new Vector3(randomDirection.x, randomDirection.y, 0f);

        return randomPosition;
    }
   
}
