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
    public int pageCur;
    public int pageTotal;
    public _string PageText;


    public UnityEngine.UI.Text TextComponent;
    public override void Initialize()
    {
        base.Initialize();

        using (SQLiteConnection db = new SQLiteConnection(DBPath))
        {
            BeginDraft.draftPlayers = db.Query<DraftPlayers>("SELECT * FROM DraftPlayers").ToList();
        }

        OLDraftPlayers = new ObservableList<DraftPlayers>();
        for (var i =0; i <26; i++)
        {
            OLDraftPlayers.Add(BeginDraft.draftPlayers[i]);
        }
        pageCur = 1;
        pageTotal = BeginDraft.draftPlayers.Count / 25;
        SetPages();

        //OLDraftPlayers.AddRange(BeginDraft.draftPlayers);
    }

    private void SetPages()
    {
        PageText.Value = "Page " + pageCur.ToString() + " of " + pageTotal.ToString();

    }
    public void IncrementPage()
    {
        if(pageCur < pageTotal)
        {
            var start = 0;
            var end = 0;
            pageCur++;
            
            OLDraftPlayers.Clear();
            end = pageCur < pageTotal ? pageCur * 25 : pageTotal - ((pageCur * 25) - pageTotal);
            start = end - 24;

            SetPages();
            for (int i = start; i < end+1; i++)
            {
                OLDraftPlayers.Add(BeginDraft.draftPlayers[i]);
            }
        }
    }
    
    public void DecrementPage()
    {
        var start = 0;
        var end = 0;

        if (pageCur != 1)
        {
            pageCur--;
            start = pageCur * 25 + 1;
            end = start + 24;

        }
        else
        {
            end = 24;
            start = 0;
        }
        OLDraftPlayers.Clear();
        SetPages();

        for (int i = start; i < end + 1; i++)
        {
            OLDraftPlayers.Add(BeginDraft.draftPlayers[i]);
        }
    }

}
