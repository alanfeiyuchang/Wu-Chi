using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeUIController : MonoBehaviour
{
    [SerializeField] Animation StartAnimation;

    public void StartButtonClicked()
    {
        StartAnimation.Play();
    }

    public void LoadScene()
    {
        SceneManager.LoadScene("Main");
    }
}
