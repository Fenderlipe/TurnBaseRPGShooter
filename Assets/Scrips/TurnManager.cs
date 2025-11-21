using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurnManager : MonoBehaviour
{

    public static TurnManager Instance;
    public bool isPlayerTurn = true;

    public List<Unit> enemyUnits = new List<Unit>();
    public List<Unit> playerUnits = new List<Unit>();

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        StartPlayerTurn();
    }


    private void StartPlayerTurn()
    {
        isPlayerTurn = true;
        ResetUnits(playerUnits);
        UnitSelection.Instance.enabled = true;

        Debug.Log("Turno del jugador");
    }

    private void StartEnemyTurn()
    {
        isPlayerTurn = false;
        ResetUnits(playerUnits);

        Debug.Log("Turno del enemigo");
    }

    IEnumerator MostrarTurno(TMP_Text textoUI, string mensaje)
    {
        textoUI.text = mensaje;
        textoUI.gameObject.SetActive(true);

        yield return new WaitForSeconds(3f);

        textoUI.gameObject.SetActive(false);
    }

    private void ResetUnits(List<Unit> units)
    {
        foreach (Unit unit in units)
        {
            unit.hasActed = false;

        }
    }

    bool AllUnitsActed(List<Unit> units)
    {
        foreach (var u in units)
        {
            if (!u.hasActed)
            {
                return false;
            }
        }
        return true;
    }

    public void CheckEndTurn()
    {
        if (isPlayerTurn)
        {
            if (AllUnitsActed(playerUnits))
                StartEnemyTurn();
        }

        else
        {
            if (AllUnitsActed(enemyUnits))
                StartPlayerTurn();
        }
    }

    void Update()
    {

    }
}