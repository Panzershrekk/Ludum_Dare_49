using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [HideInInspector]
    public bool GameStarted = false;
    [HideInInspector]
    public bool GameFinished = false;
    public GameUIManager GameUIManager;
    public Combination Combination;
    public float Unstability = 0.5f;
    public float UnstabilityGrowPerSec = 0.01f;
    public float UnstabilityDecreaseWhenSuccess = -0.1f;
    public float TimeBetweenCombination = 3f;
    public float BaseDifficultyRatio = 1;
    public float SecondForDifficultyToIncrease = 15f;
    public float DifficultyIncrease = 0.1f;
    public float _difficultyRatio = 1;
    private float _difficultyRatioToAdd = 0;
    private float _timeBeforeNextCombination;
    private bool _combinationPending = false;
    private float _elaspedTime = 0;
    private float _nextDifficultyIncreaseTime = 0f;
    void Start() {
        StartGame();
    }

    // Update is called once per frame
    void Update() {
        if (GameStarted == true && GameFinished == false) {
            _elaspedTime += Time.deltaTime;
            ChangeUnstability(UnstabilityGrowPerSec * _difficultyRatio * Time.deltaTime);
            if (_timeBeforeNextCombination > 0) {
                _timeBeforeNextCombination -= Time.deltaTime;
            } else if (_combinationPending == false) {
                Combination.GenerateCombination(3);
                _combinationPending = true;
            }
            if (_nextDifficultyIncreaseTime > 0) {
                _nextDifficultyIncreaseTime -= Time.deltaTime;
            }
            if (_nextDifficultyIncreaseTime < 0) {
                _nextDifficultyIncreaseTime = SecondForDifficultyToIncrease;
                _difficultyRatioToAdd += DifficultyIncrease;
            }
            _difficultyRatio = BaseDifficultyRatio + _difficultyRatioToAdd;
        }
    }

    public void CombinationSuccess() {
        ChangeUnstability(UnstabilityDecreaseWhenSuccess * _difficultyRatio);
        _combinationPending = false;
        _timeBeforeNextCombination = TimeBetweenCombination / _difficultyRatio;
    }

    void StartGame() {
        _nextDifficultyIncreaseTime = SecondForDifficultyToIncrease;
        GameStarted = true;
    }

    void FinishGame() {
        GameFinished = true;
    }

    //Value must be between 0.0f and 1.0f
    void ChangeUnstability(float toAdd) {
        Unstability += toAdd;
        if (Unstability < 0) {
            Unstability = 0;
        }
        if (Unstability > 1) {
            Unstability = 1;
            FinishGame();
        }
        GameUIManager.FillImage(Unstability);
    }
}
