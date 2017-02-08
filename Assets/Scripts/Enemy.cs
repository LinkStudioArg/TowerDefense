using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public SpriteRenderer enemySprite;

    [System.Serializable]
    public struct Stats
    {
        public string name;
        public float movementVel;
        public float hp;
        public float damage;
        public float shield;
        public float spawnRate;
    }


    public Stats stats;

    void Start()
    {
        enemySprite = GetComponentInParent<SpriteRenderer>();
    }

    void Update()
    {
        if (enemySprite.color != Color.white)
        {
            enemySprite.color = Color.Lerp(enemySprite.color, Color.white, Time.deltaTime);
        }
    }


    IEnumerator Apply(float time)
    {
        float tempVel = stats.movementVel;
        stats.movementVel = 0;
        enemySprite.color = Color.cyan;
        yield return new WaitForSeconds(time);
        stats.movementVel = tempVel;
    }


    public void ApplyEffect(float time)
    {
        StartCoroutine(Apply(time));
    }
}