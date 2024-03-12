using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Assets;
using Context;

public class WonStats : MonoBehaviour
{
    public static SQLdb DBManager = new SQLdb();
    public Hero hero;
    public Text xpText;
    public Text coinsText;
    public Text damageText;
    public Text EvdateRateText;
    public Text HealthRegText;
    public Text HitRateText;
    public Text CriticalChanText;
    public Text ArmourPenetText;
    public Text maxHealthTextText;
    public Text choiceLeftText;

    private int XP;
    private int Coins;
    private int damage;
    private int evadeRate;
    private int healthReg;
    private int hitRate;
    private int critcalChange;
    private int armourPent;
    private int maxHealth;
    private int choiceLeft;
    private int clickCount;
    public GameObject ContinueButton;

    void Start()
    {
        MainMenu.currentHero = DBManager.GetHero("moshe");
        Dictionary<string, object> loot = hero.WonFight();

        int randomCoins = (int)loot["Coins"];
        int randomXp = (int)loot["TotalXp"];
        bool raisedLevelAmount = (bool)loot["RaisedLevel"];
        Debug.Log(raisedLevelAmount);
        if (raisedLevelAmount) 
        {
            choiceLeft = 3;
            clickCount = 0;
        } 
        else
        {
            choiceLeft = 0;
            clickCount = 5;
        }
        if (hero != null)
        {
            XP += randomXp;
            Coins += randomCoins;
            damage += hero.Stats.Dmg;
            evadeRate += hero.Stats.EvadeRate;
            healthReg += hero.Stats.HealthRegeneration;
            hitRate += hero.Stats.HitRate;
            critcalChange += hero.Stats.CriticalChance;
            armourPent += hero.Stats.ArmourPenetration;
            maxHealth  += hero.Stats.MaxHealth;
            UpdateUI();
        }
        else
        {
            Debug.LogError("Failed to get hero data");
        }

            Button damageButton = GameObject.Find("DamageButton").GetComponent<Button>();
            damageButton.onClick.AddListener(() => { IncrementAndDecrement(IncrementDamage); });

            Button maxHealthButton = GameObject.Find("MaxHealthButton").GetComponent<Button>();
            maxHealthButton.onClick.AddListener(() => { IncrementAndDecrement(IncrementMaxHealth); });

            Button evadeRateButton = GameObject.Find("EvadeRateButton").GetComponent<Button>();
            evadeRateButton.onClick.AddListener(() => { IncrementAndDecrement(IncrementEvadeRate); });

            Button healthRegButton = GameObject.Find("HealthRegButton").GetComponent<Button>();
            healthRegButton.onClick.AddListener(() => { IncrementAndDecrement(IncrementHealthReg); });

            Button hitRateButton = GameObject.Find("HitRateButton").GetComponent<Button>();
            hitRateButton.onClick.AddListener(() => { IncrementAndDecrement(IncrementHitRate); });

            Button criticalChanButton = GameObject.Find("CriticalChanButton").GetComponent<Button>();
            criticalChanButton.onClick.AddListener(() => { IncrementAndDecrement(IncrementCriticalChan); });

            Button armourPenetButton = GameObject.Find("ArmourPenetButton").GetComponent<Button>();
            armourPenetButton.onClick.AddListener(() => { IncrementAndDecrement(IncrementArmourPenet); });
    }

    public void IncrementDamage()
    {
        if (clickCount < 3)
        {
            hero.Stats.Dmg += 2;
            damage += 2;
            clickCount++;
            UpdateUI();
        }
    }

    public void IncrementMaxHealth()
    {
        if (clickCount < 3)
        {
            hero.Stats.MaxHealth += 3;
            maxHealth += 3;
            clickCount++;
            UpdateUI();
        }
    }

    public void IncrementEvadeRate()
    {
        if (clickCount < 3)
        {
            hero.Stats.EvadeRate += 1;
            evadeRate += 1;
            clickCount++;
            UpdateUI();
        }
    }

    public void IncrementHealthReg()
    {
        if (clickCount < 3)
        {
            hero.Stats.HealthRegeneration += 2;
            healthReg += 2;
            clickCount++;
            UpdateUI();
        }
    }

    public void IncrementHitRate()
    {
        if (clickCount < 3)
        {
            hero.Stats.HitRate += 1;
            hitRate += 1;
            clickCount++;
            UpdateUI();
        }
    }

    public void IncrementCriticalChan()
    {
        if (clickCount < 3)
        {
            hero.Stats.CriticalChance += 1;
            critcalChange += 1;
            clickCount++;
            UpdateUI();
        }
    }

    public void IncrementArmourPenet()
    {
        if (clickCount < 3)
        {
            hero.Stats.ArmourPenetration += 1;
            armourPent += 1;
            clickCount++;
            UpdateUI();
        }
    }

    private void IncrementAndDecrement(UnityAction incrementMethod)
    {
        incrementMethod.Invoke(); 
        DecrementChoicePoints(); 
    }

    public void DecrementChoicePoints()
    {
        if (clickCount <= 3 && choiceLeft >= 0)
        {
            choiceLeft--;
            UpdateUI();
        }
    }


    public void UpdateUI()
    {
        xpText.text = "XP: " + XP.ToString();
        coinsText.text = "Coins: " + Coins.ToString();
        damageText.text = "Damage: " + damage.ToString();
        EvdateRateText.text = "Evade Rate: " + evadeRate.ToString();
        HealthRegText.text = "Health Regeneration: " + healthReg.ToString();
        HitRateText.text = "Hit Rate: " + hitRate.ToString();
        CriticalChanText.text = "Critical Chance: " + critcalChange.ToString();
        ArmourPenetText.text = "Armour Penetration: " + armourPent.ToString();
        maxHealthTextText.text = "Max Health: " + maxHealth.ToString();
        choiceLeftText.text = "Points Remaining " + choiceLeft.ToString() ;
        if (clickCount >= 3 | clickCount == 0)
        {
            ContinueButton.SetActive(true);
            Debug.Log("2" + hero.Inventory.Coins);
            DBManager.SaveHero(hero);
            Debug.Log("3" + hero.Inventory.Coins);
        }
        else { ContinueButton.SetActive(false);}
    }
}
