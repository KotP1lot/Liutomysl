using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Item : MonoBehaviour, IInteractable
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
        }
        if (_givesKey != Keys.None)
        {
            var keys = interactor.GetComponent<KeyInventory>();
            if (keys == null) return false;

            keys.acquireKey(_givesKey.ToString());
            message = "Отримано " + keys.getFullKeyName(_givesKey.ToString());
        }
        Destroy(gameObject);
        return true;
    }
}
