using Unity.VisualScripting;
using UnityEngine;

public class UiUnitSelectionToggle : MonoBehaviour
{
    [SerializeField] GameObject[] elementsUIToToggle;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (UnitSelection.Instance.selectedUnit == null)
        {
            deactivateALLUI();
            return;
        }

        var selectedUnit = UnitSelection.Instance.selectedUnit;
        deactivateALLUI();
       

        switch (selectedUnit.name)
        {
            case null:
                for (int i = 0; i < elementsUIToToggle.Length; i++)
                {
                    elementsUIToToggle[i].SetActive(false);
                    deactivateALLUI();
                }
                break;
            case "Ellen":
                if (elementsUIToToggle.Length > 0)
                    elementsUIToToggle[0].SetActive(true);
                break;

            case "Chomper":
                if (elementsUIToToggle.Length > 1)
                    elementsUIToToggle[1].SetActive(true);
                break;

            default:
                break;
        }
    }

    void deactivateALLUI()
    {
        for (int i = 0; i < elementsUIToToggle.Length; i++)
        {
            elementsUIToToggle[i].SetActive(false);
        }

    }
}

