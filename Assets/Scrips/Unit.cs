using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] string characterName;

    public bool hasActed = true;
    public bool hasAttacked = false;
    bool hasMoved = false;
    [SerializeField] public bool isFriendly;
    ClickToMove clickToMove;
    Shooting shooting;

    public void Awake()
    {
        clickToMove = GetComponent<ClickToMove>();
        shooting = GetComponent<Shooting>();

        clickToMove.enabled = false;
        shooting.enabled = false;
    }


    void Update()
    {

    }

    public void Run()
    {
        if (hasActed || hasMoved)
        {
            return;
        }
        if (isFriendly)
        {
            clickToMove.enabled = true;
        }
        else
        {
            Debug.Log("Unidad enemiga corriendo");
        }
            Debug.Log(characterName + " usa la acción correr");
    }

    public void Attack()
    {
        if (hasActed || hasAttacked)
        {
            return;
        }

        if (isFriendly)
        {
            shooting.enabled = true;
        }
        else
        {
            Debug.Log("se mueve pero en malvado");
        }
    }

    public void PassTurn()
    {
        if (hasActed)
        {
            return;
        }
        Debug.Log(characterName + " salta su turno");
        FinishAction();
    }


    public void StartTurnForThisUnit()
    {
        hasActed = false;
        hasAttacked = false;
        hasMoved = false;
    }

    public void FinishMovement()
    {
        clickToMove.enabled = false;
        hasMoved = true;
    }

    public void FinishAttack()
    {
        hasAttacked = true;
    }

    public void FinishAction()
    {
        hasActed = true;
        TurnManager.Instance.CheckEndTurn();
    }
}