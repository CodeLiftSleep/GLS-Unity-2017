using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite;
using System;
using System.Linq;
/// <summary>
/// These are the POCO Classes that will hold the DB Objects in SQLite and the mapping back and forth to the model automatically EF Style without the EF
/// </summary>
public class POCOCreation : MonoBehaviour {
    /// <summary>
    ///Example for usage
    /// </summary>
    // Use this for initialization
    //void Start () {
        //string SqlitePath = Application.dataPath + "/SQLite DB/Football.sqlite";
        //var DraftPlayer = new List<DraftPlayers>();
        //using (SQLiteConnection db = new SQLiteConnection(SqlitePath))
        //{
            //DraftPlayer = db.Query<DraftPlayers>("Select * From DraftPlayers").ToList();
        //}
               
	//}
	
}
public class SQLiteDb
{
    private string _path;

    public SQLiteDb(string path)
    {
        _path = path;
       
    }

    public void Create()
    {
        using (SQLiteConnection db = new SQLiteConnection(_path))
        {
            db.CreateTable<Agents>();
            db.CreateTable<Coaches>();
            db.CreateTable<DraftPlayers>();
            db.CreateTable<Owners>();
            db.CreateTable<Personnel>();
            db.CreateTable<RosterPlayers>();
            db.CreateTable<TeamDefense>();
            db.CreateTable<TeamOffense>();
            db.CreateTable<Teams>();
            db.CreateTable<Trainers>();

        }
    }
}


public partial class Agents
{
    [PrimaryKey, AutoIncrement]
    public Int32 AgentID { get; set; }

    [MaxLength(20)]
    public String FName { get; set; }

    [MaxLength(20)]
    public String LName { get; set; }

    [MaxLength(50)]
    public String College { get; set; }

    public Int32? Height { get; set; }

    public Int32? Weight { get; set; }

    public Int32? Age { get; set; }

    [MaxLength(12)]
    public String DOB { get; set; }

    public Int32? Experience { get; set; }

    [MaxLength(20)]
    public String AgentType { get; set; }

    public String ClientList { get; set; }

    public Int32? RelTeam1 { get; set; }

    public Int32? RelTeam2 { get; set; }

    public Int32? RelTeam3 { get; set; }

    public Int32? RelTeam4 { get; set; }

    public Int32? RelTeam5 { get; set; }

    public Int32? RelTeam6 { get; set; }

    public Int32? RelTeam7 { get; set; }

    public Int32? RelTeam8 { get; set; }

    public Int32? RelTeam9 { get; set; }

    public Int32? RelTeam10 { get; set; }

    public Int32? RealTeam11 { get; set; }

    public Int32? RelTeam12 { get; set; }

    public Int32? RelTeam13 { get; set; }

    public Int32? RelTeam14 { get; set; }

    public Int32? RelTeam15 { get; set; }

    public Int32? RelTeam16 { get; set; }

    public Int32? RelTeam17 { get; set; }

    public Int32? RelTeam18 { get; set; }

    public Int32? RelTeam19 { get; set; }

    public Int32? RelTeam20 { get; set; }

    public Int32? RelTeam21 { get; set; }

    public Int32? RelTeam22 { get; set; }

    public Int32? RelTeam23 { get; set; }

    public Int32? RelTeam24 { get; set; }

    public Int32? RelTeam25 { get; set; }

    public Int32? RelTeam26 { get; set; }

    public Int32? RelTeam27 { get; set; }

    public Int32? RelTEam28 { get; set; }

    public Int32? RelTeam29 { get; set; }

    public Int32? RelTeam30 { get; set; }

    public Int32? RelTeam31 { get; set; }

    public Int32? RelTeam32 { get; set; }

    public Int32? AbsentMinded { get; set; }

    public Int32? AbstractReasoning { get; set; }

    public Int32? AbstractThinker { get; set; }

    public Int32? Adaptable { get; set; }

    public Int32? Adventurous { get; set; }

    public Int32? Aggressive { get; set; }

    public Int32? Analytical { get; set; }

    public Int32? Astute { get; set; }

    public Int32? Bossy { get; set; }

    public Int32? CalmUnderPressure { get; set; }

    public Int32? Caring { get; set; }

    public Int32? Competitive { get; set; }

    public Int32? Conforming { get; set; }

    public Int32? Controlling { get; set; }

    public Int32? Critical { get; set; }

    public Int32? Diplomatic { get; set; }

    public Int32? Driven { get; set; }

    public Int32? Dutiful { get; set; }

    public Int32? EmotionallyStable { get; set; }

    public Int32? Enthusiastic { get; set; }

    public Int32? Expermiental { get; set; }

    public Int32? Expressive { get; set; }

    public Int32? Fairness { get; set; }

    public Int32? Fearful { get; set; }

    public Int32? FollowsRules { get; set; }

    public Int32? Friendly { get; set; }

    public Int32? FunLoving { get; set; }

    public Int32? GuiltProne { get; set; }

    public Int32? GreedAvoidance { get; set; }

    public Int32? HighEnergy { get; set; }

    public Int32? Honesty { get; set; }

    public Int32? Imaginative { get; set; }

    public Int32? Impatient { get; set; }

    public Int32? Impractical { get; set; }

    public Int32? Impulsive { get; set; }

    public Int32? Individualistic { get; set; }

    public Int32? Insecure { get; set; }

    public Int32? Intelligent { get; set; }

    public Int32? Loner { get; set; }

    public Int32? Mature { get; set; }

    public Int32? MentalCapacity { get; set; }

    public Int32? Modesty { get; set; }

    public Int32? Moralistic { get; set; }

    public Int32? Nurturing { get; set; }

    public Int32? Organized { get; set; }

    public Int32? Participator { get; set; }

    public Int32? Perfectionist { get; set; }

    public Int32? Polished { get; set; }

    public Int32? Private { get; set; }

    public Int32? Resourceful { get; set; }

    public Int32? SelfDoubting { get; set; }

    public Int32? SelfSufficient { get; set; }

    public Int32? StrongWilled { get; set; }

    public Int32? Stubborn { get; set; }

    public Int32? TeamPlayer { get; set; }

    public Int32? Tense { get; set; }

    public Int32? Vigilant { get; set; }

    [MaxLength(25)]
    public String Dominant { get; set; }

    [MaxLength(25)]
    public String Weakest { get; set; }
}

public partial class Coaches
{
    [PrimaryKey, AutoIncrement]
    public Int64 CoachID { get; set; }

    public Int64? TeamID { get; set; }

    public String FName { get; set; }

    public String LName { get; set; }

    public String College { get; set; }

    public Int64? Height { get; set; }

    public Int64? Weight { get; set; }

    public Int64? Age { get; set; }

    public String DOB { get; set; }

    public Int64? CoachType { get; set; }

    public String SideOfBall { get; set; }

    public Int64? Experience { get; set; }

    public String OffPhil { get; set; }

    public String DefPhil { get; set; }

    public Int64? OffAbility { get; set; }

    public Int64? DefAbility { get; set; }

    public Int64? CoachQB { get; set; }

    public Int64? CoachRB { get; set; }

    public Int64? CoachWR { get; set; }

    public Int64? CoachTE { get; set; }

    public Int64? CoachOL { get; set; }

    public Int64? DevPlayers { get; set; }

    public Int64? CoachDL { get; set; }

    public Int64? CoachLB { get; set; }

    public Int64? CoachDB { get; set; }

    public Int64? CoachST { get; set; }

    public Int64? JudgingAct { get; set; }

    public Int64? JudgingPot { get; set; }

    public Int64? JudgingQB { get; set; }

    public Int64? JudgingRB { get; set; }

    public Int64? JudgingWR { get; set; }

    public Int64? JudgingTE { get; set; }

    public Int64? JudgingOL { get; set; }

    public Int64? JudgingDL { get; set; }

    public Int64? JudgingLB { get; set; }

    public Int64? JudgingCB { get; set; }

    public Int64? JudgingSF { get; set; }

    public Int64? ValuesST { get; set; }

    public Int64? ValuesCharacter { get; set; }

    public Int64? PlaycallingSkill { get; set; }

    public Int64? RespectedByPlayers { get; set; }

    public Int64? RespectedByCoaches { get; set; }

    public Int64? SeesBigPicture { get; set; }

    public Int64? DevQB { get; set; }

    public Int64? DevRB { get; set; }

    public Int64? DevWR { get; set; }

    public Int64? DevTE { get; set; }

    public Int64? DevOL { get; set; }

    public Int64? DevDL { get; set; }

    public Int64? DevLB { get; set; }

    public Int64? DevCB { get; set; }

    public Int64? DevSF { get; set; }

    public Int64? LowerBodyTrain { get; set; }

    public Int64? UpperBodyTrain { get; set; }

    public Int64? CoreTrain { get; set; }

    public Int64? PreventInjuryTrain { get; set; }

    public Int64? StaminaTrain { get; set; }

    public Int64? AbsentMinded { get; set; }

    public Int64? AbstractReasoning { get; set; }

    public Int64? AbstractThinker { get; set; }

    public Int64? Adaptable { get; set; }

    public Int64? Adventurous { get; set; }

    public Int64? Aggressive { get; set; }

    public Int64? Analytical { get; set; }

    public Int64? Astute { get; set; }

    public Int64? Bossy { get; set; }

    public Int64? CalmUnderPressure { get; set; }

    public Int64? Caring { get; set; }

    public Int64? Competitive { get; set; }

    public Int64? Conforming { get; set; }

    public Int64? Controlling { get; set; }

    public Int64? Critical { get; set; }

    public Int64? Diplomatic { get; set; }

    public Int64? Driven { get; set; }

    public Int64? Dutiful { get; set; }

    public Int64? EmotionallyStable { get; set; }

    public Int64? Enthusiastic { get; set; }

    public Int64? Expermiental { get; set; }

    public Int64? Expressive { get; set; }

    public Int64? Fairness { get; set; }

    public Int64? Fearful { get; set; }

    public Int64? FollowsRules { get; set; }

    public Int64? Friendly { get; set; }

    public Int64? FunLoving { get; set; }

    public Int64? GuiltProne { get; set; }

    public Int64? GreedAvoidance { get; set; }

    public Int64? HighEnergy { get; set; }

    public Int64? Honesty { get; set; }

    public Int64? Imaginative { get; set; }

    public Int64? Impatient { get; set; }

    public Int64? Impractical { get; set; }

    public Int64? Impulsive { get; set; }

    public Int64? Individualistic { get; set; }

    public Int64? Insecure { get; set; }

    public Int64? Intelligent { get; set; }

    public Int64? Loner { get; set; }

    public Int64? Mature { get; set; }

    public Int64? MentalCapacity { get; set; }

    public Int64? Modesty { get; set; }

    public Int64? Moralistic { get; set; }

    public Int64? Nurturing { get; set; }

    public Int64? Organized { get; set; }

    public Int64? Participator { get; set; }

    public Int64? Perfectionist { get; set; }

    public Int64? Polished { get; set; }

    public Int64? Private { get; set; }

    public Int64? Resourceful { get; set; }

    public Int64? SelfDoubting { get; set; }

    public Int64? SelfSufficient { get; set; }

    public Int64? StrongWilled { get; set; }

    public Int64? Stubborn { get; set; }

    public Int64? TeamPlayer { get; set; }

    public Int64? Tense { get; set; }

    public Int64? Vigilant { get; set; }

    public String Dominant { get; set; }

    public String Weakest { get; set; }

    public String CoachTypeStr { get; set; }
}

public partial class DraftPlayers
{
    [PrimaryKey, AutoIncrement]
    public Int32 DraftID { get; set; }

    public Int32? AgentID { get; set; }

    [MaxLength(20)]
    public String FName { get; set; }

    [MaxLength(20)]
    public String LName { get; set; }

    [MaxLength(50)]
    public String College { get; set; }

    [MaxLength(10)]
    public String ScoutRegion { get; set; }

    public Int32? Height { get; set; }

    public Int32? Weight { get; set; }

    public Int32? Age { get; set; }

    [MaxLength(12)]
    public String DOB { get; set; }

    [MaxLength(5)]
    public String CollegePOS { get; set; }

    public Decimal? ActualGrade { get; set; }

    [MaxLength(5)]
    public String ProjNFLPos { get; set; }

    [MaxLength(20)]
    public String PosType { get; set; }

    public Decimal? ArmLength { get; set; }

    public Decimal? HandLength { get; set; }

    public Decimal? FortyYardTime { get; set; }

    public Decimal? TwentyYardTime { get; set; }

    public Decimal? TenYardTime { get; set; }

    public Decimal? ShortShuttle { get; set; }

    public Int32? BroadJump { get; set; }

    public Decimal? VertJump { get; set; }

    public Decimal? ThreeConeDrill { get; set; }

    public Int32? BenchPress { get; set; }

    public Int32? InterviewSkills { get; set; }

    public Int32? WonderlicTest { get; set; }

    public Int32? SkillsTranslateToNFL { get; set; }

    public Int32? Reaction { get; set; }

    public Int32? QAB { get; set; }

    public Int32? COD { get; set; }

    public Int32? Hands { get; set; }

    public Int32? BodyCatch { get; set; }

    public Int32? StiffArm { get; set; }

    public Int32? ReleaseOffLine { get; set; }

    public Int32? CatchWhenHit { get; set; }

    public Int32? BreaksTackles { get; set; }

    public Int32? ContactBalance { get; set; }

    public Int32? RunAfterCatch { get; set; }

    public Int32? LowerBodyStrength { get; set; }

    public Int32? UpperBodyStrength { get; set; }

    public Int32? Footwork { get; set; }

    public Int32? HandUse { get; set; }

    public Int32? JumpingAbility { get; set; }

    public Int32? PassBlockVsPower { get; set; }

    public Int32? PassBlockVsSpeed { get; set; }

    public Int32? RunBlocking { get; set; }

    public Int32? PlaySpeed { get; set; }

    public Int32? RouteRunning { get; set; }

    public Int32? KickAccuracy { get; set; }

    public Int32? AdjustToBall { get; set; }

    public Int32? Tackling { get; set; }

    public Int32? Blitz { get; set; }

    public Int32? AvoidBlockers { get; set; }

    public Int32? ShedBlock { get; set; }

    public Int32? DefeatBlock { get; set; }

    public Int32? ManToManCoverage { get; set; }

    public Int32? ZoneCoverage { get; set; }

    public Int32? RETKickReturn { get; set; }

    public Int32? RETPuntReturn { get; set; }

    public Int32? PlayStrength { get; set; }

    public Int32? QBMechanics { get; set; }

    public Int32? QBRelQuickness { get; set; }

    public Int32? QBAccuracy { get; set; }

    public Int32? QBDecMaking { get; set; }

    public Int32? QBBallHandling { get; set; }

    public Int32? QBLocateRec { get; set; }

    public Int32? QBPocketPresence { get; set; }

    public Int32? QBEscape { get; set; }

    public Int32? QBScrambling { get; set; }

    public Int32? QBRolloutRight { get; set; }

    public Int32? QBRolloutLeft { get; set; }

    public Int32? QBArmStrength { get; set; }

    public Int32? QBTouch { get; set; }

    public Int32? QBPlayAction { get; set; }

    public Int32? RBRunVision { get; set; }

    public Int32? RBSetsUpBlocks { get; set; }

    public Int32? RBPatience { get; set; }

    public Int32? WRRunDBOff { get; set; }

    public Int32? WRDisguiseRoute { get; set; }

    public Int32? OLPulling { get; set; }

    public Int32? OLSlide { get; set; }

    public Int32? OLMoveInSpace { get; set; }

    public Int32? OLSnapAbility { get; set; }

    public Int32? OLLongSnapAbility { get; set; }

    public Int32? OLAnchorAbility { get; set; }

    public Int32? OLRecover { get; set; }

    [MaxLength(15)]
    public String DLPrimaryTech { get; set; }

    [MaxLength(15)]
    public String DLSecondaryTech { get; set; }

    [MaxLength(15)]
    public String DLPassRushTech { get; set; }

    public Int32? DLRunAtHim { get; set; }

    public Int32? DLAgainstPullAbility { get; set; }

    public Int32? DLSlideABility { get; set; }

    public Int32? DLRunPursuit { get; set; }

    public Int32? DLCanTakeDoubleTeam { get; set; }

    public Int32? DLFinish { get; set; }

    public Int32? DLsetUpPassRush { get; set; }

    public Int32? LBDropDepth { get; set; }

    public Int32? LBFillGaps { get; set; }

    public Int32? DBPressBailCoverage { get; set; }

    public Int32? DBRunContain { get; set; }

    public Int32? DBBump { get; set; }

    public Int32? DBBaitQB { get; set; }

    public Int32? DBCatchUpSpeed { get; set; }

    public Int32? DBTechnique { get; set; }

    public Int32? KFakeAbility { get; set; }

    public Int32? KKickRise { get; set; }

    public Int32? PFakeAbility { get; set; }

    public Int32? PDistance { get; set; }

    public Int32? PHangTime { get; set; }

    public Int32? STCoverage { get; set; }

    public Int32? STWillingness { get; set; }

    public Int32? STAssignment { get; set; }

    public Int32? STDiscipline { get; set; }

    public Int32? Flexibility { get; set; }

    public Int32? Consistency { get; set; }

    public Int32? Instincts { get; set; }

    public Int32? Coachability { get; set; }

    public Int32? Leadership { get; set; }

    public Int32? Confidence { get; set; }

    public Int32? Clutch { get; set; }

    public Int32? WorkEthic { get; set; }

    public Int32? FilmStudy { get; set; }

    public Int32? Durability { get; set; }

    public Int32? Explosion { get; set; }

    public Int32? DeliversBlow { get; set; }

    public Int32? Toughness { get; set; }

    public Int32? ReadKeys { get; set; }

    public Int32? FieldAwareness { get; set; }

    public Int32? PlaybookKnowledge { get; set; }

    public Int32? BallSecurity { get; set; }

    public Int32? LovesFootball { get; set; }

    public Int32? Concentration { get; set; }

    public Int32? HandlesElements { get; set; }

    public Int32? Potential { get; set; }

    public Int32? Raw { get; set; }

    public Int32? AbsentMinded { get; set; }

    public Int32? AbstractReasoning { get; set; }

    public Int32? AbstractThinker { get; set; }

    public Int32? Adaptable { get; set; }

    public Int32? Adventurous { get; set; }

    public Int32? Aggressive { get; set; }

    public Int32? Analytical { get; set; }

    public Int32? Astute { get; set; }

    public Int32? Bossy { get; set; }

    public Int32? CalmUnderPressure { get; set; }

    public Int32? Caring { get; set; }

    public Int32? Competitive { get; set; }

    public Int32? Conforming { get; set; }

    public Int32? Controlling { get; set; }

    public Int32? Critical { get; set; }

    public Int32? Diplomatic { get; set; }

    public Int32? Driven { get; set; }

    public Int32? Dutiful { get; set; }

    public Int32? EmotionallyStable { get; set; }

    public Int32? Enthusiastic { get; set; }

    public Int32? Expermiental { get; set; }

    public Int32? Expressive { get; set; }

    public Int32? Fairness { get; set; }

    public Int32? Fearful { get; set; }

    public Int32? FollowsRules { get; set; }

    public Int32? Friendly { get; set; }

    public Int32? FunLoving { get; set; }

    public Int32? GuiltProne { get; set; }

    public Int32? GreedAvoidance { get; set; }

    public Int32? HighEnergy { get; set; }

    public Int32? Honesty { get; set; }

    public Int32? Imaginative { get; set; }

    public Int32? Impatient { get; set; }

    public Int32? Impractical { get; set; }

    public Int32? Impulsive { get; set; }

    public Int32? Individualistic { get; set; }

    public Int32? Insecure { get; set; }

    public Int32? Intelligent { get; set; }

    public Int32? Loner { get; set; }

    public Int32? Mature { get; set; }

    public Int32? MentalCapacity { get; set; }

    public Int32? Modesty { get; set; }

    public Int32? Moralistic { get; set; }

    public Int32? Nurturing { get; set; }

    public Int32? Organized { get; set; }

    public Int32? Participator { get; set; }

    public Int32? Perfectionist { get; set; }

    public Int32? Polished { get; set; }

    public Int32? Private { get; set; }

    public Int32? Resourceful { get; set; }

    public Int32? SelfDoubting { get; set; }

    public Int32? SelfSufficient { get; set; }

    public Int32? StrongWilled { get; set; }

    public Int32? Stubborn { get; set; }

    public Int32? TeamPlayer { get; set; }

    public Int32? Tense { get; set; }

    public Int32? Vigilant { get; set; }

    [MaxLength(25)]
    public String Dominant { get; set; }

    [MaxLength(25)]
    public String Weakest { get; set; }
}

public partial class Owners
{
    [PrimaryKey, AutoIncrement]
    public Int32 OwnerID { get; set; }

    public Int32? TeamID { get; set; }

    [MaxLength(20)]
    public String FName { get; set; }

    [MaxLength(20)]
    public String LName { get; set; }

    [MaxLength(50)]
    public String College { get; set; }

    public Int32? Height { get; set; }

    public Int32? Weight { get; set; }

    public Int32? Age { get; set; }

    [MaxLength(12)]
    public String DOB { get; set; }

    public Int32? Experience { get; set; }

    public Int32? OwnerRep { get; set; }

    public Int32? PersonalWealth { get; set; }

    public Int32? AbsentMinded { get; set; }

    public Int32? AbstractReasoning { get; set; }

    public Int32? AbstractThinker { get; set; }

    public Int32? Adaptable { get; set; }

    public Int32? Adventurous { get; set; }

    public Int32? Aggressive { get; set; }

    public Int32? Analytical { get; set; }

    public Int32? Astute { get; set; }

    public Int32? Bossy { get; set; }

    public Int32? CalmUnderPressure { get; set; }

    public Int32? Caring { get; set; }

    public Int32? Competitive { get; set; }

    public Int32? Conforming { get; set; }

    public Int32? Controlling { get; set; }

    public Int32? Critical { get; set; }

    public Int32? Diplomatic { get; set; }

    public Int32? Driven { get; set; }

    public Int32? Dutiful { get; set; }

    public Int32? EmotionallyStable { get; set; }

    public Int32? Enthusiastic { get; set; }

    public Int32? Expermiental { get; set; }

    public Int32? Expressive { get; set; }

    public Int32? Fairness { get; set; }

    public Int32? Fearful { get; set; }

    public Int32? FollowsRules { get; set; }

    public Int32? Friendly { get; set; }

    public Int32? FunLoving { get; set; }

    public Int32? GuiltProne { get; set; }

    public Int32? GreedAvoidance { get; set; }

    public Int32? HighEnergy { get; set; }

    public Int32? Honesty { get; set; }

    public Int32? Imaginative { get; set; }

    public Int32? Impatient { get; set; }

    public Int32? Impractical { get; set; }

    public Int32? Impulsive { get; set; }

    public Int32? Individualistic { get; set; }

    public Int32? Insecure { get; set; }

    public Int32? Intelligent { get; set; }

    public Int32? Loner { get; set; }

    public Int32? Mature { get; set; }

    public Int32? MentalCapacity { get; set; }

    public Int32? Modesty { get; set; }

    public Int32? Moralistic { get; set; }

    public Int32? Nurturing { get; set; }

    public Int32? Organized { get; set; }

    public Int32? Participator { get; set; }

    public Int32? Perfectionist { get; set; }

    public Int32? Polished { get; set; }

    public Int32? Private { get; set; }

    public Int32? Resourceful { get; set; }

    public Int32? SelfDoubting { get; set; }

    public Int32? SelfSufficient { get; set; }

    public Int32? StrongWilled { get; set; }

    public Int32? Stubborn { get; set; }

    public Int32? TeamPlayer { get; set; }

    public Int32? Tense { get; set; }

    public Int32? Vigilant { get; set; }

    [MaxLength(25)]
    public String Dominant { get; set; }

    [MaxLength(25)]
    public String Weakest { get; set; }
}

public partial class Personnel
{
    [PrimaryKey, AutoIncrement]
    public Int64 PersonnelID { get; set; }

    public Int64? TeamID { get; set; }

    public String FName { get; set; }

    public String LName { get; set; }

    public String College { get; set; }

    public Int64? Height { get; set; }

    public Int64? Weight { get; set; }

    public Int64? Age { get; set; }

    public String DOB { get; set; }

    public Int64? PersonnelType { get; set; }

    public String ScoutRegion { get; set; }

    public Int64? Experience { get; set; }

    public Int64? ValuesDraftPicks { get; set; }

    public Int64? ValuesCombine { get; set; }

    public Int64? ValuesCharacter { get; set; }

    public Int64? ValuesProduction { get; set; }

    public Int64? ValuesIntangibles { get; set; }

    public Int64? FranchiseTag { get; set; }

    public Int64? TransitionTag { get; set; }

    public Int64? JudgingDraft { get; set; }

    public Int64? JudgingFA { get; set; }

    public Int64? JudgingOwn { get; set; }

    public Int64? JudgingPot { get; set; }

    public Int64? JudgingQB { get; set; }

    public Int64? JudgingRB { get; set; }

    public Int64? JudgingWR { get; set; }

    public Int64? JudgingTE { get; set; }

    public Int64? JudgingOL { get; set; }

    public Int64? JudgingDL { get; set; }

    public Int64? JudgingLB { get; set; }

    public Int64? JudgingCB { get; set; }

    public Int64? JudgingSF { get; set; }

    public String OffPhil { get; set; }

    public Int64? QBImp { get; set; }

    public Int64? RBImp { get; set; }

    public Int64? FBImp { get; set; }

    public Int64? WRImp { get; set; }

    public Int64? WR2Imp { get; set; }

    public Int64? WR3Imp { get; set; }

    public Int64? LTImp { get; set; }

    public Int64? LGImp { get; set; }

    public Int64? CImp { get; set; }

    public Int64? RGImp { get; set; }

    public Int64? RTImp { get; set; }

    public Int64? TEImp { get; set; }

    public String DefPhil { get; set; }

    public Int64? DEImp { get; set; }

    public Int64? DE2Imp { get; set; }

    public Int64? DTImp { get; set; }

    public Int64? DT2Imp { get; set; }

    public Int64? NTImp { get; set; }

    public Int64? MLBImp { get; set; }

    public Int64? WLBImp { get; set; }

    public Int64? SLBImp { get; set; }

    public Int64? ROLBImp { get; set; }

    public Int64? LOLBImp { get; set; }

    public Int64? CB1Imp { get; set; }

    public Int64? CB2Imp { get; set; }

    public Int64? CB3Imp { get; set; }

    public Int64? FSImp { get; set; }

    public Int64? SSImp { get; set; }

    public String DraftStrategy { get; set; }

    public String TeamBuilding { get; set; }

    public Int64? RespectedByPlayers { get; set; }

    public Int64? RespectedByCoaches { get; set; }

    public Int64? RespectedByScouts { get; set; }

    public Int64? OrganizationalPower { get; set; }

    public Int64? AbsentMinded { get; set; }

    public Int64? AbstractReasoning { get; set; }

    public Int64? AbstractThinker { get; set; }

    public Int64? Adaptable { get; set; }

    public Int64? Adventurous { get; set; }

    public Int64? Aggressive { get; set; }

    public Int64? Analytical { get; set; }

    public Int64? Astute { get; set; }

    public Int64? Bossy { get; set; }

    public Int64? CalmUnderPressure { get; set; }

    public Int64? Caring { get; set; }

    public Int64? Competitive { get; set; }

    public Int64? Conforming { get; set; }

    public Int64? Controlling { get; set; }

    public Int64? Critical { get; set; }

    public Int64? Diplomatic { get; set; }

    public Int64? Driven { get; set; }

    public Int64? Dutiful { get; set; }

    public Int64? EmotionallyStable { get; set; }

    public Int64? Enthusiastic { get; set; }

    public Int64? Expermiental { get; set; }

    public Int64? Expressive { get; set; }

    public Int64? Fairness { get; set; }

    public Int64? Fearful { get; set; }

    public Int64? FollowsRules { get; set; }

    public Int64? Friendly { get; set; }

    public Int64? FunLoving { get; set; }

    public Int64? GuiltProne { get; set; }

    public Int64? GreedAvoidance { get; set; }

    public Int64? HighEnergy { get; set; }

    public Int64? Honesty { get; set; }

    public Int64? Imaginative { get; set; }

    public Int64? Impatient { get; set; }

    public Int64? Impractical { get; set; }

    public Int64? Impulsive { get; set; }

    public Int64? Individualistic { get; set; }

    public Int64? Insecure { get; set; }

    public Int64? Intelligent { get; set; }

    public Int64? Loner { get; set; }

    public Int64? Mature { get; set; }

    public Int64? MentalCapacity { get; set; }

    public Int64? Modesty { get; set; }

    public Int64? Moralistic { get; set; }

    public Int64? Nurturing { get; set; }

    public Int64? Organized { get; set; }

    public Int64? Participator { get; set; }

    public Int64? Perfectionist { get; set; }

    public Int64? Polished { get; set; }

    public Int64? Private { get; set; }

    public Int64? Resourceful { get; set; }

    public Int64? SelfDoubting { get; set; }

    public Int64? SelfSufficient { get; set; }

    public Int64? StrongWilled { get; set; }

    public Int64? Stubborn { get; set; }

    public Int64? TeamPlayer { get; set; }

    public Int64? Tense { get; set; }

    public Int64? Vigilant { get; set; }

    public String Dominant { get; set; }

    public String Weakest { get; set; }

    public String PersonnelTypeStr { get; set; }
}

public partial class RosterPlayers
{
    [PrimaryKey, AutoIncrement]
    public Int32 PlayerID { get; set; }

    public Int32? AgentID { get; set; }

    public Int32? TeamID { get; set; }

    [MaxLength(20)]
    public String FName { get; set; }

    [MaxLength(20)]
    public String LName { get; set; }

    [MaxLength(50)]
    public String College { get; set; }

    [MaxLength(10)]
    public String ScoutRegion { get; set; }

    public Int32? Age { get; set; }

    [MaxLength(12)]
    public String DOB { get; set; }

    public Int32? Height { get; set; }

    public Int32? Weight { get; set; }

    public Decimal? ArmLength { get; set; }

    public Decimal? HandLength { get; set; }

    [MaxLength(4)]
    public String Pos { get; set; }

    [MaxLength(20)]
    public String PosType { get; set; }

    public Decimal? FortyYardTime { get; set; }

    public Int32? Reaction { get; set; }

    public Int32? QAB { get; set; }

    public Int32? COD { get; set; }

    public Int32? Hands { get; set; }

    public Int32? BodyCatch { get; set; }

    public Int32? StiffArm { get; set; }

    public Int32? ReleaseOffLine { get; set; }

    public Int32? CatchWhenHit { get; set; }

    public Int32? BreaksTackles { get; set; }

    public Int32? ContactBalance { get; set; }

    public Int32? RunAfterCatch { get; set; }

    public Int32? LowerBodyStrength { get; set; }

    public Int32? UpperBodyStrength { get; set; }

    public Int32? Footwork { get; set; }

    public Int32? HandUse { get; set; }

    public Int32? JumpingAbility { get; set; }

    public Int32? PassBlockVsPower { get; set; }

    public Int32? PassBlockVsSpeed { get; set; }

    public Int32? RunBlocking { get; set; }

    public Int32? PlaySpeed { get; set; }

    public Int32? RouteRunning { get; set; }

    public Int32? KickAccuracy { get; set; }

    public Int32? AdjustToBall { get; set; }

    public Int32? Tackling { get; set; }

    public Int32? Blitz { get; set; }

    public Int32? AvoidBlockers { get; set; }

    public Int32? ShedBlock { get; set; }

    public Int32? DefeatBlock { get; set; }

    public Int32? ManToManCoverage { get; set; }

    public Int32? ZoneCoverage { get; set; }

    public Int32? RETKickReturn { get; set; }

    public Int32? RETPuntReturn { get; set; }

    public Int32? PlayStrength { get; set; }

    public Int32? QBMechanics { get; set; }

    public Int32? QBRelQuickness { get; set; }

    public Int32? QBAccuracy { get; set; }

    public Int32? QBDecMaking { get; set; }

    public Int32? QBBallHandling { get; set; }

    public Int32? QBLocateRec { get; set; }

    public Int32? QBPocketPresence { get; set; }

    public Int32? QBEscape { get; set; }

    public Int32? QBScrambling { get; set; }

    public Int32? QBRolloutRight { get; set; }

    public Int32? QBRolloutLeft { get; set; }

    public Int32? QBArmStrength { get; set; }

    public Int32? QBTouch { get; set; }

    public Int32? QBPlayAction { get; set; }

    public Int32? RBRunVision { get; set; }

    public Int32? RBSetsUpBlocks { get; set; }

    public Int32? RBPatience { get; set; }

    public Int32? WRRunDBOff { get; set; }

    public Int32? WRDisguiseRoute { get; set; }

    public Int32? OLPulling { get; set; }

    public Int32? OLSlide { get; set; }

    public Int32? OLMoveInSpace { get; set; }

    public Int32? OLSnapAbility { get; set; }

    public Int32? OLLongSnapAbility { get; set; }

    public Int32? OLAnchorAbility { get; set; }

    public Int32? OLRecover { get; set; }

    [MaxLength(15)]
    public String DLPrimaryTech { get; set; }

    [MaxLength(15)]
    public String DLSecondaryTech { get; set; }

    [MaxLength(15)]
    public String DLPassRushTech { get; set; }

    public Int32? DLRunAtHim { get; set; }

    public Int32? DLAgainstPullAbility { get; set; }

    public Int32? DLSlideABility { get; set; }

    public Int32? DLRunPursuit { get; set; }

    public Int32? DLCanTakeDoubleTeam { get; set; }

    public Int32? DLFinish { get; set; }

    public Int32? DLsetUpPassRush { get; set; }

    public Int32? LBDropDepth { get; set; }

    public Int32? LBFillGaps { get; set; }

    public Int32? DBPressBailCoverage { get; set; }

    public Int32? DBRunContain { get; set; }

    public Int32? DBBump { get; set; }

    public Int32? DBBaitQB { get; set; }

    public Int32? DBCatchUpSpeed { get; set; }

    public Int32? DBTechnique { get; set; }

    public Int32? KFakeAbility { get; set; }

    public Int32? KKickRise { get; set; }

    public Int32? PFakeAbility { get; set; }

    public Int32? PDistance { get; set; }

    public Int32? PHangTime { get; set; }

    public Int32? STCoverage { get; set; }

    public Int32? STWillingness { get; set; }

    public Int32? STAssignment { get; set; }

    public Int32? STDiscipline { get; set; }

    public Int32? Flexibility { get; set; }

    public Int32? Consistency { get; set; }

    public Int32? Instincts { get; set; }

    public Int32? Coachability { get; set; }

    public Int32? Leadership { get; set; }

    public Int32? Confidence { get; set; }

    public Int32? Clutch { get; set; }

    public Int32? WorkEthic { get; set; }

    public Int32? FilmStudy { get; set; }

    public Int32? Durability { get; set; }

    public Int32? Explosion { get; set; }

    public Int32? DeliversBlow { get; set; }

    public Int32? Toughness { get; set; }

    public Int32? ReadKeys { get; set; }

    public Int32? FieldAwareness { get; set; }

    public Int32? PlaybookKnowledge { get; set; }

    public Int32? BallSecurity { get; set; }

    public Int32? LovesFootball { get; set; }

    public Int32? Concentration { get; set; }

    public Int32? HandlesElements { get; set; }

    public Int32? Potential { get; set; }

    public Int32? Raw { get; set; }

    public Int32? AbsentMinded { get; set; }

    public Int32? AbstractReasoning { get; set; }

    public Int32? AbstractThinker { get; set; }

    public Int32? Adaptable { get; set; }

    public Int32? Adventurous { get; set; }

    public Int32? Aggressive { get; set; }

    public Int32? Analytical { get; set; }

    public Int32? Astute { get; set; }

    public Int32? Bossy { get; set; }

    public Int32? CalmUnderPressure { get; set; }

    public Int32? Caring { get; set; }

    public Int32? Competitive { get; set; }

    public Int32? Conforming { get; set; }

    public Int32? Controlling { get; set; }

    public Int32? Critical { get; set; }

    public Int32? Diplomatic { get; set; }

    public Int32? Driven { get; set; }

    public Int32? Dutiful { get; set; }

    public Int32? EmotionallyStable { get; set; }

    public Int32? Enthusiastic { get; set; }

    public Int32? Expermiental { get; set; }

    public Int32? Expressive { get; set; }

    public Int32? Fairness { get; set; }

    public Int32? Fearful { get; set; }

    public Int32? FollowsRules { get; set; }

    public Int32? Friendly { get; set; }

    public Int32? FunLoving { get; set; }

    public Int32? GuiltProne { get; set; }

    public Int32? GreedAvoidance { get; set; }

    public Int32? HighEnergy { get; set; }

    public Int32? Honesty { get; set; }

    public Int32? Imaginative { get; set; }

    public Int32? Impatient { get; set; }

    public Int32? Impractical { get; set; }

    public Int32? Impulsive { get; set; }

    public Int32? Individualistic { get; set; }

    public Int32? Insecure { get; set; }

    public Int32? Intelligent { get; set; }

    public Int32? Loner { get; set; }

    public Int32? Mature { get; set; }

    public Int32? MentalCapacity { get; set; }

    public Int32? Modesty { get; set; }

    public Int32? Moralistic { get; set; }

    public Int32? Nurturing { get; set; }

    public Int32? Organized { get; set; }

    public Int32? Participator { get; set; }

    public Int32? Perfectionist { get; set; }

    public Int32? Polished { get; set; }

    public Int32? Private { get; set; }

    public Int32? Resourceful { get; set; }

    public Int32? SelfDoubting { get; set; }

    public Int32? SelfSufficient { get; set; }

    public Int32? StrongWilled { get; set; }

    public Int32? Stubborn { get; set; }

    public Int32? TeamPlayer { get; set; }

    public Int32? Tense { get; set; }

    public Int32? Vigilant { get; set; }

    [MaxLength(25)]
    public String Dominant { get; set; }

    [MaxLength(25)]
    public String Weakest { get; set; }
}

public partial class TeamDefense
{
    [PrimaryKey, AutoIncrement]
    public Int64 Id { get; set; }

    [Unique(Name = "sqlite_autoindex_TeamDefense_1", Order = 0)]
    [NotNull]
    public Int64 TeamID { get; set; }

    [NotNull]
    public Int64 TotalYards { get; set; }

    [NotNull]
    public Int64 PassingYards { get; set; }

    [NotNull]
    public Int64 RushingYards { get; set; }

    [NotNull]
    public Int64 TotalFirstDowns { get; set; }

    [NotNull]
    public Int64 PassingFirstDowns { get; set; }

    [NotNull]
    public Int64 RushingFirstDowns { get; set; }

    [NotNull]
    public Int64 TotalTD { get; set; }

    [NotNull]
    public Int64 PassingTD { get; set; }

    [NotNull]
    public Int64 RushingTD { get; set; }

    [NotNull]
    public Int64 TotalINT { get; set; }

    public Int64? PointsAllowed { get; set; }

    public Int64? TotalPlays { get; set; }

    public Int64? PassingPlays { get; set; }

    public Int64? RushingPlays { get; set; }

    public Int64? Sacks { get; set; }
}

public partial class TeamOffense
{
    [PrimaryKey, AutoIncrement]
    public Int64 Id { get; set; }

    [Unique(Name = "sqlite_autoindex_TeamOffense_1", Order = 0)]
    [NotNull]
    public Int64 TeamID { get; set; }

    [NotNull]
    public Int64 TotalYards { get; set; }

    [NotNull]
    public Int64 PassingYards { get; set; }

    [NotNull]
    public Int64 RushingYards { get; set; }

    [NotNull]
    public Int64 TotalFirstDowns { get; set; }

    [NotNull]
    public Int64 PassingFirstDowns { get; set; }

    [NotNull]
    public Int64 RushingFirstDowns { get; set; }

    [NotNull]
    public Int64 TotalTD { get; set; }

    [NotNull]
    public Int64 PassingTD { get; set; }

    [NotNull]
    public Int64 RushingTD { get; set; }

    [NotNull]
    public Int64 TotalINT { get; set; }

    public Int64? PointsFor { get; set; }

    public Int64? TotalPlays { get; set; }

    public Int64? PassingPlays { get; set; }

    public Int64? RushingPlays { get; set; }

    public Int64? TimesSacked { get; set; }
}

public partial class Teams
{
    public Int64? ConfID { get; set; }

    public Int64? DivID { get; set; }

    [PrimaryKey, AutoIncrement]
    public Int64 TeamID { get; set; }

    [Indexed(Name = "OrderByName", Order = 0)]
    public String TeamName { get; set; }

    public String TeamNickname { get; set; }

    public Int64? TeamAbrev { get; set; }

    public String City { get; set; }

    public String State { get; set; }

    public Int64? MetroPopulation { get; set; }

    public String StadiumName { get; set; }

    public Int64? StadiumCapacity { get; set; }

    public Int64? LuxuryBoxNum { get; set; }

    public Int64? LuxuryBoxRevenue { get; set; }

    public Int64? Wins { get; set; }

    public Int64? Losses { get; set; }

    public Int64? Ties { get; set; }

    public Int64? DivStanding { get; set; }

    public Int64? HomeWins { get; set; }

    public Int64? HomeLosses { get; set; }

    public Int64? HomeTies { get; set; }

    public Int64? AwayWins { get; set; }

    public Int64? AwayLosses { get; set; }

    public Int64? AwayTies { get; set; }

    public Int64? DivWins { get; set; }

    public Int64? DivLosses { get; set; }

    public Int64? DivTies { get; set; }

    public Int64? ConfWins { get; set; }

    public Int64? ConfLosses { get; set; }

    public Int64? ConfTies { get; set; }

public Int64? FanSupport { get; set; }

public Int64? FanErosionSpeed { get; set; }

public Decimal? AvgTicketPrice { get; set; }

public Int64? SeasonTickets { get; set; }

public Int64? SalaryCap { get; set; }

public Int64? CurrentCap { get; set; }

public Int64? StaffSalaries { get; set; }

public Int64? NamingRightsDeal { get; set; }

public Int64? NamingRightsExpYear { get; set; }

public String NamingRightsCompany { get; set; }

public Int64? SponsorsDeals { get; set; }

public Int64? DraftPosition { get; set; }

public String DraftPicksThisYear { get; set; }

public String DraftPicksNextYear { get; set; }

public String DraftPicksThreeYears { get; set; }

public String DraftPicksFourYears { get; set; }

public String FranchiseTag { get; set; }

public Int64? TransitionTag { get; set; }

public Int64? AvgAttendance { get; set; }

public String MainColor { get; set; }

public String SecondaryColor { get; set; }

    public String TrimColor { get; set; }

public String TeamLogoPath { get; set; }

public String StadiumPic { get; set; }

public String ConfName { get; set; }

public String DivName { get; set; }
}

public partial class Trainers
{
    [PrimaryKey, AutoIncrement]
    public Int32 TrainerID { get; set; }

    public Int32? TeamID { get; set; }

    [MaxLength(20)]
    public String FName { get; set; }

    [MaxLength(20)]
    public String LName { get; set; }

    [MaxLength(50)]
    public String College { get; set; }

    public Int32? Age { get; set; }

    [MaxLength(12)]
    public String DOB { get; set; }

    public Int32? Height { get; set; }

    public Int32? Weight { get; set; }

    public Int32? Experience { get; set; }

    [MaxLength(20)]
    public String TrainerType { get; set; }

    public Int32? TreatHeadInj { get; set; }

    public Int32? TreatNeckInj { get; set; }

    public Int32? TreatShoulderInj { get; set; }

    public Int32? TreatArmInj { get; set; }

    public Int32? TreatWristInj { get; set; }

    public Int32? TreatHandInj { get; set; }

    public Int32? TreatChestInj { get; set; }

    public Int32? TreatBackInj { get; set; }

    public Int32? TreatCoreInj { get; set; }

    public Int32? TreatHipInj { get; set; }

    public Int32? TreatQuadInj { get; set; }

    public Int32? TreatHamstringInj { get; set; }

    public Int32? TreatCalfInj { get; set; }

    public Int32? TreatAnkleInj { get; set; }

    public Int32? TreatFootInj { get; set; }

    public Int32? TreatKneeInj { get; set; }

    public Int32? TreatSpinalInj { get; set; }

    public Int32? DiagnoseInj { get; set; }

    public Int32? SurgicallyRepair { get; set; }

    public Int32? AbsentMinded { get; set; }

    public Int32? AbstractReasoning { get; set; }

    public Int32? AbstractThinker { get; set; }

    public Int32? Adaptable { get; set; }

    public Int32? Adventurous { get; set; }

    public Int32? Aggressive { get; set; }

    public Int32? Analytical { get; set; }

    public Int32? Astute { get; set; }

    public Int32? Bossy { get; set; }

    public Int32? CalmUnderPressure { get; set; }

    public Int32? Caring { get; set; }

    public Int32? Competitive { get; set; }

    public Int32? Conforming { get; set; }

    public Int32? Controlling { get; set; }

    public Int32? Critical { get; set; }

    public Int32? Diplomatic { get; set; }

    public Int32? Driven { get; set; }

    public Int32? Dutiful { get; set; }

    public Int32? EmotionallyStable { get; set; }

    public Int32? Enthusiastic { get; set; }

    public Int32? Expermiental { get; set; }

    public Int32? Expressive { get; set; }

    public Int32? Fairness { get; set; }

    public Int32? Fearful { get; set; }

    public Int32? FollowsRules { get; set; }

    public Int32? Friendly { get; set; }

    public Int32? FunLoving { get; set; }

    public Int32? GuiltProne { get; set; }

    public Int32? GreedAvoidance { get; set; }

    public Int32? HighEnergy { get; set; }

    public Int32? Honesty { get; set; }

    public Int32? Imaginative { get; set; }

    public Int32? Impatient { get; set; }

    public Int32? Impractical { get; set; }

    public Int32? Impulsive { get; set; }

    public Int32? Individualistic { get; set; }

    public Int32? Insecure { get; set; }

    public Int32? Intelligent { get; set; }

    public Int32? Loner { get; set; }

    public Int32? Mature { get; set; }

    public Int32? MentalCapacity { get; set; }

    public Int32? Modesty { get; set; }

    public Int32? Moralistic { get; set; }

    public Int32? Nurturing { get; set; }

    public Int32? Organized { get; set; }

    public Int32? Participator { get; set; }

    public Int32? Perfectionist { get; set; }

    public Int32? Polished { get; set; }

    public Int32? Private { get; set; }

    public Int32? Resourceful { get; set; }

    public Int32? SelfDoubting { get; set; }

    public Int32? SelfSufficient { get; set; }

    public Int32? StrongWilled { get; set; }

    public Int32? Stubborn { get; set; }

    public Int32? TeamPlayer { get; set; }

    public Int32? Tense { get; set; }

    public Int32? Vigilant { get; set; }

    [MaxLength(25)]
    public String Dominant { get; set; }

    [MaxLength(25)]
    public String Weakest { get; set; }
}

public partial class DraftPick
{
    [PrimaryKey, AutoIncrement]
    public Int32 PickID { get; set; }

    [NotNull]
    public Int32 Year { get; set; }

    [NotNull]
    public Int32 DraftRound { get; set; }

    public Int32? PickNumRound { get; set; }

    public Int32? PickNumOverall { get; set; }

    [NotNull]
    public Int32 PickTeamIDCurr { get; set; }

    [NotNull]
    public Int32[] PickTeamIDArray { get; set; }

    public Double PickValue { get; set; }

    [NotNull]
    public string PickType { get; set; }

    public string PlayerFName { get; set; }
    public string PlayerLName { get; set; }
    public Int32? PlayerID { get; set; }
    public string PlayerCollege { get; set; }
    public string PlayerPos { get; set; }
}
