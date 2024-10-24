using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform target; // referencia o nucleo
    public float speed = 2f; // velocidade do inimigo
    public float damage = 1.0f;
    private float distance;

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            Vector3 dir = (target.position - transform.position);
            transform.position += (dir.normalized * speed * Time.deltaTime);
            distance = dir.magnitude;

            if (distance <=15f) //tirar isso daqui depois
            {
                target.gameObject.GetComponent<Core>().damageCore(damage);
                setTarget(null);
                Destroy(this.gameObject);
                return;
            }
        }
    }

    public void setTarget(Transform _target)
    {
        this.target = _target;
    }
}
