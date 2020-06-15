using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTestManager : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            PlayerScript.MaxHp = 0;
            PlayerScript.Hp = 0;
            Debug.Log("Player Dead");
        }
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            PlayerScript.Attack++;
            Debug.Log("Attack 1Up / Attack : " + PlayerScript.Attack);
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            PlayerScript.Attackspeed++;
            Debug.Log("Attackspeed 1Up / Attackspeed : " + PlayerScript.Attackspeed);
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            PlayerScript.MaxHp++;
            PlayerScript.Hp = PlayerScript.MaxHp;
            UIManager.Instance.SetHpIcon(PlayerScript.MaxHp, PlayerScript.Hp);
            Debug.Log("MaxHp Up / Max Hp : " + PlayerScript.MaxHp);
        }

    }
}
