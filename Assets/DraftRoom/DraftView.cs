using MarkLight;
using MarkLight.Views.UI;
using SQLite;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using static GlobalRefs;
using static BeginDraft;
using UnityEngine;
using System;
using System.Globalization;

public class DraftView : UIView {


    public ObservableList<playerPicks> OLDraftPlayers;
    public ViewSwitcher HeaderSwitcher;
    public int pageCur;
    public int pageTotal;
    public _string PageText;
    private int start;
    private int end;
    private int Draftpages; 
    private List<RosterPlayers> teamPlayerList;
    private List<TeamNeeds> teamNeeds;
    enum CurrentType { DraftBoard = 1, DraftedPlayers, BestAvailable, TeamRoster, TeamNeeds }
    private CurrentType draftViewType = new CurrentType();
    public UnityEngine.UI.Text TextComponent;

    public override void Initialize()
    {
        base.Initialize();
        //TODO Implement Mersenne Twister
        userTeamId = UnityEngine.Random.Range(1, 10);
        draftViewType = CurrentType.DraftBoard;

        OLDraftPlayers = new ObservableList<playerPicks>();
        ResetPaging();
        CreateList(draftViewType);
        HeaderSwitcher.SwitchTo(0);
       
        //OLDraftPlayers.AddRange(BeginDraft.draftPlayers);
    }

    private void CreateList(CurrentType draftviewType)
    {
       
        OLDraftPlayers.Clear(); //Clear out the list
        

        switch (draftviewType)
        {   //Need to write code to remove players from draft list once they are selected.
            case CurrentType.DraftBoard:  case CurrentType.BestAvailable:

                var SQL = draftviewType == CurrentType.BestAvailable ? "SELECT * FROM DraftPlayers EXCEPT SELECT DraftID FROM DraftedPlayers ORDER BY DESC LIMIT 25" : "SELECT * FROM DraftPlayers WHERE DraftID Between " + start + " AND " + end;
                using (SQLiteConnection db = new SQLiteConnection(DBPath))
                {
                    draftPlayers = db.Query<DraftPlayers>(SQL).ToList();
                    pageTotal = db.ExecuteScalar<int>("SELECT COUNT(*) FROM DraftPlayers")/25;                   
                }

                SetPages();

                for (int i = 0; i < 26; i++) //we are always geting 25 players at a time, no matter what the range is.
                {
                    //Add in the appropriate data from the "super-class"
                    OLDraftPlayers.Add(new playerPicks
                    {
                        Name = draftPlayers[i].FName + " " + draftPlayers[i].LName,
                        Position = draftPlayers[i].CollegePOS,
                        Height = draftPlayers[i].Height / 12 + "'" + draftPlayers[i].Height % 12 + "\"",
                        Weight = draftPlayers[i].Weight.ToString() + " lbs.",
                        College = draftPlayers[i].College,
                        Grade = draftPlayers[i].ActualGrade.ToString(),
                        ProjRound = "TBD",
                        FortyTime = draftPlayers[i].FortyYardTime.ToString()
                    });
                }
                break;
            case CurrentType.DraftedPlayers:
                //Get a list of all the curerntly drafted players
                for (int i = 0; i < draftPicks.Count - 1; i++)
                {
                    //Check to see if there is a playerID value
                    if (draftPicks[i].PlayerID.HasValue) 
                      
                    {
                        Debug.Log(draftPicks[i].PlayerID);
                        //grab the current playerID and use it to lookup the appropriate info from the players table
                        using (SQLiteConnection db = new SQLiteConnection(DBPath))
                        {
                            draftPlayers = db.Query<DraftPlayers>("SELECT * FROM DraftPlayers WHERE DraftID = " + Convert.ToInt32(draftPicks[i].PlayerID)).ToList();
                            pageTotal = db.ExecuteScalar<int>("SELECT COUNT(*) FROM DraftPick") / 25;
                        }
                        //Add in the appropriate data from the "super-class"
                        OLDraftPlayers.Add(new playerPicks
                        {
                            Round = draftPicks[i].DraftRound.ToString(),
                            PickInRound = draftPicks[i].PickNumRound.ToString(),
                            Team = teams[draftPicks[i].PickTeamIDCurr].TeamNickname,
                            Name = draftPicks[i].PlayerFName + " " + draftPicks[i].PlayerLName,
                            Position = draftPicks[i].PlayerPos,
                            Height = draftPlayers[0].Height / 12 + "'" + draftPlayers[0].Height % 12 + "\"",
                            Weight = draftPlayers[0].Weight.ToString() + " lbs.",
                            College = draftPicks[i].PlayerCollege,
                            Age = draftPlayers[0].Age.ToString(),
                            Grade = draftPlayers[0].ActualGrade.ToString(),
                        });
                    }
                    else { return; }
                }
                break;
                //Load the team roster
            case CurrentType.TeamRoster:
                if (teamPlayerList == null) //if we already have the list no need to get it again.
                {
                    using (SQLiteConnection db = new SQLiteConnection(DBPath))
                    {
                        teamPlayerList = db.Query<RosterPlayers>("SELECT * FROM RosterPlayers WHERE TeamID = ?", userTeamId).OrderBy(x => x.Pos).ToList();                       
                    }
                    pageTotal = teamPlayerList.Count % 25 == 0? teamPlayerList.Count / 25: (teamPlayerList.Count / 25) + 1;
                }

                for(var i = start; i < end ; i++)
                {
                    OLDraftPlayers.Add(new playerPicks
                    {
                        Name = teamPlayerList[i].FName + " " + teamPlayerList[i].LName,
                        Position = teamPlayerList[i].Pos,
                        Depth = "TBD",
                        Age = teamPlayerList[i].Age.ToString(),
                        Height = teamPlayerList[i].Height/12 + "'" + teamPlayerList[i].Height % 12 + "\"",
                        Weight = teamPlayerList[i].Weight.ToString() + "lbs.",
                        OvRtg = UnityEngine.Random.Range(50,100).ToString(),
                        PosRtg = UnityEngine.Random.Range(50,100).ToString(),
                        AthRtg = UnityEngine.Random.Range(50,100).ToString(),
                        IntRtg = UnityEngine.Random.Range(50,100).ToString(),
                        Contract = UnityEngine.Random.Range(1,6) + " Years, " + UnityEngine.Random.Range(450000,21000000).ToString("N", new CultureInfo("en-US"))
                    });
                }
                break;
            case CurrentType.TeamNeeds:
                if(teamNeeds == null)
                {
                    using (SQLiteConnection db = new SQLiteConnection(DBPath))
                    {
                        teamNeeds = db.Query<TeamNeeds>("SELECT * FROM TeamNeeds WHERE TeamID = ?", userTeamId).OrderBy(x => x.Position).ToList();
                    }
                    pageTotal = 1;
                    foreach(var need in teamNeeds)
                    {
                        OLDraftPlayers.Add(new playerPicks
                        {
                            Position = need.Position,
                            Need = need.Status,
                            Importance = need.Importance.ToString()
                        });
                    }
                }
                break;         
        }
    }


    //Increase the page by 1(25 players)
    public void IncrementPage()
    {
  
        if(pageCur < pageTotal)
        {
            pageCur++;
            OLDraftPlayers.Clear();
            if (pageCur == pageTotal)
            {
                
                switch (draftViewType)
                {
                    case CurrentType.DraftBoard:
                        start = draftPlayers.Count - 1 - end;
                        end = draftPlayers.Count - 1;
                        break;
                    case CurrentType.TeamRoster:
                        start = teamPlayerList.Count - 1 - end;
                        end = teamPlayerList.Count - 1;
                        break;
                    case CurrentType.DraftedPlayers:
                        break;
                    case CurrentType.TeamNeeds:
                        start = 0;
                        end = teamNeeds.Count - 1;
                        break;
                }
                //end = draftPlayers.Count - 1;
            }
            else
            {
                start = end + 1;
                end += 25;
            }
            CreateList(draftViewType);
            SetPages();
           
        }
    }
    //Decrease the page by 1(25 players)
    public void DecrementPage()
    {
        if (pageCur != 1)
        {
            pageCur--;
            end = start - 1;
            start -= 25;

            OLDraftPlayers.Clear();
            SetPages();

            CreateList(draftViewType);
        }
    }

    private void SetPages()
    {
        PageText.Value = "Page " + pageCur.ToString() + " of " + pageTotal.ToString();
    }
    //This brings back a list of all the drafted players so far
    public void GetDraftedPlayers()
    {
        draftViewType = CurrentType.DraftedPlayers;
        HeaderSwitcher.SwitchTo(1);
        OLDraftPlayers.Clear();
        //set up the paging
        ResetPaging(OLDraftPlayers.Count);
        CreateList(draftViewType);             
    }

    public void GetBestAvailable()
    {
        draftViewType = CurrentType.DraftBoard;
        HeaderSwitcher.SwitchTo(0);
        OLDraftPlayers.Clear();
        //set up the paging
        ResetPaging(draftPlayers.Count);
        
        

        for (int i = start; i < end + 1; i++)
        {
            if (draftPlayers.Count == 0)
            {
                //TODO: Activate a Label in the middle of the Panel that says "No picks made yet" in big black letters
            }
            else
            {
                CreateList(draftViewType);
                //Sort List by Grade
                draftPlayers = draftPlayers.OrderByDescending(x => x.ActualGrade).ToList();
            }
        }


    }
    public void GetTeamRoster()
    {
        draftViewType = CurrentType.TeamRoster;
        HeaderSwitcher.SwitchTo(2);
        CreateList(draftViewType);

        //Get players on the userTeamID from the players Roster--->random for now, will be based on the userTeamId upon gamestart/gameload
        ResetPaging(teamPlayerList.Count);

    }

    public void GetTeamNeeds()
    {
        draftViewType = CurrentType.TeamNeeds;
        HeaderSwitcher.SwitchTo(3);
        CreateList(draftViewType);
    }
    private void ResetPaging( int numItems = 0)
    {
        end = 26;
        start = 0;
        pageCur = 1;        
        pageTotal = numItems < 25 ? 1: numItems/25;
        SetPages();
    }

}

 public class playerPicks
{
    public string Name;
    public string Depth;
    public string Position;
    public string Height;
    public string Weight;
    public string College;
    public string Grade;
    public string ProjRound;
    public string FortyTime;
    public string Round;
    public string PickInRound;
    public string Team;
    public string Need;
    public string Importance;
    public string Age;
    public string OvRtg;
    public string PosRtg;
    public string IntRtg;
    public string AthRtg;
    public string Contract;
}