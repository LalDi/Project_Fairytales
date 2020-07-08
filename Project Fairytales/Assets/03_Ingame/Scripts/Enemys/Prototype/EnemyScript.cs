using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyScript : Enemy
{
    private void Start()
    {
        attackDelay = 4;
        hp = 50;
        base.Init();
    }

    public override void AttackPattern()
    {
        ObjectManager.SpawnPool("EnemyBullet", transform.position, Quaternion.Euler(0, 0, 180));
    }
}
