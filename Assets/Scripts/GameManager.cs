using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject retryButton;
    [SerializeField] GameObject inGameUI;


    private void Start()
    {
        retryButton.SetActive(false);
    }
    public void PlayerDied()
    {
        retryButton.SetActive(true);
        inGameUI.SetActive(false);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }
}
