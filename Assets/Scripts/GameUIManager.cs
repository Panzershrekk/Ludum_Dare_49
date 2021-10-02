using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameUIManager : MonoBehaviour {

    public Image FilledImage;
    private float _timer = 0f;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
    }

    public void FillImage(float fillAmount) {
        /*_timer += Time.deltaTime;
        float value = Mathf.Lerp(FilledImage.fillAmount, fillAmount, _timer);*/
        FilledImage.fillAmount = fillAmount;
    }
}
