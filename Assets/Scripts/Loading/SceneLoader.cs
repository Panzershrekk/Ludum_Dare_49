using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
    public Animator transition;
    public float transitionTime = 1f;

    // Update is called once per frame

    public void OnStartClick(int index) {
        StartCoroutine(LoadLevel(index));
    }

    IEnumerator LoadLevel(int levelIndex) {
        transition.SetTrigger("Out");
        AudioManager.Instance.Play("Menu_click");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }
}