using UnityEngine;
using System.Collections;
using System.Collections.Generic; 
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq; 

public class ShowPanels : MonoBehaviour {

	public GameObject optionsPanel;							//Store a reference to the Game Object OptionsPanel 
	public GameObject optionsTint;							//Store a reference to the Game Object OptionsTint 
	public GameObject menuPanel;							//Store a reference to the Game Object MenuPanel 
	public GameObject pausePanel;                           //Store a reference to the Game Object PausePanel 
    public GameObject creditsPanel;
    [Space(10)]
    public GameObject gameOverPanel;
    public InputField inField;
    [Space(10)]
    public GameObject highScorePanel;

    public Slider musicSlider;
    public Slider fxSlider; 

    private GameObject activePanel;                         
    private MenuObject activePanelMenuObject;
    private EventSystem eventSystem;



    private void SetSelection(GameObject panelToSetSelected)
    {

        activePanel = panelToSetSelected;
        activePanelMenuObject = activePanel.GetComponent<MenuObject>();
        if (activePanelMenuObject != null)
        {
            activePanelMenuObject.SetFirstSelected();
        }
    }

    public void Start()
    {
        //SetSelection(menuPanel);
    }

    //Call this function to activate and display the Options panel during the main menu
    public void ShowOptionsPanel()
	{
		optionsPanel.SetActive(true);
		optionsTint.SetActive(true);
        menuPanel.SetActive(false);
        SetSelection(optionsPanel);

    }

    public void ShowCreditsPanel()
    {
        creditsPanel.SetActive(true); 
        optionsTint.SetActive(true);
        menuPanel.SetActive(false);
        SetSelection(creditsPanel); 
    }

    public void HideCreditsPanel()
    {
        creditsPanel.SetActive(false);
        optionsTint.SetActive(false);
        menuPanel.SetActive(true);
        SetSelection(menuPanel); 
    }

	//Call this function to deactivate and hide the Options panel during the main menu
	public void HideOptionsPanel()
	{
        menuPanel.SetActive(true);
        optionsPanel.SetActive(false);
		optionsTint.SetActive(false);
	}

	//Call this function to activate and display the main menu panel during the main menu
	public void ShowMenu()
	{
		menuPanel.SetActive (true);
        SetSelection(menuPanel);
    }

	//Call this function to deactivate and hide the main menu panel during the main menu
	public void HideMenu()
	{
		menuPanel.SetActive (false);
	}
	
	//Call this function to activate and display the Pause panel during game play
	public void ShowPausePanel()
	{
		pausePanel.SetActive (true);
		optionsTint.SetActive(true);
        SetSelection(pausePanel);
    }

	//Call this function to deactivate and hide the Pause panel during game play
	public void HidePausePanel()
	{
		pausePanel.SetActive (false);
		optionsTint.SetActive(false);

	}

    public void ShowScores()
    {
        highScorePanel.SetActive(true);
        optionsTint.SetActive(true);
        menuPanel.SetActive(false);
        SetSelection(highScorePanel);
    }

    public void HideScores()
    {
        highScorePanel.SetActive(false);
        optionsTint.SetActive(false);
        menuPanel.SetActive(true); 
    }

    public void ShowGameOverPanel()
    {
        Dictionary<string, int> scores = GetScores();


        string highestScoreString = (scores.FirstOrDefault(x => x.Value == scores.Values.Max()).Key);
        int highestScore = 0;
        int.TryParse(highestScoreString, out highestScore);

        if (scores.Count < 3 || GameHandler.Instance.score > highestScore)
        {
            gameOverPanel.SetActive(true);
            optionsTint.SetActive(true);
            //highScorePanel.SetActive(true); 
            menuPanel.SetActive(false);
            //SetSelection(gameOverPanel);
        }
        else
        {
            ShowMenu();
        }
        
        
      
    }

    public void SubmitScore()
    {
        // TODO store score
        SaveScore(inField.text, GameHandler.Instance.score);
        gameOverPanel.SetActive(false); 
        optionsTint.SetActive(false);
        menuPanel.SetActive(true);
        ShowMenu(); 
    }

    public void SaveScore(string name, int score)
    {
        string scoreStr = name + "," + score.ToString() + "|";
        string prefsStr = PlayerPrefs.GetString("Scores");
        prefsStr += scoreStr;
        PlayerPrefs.SetString("Scores", prefsStr); 
    }

    public Dictionary<string, int> GetScores()
    {
        Dictionary<string, int> scores = new Dictionary<string, int>();
        Dictionary<string, string> dict = new Dictionary<string, string>(); 

        string text = PlayerPrefs.GetString("Scores");
        if(text.Length > 0)
        {
            dict = text.Split('|')
          .Select(s => s.Split(','))
          .ToDictionary(key => key[0].Trim(), value => value[1].Trim());
        }
        

        foreach (KeyValuePair<string, string> entry in dict)
        {
            // do something with entry.Value or entry.Key
            int score = 0;
            int.TryParse(entry.Value, out score);
            if(score > 0)
            {
                scores.Add(entry.Key, score); 
            }
            
        }

        return scores; 
    }

    public float GetMusicVolume()
    {
        return musicSlider.value; 
    }

    public float GetFXVolume()
    {
        return fxSlider.value; 
    }
}


