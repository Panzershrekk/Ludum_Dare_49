using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combination : MonoBehaviour {
    public GameManager GameManager;
    public List<InputScriptable> ListOfKey = new List<InputScriptable>();

    public List<InputScriptable> CombinationToDo = new List<InputScriptable>();
    
    void Update() {
        if (CombinationToDo.Count > 0) {
            if (Input.GetKeyDown(CombinationToDo[0].key)) {
                Debug.Log("Good press " + CombinationToDo[0]);
                CombinationToDo.RemoveAt(0);
                if (CombinationToDo.Count <= 0) {
                    GameManager.CombinationSuccess();
                }
            }
        }
    }

    public void GenerateCombination(int lenght) {
        for (int i = 0; i < lenght; i++) {
            CombinationToDo.Add(PickRandomInput());
        }
    }

    private InputScriptable PickRandomInput() {
        return ListOfKey[Random.Range(0, ListOfKey.Count)];
    }
}
