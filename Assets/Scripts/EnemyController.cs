using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IDamageable
{
    [SerializeField] Transform player;
    [SerializeField] Bullet bulletPrefab;
    [SerializeField] int damage = 1;
    void Start()
    {
        StartCoroutine(BulletShootCoroutine());
    }

    void Update()
    {
        if (health == 0) Debug.Log("Victory");
    }

    IEnumerator BulletShootCoroutine()
    {
        float x = player.position.x;
        float y = transform.position.y;

        Bullet bullet = Instantiate(bulletPrefab, new Vector2(x, y), Quaternion.identity);
        bullet.SetDamage(damage);

        yield return new WaitForSecondsRealtime(1f);

        StartCoroutine(BulletShootCoroutine());
    }

    [SerializeField] int health = 20;
    public void GetDamage(int damage)
    {
        if (health == 0) return;
        this.health -= damage;
        Debug.Log("Enemy: " + health);
    }
}

