using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static POCOCreation;
using SQLite;
using System.Linq;
/// <summary>
/// This class is used to hold global variables or references as needed
/// </summary>
public class GlobalRefs : MonoBehaviour {
    public static List<Teams> teams = new List<Teams>();
    public static string DBPath { get; set; } = Application.dataPath + "/SQLite DB/Football.sqlite";
    private void Awake()
    {
         using (SQLiteConnection db = new SQLiteConnection(DBPath)){
            teams = db.Query<Teams>("SELECT * FROM Teams").ToList();
            //we need to insert a blank team at the 0 index so we maintain the proper ID's in the list
            teams.Insert(0, new Teams());
        }
    }
    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
