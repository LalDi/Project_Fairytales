using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public abstract class Enemy : MonoBehaviour
{
    protected static int hp;
    protected float attackDelay;
    Vector3 trans;

    public delegate void EnemyDead();
    public static event EnemyDead OnEnemyDead;

    public static int Hp { get { return hp; } set { hp = value; } }

    protected void Init()
    {
        trans = transform.position;
        StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(attackDelay);
        AttackPattern();
        StartCoroutine(Attack());
        yield return 0;
    }

    public abstract void AttackPattern();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            transform.position = trans;
            transform.DOShakePosition(0.1f, 0.3f);
            collision.gameObject.SetActive(false);
            hp -= PlayerScript.Attack;

            if (hp <= 0)
            {
                PlayerScript.NowExp += 10;
                UIManager.Instance.SetExpBar(PlayerScript.NowExp);
                OnEnemyDead();
                gameObject.SetActive(false);
            }

        }
    }
}
