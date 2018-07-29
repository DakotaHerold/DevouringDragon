using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI; 

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

    public void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
        optionsTint.SetActive(true);
        menuPanel.SetActive(false);
        SetSelection(gameOverPanel);
    }

    public void SubmitScore()
    {
        // TODO store score
        gameOverPanel.SetActive(false); 
        optionsTint.SetActive(false);
        menuPanel.SetActive(true);
        ShowMenu(); 
    }
}
