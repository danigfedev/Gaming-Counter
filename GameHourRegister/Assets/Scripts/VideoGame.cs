//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

public class VideoGame{

    private string Title;
    private string Platform;
    private string Price;
    private string Time;

    public VideoGame()//Default COnstructor
    {

    }

    public VideoGame(string title, string platform, string price, string time)
    {
        Title = title;
        Platform = platform;
        Price = price;
        Time = time;
    }

    public string GetTitle()
    {
        return Title;
    }

    public void SetTitle(string t)
    {
        Title = t;
    }

    public string GetPlatform()
    {
        return Platform;
    }

    public void SetPlatform(string pl)
    {
        Platform = pl;
    }

    public string GetPrice()
    {
        return Price;
    }

    public void SetPrice(string pr)
    {
        Price = pr;
    }

    public string GetTime()
    {
        return Time;
    }
    
    public void SetTime(string time)
    {
        Time = time;
    }
}
