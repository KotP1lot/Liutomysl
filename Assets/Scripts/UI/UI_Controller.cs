using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UI_Controller : MonoBehaviour
{
    public GameObject messageBox;
    public GameObject interactPrompt;
    public ProgressBar HpBar;
    public ProgressBar StaminaBar;
    public CounterBar DamageBar;
    public CounterBar AtkSpdBar;
    public GameObject pauseMenu;
    public TextMeshProUGUI itemCounter;
    private int itemCount=0;

    private int mainMenuID =1;
    public bool messageActive { get; private set; }

    private TextMeshProUGUI messageBoxText;
    private TextMeshProUGUI interactPromptText;

    void Start()
    {
        messageBoxText = messageBox.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        interactPromptText = interactPrompt.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        messageBox.SetActive(false);
        interactPrompt.SetActive(false);
        pauseMenu.SetActive(false);
    }

    void Update()
    {
        
    }

    public void showInteractPrompt(string text)
    {
        interactPromptText.text = text;
        interactPrompt.SetActive(true);
    }

    public void hideInteractPrompt()
    {
        interactPromptText.text = "NO PROMPT";
        interactPrompt.SetActive(false);
    }

    public void showMessage(string text, TextAlignmentOptions alignment)
    {
        messageBoxText.text = text;
        messageBoxText.alignment = alignment;
        messageBox.SetActive(true);
        messageActive = true;

        StartCoroutine(messageAutoHide());
    }

    IEnumerator messageAutoHide()
    {
        yield return new WaitForSeconds(20);

        if (messageActive) hideMessage();
    }

    public void hideMessage()
    {
        messageBoxText.text = "NO MESSAGE";
        messageBox.SetActive(false);
        messageActive = false;
    }

    public void ToggleFullScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void ShowPauseMenu()
    {
        pauseMenu.SetActive(true);
    }

    public void HidePauseMenu()
    {
        pauseMenu.SetActive(false);
    }

    public void ItemFound()
    {
        itemCount++;
        itemCounter.text = $"Знайдено покращень {itemCount}/12";
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(mainMenuID);
    }
}
