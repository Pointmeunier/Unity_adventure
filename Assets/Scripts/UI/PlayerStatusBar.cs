using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusBar : MonoBehaviour
{
    public Image healthImage;
    public Image healthDelayImgge;

    private void Update()
    {
        if(healthDelayImgge.fillAmount > healthImage.fillAmount)
        {
            healthDelayImgge.fillAmount -=Time.deltaTime;
        }
    }
    public void OnHealthChange(float percentage)
    {
        healthImage.fillAmount = percentage;
    }

}
