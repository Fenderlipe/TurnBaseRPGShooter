using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance;
    public bool isPlayerTurn = true;

    public List<Unit> enemyUnits = new List<Unit>();
    public List<Unit> playerUnits = new List<Unit>();

    public TMP_Text turnoAliado, turnoEnemigo;

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
        StartCoroutine(MostrarTurno(turnoAliado, "Turno de los aliados"));
    }

    private void StartEnemyTurn()
    {
        isPlayerTurn = false;
        ResetUnits(enemyUnits);
        StartCoroutine(MostrarTurno(turnoEnemigo, "turno de los enemigos"));

        foreach (Unit u in enemyUnits)
        {
            // Solo activar AI de unidades vivas
            Character character = u.GetComponent<Character>();
            if (character != null && !character.IsAlive())
            {
                u.hasActed = true; // Marcar como actuado si está muerto
                continue;
            }

            EnemyAI ai = u.GetComponent<EnemyAI>();
            if (ai != null)
                ai.enabled = true;
        }
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
            // Solo resetear unidades vivas
            Character character = unit.GetComponent<Character>();
            if (character != null && character.IsAlive())
            {
                unit.StartTurnForThisUnit();
            }
            else
            {
                // Las unidades muertas ya han actuado
                unit.hasActed = true;
                unit.hasAttacked = true;
                unit.hasMoved = true;
            }
        }
    }

    bool AllUnitsActed(List<Unit> units)
    {
        foreach (var u in units)
        {
            // Verificar si la unidad está viva
            Character character = u.GetComponent<Character>();

            // Si está muerta, considerarla como que ya actuó
            if (character != null && !character.IsAlive())
            {
                continue; // Ignorar unidades muertas
            }

            // Si está viva y no ha actuado, el turno no ha terminado
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
                StartCoroutine(ChangeTurnAfterDelay(false));
        }
        else
        {
            if (AllUnitsActed(enemyUnits))
                StartCoroutine(ChangeTurnAfterDelay(true));
        }
    }

    private IEnumerator ChangeTurnAfterDelay(bool toPlayerTurn)
    {
        yield return new WaitForSeconds(1f);

        if (toPlayerTurn)
        {
            StartPlayerTurn();
        }
        else
        {
            StartEnemyTurn();
        }
    }

    void Update()
    {

    }
}