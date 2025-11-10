using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] string characterName;

    public bool hasActed = true;
    void Start()
    {
        hasActed = true;
    }


    void Update()
    {

    }

    public void Run()
    {
        if (hasActed)
        {
            return;
        }
        Debug.Log(characterName + " usa la acción correr");
        FinishAction();
    }

    public void Attack()
    {
        if (hasActed)
        {
            return;
        }
        Debug.Log(characterName + " usa la acción Atacar");
        FinishAction();
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
    }

    public void FinishAction()
    {
        hasActed = true;
        TurnManager.Instance.CheckEndTurn();
    }
}