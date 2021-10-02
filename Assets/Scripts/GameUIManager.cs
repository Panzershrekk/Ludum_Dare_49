using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GameUIManager : MonoBehaviour {

    public Animator EndAnimator;
    public TextMeshProUGUI TimerText;
    public Image FilledImage;
    public float LerpSpeed;

    public void FillImage(float fillAmount) {
        FilledImage.fillAmount = Mathf.Lerp(FilledImage.fillAmount, fillAmount, Time.deltaTime * LerpSpeed);
    }

    public void UpdateTimerText(float time) {
        TimeSpan timeSpan = TimeSpan.FromSeconds(time);
        string timeText = string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
        TimerText.text = timeText;
    }

    public void DisplayFinish() {
        EndAnimator.SetTrigger("End");
    }
}
