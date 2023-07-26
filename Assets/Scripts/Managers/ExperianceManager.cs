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
    [SerializeField] private string levelMenu;
    [SerializeField] private Button[] upgradeButtons;
    [HideInInspector] public PlayerAbility playerAbility;

    public PlayerLoadout[] upgrades;
    public bool isUpgrading {get; private set;}

    private void Awake() {
        Instance = this;
        level = 1;
        maxLevel = false;
        UpdateBar();
        isUpgrading = false;
    }

    public void GainExp(int amount){
        exp += amount;
        UpdateBar();
        if(exp >= expToLevel.Evaluate(level) && !maxLevel) {
            LevelUp(exp - (int)expToLevel.Evaluate(level));
        } 
    }

    private void LevelUp(int excessExp) {
        isUpgrading = true;
        AudioManager.Instance.Play(levelUpSound);
        level ++;
        levelText.text = $"Lv {level}";
        exp = excessExp;
        UIHandler.Instance.SwapTo(levelMenu);
        Time.timeScale = 0f; //Don't want to do this, just lazy
        for(int i = 0; i < upgradeButtons.Length && i < upgrades.Length; i++) {
            upgradeButtons[i].gameObject.SetActive(true);
            upgradeButtons[i].GetComponentInChildren<Image>().sprite = upgrades[i].uiSprite;
        }
    }

    public void UpgradePlayer(int id) {
        playerAbility.LoadPlayerLoadout(upgrades[id]);
        finishLevelUp();
    }

    private void finishLevelUp() {
        isUpgrading = false;
        foreach(Button button in upgradeButtons) {
            button.gameObject.SetActive(false);
        }
        
        UIHandler.Instance.SwapTo();
        Time.timeScale = 1f;
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
