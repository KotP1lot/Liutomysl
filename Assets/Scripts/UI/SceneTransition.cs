using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    private Animator animator;
    private int nextSceneID;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadNextLevel(int index)
    {
        animator.SetTrigger("Start");
        nextSceneID = index;
    }

    public void DeathTransition(int index)
    {
        animator.SetTrigger("Death");
        nextSceneID = index;
    }

    public void SleepTransition(int index)
    {
        animator.SetTrigger("Sleep");
        nextSceneID = index;
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(nextSceneID);
    }
    
    public void ToggleFullScreen()
    {
        //Screen.fullScreen = !Screen.fullScreen;

        Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, !Screen.fullScreen);
    }
    public void ExitTheGame()
    {
        Application.Quit();
    }
}
