using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combination : MonoBehaviour {
    public GameManager GameManager;

    public List<Element> Elements = new List<Element>();
    public List<InputScriptable> ListOfKey = new List<InputScriptable>();

    public List<InputScriptable> CombinationToDo = new List<InputScriptable>();

    private Element _element;
    private Element _lastElement = null;
    private int _validatedIndex = 0;
    private int _comboPoint = 0;
    private int _maxComboPoint = 0;

    void Update() {
        if (GameManager.GameStarted == true && GameManager.GameFinished == false) {
            if (CombinationToDo.Count > 0) {
                if (Input.GetKeyDown(CombinationToDo[0].key)) {
                    _comboPoint += 1;
                    _element.ValidateInput(_validatedIndex);
                    _validatedIndex += 1;
                    CombinationToDo.RemoveAt(0);
                    if (CombinationToDo.Count <= 0) {
                        _element.PlayValidateAnim();
                        _element.ResetElement();
                        if (_comboPoint == _maxComboPoint) {
                            GameManager.CombinationSuccess(true);
                        } else {
                            GameManager.CombinationSuccess(false);
                        }
                    }
                } else if (Input.anyKeyDown && !Input.GetKeyDown(CombinationToDo[0].key)) {
                    _comboPoint = 0;
                }
            }
        }
    }

    public void GenerateCombination(int lenght) {
        _validatedIndex = 0;
        _comboPoint = 0;
        _maxComboPoint = lenght;
        _element = PickRandomElement();
        _lastElement = _element;
        //Check for the anim
        for (int i = 0; i < lenght; i++) {
            InputScriptable input = PickRandomInput();
            CombinationToDo.Add(input);
            _element.CreateInput(input);
        }
        _element.PlaySelectAnim();
    }

    private InputScriptable PickRandomInput() {
        return ListOfKey[Random.Range(0, ListOfKey.Count)];
    }

    private Element PickRandomElement() {
        List<Element> eleCpy = new List<Element>(Elements);
        if (_lastElement != null) {
            eleCpy.Remove(_lastElement);
        }
        return eleCpy[Random.Range(0, eleCpy.Count)];
    }
}
