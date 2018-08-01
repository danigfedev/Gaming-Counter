using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Runtime.Serialization.Formatters.Binary;

public static class GLOBAL {

    //*********************************** VARIABLES ****************************************

    #region Public Variables 

    

    #endregion


    #region Private Variables

    private static float g_Time;
    private static List<VideoGame> GameList;
    private static GameObject InfoPanel = null;

    #endregion

    //*********************************** METHODS ****************************************
    #region Public Methods

    public static void SetGlobalTime(float time)
    {
        Debug.Log("Registered time: " + time);
        g_Time = time;

    }

    public static float GetGlobalTime()
    {
        return g_Time;
    }

    public static void InitializeGameList()
    {
        GameList = new List<VideoGame>();
    }

    public static int AddNewGameToList(VideoGame newGame)
    {
        GameList.Add(newGame);
        return GameList.Count;
    }

    public static VideoGame getGameFromList(int index)
    {
        return GameList[index];
    }

    public static void SetInfoPanel(GameObject panel)
    {
        InfoPanel = panel;
    }

    public static GameObject GetInfoPanel()
    {
        return InfoPanel;
    }

    #endregion
}
