﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScrollingTextMgr : MonoBehaviour {

    public TextMeshProUGUI TextMeshProComponent;
    public float ScrollSpeed = 1;

    private TextMeshProUGUI m_cloneTextObj;
    private RectTransform m_textRectTransform;
    private string origText;
    private bool hasTextChanged;

    private void Awake()
    {     
        m_textRectTransform = TextMeshProComponent.GetComponent<RectTransform>();

        m_cloneTextObj = Instantiate(TextMeshProComponent) as TextMeshProUGUI;
        RectTransform cloneRectTransform = m_cloneTextObj.GetComponent<RectTransform>();
        cloneRectTransform.SetParent(m_textRectTransform);
        cloneRectTransform.anchorMin = new Vector2(1, 0.5f);
        cloneRectTransform.localScale = new Vector3(1, 1, 1);
        origText = TextMeshProComponent.text;
       
    }

    void OnEnable()
    {
        // Subscribe to event fired when text object has been regenerated.
        TMPro_EventManager.TEXT_CHANGED_EVENT.Add(ON_TEXT_CHANGED);

    }

    void OnDisable()
    {
        TMPro_EventManager.TEXT_CHANGED_EVENT.Remove(ON_TEXT_CHANGED);
    }

    //checks to see if it the text has changed
    void ON_TEXT_CHANGED(Object obj)
    {
        if (obj == TextMeshProComponent)
            hasTextChanged = true;
    }

    // Use this for initialization
    IEnumerator Start () {
        float width = TextMeshProComponent.preferredWidth;
        Vector3 startPosition = m_textRectTransform.position;

        float scrollPosition = 0;

        while (true)
        {
            
           //recompute the width of the REctTransfrom if the text object has changed
           if (hasTextChanged)
            {
                width = TextMeshProComponent.preferredWidth;
                m_cloneTextObj.text = TextMeshProComponent.text;               
            }
            TextMeshProComponent.text = origText + "  " + DraftClock.TimeDisplay;
            m_cloneTextObj.rectTransform.position = new Vector3(m_cloneTextObj.rectTransform.position.x,-4.205f, m_cloneTextObj.rectTransform.position.z);
            if (m_cloneTextObj.rectTransform.position.x <= -15) scrollPosition = -m_cloneTextObj.rectTransform.position.x;
            //Scroll the text across the screen by moving the RectTransform
            m_textRectTransform.position = new Vector3((-scrollPosition % width), startPosition.y, startPosition.z);
            scrollPosition += ScrollSpeed  * Time.deltaTime;
            Debug.Log(m_cloneTextObj.rectTransform.position.x);
            yield return null;
        }
	}
    
	// Update is called once per frame
	void Update () {
        if (DraftClock.m_timeLeft > 0f)
        {
            //Update Countdown Clock
            DraftClock.m_timeLeft -= Time.deltaTime;
            DraftClock.Minutes = DraftClock.GetLeftMinutes();
            DraftClock.Seconds = DraftClock.GetLeftSeconds();

            if (DraftClock.m_timeLeft > 0f)
            {
                DraftClock.TimeDisplay = DraftClock.Minutes + ":" + DraftClock.Seconds.ToString("00");
            }
            else
            {
                DraftClock.TimeDisplay = "00:00";
            }

        }
    }


}
