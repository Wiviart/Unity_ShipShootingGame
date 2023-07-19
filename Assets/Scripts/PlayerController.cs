using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable
{
    Rigidbody2D rigid;
    Animator animator;
    private bool canShoot;
    float timer = 1;
    [SerializeField] int damage = 1;
    [SerializeField] Bullet bulletPrefab;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (health == 0) Debug.Log("Loss");

        Movement();
        BulletShoot();
    }

    void Movement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        rigid.velocity = new Vector2(x, y);

        AnimationLeftAndRight(x);

        ClampPosition();
    }

    void ClampPosition()
    {
        Vector3 objectPos = Camera.main.WorldToViewportPoint(transform.position);
        objectPos.x = Mathf.Clamp01(objectPos.x);
        objectPos.y = Mathf.Clamp01(objectPos.y);
        transform.position = Camera.main.ViewportToWorldPoint(objectPos);
    }
    
    void AnimationLeftAndRight(float x)
    {
        if (x < 0) PlayAnimation("left");
        else if (x > 0) PlayAnimation("right");
        else if (x == 0) PlayAnimation("center");
    }

    void PlayAnimation(string name)
    {
        animator.CrossFade(name, 0);
    }

    void BulletShoot()
    {
        timer += Time.deltaTime;

        if (timer >= 1)
            canShoot = true;

        if (Input.GetMouseButton(0) && canShoot)
        {
            Bullet bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.SetDamage(damage);
            timer = 0;
            canShoot = false;
        }
    }


    [SerializeField] int health = 10;
    public void GetDamage(int damage)
    {
        if (health == 0) return;
        health -= damage;
        Debug.Log("Player: " + health);
    }
}
