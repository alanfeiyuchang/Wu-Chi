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
    [SerializeField] GameManager gameManager;
    private Animation anim;

    private void Start()
    {
        closeMenu();
        anim = GetComponent<Animation>();
    }
    private void closeMenu()
    {
        pauseMenu.SetActive(false);
        deathMenu.SetActive(false);
    }
    public void StartButtonClicked() => SceneManager.LoadScene(1);

    public void RestartButtonClicked() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    public void HomeButtonClicked() => SceneManager.LoadScene("Home");

    public void ResumeButtonClicked() => gameManager.SwitchGameState(GameManager.GameState.Playing);

    public void Pause()
    {
        /*closeMenu();
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;*/

        anim.clip = pauseClip;
        anim.Play();
    }

    public void Resume()
    {
        /*closeMenu();
        Time.timeScale = 1f;*/
        anim.clip = resumeClip;
        anim.Play();
    }

    public void Death()
    {
        closeMenu();
        deathMenu.SetActive(true);
    }
}
