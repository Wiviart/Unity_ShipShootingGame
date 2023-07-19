using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public enum Target
    {
        Player,
        Enemy,
        Respawn
    }
    public Target target;

    [SerializeField] int damage;
    [SerializeField] int direction = 1;

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // if (collision.CompareTag(Target.Respawn.ToString()))
        // {
        //     Destroy(gameObject);
        //     return;
        // }

        if (!collision.CompareTag(target.ToString())) return;

        collision.GetComponent<IDamageable>().GetDamage(damage);
        Destroy(gameObject);
    }

    void Update()
    {
        transform.position += new Vector3(0, 0.01f * direction, 0);

        if (transform.position.y > 10 || transform.position.y < -10)
            Destroy(gameObject);
    }
}

