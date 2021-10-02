using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combination : MonoBehaviour {
    public GameManager GameManager;

    public List<Element> Elements = new List<Element>();
    public List<InputScriptable> ListOfKey = new List<InputScriptable>();

    public List<InputScriptable> CombinationToDo = new List<InputScriptable>();

    private Element _element;
    private int _validatedIndex = 0;

    void Update() {
        if (CombinationToDo.Count > 0) {
            if (Input.GetKeyDown(CombinationToDo[0].key)) {
                Debug.Log("Good press " + CombinationToDo[0]);
                _element.ValidateInput(_validatedIndex);
                _validatedIndex += 1;
                CombinationToDo.RemoveAt(0);
                if (CombinationToDo.Count <= 0) {
                    _element.ResetElement();
                    GameManager.CombinationSuccess();
                }
            }
        }
    }

    public void GenerateCombination(int lenght) {
        _validatedIndex = 0;
        _element = PickRandomElement();
        //Check for the anim
        for (int i = 0; i < lenght; i++) {
            InputScriptable input = PickRandomInput();
            CombinationToDo.Add(input);
            _element.CreateInput(input);
        }
    }

    private InputScriptable PickRandomInput() {
        return ListOfKey[Random.Range(0, ListOfKey.Count)];
    }

    private Element PickRandomElement() {
        return Elements[Random.Range(0, Elements.Count)];
    }
}
