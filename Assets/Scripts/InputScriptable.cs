using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Input", menuName = "ScriptableObjects/Input", order = 1)]
public class InputScriptable : ScriptableObject {
    public KeyCode key;
    public Image image;
}
