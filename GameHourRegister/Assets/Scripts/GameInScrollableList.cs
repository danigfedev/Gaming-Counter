using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameInScrollableList : MonoBehaviour, IPointerClickHandler
{
    //public Text debug;

    private void Start()
    {
        //debug = GameObject.Find("debug").GetComponent<Text>();
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        string elementName = EventSystem.current.currentSelectedGameObject.name;
        elementName = elementName.Replace("(Clone)", "");
        //debug.text = elementName;
        string[] elementFragments = elementName.Split('_');
        int index = int.Parse(elementFragments[1]);

        GameObject InfoPanel = GLOBAL.GetInfoPanel();
        PlayerPrefs.SetInt("gameIndex", index);
        InfoPanel.SetActive(true);
    }


}
