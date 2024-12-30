using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehavior : MonoBehaviour
{

    [Header("Atributos Gerais")]
    public float range = 15f;
    public string enemyTag = "Enemy";
    public float rotationModifier;
    public float rotationSpeed;

    [Header("Usar Balas (Torreta Default)")]
    public float fireRate = 1f;
    private float fireCountdown = 0f;
    public GameObject bullet;


    [Header("Usar Laser (Torreta Laser)")]
    public bool useLaser = false;
    public LineRenderer lineRenderer;

    [Header("Referencias")]
    public Transform Head;
    public Transform partToRotate;
    public Transform target;
    public Transform FirePoint;
    public SpriteRenderer TurretSprite;


    [Header("Sprites")]
    public Sprite[] sprites;

    private void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    private void Update()
    {
        if(target == null)
        {
            if(useLaser)
            {
                if(lineRenderer.enabled)
                    lineRenderer.enabled = false;   
            }

            return;
        }

        //LockOnTarget();
        Vector3 dir = target.position - Head.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - rotationModifier;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        partToRotate.rotation = Quaternion.Slerp(partToRotate.rotation, rotation, Time.deltaTime * rotationSpeed);
        changeSprite(Mathf.Abs(angle));


        if (useLaser)
        {
            Laser();
        }
        else
        {
            if(fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }

            fireCountdown -= Time.deltaTime;
        }
    }



    void LockOnTarget()
    {
        
    }


    void Laser()
    {
        if(!lineRenderer.enabled)
            lineRenderer.enabled = true;
        

        lineRenderer.SetPosition(0, FirePoint.position);
        lineRenderer.SetPosition(1, target.position);
    }
    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bullet, FirePoint.position, FirePoint.rotation);
        Bullet bullettemp = bulletGO.GetComponent<Bullet>();

        if (bullettemp != null)
            bullettemp.Seek(target);
    }
    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;


        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if(nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        } 
        else
        {
            target = null;
        }
    }

    private void changeSprite(float rotation)
    {
        if(rotation < 20 || rotation > 340)
        {
            TurretSprite.sprite = sprites[0];
        }
        else if(rotation >= 20 && rotation < 66.6)
        {
            TurretSprite.sprite = sprites[1];
        }
        else if(rotation >= 66.6 && rotation < 113.2)
        {
            TurretSprite.sprite = sprites[2];
        }
        else if(rotation >= 113.2 && rotation < 160.0)
        {
            TurretSprite.sprite = sprites[3];
        }
        else if(rotation >= 160 && rotation < 200)
        {
            TurretSprite.sprite = sprites[4];
        }
        else if(rotation >= 200 && rotation < 246.6)
        {
            TurretSprite.sprite = sprites[5];
        }
        else if(rotation >= 246.6 && rotation <293.2)
        {
            TurretSprite.sprite = sprites[6];
        }
        else if(rotation >= 293.2 && rotation <= 340)
        {
            TurretSprite.sprite = sprites[7];
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
