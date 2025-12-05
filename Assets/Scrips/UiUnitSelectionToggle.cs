using Unity.VisualScripting;
using UnityEngine;

public class UiUnitSelectionToggle : MonoBehaviour
{

    [SerializeField] GameObject[] elementUIToToggle;


    void Update()
    {
        if (!TurnManager.Instance.isPlayerTurn)
        {
            DeactivateAllUIElements();
            return;
        }

        string unitSelection = UnitSelection.Instance.selectedUnit != null
            ? UnitSelection.Instance.selectedUnit.name
            : null;

        switch (unitSelection)
        {
            case null:
                for (int i = 0; i < elementUIToToggle.Length; i++)
                {
                    elementUIToToggle[i].SetActive(false);
                }
                break;

            case "Chomper":
                DeactivateAllUIElements();
                if (elementUIToToggle.Length > 0)
                    elementUIToToggle[0].SetActive(true);
                break;

            case "Ellen":
                DeactivateAllUIElements();
                if (elementUIToToggle.Length > 1)
                    elementUIToToggle[1].SetActive(true);
                break;

            default:
                break;
        }
    }

    void DeactivateAllUIElements()
    {
        for (int i = 0; i < elementUIToToggle.Length; i++)
        {
            elementUIToToggle[i].SetActive(false);
        }
    }
}

