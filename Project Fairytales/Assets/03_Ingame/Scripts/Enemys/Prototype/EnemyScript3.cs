using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript3 : Enemy
{
    private void Start()
    {
        base.Init();
        hp = 50;
        attackDelay = 4;
    }

    public override void AttackPattern()
    {
        ObjectManager.SpawnPool("EnemyBullet", transform.position, Quaternion.Euler(0, 0, 180));
    }
}
