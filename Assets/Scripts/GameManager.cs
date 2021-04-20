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

    public void Win() 
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].SetActive(false);
        }

        Turret[] turrets = FindObjectsOfType<Turret>();
        for (int i = 0; i < turrets.Length; i++)
        {
            turrets[i].SetActive(false);
        }

        FPPlayerController player = FindObjectOfType<FPPlayerController>();
        player.Disable();

        retryButton.SetActive(true);
        inGameUI.SetActive(false);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }
}
