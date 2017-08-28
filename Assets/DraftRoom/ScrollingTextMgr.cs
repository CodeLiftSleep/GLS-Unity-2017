using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Data;
using System.Text;

public class ScrollingTextMgr : MonoBehaviour {
    public Button StartDraftBtn;
    public TextMeshProUGUI TextMeshProComponent;
 
    public float ScrollSpeed = 1;
    public DataTable DraftDT = new DataTable();
    private TextMeshProUGUI m_cloneTextObj;
    private RectTransform m_textRectTransform;
    private static string origText;
    private bool hasTextChanged;
    private static string updateText;
  
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
    void ON_TEXT_CHANGED(UnityEngine.Object obj)
    {
        if (obj == TextMeshProComponent)
            hasTextChanged = true;
    }

    // Use this for initialization
    void Start () {
        float width = TextMeshProComponent.preferredWidth;
        Vector3 startPosition = m_textRectTransform.position;
        float scrollPosition = 0;

        //Start when the start button is clicked
        StartDraftBtn.onClick.AddListener(() => StartScroll(width, startPosition, scrollPosition));
	}

    private void StartScroll (float width, Vector3 startPosition, float scrollPosition)
    {
        StartCoroutine(StartDraft(width, startPosition, scrollPosition));
    }
    IEnumerator StartDraft(float width, Vector3 startPosition, float scrollPosition)
    {
        while (true)
        {

            //recompute the width of the REctTransfrom if the text object has changed
            if (hasTextChanged)
            {
                width = TextMeshProComponent.preferredWidth;
                m_cloneTextObj.text = TextMeshProComponent.text;
            }
            TextMeshProComponent.text = origText;
            m_cloneTextObj.rectTransform.position = new Vector3(m_cloneTextObj.rectTransform.position.x, -4.52f, m_cloneTextObj.rectTransform.position.z);
            if (m_cloneTextObj.rectTransform.position.x <= -15) scrollPosition = -m_cloneTextObj.rectTransform.position.x;
            //Scroll the text across the screen by moving the RectTransform
            
            if (width != 0)
            {
                m_textRectTransform.position = new Vector3((-scrollPosition % width), startPosition.y, startPosition.z);
                scrollPosition += ScrollSpeed * Time.deltaTime;
            }
           
            
            yield return null;
        }
    }

    /// <summary>
    /// This will update the text component every time a draft pick comes in
    /// </summary>
    /// <param name="NewPick"></param>
    /// <param name="TeamOnClockID"></param>
    public static void UpdateText(DraftPick NewPick, int TeamOnClockID)
    {
        //Create a stringbuilder for learge string joining operations.
        
        //we need to get the information as to what Team this is
        string teamLogo = GlobalRefs.teams[NewPick.PickTeamIDCurr].TeamNickname + "Logo";
        updateText =  @"   <sprite name=""" +teamLogo+ "" + "><color=white> Pick " + NewPick.PickNumRound + ":</color><color=black><b> " + NewPick.PlayerFName + " " +
            NewPick.PlayerLName + " " + NewPick.PlayerPos + ",  " + NewPick.PlayerCollege + "</b></color>";

        origText += updateText;
    }
    // Update is called once per frame
    void Update()
    {

    }

}
