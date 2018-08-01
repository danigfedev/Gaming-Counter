using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Timer : MonoBehaviour {

    #region Public Variables

    public Text chrono;
    public Button ChronoBTN;

    #endregion

    #region Private Variables

    private bool m_StartTimer = false;

    private System.DateTime m_InitialTime;
    private int m_StartTime = 0;
    private int m_CurrentTime = 0;
    private int m_StoredTime = 0;

    private int h = 0, m = 0, s = 0;

    #endregion
    
	// Update is called once per frame
	void Update () {

        //Debug.Log("Time: " + Time.time + " realTime: " + Time.realtimeSinceStartup);
        if (m_StartTimer)
        {
            //int newTime = (System.DateTime.Now - m_InitialTime).TotalSeconds;
            double a = (System.DateTime.Now - m_InitialTime).TotalSeconds;
            
            Debug.Log("Total Seconds: " + System.Convert.ToInt32(System.Math.Floor(a)));
            int totalSeconds = System.Convert.ToInt32(System.Math.Floor(a));
            //chrono.text = System.Convert.ToInt32(System.Math.Floor(a)).ToString();
            ConvertSecondsToTime(totalSeconds, out h, out m, out s);
            chrono.text= h + "h " + m + "m " + s + "s";
        }
	}

    public void StartTimer()
    {
        
        if (!m_StartTimer)//Start Timer
        {
            m_InitialTime = System.DateTime.Now;
            ChronoBTN.transform.GetComponentInChildren<Text>().text = "Stop";
            //m_CurrentTime = 0;
            //m_StartTime = Time.time;
            m_StartTimer = true;
            //GLOBAL.setTime(m_CurrentTime);
            GLOBAL.SetGlobalTime(m_CurrentTime);

        }
        else//Pause Timer
        {   
            ChronoBTN.transform.GetComponentInChildren<Text>().text = "Start";
            m_StartTimer = false;
            m_StoredTime = m_CurrentTime;
            GLOBAL.SetGlobalTime(m_CurrentTime);
        }
        
    }

    private void ConvertSecondsToTime(int seconds, out int h, out int m, out int s)
    {
        s = Mathf.FloorToInt(seconds % 60);//secs
        float mins = seconds / 60;
        m = Mathf.FloorToInt(mins % 60);//mins
        h = Mathf.FloorToInt(mins / 60);

        //Debug.Log("H: " + h + " M: " + m + " S: " + s);
    }

    private int ConvertTimeToSeconds(int hours, int mins, int secs)
    {
        int totalSeconds = secs + mins * 60 + hours * 3600;
        return totalSeconds;
    }


}
