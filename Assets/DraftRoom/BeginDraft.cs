using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SQLite;
using System.Linq;
using TMPro;
using DG.Tweening;
using static GlobalRefs;
using static ScrollingTextMgr;
using System;
using MarkLight;

/// <summary>
/// We are going to get the draft prepared and then give a splash screen for the user.  Once he clicks OK, the draft will begin with the first team
/// on the clock.  We will use a CoRoutine to run the draft and loop while there are picks left in the draft.
/// </summary>
public class BeginDraft : MonoBehaviour {
    public int numPicks = 256;
    public TextMeshProUGUI OnClockUpdateLogo;
    public TextMeshProUGUI UpdateClockTime;
    public TextMeshProUGUI UpdatePicksTB;
    public GameObject UpdatePicksPanel;
    private Image img;
    int i;
    private bool DisplayLatest;
    private bool EndDraft;
    private bool DraftStart;
    private bool pickFlashing;
    private string nextPicks;
    private bool firstLoop = true; 
    private int teamOnClockID;
    public Button startDraftBtn;
    private AudioSource pickChime;
    private delegate void PickIn(DraftPick PickMade, int NextTeamOnClock, DraftPlayers PlayerSelected);
    PickIn PickIsIn;

    //Create a list of Draft Picks
    public static List<DraftPick> draftPicks { get; set; }
    public static List<DraftPlayers> draftPlayers { get; set; }

    // Use this for initialization
    void Start () {

        pickChime = GetComponent<AudioSource>();
        //Attach the multicast delegate children
        PickIsIn += ResetClock;
        PickIsIn += UpdateDraftInfo;
        PickIsIn += FlashText;
        //PickIsIn += UpdateTicker;

        img = UpdatePicksPanel.GetComponent<Image>();
        
        //going to trigger the static button passed to the MarkView UI to click when we click the cloned button
        //nxtBtnClone.onClick.AddListener(() => ;
        //now we need to pull in the list of players in the draft
        using (SQLiteConnection db = new SQLiteConnection(DBPath))
        {
            
            draftPicks = db.Query<DraftPick>("SELECT * FROM DraftPick").ToList();
            teams = db.Query<Teams>("SELECT * FROM Teams").ToList();
            teams.Insert(0, new Teams());
        }

        //hook up the Start Button listener 
        startDraftBtn.onClick.AddListener(() => BeginCycle() );
       
	}

    private void BeginCycle()
    {
        StartCoroutine(StartDraft());
    }
   IEnumerator StartDraft()
    {
        //We are going to be in the loop here until the draft ends with the final pick
        while (!EndDraft)
        {
            //We are going to check if its the first time through so we can run the draft ticker 
            if(firstLoop)
            {
                string teamLogo;
               //Find Out Who is the team on the clock
                teamOnClockID = draftPicks[i].PickTeamIDCurr;
                teamLogo = teams[teamOnClockID].TeamNickname + "Logo";
                OnClockUpdateLogo.text = @"<sprite name=""" + teamLogo + ">";
                GetNextPicks();
                DraftStart = true;
                firstLoop = false;
                yield return null;             
            }
            else
            {
                yield return new WaitForSecondsRealtime(UnityEngine.Random.Range(5,10));
                //Call the delegate to run the various Pick Is In operations
                PickIsIn(draftPicks[i], draftPicks[i + 1].PickTeamIDCurr, draftPlayers[i + 2]);
                draftPlayers.RemoveAt(i + 2);
                i++;
                
            } 
        }
    }

    /// <summary>
    /// display next 12 picks on the board
    /// </summary>
    private void GetNextPicks()
    {
        var n=0;
        UpdatePicksTB.text = "<size=32>Upcoming Picks: </size>";
        for (n = i+1; n < i+13;n++)
        {
            UpdatePicksTB.alignment = TextAlignmentOptions.Left;
            UpdatePicksTB.text += "  <size=30><b><color=#9C2A12>" + draftPicks[n].PickNumOverall + ".</color> <color=#802714>" + teams[draftPicks[n].PickTeamIDCurr].TeamAbrev +"</color></b></size>";
            UpdatePicksTB.DOFade(1, 2.5f);
        }
    }
    private void ShowLatestPick(DraftPick PickMade, int NextTeamOnClock, DraftPlayers PlayerSelected)
    {        
        img.CrossFadeColor(Color.red, 1.5f, true, false);
        UpdatePicksTB.DOFade(1, 2.5f);
        UpdatePicksTB.alignment = TextAlignmentOptions.Center;
        UpdatePicksTB.text = "<size=+30><b>PICK IS IN</b></size>";
        StartCoroutine(DisplayLatestPick(PickMade, NextTeamOnClock,PlayerSelected));
    }

    private IEnumerator DisplayLatestPick(DraftPick PickMade, int NextTeamOnClock, DraftPlayers PlayerSelected)
    {
        DisplayLatest = true;
        //We are going to display the player selected here
        yield return new WaitForSeconds(6f);
        UpdatePicksTB.text = @"<size=-5><b>" + teams[PickMade.PickTeamIDCurr].TeamName + " " + teams[PickMade.PickTeamIDCurr].TeamNickname + "          " + PickMade.PlayerFName + " " + PickMade.PlayerLName + "   <color=black>" + PickMade.PlayerPos + "</color>   " +
                             PickMade.PlayerCollege + "</b></size> \n" + "<size=-10><b> Height: " + Math.Round((PlayerSelected.Height.Value/12)*1.0,0)+ "' " + PlayerSelected.Height.Value % 12 + "\"" + "   Weight: " +
                             PlayerSelected.Weight.Value + " lbs.   40 Yard Time: " + PlayerSelected.FortyYardTime.Value + " seconds";

        //switch back to the normal panel
        yield return new WaitForSeconds(18f);
        img.CrossFadeColor(new Color(194, 194, 194, 233), 1.5f, true, false);
        UpdatePicksTB.DOFade(0, 2.5f);
        GetNextPicks();
        UpdateTicker(PickMade, NextTeamOnClock, PlayerSelected);
    }

    void ResetClock(DraftPick PickMade, int NextTeamOnClockId, DraftPlayers PlayerSelected)
    {
        //reset the draft clock to the appropriate amount of time based on what round it is---time is in seconds!
        switch (PickMade.DraftRound) {
            case 1: DraftClock.m_timeLeft = 600f;
                break;
            case 2: DraftClock.m_timeLeft = 420f;
                break;
            case int n when (n < 7): DraftClock.m_timeLeft = 300f;
                break;
            default: DraftClock.m_timeLeft = 240f;
                break;
        } 
        
    }
    /// <summary>
    /// We need to update the Draft Board, add the players info to the current pick, etc
    /// </summary>
    /// <param name="PickMade"></param>
    void UpdateDraftInfo(DraftPick PickMade, int NextTeamOnClockId, DraftPlayers PlayerSelected)
    {
        //Add the information to the Pick
        PickMade.PlayerFName = PlayerSelected.FName;
        PickMade.PlayerLName = PlayerSelected.LName;
        PickMade.PlayerID = PlayerSelected.DraftID;
        PickMade.PlayerCollege = PlayerSelected.College;
        PickMade.PlayerPos = PlayerSelected.CollegePOS;

        //Play the Sound to indicate pick is in
       
        pickChime.Play();
       
        //TODO: Update Big Board once it's created      
    }
    /// <summary>
    /// send the update to the ticker with the player information and get the next team on the clock
    /// </summary>
    /// <param name="PickMade"></param>
    /// <param name="PlayerSelected"></param>
    void UpdateTicker(DraftPick PickMade, int NextTeamOnClockId, DraftPlayers PlayerSelected)
    {
        
        UpdateText(PickMade, NextTeamOnClockId);
       
    }

    /// <summary>
    /// This is where we Flash "Pick Is In" Text in place of the Time
    /// </summary>
    /// <param name="PickMade"></param>
    /// <param name="NextTeamOnClock"></param>
    /// <param name="PlayerSelected"></param>
    private void FlashText(DraftPick PickMade, int NextTeamOnClock, DraftPlayers PlayerSelected)
    {
        pickFlashing = true;
        UpdatePicksTB.DOFade(0, 4f);
        StartCoroutine(Blinking());
        StartCoroutine(NotBlinking(PickMade, NextTeamOnClock, PlayerSelected));
    }
    private IEnumerator Blinking()
    {
        while(pickFlashing)
        {
            UpdateClockTime.text = "<color=yellow> PICK \n IS IN!</color>";
            yield return new WaitForSeconds(0.5f);
            UpdateClockTime.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }

    private IEnumerator NotBlinking(DraftPick PickMade, int NextTeamOnClock, DraftPlayers PlayerSelected)
    {
        var teamLogo = teams[NextTeamOnClock].TeamNickname + "Logo";
        yield return new WaitForSeconds(5f);      
        pickFlashing = false;
        ShowLatestPick(PickMade, NextTeamOnClock, PlayerSelected);
        OnClockUpdateLogo.text = @"<sprite name=""" + teamLogo + ">";
    }



    // Update is called once per frame
    void Update () {

        //set the clock to the proper display each update once the draft has started      
        if (DraftStart)
        {
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
        if(!pickFlashing) UpdateClockTime.text = "<size=70%><color=white>Round " + draftPicks[i].DraftRound.ToString() + " Pick " + draftPicks[i].PickNumRound + "</color></size>\n" + DraftClock.TimeDisplay;

    }
}



