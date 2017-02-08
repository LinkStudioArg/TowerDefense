using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    //Auxiliares
    float cooldownTime;




    public GameObject currentTarget;

    public SpriteRenderer turretSprite;
    public SpriteRenderer rangeSprite;

    public enum TurretClass
    {
        Shooter,
        AOE
    };

    public Stats stats;

    public LayerMask enemyLayer;

    [System.Serializable]
    public struct Stats
    {
        public string name;
        public string description;
        public float cost;
        public float damage;
        public float fireRate;
        public float range;
        public float rotationSpeed;
        public TurretClass turretClass;
    }

    void FindEnemy()
    {
        if (currentTarget == null)
        {

            float dist = stats.range;
            int index = 0;
            Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, this.stats.range, enemyLayer.value);
            for (int i = 0; i < cols.Length; i++)
            {
                if (Mathf.Abs(Vector3.Distance(cols[i].transform.position, this.transform.position)) < dist)
                {
                    dist = Mathf.Abs(Vector3.Distance(cols[i].transform.position, this.transform.position));
                    index = i;
                }
            }
            if (cols.Length > 0)
                currentTarget = cols[index].gameObject;
            else
                currentTarget = null;
        }
        else
        {
            if (Mathf.Abs(Vector3.Distance(currentTarget.transform.position, this.transform.position)) > this.stats.range)
            {
                currentTarget = null;
            }
        }
    }

    void Move()
    {
        Vector2 dir = currentTarget.transform.position - transform.position;

        Quaternion rotateTo = Quaternion.LookRotation(Vector3.forward, dir);

        transform.localRotation = Quaternion.RotateTowards(transform.rotation, rotateTo, this.stats.rotationSpeed * Time.deltaTime);

    }

    void Fire()
    {
        if (cooldownTime >= stats.fireRate)
        {
            switch (stats.turretClass)
            {
                case TurretClass.Shooter:

                    RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, Mathf.Infinity, enemyLayer.value);
                    if (hit.collider != null)
                    {
                        if (hit.collider.gameObject == currentTarget)
                        {
                            currentTarget.GetComponent<Enemy>().stats.hp -= this.stats.damage;
                        }
                    }

                    break;

                case TurretClass.AOE:

                    StartCoroutine(AOE());

                    break;
            }
            cooldownTime = 0;
        }
    }

    void Start()
    {
        cooldownTime = stats.fireRate;
        rangeSprite.color = new Color(rangeSprite.color.r, rangeSprite.color.g, rangeSprite.color.b, 0f);
        rangeSprite.transform.localScale = new Vector3(stats.range, stats.range, rangeSprite.transform.localScale.z);
        StartCoroutine(FindEnemyCR());
    }
    IEnumerator FindEnemyCR() { FindEnemy(); yield return new WaitForSeconds(0.5f); StartCoroutine(FindEnemyCR()); }
    // Update is called once per frame
    void Update()
    {
        if (turretSprite.color != Color.white)
        {
            turretSprite.color = Color.Lerp(turretSprite.color, Color.white, Time.deltaTime);
        }

       // FindEnemy();

        if (cooldownTime < stats.fireRate)
            cooldownTime += Time.deltaTime;
        if (currentTarget)
        {
            if (stats.turretClass == TurretClass.Shooter)
            {
                Move();

            }
            Fire();

        }

        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log(Time.time);
            rangeSprite.color = new Color(rangeSprite.color.r, rangeSprite.color.g, rangeSprite.color.b, .5f);
        }

        if (Input.GetButtonUp("Jump"))
        {
            rangeSprite.color = new Color(rangeSprite.color.r, rangeSprite.color.g, rangeSprite.color.b, 0f);
        }
    }


    IEnumerator Apply(Enemy enemy, float time)
    {
        float tempVel = enemy.stats.movementVel;
        enemy.stats.movementVel = 0;
        enemy.enemySprite.color = Color.cyan;
        yield return new WaitForSeconds(time);
        enemy.stats.movementVel = tempVel;
    }

    IEnumerator AOE()
    {
        yield return new WaitForSeconds(.2f);
        turretSprite.color = Color.blue;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, stats.range, enemyLayer);

        foreach (Collider2D col in colliders)
        {
            //col.gameObject.GetComponent<Enemy>().ApplyEffect(stats.damage);
            StartCoroutine(Apply(col.gameObject.GetComponent<Enemy>(), stats.damage));
        }
    }
}