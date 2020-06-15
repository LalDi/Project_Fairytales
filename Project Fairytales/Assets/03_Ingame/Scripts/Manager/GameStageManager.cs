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
            if (y != 0 && Maps[y - 1, x].isGoboss)
                UIManager.Instance.OnBossPoint();
        }
    }

    public bool CheckMove(string direction)
    {
        switch (direction)
        {
            case "Left":    if (x - 1 >= 0)     return true;    break;
            case "Top":     if (y - 1 >= 0)     return true;    break;
            case "Right":   if (x + 1 < Line)   return true;    break;
            case "Bottom":  if (y + 1 < Line)   return true;    break;
        }
        return false;
    }

    public void ChangeMap(int value, bool isGoX)
    {
        if (isGoX)
            x += value;
        else
            y += value;

        NowMap = Maps[y, x];
        for (int i = 0; i < 5; i++)
            if (!NowMap.isClear)
                NowMap.Enemy[i].SetActive(true);
    }
}
