using System;
using Unity.VisualScripting;
using UnityEngine;

public class UnitSelection : MonoBehaviour
{
    public static UnitSelection Instance;
    public Unit selectedUnit;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        if (TurnManager.Instance.isPlayerTurn)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100f))
            {
                Unit unit = hit.collider.GetComponent<Unit>();
                if (unit != null && unit.isFriendly && !unit.hasActed)
                {
                    SelectUnit(unit);
                }
                else
                {
                    Debug.Log("No soy una unidad");
                }
            }
        }
    }

    private void SelectUnit(Unit unit)
    {
        selectedUnit = unit;
    }

    public void ClearSelection()
    {
        if(selectedUnit != null)
        {
            selectedUnit = null;
        }
    }
}