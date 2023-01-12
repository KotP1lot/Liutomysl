using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class Chest : MonoBehaviour, IInteractable
{
    enum Upgrade { None, HP, SP, Damage, AtkSpd }

    [SerializeField] private string _prompt;
    [SerializeField] private Animator _animator;
    [SerializeField] private Collider2D _chestCollider;
    [SerializeField] private Upgrade _givesUpgrade = 0;
    private string message;
    public string InteractedMessage => message;
    public TextAlignmentOptions MessageAlignment => TextAlignmentOptions.Center;
    public string InteractionPrompt => _prompt;

    public bool Interact(Interactor interactor)
    {
        var player = interactor.GetComponent<Player>();
        if (player == null) return false;

        switch (_givesUpgrade)
        {
            case Upgrade.HP:
                {
                    message = "�� ������� ���������� ������'�!\n ����������� ������� ������'� ��������!";
                    player.UpgradeHP();
                    break;
                }
            case Upgrade.SP:
                {
                    message = "�� ������� ���������� �����������!\n ����������� ������� ����������� ��������!";
                    player.UpgradeSP();

                    break;
                }
            case Upgrade.Damage:
                {
                    message = "�� ������� ���������� ����!\n ����� ��� �� ������� ��������!";
                    player.UpgradeDamage();
                    break;
                }
            case Upgrade.AtkSpd:
                {
                    message = "�� ������� ���������� ��������!\n �������� ����� ��������!";
                    player.UpgradeAtkSpd();
                    break;
                }
        }
        player.UI.ItemFound();

        _animator.SetTrigger("open_chest");
        _chestCollider.enabled = false;

        return true;
    }
}
