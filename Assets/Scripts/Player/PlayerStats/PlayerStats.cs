using System.Collections.Generic;
using UnityEngine;

public enum PlayerCharacteristicsEnum
{
    Vitality,
    Endurance,
    Strenght,
    Dexterity
}
public class PlayerStats
{
    #region Current HP and Endurance
    public float CurrentHealthPoint { get; private set; }
    public float Stamina { get; private set; }
    #endregion

    #region Level and Characteristics
    public int NextLevelCost { get; private set; }
    public int PlayerLevel { get; private set; }
    public int SkillPoint { get; private set; }
    public float MaxHealthPoint { get; private set; }
    public float MaxStamina { get; private set; }

    //public int Vitality { get; private set; }
    //public int Endurance { get; private set; }
    //public int Strenght { get; private set; }
    //public int Dexterity { get; private set; }
    public Dictionary<PlayerCharacteristicsEnum, int> CharacteristicsDictionary { get; private set; }
    #endregion

    #region Other Variables
    private PlayerData playerData;
    #endregion 
    public PlayerStats(PlayerData playerData, int playerLevel = 0, int skillPoint= 0, int vitality = 0, int endurance = 0, int strenght = 0, int dexterity = 0)
    {
        NextLevelCost = 1000;
        PlayerLevel = playerLevel;
        SkillPoint = skillPoint;
        CharacteristicsDictionary.Add(PlayerCharacteristicsEnum.Strenght, strenght);
        CharacteristicsDictionary.Add(PlayerCharacteristicsEnum.Vitality, vitality);
        CharacteristicsDictionary.Add(PlayerCharacteristicsEnum.Endurance, endurance);
        CharacteristicsDictionary.Add(PlayerCharacteristicsEnum.Dexterity, dexterity);
        this.playerData = playerData; 
    }

    #region Public Func
    public void HPDecrease(float HPlost)
    {
        CurrentHealthPoint -= HPlost;
    }
    public void HPIncrement(float HPheal)
    {
        CurrentHealthPoint += HPheal;
    }
    public void StaminaDecrease(float StaminaLost)
    {
        Stamina -= StaminaLost;
    }
    public void StaminaIncrement(float StaminaRepair)
    {
        Stamina += StaminaRepair;
    }
    public void PlayerLevelUp()
    {
        PlayerLevel ++;
        SkillPoint ++;
        //PlayerLevel += countLevelsUp; //int countLevelsUp
        //SkillPoint += countLevelsUp;
    }
    public void PlayerCharacteristicsUp(Dictionary<PlayerCharacteristicsEnum, int> playerStatsUpDictionary)
    {
        foreach(PlayerCharacteristicsEnum keyCode in playerStatsUpDictionary.Keys)
        {
            CharacteristicsDictionary[keyCode] += playerStatsUpDictionary[keyCode];
        }
        UpdateCharacteristics();
    }
    #endregion

    #region Private Func
    private void UpdateCharacteristics() {
        float defHP = playerData.defaultHealthPoint;
        int ChrstHP = CharacteristicsDictionary[PlayerCharacteristicsEnum.Vitality];

        float defEndurance = playerData.defaultHealthPoint;
        int ChrstEndurance = CharacteristicsDictionary[PlayerCharacteristicsEnum.Vitality];

        MaxHealthPoint = defHP + ChrstHP * playerData.HPGrowth * defHP;
        MaxStamina = defEndurance + ChrstEndurance * playerData.EnduranceGrowth * defEndurance;
    }
    #endregion
}
