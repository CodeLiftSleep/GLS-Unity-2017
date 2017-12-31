using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SQLite;
using System.Linq;
using TMPro;
using DG.Tweening;
using static GLS.GlobalRefs;
using static Draft.ScrollingTextMgr;

using System;
using MarkLight;
using GLS;

namespace Draft
{
    /// <summary>
    /// We are going to get the draft prepared and then give a splash screen for the user.  Once he clicks OK, the draft will begin with the first team
    /// on the clock.  We will use a CoRoutine to run the draft and loop while there are picks left in the draft.
    /// </summary>
    public class BeginDraft : MonoBehaviour, IEvaluate
    {
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
        public static OvDraftAI[] TeamDraftAI;
        private int teamOnClockID;
        public Button startDraftBtn;
        private AudioSource pickChime;
        private delegate void PickIn(GLS.DraftPick PickMade, int NextTeamOnClock, GLS.DraftPlayers PlayerSelected);
        PickIn PickIsIn;

        //Create a list of Draft Picks
        public static List<GLS.DraftPick> draftPicks { get; set; }
        public static List<GLS.DraftPlayers> draftPlayers { get; set; }
        public string Result { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        //This keeps a list of all the trade offers that are on the table...check the list before deciding what to do.
        public static List<TradeOffer> TradeOffers = new List<TradeOffer>();
        public static List<GLS.DraftDepth> draftDepth = new List<GLS.DraftDepth>();

        // Use this for initialization
        void Start()
        {

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

                draftPicks = db.Query<GLS.DraftPick>("SELECT * FROM DraftPick").ToList();
                teams = db.Query<GLS.Teams>("SELECT * FROM Teams").ToList();
                teams.Insert(0, new GLS.Teams());

                //create the DraftAI structs to use foreach team
                for (int i = 1; i < teams.Count + 1; i++)
                {
                    TeamDraftAI[i] = new OvDraftAI();
                    TeamDraftAI[i].posNeedTeam = db.Query<GLS.TeamNeeds>("SELECT * FROM TeamNeeds WHERE TeamID = ?", i).ToList();
                }
            }


            //hook up the Start Button listener 
            startDraftBtn.onClick.AddListener(() => BeginCycle());

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
                if (firstLoop)
                {
                    string teamLogo;
                    //Find Out Who is the team on the clock
                    teamOnClockID = draftPicks[i].PickTeamIDCurr;
                    teamLogo = teams[teamOnClockID].TeamNickname + "Logo";
                    OnClockUpdateLogo.text = @"<sprite name=""" + teamLogo + ">";
                    Evaluate();
                    GetNextPicks();
                    DraftStart = true;
                    firstLoop = false;
                    yield return null;
                }
                else
                {
                    yield return new WaitForSecondsRealtime(UnityEngine.Random.Range(5, 10));
                    //Call the delegate to run the various Pick Is In operations
                    Evaluate();
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
            var n = 0;
            UpdatePicksTB.text = "<size=32>Upcoming Picks: </size>";
            for (n = i + 1; n < i + 13; n++)
            {
                UpdatePicksTB.alignment = TextAlignmentOptions.Left;
                UpdatePicksTB.text += "  <size=30><b><color=#9C2A12>" + draftPicks[n].PickNumOverall + ".</color> <color=#802714>" + teams[draftPicks[n].PickTeamIDCurr].TeamAbrev + "</color></b></size>";
                UpdatePicksTB.DOFade(1, 2.5f);
            }
        }
        private void ShowLatestPick(GLS.DraftPick PickMade, int NextTeamOnClock, GLS.DraftPlayers PlayerSelected)
        {
            img.CrossFadeColor(Color.red, 1.5f, true, false);
            UpdatePicksTB.DOFade(1, 2.5f);
            UpdatePicksTB.alignment = TextAlignmentOptions.Center;
            UpdatePicksTB.text = "<size=+30><b>PICK IS IN</b></size>";
            StartCoroutine(DisplayLatestPick(PickMade, NextTeamOnClock, PlayerSelected));
        }

        private IEnumerator DisplayLatestPick(GLS.DraftPick PickMade, int NextTeamOnClock, GLS.DraftPlayers PlayerSelected)
        {
            DisplayLatest = true;
            //We are going to display the player selected here
            yield return new WaitForSeconds(6f);
            UpdatePicksTB.text = @"<size=-5><b>" + teams[PickMade.PickTeamIDCurr].TeamName + " " + teams[PickMade.PickTeamIDCurr].TeamNickname + "          " + PickMade.PlayerFName + " " + PickMade.PlayerLName + "   <color=black>" + PickMade.PlayerPos + "</color>   " +
                                 PickMade.PlayerCollege + "</b></size> \n" + "<size=-10><b> Height: " + Math.Round((PlayerSelected.Height.Value / 12) * 1.0, 0) + "' " + PlayerSelected.Height.Value % 12 + "\"" + "   Weight: " +
                                 PlayerSelected.Weight.Value + " lbs.   40 Yard Time: " + PlayerSelected.FortyYardTime.Value + " seconds";

            //switch back to the normal panel
            yield return new WaitForSeconds(18f);
            img.CrossFadeColor(new Color(194, 194, 194, 233), 1.5f, true, false);
            UpdatePicksTB.DOFade(0, 2.5f);
            GetNextPicks();
            UpdateTicker(PickMade, NextTeamOnClock, PlayerSelected);
        }

        void ResetClock(GLS.DraftPick PickMade, int NextTeamOnClockId, GLS.DraftPlayers PlayerSelected)
        {
            //reset the draft clock to the appropriate amount of time based on what round it is---time is in seconds!
            switch (PickMade.DraftRound)
            {
                case 1:
                    DraftClock.m_timeLeft = 600f;
                    break;
                case 2:
                    DraftClock.m_timeLeft = 420f;
                    break;
                case int n when (n < 7):
                    DraftClock.m_timeLeft = 300f;
                    break;
                default:
                    DraftClock.m_timeLeft = 240f;
                    break;
            }

        }
        /// <summary>
        /// We need to update the Draft Board, add the players info to the current pick, etc
        /// </summary>
        /// <param name="PickMade"></param>
        void UpdateDraftInfo(GLS.DraftPick PickMade, int NextTeamOnClockId, GLS.DraftPlayers PlayerSelected)
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
        void UpdateTicker(GLS.DraftPick PickMade, int NextTeamOnClockId, GLS.DraftPlayers PlayerSelected)
        {

            UpdateText(PickMade, NextTeamOnClockId);

        }

        /// <summary>
        /// This is where we Flash "Pick Is In" Text in place of the Time
        /// </summary>
        /// <param name="PickMade"></param>
        /// <param name="NextTeamOnClock"></param>
        /// <param name="PlayerSelected"></param>
        private void FlashText(GLS.DraftPick PickMade, int NextTeamOnClock, GLS.DraftPlayers PlayerSelected)
        {
            pickFlashing = true;
            UpdatePicksTB.DOFade(0, 4f);
            StartCoroutine(Blinking());
            StartCoroutine(NotBlinking(PickMade, NextTeamOnClock, PlayerSelected));
        }
        private IEnumerator Blinking()
        {
            while (pickFlashing)
            {
                UpdateClockTime.text = "<color=yellow> PICK \n IS IN!</color>";
                yield return new WaitForSeconds(0.5f);
                UpdateClockTime.text = "";
                yield return new WaitForSeconds(0.5f);
            }
        }

        private IEnumerator NotBlinking(GLS.DraftPick PickMade, int NextTeamOnClock, GLS.DraftPlayers PlayerSelected)
        {
            var teamLogo = teams[NextTeamOnClock].TeamNickname + "Logo";
            yield return new WaitForSeconds(5f);
            pickFlashing = false;
            ShowLatestPick(PickMade, NextTeamOnClock, PlayerSelected);
            OnClockUpdateLogo.text = @"<sprite name=""" + teamLogo + ">";
        }



        // Update is called once per frame
        void Update()
        {

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
            if (!pickFlashing) UpdateClockTime.text = "<size=70%><color=white>Round " + draftPicks[i].DraftRound.ToString() + " Pick " + draftPicks[i].PickNumRound + "</color></size>\n" + DraftClock.TimeDisplay;

        }
        //Here we evaluate what the team is wanting to do.
        public void Evaluate()
        {
            throw new NotImplementedException();
        }

        public class OvDraftAI
        {
            public int TeamID;
            //groups of players this team is targeting---GroupOne are the ones they want most, group two the next group, etc...
            public List<GLS.DraftPlayers> GroupOne;
            public List<GLS.DraftPlayers> GroupTwo;
            public List<GLS.DraftPlayers> GroupThree;
            public List<GLS.DraftPlayers> GroupFour;
            public List<GLS.DraftPlayers> GroupFive;

            
            public List<GLS.DraftPlayers> QB;
            public List<GLS.DraftPlayers> RB;
            public List<GLS.DraftPlayers> FB;
            public List<GLS.DraftPlayers> WR;
            public List<GLS.DraftPlayers> TE;
            public List<GLS.DraftPlayers> OT;
            public List<GLS.DraftPlayers> OG;
            public List<GLS.DraftPlayers> OC;
            public List<GLS.DraftPlayers> DE;
            public List<GLS.DraftPlayers> DT;
            public List<GLS.DraftPlayers> OLB;
            public List<GLS.DraftPlayers> ILB;
            public List<GLS.DraftPlayers> CB;
            public List<GLS.DraftPlayers> FS;
            public List<GLS.DraftPlayers> SS;
            public List<GLS.DraftPlayers> K;
            public List<GLS.DraftPlayers> P;

            //type of player the team is looking for at that position---PartTimeStarter would be like Slot WR or Nickel CB--Key Role Player like Pass Rush Secialist, etc
            playerType playerTypes;
            //types of players the team needs based on posStrength
            public List<GLS.TeamNeeds> posNeedTeam;
            //Dictionary of relative strength of each position on the team
            public Dictionary<string, int> posStrengthTeam;
            //Dictionary of the % dropoff between best player at this position and next best player
            public List<GLS.PosDropOff> dropOffPercentage;
            public enum FirstRndStrat { MakeSplash = 1, AggressiveUp, AggressiveDown, AccumulatePicks };
            public enum SecondRndStrat { }

            //default constructor---initialize the varius components
            public OvDraftAI()
            {
                playerTypes = new playerType();
                GroupOne = new List<GLS.DraftPlayers>();
                GroupTwo = new List<GLS.DraftPlayers>();
                GroupThree = new List<GLS.DraftPlayers>();
                GroupFour = new List<GLS.DraftPlayers>();
                GroupFive = new List<GLS.DraftPlayers>();
                posNeedTeam = new List<GLS.TeamNeeds>();

                QB = new List<GLS.DraftPlayers>();
                RB = new List<GLS.DraftPlayers>();
                FB = new List<GLS.DraftPlayers>();
                WR = new List<GLS.DraftPlayers>();
                TE = new List<GLS.DraftPlayers>();
                OT = new List<GLS.DraftPlayers>();
                OG = new List<GLS.DraftPlayers>();
                OC = new List<GLS.DraftPlayers>();
                DE = new List<GLS.DraftPlayers>();
                DT = new List<GLS.DraftPlayers>();
                OLB = new List<GLS.DraftPlayers>();
                ILB = new List<GLS.DraftPlayers>();
                CB = new List<GLS.DraftPlayers>();
                FS = new List<GLS.DraftPlayers>();
                SS = new List<GLS.DraftPlayers>();
                K = new List<GLS.DraftPlayers>();
                P = new List<GLS.DraftPlayers>();

                //Get a list of all players at each position
                using (SQLiteConnection db = new SQLiteConnection(DBPath))
                {
                    QB = db.Query<GLS.DraftPlayers>("SELECT * FROM DraftPlayers WHERE CollegePOS = ?", "QB").OrderByDescending(x => x.ActualGrade).ToList();
                    RB = db.Query<GLS.DraftPlayers>("SELECT * FROM DraftPlayers WHERE CollegePOS = ?", "RB").OrderByDescending(x => x.ActualGrade).ToList();
                    FB = db.Query<GLS.DraftPlayers>("SELECT * FROM DraftPlayers WHERE CollegePOS = ?", "FB").OrderByDescending(x => x.ActualGrade).ToList();
                    WR = db.Query<GLS.DraftPlayers>("SELECT * FROM DraftPlayers WHERE CollegePOS = ?", "WR").OrderByDescending(x => x.ActualGrade).ToList();
                    TE = db.Query<GLS.DraftPlayers>("SELECT * FROM DraftPlayers WHERE CollegePOS = ?", "TE").OrderByDescending(x => x.ActualGrade).ToList();
                    OT = db.Query<GLS.DraftPlayers>("SELECT * FROM DraftPlayers WHERE CollegePOS = ?", "OT").OrderByDescending(x => x.ActualGrade).ToList();
                    OG = db.Query<GLS.DraftPlayers>("SELECT * FROM DraftPlayers WHERE CollegePOS = ?", "OG").OrderByDescending(x => x.ActualGrade).ToList();
                    OC = db.Query<GLS.DraftPlayers>("SELECT * FROM DraftPlayers WHERE CollegePOS = ?", "OC").OrderByDescending(x => x.ActualGrade).ToList();
                    DE = db.Query<GLS.DraftPlayers>("SELECT * FROM DraftPlayers WHERE CollegePOS = ?", "DE").OrderByDescending(x => x.ActualGrade).ToList();
                    DT = db.Query<GLS.DraftPlayers>("SELECT * FROM DraftPlayers WHERE CollegePOS = ?", "DT").OrderByDescending(x => x.ActualGrade).ToList();
                    OLB = db.Query<GLS.DraftPlayers>("SELECT * FROM DraftPlayers WHERE CollegePOS = ?", "OLB").OrderByDescending(x => x.ActualGrade).ToList();
                    ILB = db.Query<GLS.DraftPlayers>("SELECT * FROM DraftPlayers WHERE CollegePOS = ?", "ILB").OrderByDescending(x => x.ActualGrade).ToList();
                    CB = db.Query<GLS.DraftPlayers>("SELECT * FROM DraftPlayers WHERE CollegePOS = ?", "CB").OrderByDescending(x => x.ActualGrade).ToList();
                    SS = db.Query<GLS.DraftPlayers>("SELECT * FROM DraftPlayers WHERE CollegePOS = ?", "SS").OrderByDescending(x => x.ActualGrade).ToList();
                    FS = db.Query<GLS.DraftPlayers>("SELECT * FROM DraftPlayers WHERE CollegePOS = ?", "FS").OrderByDescending(x => x.ActualGrade).ToList();
                    K = db.Query<GLS.DraftPlayers>("SELECT * FROM DraftPlayers WHERE CollegePOS = ?", "K").OrderByDescending(x => x.ActualGrade).ToList();
                    P = db.Query<GLS.DraftPlayers>("SELECT * FROM DraftPlayers WHERE CollegePOS = ?", "P").OrderByDescending(x => x.ActualGrade).ToList();
                }
                posStrengthTeam = new Dictionary<string, int>();
                dropOffPercentage = new List<GLS.PosDropOff>();
            }
        }
        public enum playerType { Starter = 1, PartTimeStarter, KeyRolePlayer, PrimaryBackup, Depth, StarterBackup, StarterDepth, KRPBackup, KRPDepth, BackupDepth };

        //This class keeps trade offers between teams
        public class TradeOffer
        {
            public int InitiatingTeamID;
            public int TradePartnerTeamID;
            public int[] InitiatingPickNums;
            public int[] TradePartnerPickNums;
            public double InitiatingTeamPickValue; //the amount of "points" the initiating team is offering in points
            public double TradePartnerPickValue; //the amount of "points" the trade partner team is offering in 
            public int[] InitiatingTeamPlayerIDs; //player ID's the initiating team is offering in addition to picks
            public int[] TradePartnerPlayerIDs; //player ID's the trade partner team is offering in addition to picks
        }
    }
}


