using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Settings : MonoBehaviour {
    public CanvasGroup canvasGrp;
    public Text psToolTip;
    public Text rsToolTip;

	// Use this for initialization
	void Start () {
        CanvasGroup myCanvasGrp = canvasGrp.GetComponent<CanvasGroup>();
        psToolTip = GameObject.Find("PSTooltipTT").GetComponent<Text>();
        psToolTip.gameObject.SetActive (false);

        rsToolTip = GameObject.Find("RSTooltipTT").GetComponent<Text>();
        rsToolTip.gameObject.SetActive (false);
        myCanvasGrp.DOFade(1, 6);
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
