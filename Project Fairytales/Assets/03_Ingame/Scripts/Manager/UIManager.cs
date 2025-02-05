﻿using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Singleton
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (!_instance)
                _instance = FindObjectOfType(typeof(UIManager)) as UIManager;

            return _instance;
        }
    }
    #endregion

    public GameObject Pause;
    public Image[] HpIcon = new Image[5];
    public Sprite[] HpSprite = new Sprite[2];
    public Text HpText;
    public Image ExpBar;
    public Button[] MovePoint = new Button[4]; // Left, Top, Right, Bottom
    public Button BossMovePoint;

    public delegate void ClearMap();
    public static event ClearMap OnClearMap;


    public void ClickPause()
    {
        if (!Pause.activeSelf)
        {
            Pause.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void ResumeButton()
    {
        if (Pause.activeSelf)
        {
            Pause.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void ResetButton()
    {
        Time.timeScale = 1;
        
    }

    public void TitleButton()
    {
        Time.timeScale = 1;

    }

    public void SetHpIcon(int MaxHp, int Hp)
    {
        MaxHp--; Hp--;
        if (MaxHp < 5)
        {
            if (HpText.IsActive())
                HpText.gameObject.SetActive(false);
            
            for (int i = 0; i < 5; i++)
            {
                if (i <= MaxHp)
                {
                    HpIcon[i].gameObject.SetActive(true);
                    if (i <= Hp)
                        HpIcon[i].sprite = HpSprite[1];
                    else
                        HpIcon[i].sprite = HpSprite[0];
                }
                else
                    HpIcon[i].gameObject.SetActive(false);
            }
        }
        else
        {
            for (int i = 0; i < 5; i++)
                HpIcon[i].gameObject.SetActive(false);
            for (int i = 0; i < 3; i++)
            {
                HpIcon[i].gameObject.SetActive(true);
                if (i <= Hp)
                    HpIcon[i].sprite = HpSprite[1];
                else
                    HpIcon[i].sprite = HpSprite[0];
            }
            HpText.gameObject.SetActive(true);
            HpText.text = "+ " + (MaxHp - 2).ToString();
        }
    }

    public void SetExpBar(int NowExp, int MaxExp = 100)
    {
        ExpBar.fillAmount = (float)NowExp / MaxExp;
    }

    public void OnMovePoint()
    {
        if (GameStageManager.Instance.CheckMove("Left"))
            if (!MovePoint[0].gameObject.activeSelf)
                MovePoint[0].gameObject.SetActive(true);

        if (GameStageManager.Instance.CheckMove("Top"))
            if (!MovePoint[1].gameObject.activeSelf)
                MovePoint[1].gameObject.SetActive(true);

        if (GameStageManager.Instance.CheckMove("Right"))
            if (!MovePoint[2].gameObject.activeSelf)
                MovePoint[2].gameObject.SetActive(true);

        if (GameStageManager.Instance.CheckMove("Bottom"))
            if (!MovePoint[3].gameObject.activeSelf)
                MovePoint[3].gameObject.SetActive(true);
    }

    public void OffMovePoint()
    {
        foreach (Button obj in MovePoint)
        {
            if (obj.gameObject.activeSelf)
                obj.gameObject.SetActive(false);
        }
        if (BossMovePoint.gameObject.activeSelf)
            BossMovePoint.gameObject.SetActive(false);
    }

    public void OnBossPoint()
    {
        if (MovePoint[1].gameObject.activeSelf)
            MovePoint[1].gameObject.SetActive(false);
        BossMovePoint.gameObject.SetActive(true);
    }

}
