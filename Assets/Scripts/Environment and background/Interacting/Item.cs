using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Item : MonoBehaviour, IInteractable, IDataPersistence
{
    enum Keys { None, Prison, Lift, Mason, Boss }
    enum Upgrade { None, HP, SP, Damage, AtkSpd }

    [SerializeField] private string _prompt;
    [SerializeField] private Keys _givesKey=Keys.None;
    [SerializeField] private Upgrade _givesUpgrade=Upgrade.None;
    private string message;
    public string InteractedMessage => message;
    public TextAlignmentOptions MessageAlignment => TextAlignmentOptions.Center;
    public string InteractionPrompt => _prompt;

    [SerializeField] private string id;
    private bool collected;

    [ContextMenu("Generate guid for id")]

    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    public bool Interact(Interactor interactor)
    {
        if (_givesUpgrade != Upgrade.None)
        {
            var player = interactor.GetComponent<Player>();
            if (player == null) return false;

            switch (_givesUpgrade)
            {
                case Upgrade.HP:
                    {
                        message = "Ви знайшли покращення Здоров'я!\n Максимальну кількість здоров'я збільшено!";
                        player.UpgradeHP();
                        break;
                    }
                case Upgrade.SP:
                    {
                        message = "Ви знайшли покращення Витривалості!\n Максимальну кількість витривалості збільшено!";
                        player.UpgradeSP();

                        break;
                    }
                case Upgrade.Damage:
                    {
                        message = "Ви знайшли покращення Сили!\n Шкоду яку ви завдаєте збільшено!";
                        player.UpgradeDamage();
                        break;
                    }
                case Upgrade.AtkSpd:
                    {
                        message = "Ви знайшли покращення Швидкості!\n Швидкість атаки збільшено!";
                        player.UpgradeAtkSpd();
                        break;
                    }
            }
            player.UI.ItemFound();
            collected = true;
            DataPersistenceManager.instance.SaveGame();
        }
        if (_givesKey != Keys.None)
        {
            var keys = interactor.GetComponent<KeyInventory>();
            if (keys == null) return false;
    
            keys.acquireKey(_givesKey.ToString());
            message = "Отримано " + keys.getFullKeyName(_givesKey.ToString());

            collected = true;
            DataPersistenceManager.instance.SaveGame();
        }
        Destroy(gameObject);
        return true;
    }

    public void LoadData(GameData data)
    {
        data.objectsInteracted.TryGetValue(id, out collected);
        if (collected)
        {
            gameObject.SetActive(false);
        }
    }

    public void SaveData(ref GameData data)
    {
        if (data.objectsInteracted.ContainsKey(id))
        {
            data.objectsInteracted.Remove(id);
        }
        data.objectsInteracted.Add(id, collected);
    }
}
