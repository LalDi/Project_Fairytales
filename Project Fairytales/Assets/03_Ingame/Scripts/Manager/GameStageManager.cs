using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStageManager : MonoBehaviour
{
    #region Singleton
    private static GameStageManager _instance;
    public static GameStageManager Instance
    {
        get
        {
            if (!_instance)
                _instance = FindObjectOfType(typeof(GameStageManager)) as GameStageManager;

            return _instance;
        }
    }
    #endregion

    public int Line, Min, Max;
    private MapScript Mapping/* = new MapScript(5, 10, 25)*/;
    private MapScript.Map[,] Maps;
    private MapScript.Map NowMap;
    private int x, y;

    public GameObject[] Enemys;
    public GameObject EnemyParent;

    public enum Direction { Left, Top, Right, Bottom }

    public int NowEnemy;

    private void Awake()
    {
        Mapping = new MapScript(Line, Min, Max);
        Maps = Mapping.Init();
        do
        {
            x = Random.Range(0, Line);
            y = Random.Range(0, Line);
            NowMap = Maps[y, x];
        } while (!NowMap.isLive);

        NowEnemy = NowMap.NowEnemy;

        foreach (MapScript.Map map in Maps)
        {
            if (map.isLive)
            {
                for (int i = 0; i < 5; i++)
                { 
                    map.Enemy[i] = Instantiate(Enemys[Random.Range(0, 3)], new Vector3(-8 + 4 * i, 10, 0), new Quaternion(0, 0, 0, 0), EnemyParent.transform);
                    if (map != NowMap)
                        map.Enemy[i].SetActive(false);
                }
            }
        }

        Enemy.OnEnemyDead += EnemyDead;
    }

    private void EnemyDead()
    {
        NowMap.NowEnemy--;
        NowEnemy = NowMap.NowEnemy;

        if (NowEnemy == 0)
        {
            NowMap.isClear = true;
            UIManager.Instance.OnMovePoint();
            if (Maps[y, x].isGoboss)
                UIManager.Instance.OnBossPoint();
        }
    }

    public bool CheckMove(Direction direction)
    {
        switch (direction)
        {
            case Direction.Left:    if (x - 1 >= 0)     if (Maps[y, x - 1].isLive)  return true;    break;
            case Direction.Top:     if (y - 1 >= 0)     if (Maps[y - 1, x].isLive)  return true;    break;
            case Direction.Right:   if (x + 1 < Line)   if (Maps[y, x + 1].isLive)  return true;    break;
            case Direction.Bottom:  if (y + 1 < Line)   if (Maps[y + 1, x].isLive)  return true;    break;
        }
        return false;
    }

    public void ChangeMap(Direction direction)
    {
        switch (direction)
        {
            case Direction.Left:    x--;    break;
            case Direction.Top:     y--;    break;
            case Direction.Right:   x++;    break;
            case Direction.Bottom:  y++;    break;
        }
        NowMap = Maps[y, x];

        UIManager.Instance.OffMovePoint();

        if (NowMap.isClear)
        {
            UIManager.Instance.OnMovePoint();
            if (NowMap.isGoboss)
                UIManager.Instance.OnBossPoint();
        }
        else
        {
            for (int i = 0; i < 5; i++)
                NowMap.Enemy[i].SetActive(true);
        }
    }
}
