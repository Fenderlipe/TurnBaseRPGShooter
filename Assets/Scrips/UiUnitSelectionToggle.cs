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
        if (UnitSelection.Instance == null)
            return;

        var unitSelected = UnitSelection.Instance.selectedUnit.name;
        deactivateALLUI();

        if (selectedUnit == null)
            return;


        switch (unitSelected)
        {
            case "Ellen":
                if (elementsUIToToggle.Length > 0)
                    elementsUIToToggle[0].SetActive(true);
                break;

            case "Chomper":
                if (elementsUIToToggle.Length > 1)
                    elementsUIToToggle[0].SetActive(true);
                break;

            default:
                break;
        }
    }

    void deactivateALLUI()
    {

    }
}

