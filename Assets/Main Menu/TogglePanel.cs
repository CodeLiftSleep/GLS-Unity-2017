using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglePanel : MonoBehaviour {

    public void TogglePanelBtn(GameObject panel)
    {
        panel.SetActive(!panel.activeSelf);
    }

    public void QuitBtn(GameObject panel)
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}

