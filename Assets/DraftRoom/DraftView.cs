using MarkLight;
using MarkLight.Views.UI;
using SQLite;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using static GlobalRefs;
using UnityEngine;
using System;

public class DraftView : UIView {

    public ObservableList<DraftPlayers> OLDraftPlayers;
    public ViewSwitcher HeaderSwitcher;
    public int pageCur;
    public int pageTotal;
    public _string PageText;
    private int start;
    private int end;
    private List<RosterPlayers> teamPlayerList;

    public UnityEngine.UI.Text TextComponent;
    public override void Initialize()
    {
        base.Initialize();
        //TODO Implement Mersenne Twister
        userTeamId = UnityEngine.Random.Range(1, 32);

        using (SQLiteConnection db = new SQLiteConnection(DBPath))
        {
            BeginDraft.draftPlayers = db.Query<DraftPlayers>("SELECT * FROM DraftPlayers").ToList();
            teamPlayerList = db.Query<RosterPlayers>("SELECT * FROM RosterPlayers WHERE TeamID = " + userTeamId).ToList();
        }

        OLDraftPlayers = new ObservableList<DraftPlayers>();
        for (var i =0; i <25; i++)
        {
            OLDraftPlayers.Add(BeginDraft.draftPlayers[i]);
        }
        ResetPaging(BeginDraft.draftPlayers.Count);
        
       
        //OLDraftPlayers.AddRange(BeginDraft.draftPlayers);
    }

    private void SetPages()
    {
        PageText.Value = "Page " + pageCur.ToString() + " of " + pageTotal.ToString();
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
                start = BeginDraft.draftPlayers.Count -1 - end;
                end = BeginDraft.draftPlayers.Count - 1;
            }
            else
            {
                start = end + 1;
                end += 25;
            }
            SetPages();
            for (int i = start; i < end+1; i++)
            {
                OLDraftPlayers.Add(BeginDraft.draftPlayers[i]);
            }
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

            for (int i = start; i < end + 1; i++)
            {
                OLDraftPlayers.Add(BeginDraft.draftPlayers[i]);
            }
        }
    }
    //This brings back a list of all the drafted players so far
    public void GetDraftedPlayers()
    {
        HeaderSwitcher.SwitchTo(1);
        OLDraftPlayers.Clear();
        //set up the paging
        ResetPaging(OLDraftPlayers.Count);

        for (int i = start; i < end + 1; i++)
        {
            if (BeginDraft.draftPicks.Count == 0 )
            {
                //TODO: Activate a Label in the middle of the Panel that says "No picks made yet" in big black letters
            }
            else
            {
                //Add the picks that are already made to the list
                while(i < BeginDraft.draftPicks.Count)
                {
                    OLDraftPlayers.Add(BeginDraft.draftPicks[i]);
                    i++;
                }
            }
        }

    }

    public void GetBestAvailable()
    {
        HeaderSwitcher.SwitchTo(0);
        OLDraftPlayers.Clear();
        //set up the paging
        ResetPaging(BeginDraft.draftPlayers.Count);
        //Sort List by Grade
        BeginDraft.draftPlayers = BeginDraft.draftPlayers.OrderByDescending(x => x.ActualGrade).ToList();

        for (int i = start; i < end + 1; i++)
        {
            if (BeginDraft.draftPlayers.Count == 0)
            {
                //TODO: Activate a Label in the middle of the Panel that says "No picks made yet" in big black letters
            }
            else
            {
                //Add the picks that are already made to the list
                while (i < end-start)
                {
                    OLDraftPlayers.Add(BeginDraft.draftPlayers[i]);
                    i++;
                }
            }
        }


    }
    public void GetTeamRoster()
    {
        HeaderSwitcher.SwitchTo(2);
        OLDraftPlayers.Clear();
        //Get players on the userTeamID from the players Roster--->random for now, will be based on the userTeamId upon gamestart/gameload
        ResetPaging(teamPlayerList.Count);

        //default order is by position
        teamPlayerList = teamPlayerList.OrderBy(x => x.Pos).ToList();
        for (int i = start; i < end + 1; i++)
        {
            if (teamPlayerList.Count == 0)
            {
                //TODO: Activate a Label in the middle of the Panel that says "No picks made yet" in big black letters
            }
            else
            {
                //Add the picks that are already made to the list
                while (i < end-start)
                {
                    OLDraftPlayers.Add(teamPlayerList[i]);
                    i++;
                }
            }
        }
    }

    public void GetTeamNeeds()
    {
        HeaderSwitcher.SwitchTo(3);
        OLDraftPlayers.Clear();
    }
    private void ResetPaging(int numItems)
    {
        end = 24;
        start = 0;
        pageCur = 1;        
        pageTotal = numItems < 25 ? 1: numItems/25;
        SetPages();
    }

}
