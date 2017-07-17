using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MainMenuScript : MonoBehaviour {

    public Button start;
    public Button exit;
    public Button settings;
    public GameObject panel;
    public CanvasGroup canvasGrp;
    
    // Use this for initialization
    void Start () {
       
        CanvasGroup myCanvasGroup = canvasGrp.GetComponent<CanvasGroup>();
        DOTween.Init();
        myCanvasGroup.DOFade(1, 6);

        Button startBtn = start.GetComponent<Button>();
        startBtn.onClick.AddListener(StartGame);

        Button exitBtn = exit.GetComponent<Button>();
        //exitBtn.onClick.AddListener(QuitGame(panel));

        Button settingsBtn = settings.GetComponent<Button>();
        settingsBtn.onClick.AddListener(delegate { GetSettings(myCanvasGroup); });
	}

    //pass in the canvasgroup object to the event listener so we can fade it out before switching scenes
    private void GetSettings(CanvasGroup myCanvasGrp)
    {
        myCanvasGrp.DOFade(0, 3);
        StartCoroutine(WaitForFade(myCanvasGrp));
       
    }
    private IEnumerator WaitForFade (CanvasGroup myCanvasGrp)
    {
        do
        {
            yield return null;
        } while (myCanvasGrp.alpha > 0);

        SceneManager.LoadSceneAsync("Assets/Settings/Settings.unity", LoadSceneMode.Single);
    }
    private void StartGame()
    {
        //throw new NotImplementedException();
    }

    // Update is called once per frame
    void Update () {
		
	}
}

