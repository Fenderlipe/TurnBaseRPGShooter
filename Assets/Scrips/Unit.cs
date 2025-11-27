using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] public string characterName;

    public bool hasActed = true;
    public bool hasAttacked = false;
    public bool hasMoved = false;
    [SerializeField] public bool isFriendly;
    ClickToMove clickToMove;
    Shooting shooting;
    GameObject targetSelection;
    PlayerCharacter playerCharacter;

    public void Awake()
    {
        clickToMove = GetComponent<ClickToMove>();
        shooting = GetComponent<Shooting>();
        playerCharacter = GetComponent<PlayerCharacter>();

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
            playerCharacter.targetSelectionPanel.SetActive(true);
            shooting.enabled = true;
            targetSelection.SetActive(true);
        }
        else
        {
            Debug.Log(characterName + " usa la accion atacar");
            FinishAttack();
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
        playerCharacter.targetSelectionPanel.SetActive(false);
        hasAttacked = true;
    }

    public void FinishAction()
    {
        hasActed = true;
        TurnManager.Instance.CheckEndTurn();
    }
}