using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using SQLite;
using static GlobalRefs;
using static BeginDraft;
using System.Reflection;

//This class contains static methods to help the team decide what to do with their draft picks
public class DraftAI : MonoBehaviour {
    public enum teamStates { TotalRebuild = 1, Rebuilding, Reloading, Transition, Building, WinNow }
    public enum capStates { CapConcious = 1, SpendOnOwn, SpendOnFA, Flexible }
    
    //Get the teamState for this team
    public static teamStates GetTeamStates(int TeamID)
    {
        Enum.TryParse(teams[TeamID].TeamState, out teamStates teamState);
        return teamState;
        
    }

    //This is going to determine what the AI does for each team
    public static void DetermineAIPath(int TeamID, teamStates teamState, OvDraftAI draftAI) {
        MovementTriggerUp(TeamID, teamState, draftAI);
        MovementTriggerDown(TeamID, teamState, draftAI);
    }
    
    //Here we check for things that will trigger the AI to try and trade up...
    public static void MovementTriggerUp(int TeamID, teamStates teamState, OvDraftAI draftAI)
    {
        var playerList = new List<DraftPlayers>();
        //Move up
        switch (teamState) {
            //team will not be attempting to trade up---they want to trade down
            case teamStates.TotalRebuild:
                
                break;
                //Team doesn't want to trade up, they want to trade down(unless it's for a QB)
            case teamStates.Rebuilding:

                break;
                //Team will be more likely to trade up 
            case teamStates.Building:

                break;
                //Team will have all options on the table
            case teamStates.Transition:

                break;
                //Team can do either
            case teamStates.Reloading:

                break;
                //Most likely to trade-up as they think they are "one player away"
            case teamStates.WinNow:
                //we need to check their Group One list and see if there are any players on it they have highly graded
                if (draftAI.GroupOne.Count> 1)
                {
                    //We already have the list of draft players in this group---let's see if they are worth trading up for.
                   if(CheckGroupValue(draftAI.GroupOne, TeamID))
                    {

                    }
                }
                break;
        }       
    }
    /// <summary>
    /// Check the grouped players too see if they are worth trading up for based on various conditions
    /// </summary>
    /// <param name="GroupToCheck"></param>
    private static Boolean CheckGroupValue(List<DraftPlayers> GroupToCheck, int TeamID)
    {
        bool tradeUp = false;
        //check to see if one of the Group One players matches the team needs
        var matches = from player in GroupToCheck
                    from need in TeamDraftAI[TeamID].posNeedTeam
                    where player.CollegePOS == need.Position && need.PlayerType == "Starter"
                    select player;

        //check to see their team needs.  Teams don't trade up unless they have a team need for a starter
        if (matches != null) tradeUp = true;
        //check out how deep the draft is at this position...deep drafts are less likely to trade up with shallow drafts more likely
        var depth = from player in GroupToCheck
                    from deep in draftDepth
                    where player.CollegePOS == deep.Position
                    select deep;

        //if they do---check to see the dropoff between this player and the next player at their position---depending on how
        //much of a dropoff there is will determine if they are willing to trade up

        //check to see if the current team is willing to trade---if not, keep going down the list of teams to see if they are willing to trade

        //If a team is willing to trade, make an offer and then start negotiating.  Depending on the team's willingness to trade, there might need
        //to be a premium paid to make the deal.

       

        return tradeUp;
    }

    //Here we check for things that will trigger the AI to try and trade down
    public static void MovementTriggerDown(int TeamID, teamStates teamState, OvDraftAI draftAI)
    {

    }
	// Update is called once per frame
	void Update () {
		
	}
}
