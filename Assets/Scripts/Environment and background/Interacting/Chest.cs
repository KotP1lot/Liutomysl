using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class Chest : MonoBehaviour, IInteractable, IDataPersistence
{
    enum Upgrade { None, HP, SP, Damage, AtkSpd }

    [SerializeField] private string _prompt;
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioClip _clip;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private Collider2D _chestCollider;
    [SerializeField] private Upgrade _givesUpgrade = 0;
    private string message;
    public string InteractedMessage => message;
    public TextAlignmentOptions MessageAlignment => TextAlignmentOptions.Center;
    public string InteractionPrompt => _prompt;

    [SerializeField] private string id;
    private bool opened;

    [ContextMenu("Generate guid for id")]

    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }
    public bool Interact(Interactor interactor)
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
        _audioSource.PlayOneShot(_clip);
        _animator.SetTrigger("open_chest");
        _chestCollider.enabled = false;

        opened = true;
        DataPersistenceManager.instance.SaveGame();

        return true;
    }

    public void LoadData(GameData data)
    {
        data.objectsInteracted.TryGetValue(id, out opened);
        if (opened)
        {
            _animator.SetTrigger("open_chest");
            _chestCollider.enabled = false;
        }
    }

    public void SaveData(ref GameData data)
    {
        if (data.objectsInteracted.ContainsKey(id))
        {
            data.objectsInteracted.Remove(id);
        }
        data.objectsInteracted.Add(id, opened);
    }
}
