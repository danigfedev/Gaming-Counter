using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class XML_Management : MonoBehaviour {

    #region ---------------------------------- Public Variables ------------------------------------------------------
    public Text debug;

    public GameObject ListContent;
    public GameObject AddNewGamePanel;
    public GameObject InfoPanel;

    public InputField NameInput;
    public InputField PlatformInput;
    public Dropdown PlatformSelection;
    public InputField PriceInput;
    public InputField TimeInput;
    public InputField HourInput;
    public InputField MinInput;
    public InputField SecInput;


    #endregion

    #region Private Variables

    private XmlDocument m_xml;
    private XmlNodeList m_XmlRoot;
    private XmlNodeList m_gamesNodeList;

    private string m_name = "";
    private string m_platform = "";
    private string m_price = "";
    private string m_time = "";
    private string m_hour = "0";
    private string m_min = "0";
    private string m_sec = "0";

    //private string savePath = "C:/Users/Daniel/Desktop/games.xml";
    private string savePath;

    #endregion

    void Start ()
    {
        AddNewGamePanel.SetActive(false);
        InfoPanel.SetActive(false);
        GLOBAL.SetInfoPanel(InfoPanel);

        savePath = Application.persistentDataPath + Path.DirectorySeparatorChar + "gamelist.xml";

        m_xml = new XmlDocument();
        //Debug.Log(savePath);

        if (File.Exists(savePath))
        {
            //Debug.Log("LOADING");
            m_xml.Load(savePath);

        }
        else
        {
            XmlNode rootNode = m_xml.CreateNode("element", "gamelist", "");
            m_xml.AppendChild(rootNode);

            m_xml.Save(savePath);
        }

        //Loop through all data in xml and store in List (GLOBAL)
        m_XmlRoot = m_xml.SelectNodes("gamelist");
        m_gamesNodeList = m_xml.SelectNodes("gamelist/game");

        //POPULATING GAMELIST AND SCROLLABLE LIST
        //GLOBAL.GameList = new List<VideoGame>();


        GLOBAL.InitializeGameList();
        for(int i=0; i< m_gamesNodeList.Count; i++)
        {
            XmlNodeList gameData = m_gamesNodeList[i].ChildNodes;
            string title = gameData[0].FirstChild.Value;
            string platform = gameData[1].FirstChild.Value;
            string price = gameData[2].FirstChild.Value;
            string time = gameData[3].FirstChild.Value;
            AddNewGameToList(title, platform, price, time);
        }       

        //Clear Scrollable list
        //Populate Scrollable list:
        //for(int j=0; j< GLOBAL.GameList.Count;j++)
    }

    #region ---------------------------------- Public Methods ------------------------------------------------------

    //public void onElementClick()
    //{
    //    string elementName = EventSystem.current.currentSelectedGameObject.name;
    //    debug.text = elementName;
    //    elementName.Replace("(Clone)", "");
    //    string[] elementFragments = elementName.Split('_');
    //    int index = int.Parse(elementFragments[1]);

    //    PlayerPrefs.SetInt("gameIndex", index);
    //    InfoPanel.SetActive(true);
    //}

    private void AddNewGameToList(string title, string platform, string price, string time)
    {
        VideoGame newGame = new VideoGame(title, platform, price, time);

        //Populating GLOBAL.GameList
        int gameCount = GLOBAL.AddNewGameToList(newGame);

        //Populating scrollable list
        GameObject newListElement = Resources.Load<GameObject>("Prefabs/GameButton");
        newListElement.gameObject.name = "Game_" + (gameCount - 1).ToString();//Renaming element on list in order to access the right element on the list
        newListElement.transform.Find("GameTitle").GetComponent<Text>().text = title;
        newListElement.transform.Find("GamePlatform").GetComponent<Text>().text = platform;
        newListElement.transform.Find("GamePrice").GetComponent<Text>().text = price;

        
        string[] timeSplit = time.Split('+');
        string hours = timeSplit[0], mins = timeSplit[1], secs = timeSplit[2];
        /*
        if (hours == "") hours = 0.ToString();
        if (mins == "") mins = 0.ToString();
        if (secs == "") secs = 0.ToString();
        */
        time = hours + "h " + mins + "m " + secs + "s";//That's what's shown in the list
        //if (time == "") time = 0.ToString();

        newListElement.transform.Find("GameTime").GetComponent<Text>().text = time;//CONVERT THIS TO H:M:S

        Instantiate(newListElement, ListContent.transform);

    }

    private void AddNewGameToXML(string title, string platform, string price, string time)
    {
        XmlNode newGame = m_xml.CreateNode("element", "game", "");

        XmlNode newName = m_xml.CreateNode("element", "name", "");
        XmlNode newPlatform = m_xml.CreateNode("element", "platform", "");
        XmlNode newPrice = m_xml.CreateNode("element", "price", "");
        XmlNode newTime = m_xml.CreateNode("element", "time", "");


        newName.InnerText = title;
        newPlatform.InnerText = platform;
        newPrice.InnerText = price;
        //newTime.InnerText = GLOBAL.GetGlobalTime().ToString();
        if (time == "") time = 0.ToString();
        newTime.InnerText = time;

        newGame.AppendChild(newName);
        newGame.AppendChild(newPlatform);
        newGame.AppendChild(newPrice);
        newGame.AppendChild(newTime);

        m_XmlRoot[0].AppendChild(newGame);

        m_xml.Save(savePath);

    }

    #endregion



    #region ---------------------------------- Public Methods ------------------------------------------------------


    public void OpenNewGamePanel()
    {
        if (AddNewGamePanel != null)
        {
            AddNewGamePanel.SetActive(true);
        }
    }

    public void CancelNewGame()
    {
        //Reset parameters:
        AddNewGamePanel.SetActive(false);
        NameInput.text = "";
        PlatformInput.text = "";
        PriceInput.text = "";
        TimeInput.text = "";
    }

    public void RegisterNewGame()
    {
        //TODO: Check if data introduced:

        //Add new game to list
        //m_time = m_hour.ToString() + "h" + m_min.ToString() + "m" + m_sec.ToString() + "s";
        m_time = m_hour + "+" + m_min + "+" + m_sec;
        AddNewGameToList(m_name, m_platform, m_price, m_time);
        //Add game to Xml
        AddNewGameToXML(m_name, m_platform, m_price, m_time);

        AddNewGamePanel.SetActive(false);
    }

    public void onNameEnter()
    {
        m_name = NameInput.text;
        Debug.Log("INPUT: " + m_name);
    }

    public void onPlatformDropdownChange(Dropdown change)
    {
        //DropDown Menu:
        m_platform = change.options[change.value].text;//you can access the image as well
        Debug.Log(m_platform);
    }

    public void onPlatformEnter()
    {
        m_platform = PlatformInput.text;
    }

    public void onPriceEnter()
    {
        m_price = PriceInput.text;
    }

    public void onTimeEnter()
    {
        m_time = TimeInput.text;
    }

    public void onHourEnter()
    {
        m_hour = HourInput.text;
    }

    public void onMinuteEnter()
    {
        m_min = MinInput.text;
    }

    public void onSecondEnter()
    {
        m_sec = SecInput.text;
    }

    #endregion
}
