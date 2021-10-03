using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public Animator CauldronAnim;
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
    public int BaseNumberOnCombination = 3;
    public float BaseDifficultyRatio = 1;
    public float SecondForDifficultyToIncrease = 15f;
    public float SecondToHoldForWin = 120f;
    public float DifficultyIncrease = 0.1f;
    public float ComboMultiplier = 1.5f;
    public float _difficultyRatio = 1;
    private float _difficultyRatioToAdd = 0;
    private float _timeBeforeNextCombination;
    private bool _combinationPending = false;
    private float _elaspedTime = 0;
    private float _nextDifficultyIncreaseTime = 0f;

    void Start() {
        GameUIManager.UpdateTimerText(0);
        CauldronAnim.Play("Cauldron_bounce");
    }

    // Update is called once per frame
    void Update() {
        if (GameStarted == true && GameFinished == false) {
            _elaspedTime += Time.deltaTime;
            GameUIManager.UpdateTimerText(_elaspedTime);
            ChangeUnstability(UnstabilityGrowPerSec * _difficultyRatio * Time.deltaTime);
            if (_timeBeforeNextCombination > 0) {
                _timeBeforeNextCombination -= Time.deltaTime;
            } else if (_combinationPending == false) {
                Combination.GenerateCombination(BaseNumberOnCombination + (int)(_difficultyRatio / 5));
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

    public void CombinationSuccess(bool combo) {
        if (combo == false) {
            ChangeUnstability(UnstabilityDecreaseWhenSuccess * (1 + _difficultyRatio / 10));
        } else {
            ChangeUnstability(UnstabilityDecreaseWhenSuccess * (1 + _difficultyRatio / 10) * ComboMultiplier);
        }
        _combinationPending = false;
        _timeBeforeNextCombination = TimeBetweenCombination / (1 + _difficultyRatio);
    }

    public void StartGame() {
        _nextDifficultyIncreaseTime = SecondForDifficultyToIncrease;
        GameStarted = true;
    }

    void FinishGame() {
        GameFinished = true;
        if (_elaspedTime > SecondToHoldForWin) {
            Debug.Log("YOU WON");
        }
        GameUIManager.DisplayFinish();
    }

    //Value must be between 0.0f and 1.0f
    void ChangeUnstability(float toAdd) {
        Unstability += toAdd;
        if (Unstability > 0.75f) {
            CauldronAnim.Play("Cauldron_Unstable_75");
        } else if (Unstability > 0.50f) {
            CauldronAnim.Play("Cauldron_Unstable_50");
        } else if (Unstability > 0.25f) {
            CauldronAnim.Play("Cauldron_Unstable_25");
        } else {
            CauldronAnim.Play("Cauldron_bounce");
        }
        if (Unstability < 0) {
            Unstability = 0;
        }
        if (Unstability > 1) {
            Unstability = 1;
            FinishGame();
            CauldronAnim.Play("Idle");
        }
        GameUIManager.FillImage(Unstability);
    }
}
