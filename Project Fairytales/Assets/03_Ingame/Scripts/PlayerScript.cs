using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerScript : CharacterScript
{
    Character nowCharacter;
    List CharacterName = List.Original_1;

    Vector2 clickVec;
    [SerializeField] private static int hp;
    [SerializeField] private static int maxhp;
    [SerializeField] private static int attack;
    [SerializeField] private static int attackspeed;
    [SerializeField] private static int nowExp;
    [SerializeField] private static int maxExp = 100;

    private bool b_damaged = false;

    public static int Hp { get { return hp; } set { hp = value; } }
    public static int MaxHp { get { return maxhp; } set { maxhp = value; } }
    public static int Attack { get { return attack; } set { attack = value; } }
    public static int Attackspeed { get { return attackspeed; } set { attackspeed = value; } }
    public static int NowExp { get { return nowExp; } set { nowExp = value; } }
    public static int MaxExp { get { return maxExp; } set { maxExp = value; } }

    private void Start()
    {
        
        switch (CharacterName)
        {
            case List.Original_1:
                nowCharacter = Original_1;
                break;
            case List.Original_2:
                nowCharacter = Original_2;
                break;
            case List.Snowwhite:
                nowCharacter = Snowwhite;
                break;
            case List.RedHood:
                nowCharacter = RedHood;
                break;
            case List.Alice:
                nowCharacter = Alice;
                break;
            case List.HanselGretel:
                nowCharacter = HanselGretel;
                break;
            case List.Dorothy:
                nowCharacter = Dorothy;
                break;
            case List.Peterpan:
                nowCharacter = Peterpan;
                break;
        }

        maxhp = nowCharacter.MaxHp;
        hp = maxhp;
        attack = nowCharacter.Attack;
        attackspeed = nowCharacter.AttackSpeed;

        Debug.Log(maxhp + " " + attack + " " + attackspeed);

        /*
        maxhp = 3;
        hp = maxhp;
        attack = 2;
        attackspeed = 10;
        */

        UIManager.Instance.SetHpIcon(maxhp, hp);
        StartCoroutine(Shoot());
    }

    void FixedUpdate()
    {
        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())//Input.GetTouch(0).fingerId
        {
            //clickVec = Input.mousePosition;
            //clickVec = camera.ScreenToWorldPoint(clickVec);
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position -= Camera.main.transform.position;
        }
    }

    IEnumerator Shoot()
    {
        var delay = 1f / Attackspeed;
        yield return new WaitForSeconds(delay);
        ObjectManager.SpawnPool("PlayerBullet", transform.position, transform.rotation);
        StartCoroutine(Shoot());
        yield return null;
    }

    IEnumerator Damaged()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        Color Red = new Color(255, 0, 0);
        Color Default = new Color(255, 255, 255);
        b_damaged = true;
        for (int i = 0; i < 6; i++)
        {
            if (i % 2 == 0)
                renderer.color = Red;
            else
                renderer.color = Default;
            yield return new WaitForSeconds(0.1f);
        }
        b_damaged = false;
        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet") && !b_damaged)
        {
            collision.gameObject.SetActive(false);
            //StopCoroutine(Damaged()); 무적시간에 피격시 무적시간이 증가되는 것을 방지
            StartCoroutine(Damaged());
            hp--;
            UIManager.Instance.SetHpIcon(maxhp, hp);
        }
    }

}