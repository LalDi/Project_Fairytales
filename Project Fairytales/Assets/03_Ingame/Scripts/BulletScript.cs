using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public int speed;
    public enum Object { Player, Enemy, };
    public Object nowObject;

    void FixedUpdate()
    {
        if (nowObject == Object.Player)
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
        }
        else if (nowObject == Object.Enemy)
        {
            Vector3 rotate = transform.localRotation * Vector3.up;
            rotate.Normalize();
            transform.position += rotate * speed * Time.deltaTime;
        }
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
}
