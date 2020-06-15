using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    public enum List { Original_1, Original_2, Snowwhite, RedHood, Alice, HanselGretel, Dorothy, Peterpan };

	[System.Serializable]
	public struct Character
	{
		public Sprite Sprite;
		public List Name;

		public int MaxHp;
		public int Attack;
		public int AttackSpeed;

		//int DefaultItemCode;
	}

	public Character Original_1; // 남자
	public Character Original_2; // 여자
	public Character Snowwhite;
	public Character RedHood;
	public Character Alice;
	public Character HanselGretel;
	public Character Dorothy;
	public Character Peterpan;
}
