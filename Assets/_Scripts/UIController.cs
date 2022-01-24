using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject deathMenu;
    [SerializeField] AnimationClip pauseClip;
    [SerializeField] AnimationClip resumeClip;
    [SerializeField] AnimationClip pauseClip2;
    [SerializeField] AnimationClip resumeClip2;
    [SerializeField] AnimationClip startClip;
    [SerializeField] GameObject canvasBackground;
    private Animation anim;

    private void Start()
    {
        canvasBackground.SetActive(true);
        canvasBackground.GetComponent<CanvasGroup>().alpha = 1f;
        closeMenu();
        anim = GetComponent<Animation>();
        anim.clip = startClip;
        anim.Play();
    }
    private void closeMenu()
    {
        pauseMenu.SetActive(false);
        deathMenu.SetActive(false);
    }
    public void StartButtonClicked() => SceneManager.LoadScene(1);

    public void RestartButtonClicked() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    public void HomeButtonClicked() => SceneManager.LoadScene("Home");

    public void ResumeButtonClicked() => GameManager.instance.SwitchGameState(GameManager.GameState.Playing);

    public void Pause()
    {
        /*closeMenu();
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;*/
        if (GameManager.instance.CharacterInControl == 0)
            anim.clip = pauseClip;
        else
            anim.clip = pauseClip2;
        anim.Play();
    }

    public void Resume()
    {
        /*closeMenu();
        Time.timeScale = 1f;*/
        if (GameManager.instance.CharacterInControl == 0)
            anim.clip = resumeClip;
        else
            anim.clip = resumeClip2;
        anim.Play();
    }

    /*public void Death()
    {
        closeMenu();
        deathMenu.SetActive(true);
    }*/
}
