using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanelHandler : MonoBehaviour {

    #region Public Variables

    //public GameObject InfoSubPanel;
    public Text gameName;
    public Text gamePlatform;
    public Text gamePrice;
    public Text gameTime;

    #endregion

    #region Private Variables

    private int gameIndex;

    #endregion



    private void OnEnable()
    {
        //Read index from playerPrefs and store it
        gameIndex = PlayerPrefs.GetInt("gameIndex");
        VideoGame game = GLOBAL.getGameFromList(gameIndex);
        
        //Update panel info
        gameName.text = game.GetTitle();
        gamePlatform.text = game.GetPlatform();
        gamePrice.text = game.GetPrice();
        //gameTime.text = game.GetTime();
        string time= game.GetTime();

        string[] timeSplit = time.Split('+');
        string hours = timeSplit[0], mins = timeSplit[0], secs = timeSplit[0];
        time = hours + "h " + mins + "m " + secs + "s";//That's what's shown in the list

        gameTime.text = time;


        //Enable panel(child)
        //InfoSubPanel.SetActive(true);

    }


    public void CloseInfoPanel()
    {
        //Delete index from player prefs:
        PlayerPrefs.DeleteKey("gameIndex");
        //InfoSubPanel.SetActive(false);
        this.gameObject.SetActive(false);
    }

    //Methods: control timer:
    //Save
    //Close
    //Edit----???????
}
