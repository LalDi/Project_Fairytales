using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScript : MonoBehaviour
{
    public class Map
    {
        public bool isLive = false;

        public GameObject[] Enemy = new GameObject[5];
        public bool isGoboss = false;
        public bool isClear = false;
        public int NowEnemy = 5;
    }


    public MapScript(int Line, int Min, int Max)
    {
        m_Line = Line;
        m_Max = Max;
        m_Min = Min;
    }

    public MapScript(int Line, int Min)
    {
        m_Line = Line;
        m_Max = Line * Line;
        m_Min = Min;
    }

    static int m_Line, m_Max, m_Min;

    //public Map[,] m_Map;
    public Map m_BossMap = new Map();

    public Map[,] Init()
    {
        int x = m_Line / 2, y = 0;

        bool b_CreateMap = false;
        int RoomCounter = 0;

        Map[,] m_Map = new Map[m_Line, m_Line];
        for (int i = 0; i < m_Line; i++)
            for (int j = 0; j < m_Line; j++)
                m_Map[i, j] = new Map();

        do
        {
            RoomCounter = 0;

            CreateMap(x, y, m_Map);

            m_Map[y, x].isLive = true;
            m_Map[y, x].isGoboss = true;

            foreach (Map Maps in m_Map)
            {
                if (Maps.isLive == true)
                    RoomCounter++;
            }

            if (RoomCounter < m_Min)
            {
                foreach (Map Maps in m_Map)
                    Maps.isLive = false;
                b_CreateMap = true;
            }
            else if (RoomCounter > m_Max)
            {
                foreach (Map Maps in m_Map)
                    Maps.isLive = false;
                b_CreateMap = true;
            }
            else
                b_CreateMap = false;
        } while (b_CreateMap);
        return m_Map;
    }

    void CreateMap(int x, int y, Map[,] m_Map)
    {
        for (int i = 0; i < 4; i++)
        {
            switch (i)
            {
                case 0:
                    if (y - 1 >= 0)
                    {
                        if (m_Map[y - 1, x].isLive == false)
                        {
                            m_Map[y - 1, x].isLive = RandomBool();
                            if (m_Map[y - 1, x].isLive == true)
                                CreateMap(x, y - 1, m_Map);
                        }
                    }
                    break;
                case 1:
                    if (y + 1 < m_Line)
                    {
                        if (m_Map[y + 1, x].isLive == false)
                        {
                            m_Map[y + 1, x].isLive = RandomBool();
                            if (m_Map[y + 1, x].isLive == true)
                                CreateMap(x, y + 1, m_Map);
                        }
                    }
                    break;
                case 2:
                    if (x - 1 >= 0)
                    {
                        if (m_Map[y, x - 1].isLive == false)
                        {
                            m_Map[y, x - 1].isLive = RandomBool();
                            if (m_Map[y, x - 1].isLive == true)
                                CreateMap(x - 1, y, m_Map);
                        }
                    }
                    break;
                case 3:
                    if (x + 1 < m_Line)
                    {
                        if (m_Map[y, x + 1].isLive == false)
                        {
                            m_Map[y, x + 1].isLive = RandomBool();
                            if (m_Map[y, x + 1].isLive == true)
                                CreateMap(x + 1, y, m_Map);
                        }
                    }
                    break;
            }
        }
    }

    bool RandomBool()
    {
        int temp;

        temp = Random.Range(0, 2);

        return temp == 0 ? true : false;
    }

}
