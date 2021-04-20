using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEndDetector : MonoBehaviour
{
    [SerializeField] GameObject enemyToKillFirst;
    GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (enemyToKillFirst == null) gm.Win();
    }
}
