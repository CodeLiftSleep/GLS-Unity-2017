using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using SQLite;
using static GLS.GlobalRefs;
using static Draft.BeginDraft;
using System.Reflection;
using GLS;

namespace Draft
{
    //This class contains static methods to help the team decide what to do with their draft picks
    public class DraftAI : MonoBehaviour, ITeams
    {

        public static int tradeUpPercentage = 25;
        public static int tradeDownPercentage = 25;

        public string Result { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        //Get the teamState for this team
        public static TeamStates GetTeamStates(int TeamID)
        {
            Enum.TryParse(teams[TeamID].TeamState, out TeamStates teamState);
            return teamState;

        }

        //This is going to determine what the AI does for each team
        public static void DetermineAIPath(int TeamID, TeamStates teamState, OvDraftAI draftAI)
        {
            MovementTriggerUp(TeamID, teamState, draftAI);
            MovementTriggerDown(TeamID, teamState, draftAI);
        }

        //Here we check for things that will trigger the AI to try and trade up...
        public static void MovementTriggerUp(int TeamID, TeamStates teamState, OvDraftAI draftAI)
        {
            var playerList = new List<GLS.DraftPlayers>();
            //Move up
            switch (teamState)
            {
                //team will not be attempting to trade up---they want to trade down
                case TeamStates.TotalRebuild:

                    break;
                //Team doesn't want to trade up, they want to trade down(unless it's for a QB)
                case TeamStates.Rebuilding:

                    break;
                //Team will be more likely to trade up 
                case TeamStates.Building:

                    break;
                //Team will have all options on the table
                case TeamStates.Transition:

                    break;
                //Team can do either
                case TeamStates.Reloading:

                    break;
                //Most likely to trade-up as they think they are "one player away"
                case TeamStates.WinNow:
                    //we need to check their Group One list and see if there are any players on it they have highly graded
                    if (draftAI.GroupOne.Count > 1)
                    {
                        //We already have the list of draft players in this group---let's see if they are worth trading up for.
                        if (CheckGroupValue(draftAI.GroupOne, TeamID))
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
        private static Boolean CheckGroupValue(List<DraftPlayers> GroupToCheck, int TeamID, OvDraftAI draftAI)
        {
            bool tradeUp = false;
            //check to see if one of the Group One players matches the team needs
            var matches = from player in GroupToCheck
                          from need in TeamDraftAI[TeamID].posNeedTeam
                          where player.CollegePOS == need.Position && need.Status == "Starter"
                          select player;

            //check to see their team needs.  Teams don't trade up unless they have a team need for a starter
            if (matches != null) tradeUp = true;

            if (tradeUp)
            {  //This only runs if a team passes the initial trade up check

                //check out how deep the draft is at this position...deep drafts are less likely to trade up with shallow drafts more likely
                var depth = from player in GroupToCheck
                            from deep in draftDepth
                            where player.CollegePOS == deep.Position
                            select deep;

                foreach (var deep in depth)
                {
                    switch (deep.Depth)
                    {
                        case "Stacked":
                            tradeUpPercentage = 10;
                            break;
                        case "Deep":
                            tradeUpPercentage = 15;
                            break;
                        case "TopHeavy":
                            tradeUpPercentage = 20;
                            break;
                        case "Normal": //baseline of 25%
                            break;
                        case "LackingButDeep":
                            tradeUpPercentage = 40;
                            break;
                        case "Shallow":
                            tradeUpPercentage = 55;
                            break;
                        case "Poor":
                            tradeUpPercentage = 70;
                            break;
                    }
                    
                           foreach(var drop in draftAI.dropOffPercentage)
                            {
                                if(drop.Position == deep.Position)
                                {
                                    drop.TopPlayerGrade = draftAI.
                                }
                            }
                            break;
                    }
                }
            }
            //if they do---check to see the dropoff between this player and the next player at their position---depending on how
            //much of a dropoff there is will determine if they are willing to trade up
           


            //check to see if the current team is willing to trade---if not, keep going down the list of teams to see if they are willing to trade

            //If a team is willing to trade, make an offer and then start negotiating.  Depending on the team's willingness to trade, there might need
            //to be a premium paid to make the deal.



            return tradeUp;
        }

        //Here we check for things that will trigger the AI to try and trade down
        public static void MovementTriggerDown(int TeamID, TeamStates teamState, OvDraftAI draftAI)
        {

        }
        // Update is called once per frame
        void Update()
        {

        }

        public void Evaluate()
        {
            throw new NotImplementedException();
        }
    }
}