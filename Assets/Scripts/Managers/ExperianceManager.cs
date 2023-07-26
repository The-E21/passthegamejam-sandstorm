using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExperianceManager : MonoBehaviour
{
    public static ExperianceManager Instance {get; private set;}
    private int exp;

    [SerializeField] private AnimationCurve expToLevel;
    public int level { get; private set; }
    private bool maxLevel;

    [SerializeField] private Slider expBar;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private GameObject maxLevelText;
    [SerializeField] private string levelUpSound;

    public PlayerLoadout[] upgrades;

    private void Awake() {
        Instance = this;
        level = 1;
        maxLevel = false;
        UpdateBar();
    }

    public void GainExp(int amount){
        exp += amount;
        UpdateBar();
        if(exp >= expToLevel.Evaluate(level) && !maxLevel) {
            LevelUp(exp - (int)expToLevel.Evaluate(level));
        } 
    }

    private void LevelUp(int excessExp) {
        AudioManager.Instance.Play(levelUpSound);
        level ++;
        levelText.text = $"Lv {level}";
        exp = excessExp;
        UpdateBar();
        if(expToLevel.Evaluate(level) == 0) {
            AtMaxLevel();
        }
    }

    private void AtMaxLevel(){
        maxLevel = true;
        maxLevelText.SetActive(true);
    }

    private void UpdateBar(){
        if(!expBar.enabled)
            return;

        if(expToLevel.Evaluate(level) == 0){
            expBar.value = 1;
            expBar.maxValue = 1;
            expBar.enabled = false;
            return;
        }
        expBar.value = exp;
        expBar.maxValue = expToLevel.Evaluate(level);
    }
}
