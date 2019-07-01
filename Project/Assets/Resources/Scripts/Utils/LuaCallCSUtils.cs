using UnityEngine;
using System.Collections.Generic;

[XLua.LuaCallCSharp]

public class LuaCallCSUtils {

    public static LuaCallCSUtils m_instance;

    public LuaCallCSUtils()
    {
        if (m_instance == null)
        {
            m_instance = this;
        }
    }

    public static void PrintTest()
    {
        Debug.Log("PrintTest");
    }

    public static int GetPlayerGold()
    {
        return GameManager.m_instance.GetPlayerGold();
    }

    public static int GetRandomValue(int value)
    {
        return Random.Range(1, value);
    }

    public static void UnloadGameScene()
    {
        GameManager.m_instance.LoadScene("Void");
    }

    public static void LoadGameScene()
    {
        GameManager.m_instance.LoadGameScene();
    }

    public static bool PlayerPrefsHasKey(string key)
    {
        return PlayerPrefs.HasKey(key);
    }

    public static void PlayerPrefSetInt(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
    }
    
    public static void RoleStartMove()
    {
        TileManager.m_instance.StartMove();
    }

}
