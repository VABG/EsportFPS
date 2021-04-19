using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUI : MonoBehaviour
{
    [SerializeField] UnityEngine.UI.Text totalBullets;
    [SerializeField] List<GameObject> bulletIcons;
    [SerializeField] List<GameObject> grenadeIcons;

    public void SetBullets(int bullets, int remainingAmmo)
    {
        totalBullets.text = (remainingAmmo - bullets).ToString();
        for (int i = 0; i < bulletIcons.Count; i++)
        {
            if (i < bullets) bulletIcons[i].SetActive(true);
            else bulletIcons[i].SetActive(false);
        }
    }

    public void SetGrenadesUI(int grenades)
    {
        for (int i = 0; i < grenadeIcons.Count; i++)
        {
            if (i < grenades) grenadeIcons[i].SetActive(true);
            else grenadeIcons[i].SetActive(false);
        }
    }

}
