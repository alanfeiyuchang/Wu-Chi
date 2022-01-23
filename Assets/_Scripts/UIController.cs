using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject deathMenu;

    private void Start()
    {
        closeMenu();
    }
    private void closeMenu()
    {
        pauseMenu.SetActive(false);
        deathMenu.SetActive(false);
    }
    public void StartButtonClicked() => SceneManager.LoadScene(1);

    public void RestartButtonClicked() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    public void HomeButtonClicked() => SceneManager.LoadScene("Home");

    public void Pause()
    {
        closeMenu();
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        closeMenu();
        Time.timeScale = 1f;
    }

    public void Death()
    {
        closeMenu();
        deathMenu.SetActive(true);
    }
}
