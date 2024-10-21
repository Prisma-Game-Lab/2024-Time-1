using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform target; // referencia o nucleo
    public float speed = 2f; // velocidade do inimigo

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            Vector3 dir = (target.position - transform.position).normalized;
            transform.position += (dir.normalized * speed * Time.deltaTime);
        }
    }
}
