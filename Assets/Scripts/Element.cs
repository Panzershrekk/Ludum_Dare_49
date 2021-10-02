using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Element : MonoBehaviour {
    public GameManager GameManager;
    public Vector3 offset = Vector3.zero;
    public GameObject LayoutPrefab;
    public GameObject ImageInputPrefab;
    private GameObject _layout;

    // Start is called before the first frame update
    void Start() {
        _layout = Instantiate(LayoutPrefab, FindObjectOfType<Canvas>().transform);
    }

    // Update is called once per frame
    void Update() {
        _layout.transform.position = Camera.main.WorldToScreenPoint(transform.position + offset);
    }

    public void CreateInput(InputScriptable inputScriptable) {
        GameObject obj = Instantiate(ImageInputPrefab, _layout.transform);
    }

    public void ValidateInput(int number) {
        Transform t = _layout.transform.GetChild(number);
        Animator anim = t.GetComponent<Animator>();
        anim.SetTrigger("Correct");
    }

    public void ResetElement() {
        foreach (Transform t in _layout.transform) {
            if (GameManager.TimeBetweenCombination / GameManager._difficultyRatio > 0.15f) {
                Destroy(t.gameObject, 0.15f);
            } else {
                Destroy(t.gameObject);
            }
        }
    }
}
