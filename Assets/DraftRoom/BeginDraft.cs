using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SQLite;
using System.Linq;
using TMPro;
using static GlobalRefs;
using static ScrollingTextMgr;
using System;

/// <summary>
/// We are going to get the draft prepared and then give a splash screen for the user.  Once he clicks OK, the draft will begin with the first team
/// on the clock.  We will use a CoRoutine to run the draft and loop while there are picks left in the draft.
/// </summary>
public class BeginDraft : MonoBehaviour {
    public int numPicks = 256;
    public TextMeshProUGUI OnClockUpdateLogo;
    public TextMeshProUGUI UpdateClockTime;
    int i;
    private bool EndDraft;
    private bool DraftStart;
    private bool pickFlashing;
    private bool firstLoop = true; 
    private int teamOnClockID;
    public Button startDraftBtn;
    private AudioSource pickChime;
    private delegate void PickIn(DraftPick PickMade, int NextTeamOnClock, DraftPlayers PlayerSelected);
    PickIn PickIsIn;

    //Create a list of Draft Picks
    List<DraftPick> draftPicks = new List<DraftPick>();
    List<DraftPlayers> draftPlayers = new List<DraftPlayers>();

    // Use this for initialization
    void Start () {

        
        pickChime = GetComponent<AudioSource>();
        //Attach the multicast delegate children
        PickIsIn += ResetClock;
        PickIsIn += UpdateDraftInfo;
        PickIsIn += FlashText;
        //PickIsIn += UpdateTicker;
        
    
       //now we need to pull in the list of players in the draft
        using (SQLiteConnection db = new SQLiteConnection(DBPath))
        {
            draftPlayers = db.Query<DraftPlayers>("SELECT * FROM DraftPlayers").ToList();
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
                DraftStart = true;
                firstLoop = false;
                yield return null;             
            }
            else
            {
                yield return new WaitForSecondsRealtime(30f);
                //Call the delegate to run the various Pick Is In operations
                PickIsIn(draftPicks[i], draftPicks[i + 1].PickTeamIDCurr, draftPlayers[i + 2]);
                draftPlayers.RemoveAt(i + 2);
                i++;
                
            } 
        }
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
        var teamLogo = teams[NextTeamOnClockId].TeamNickname + "Logo";
        UpdateText(PickMade, NextTeamOnClockId);
        OnClockUpdateLogo.text = @"<sprite name=""" + teamLogo + ">";
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
        yield return new WaitForSeconds(5f);      
        pickFlashing = false;
        UpdateTicker(PickMade, NextTeamOnClock, PlayerSelected);

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



