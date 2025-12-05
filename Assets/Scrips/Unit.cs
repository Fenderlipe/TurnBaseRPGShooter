using UnityEngine;
using TMPro;
using System.Collections;

public class Unit : MonoBehaviour
{
    [SerializeField] public string characterName;

    public bool hasActed = false;
    public bool hasAttacked = false;
    public bool hasMoved = false;
    [SerializeField] public bool isFriendly;
    ClickToMove clickToMove;
    Shooting shooting;
    [SerializeField] GameObject targetSelection;
    PlayerCharacter playerCharacter;


    public TMP_Text endTurn;

    private void Awake()
    {
        clickToMove = GetComponent<ClickToMove>();
        shooting = GetComponent<Shooting>();
        playerCharacter = GetComponent<PlayerCharacter>();

        clickToMove.enabled = false;
        shooting.enabled = false;
    }
    public void StartTurnForThisUnit()
    {
        hasActed = false;
        hasAttacked = false;
        hasMoved = false;
    }


    public void Run()
    {
        if (hasActed || hasMoved)
        {
            return;
        }

        if (isFriendly && !hasActed && !hasMoved)
        {
            clickToMove.enabled = true;
        }
        else
        {
            Debug.Log("se mueve pero en malvado");
        }
        StartCoroutine(MostrarAccion(endTurn, characterName + " usa la acción de correr"));
        Debug.Log(characterName + " usa la accion correr");
        FinishAction();
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
            targetSelection.SetActive(true);
            shooting.enabled = true;

        }
        else
        {
            Debug.Log("Ataca pero en malvado");
        }
        StartCoroutine(MostrarAccion(endTurn, characterName + " usa la acción de atacar"));

        Debug.Log(characterName + " usa la accion atacar");
        //FinishAttack();
    }

    public void PassTurn()
    {
        if (hasActed)
        {
            return;
        }
        StartCoroutine(MostrarAccion(endTurn, characterName + " finaliza el turno"));

        Debug.Log(characterName + " pasa su turno");
        FinishAction();
    }

    IEnumerator MostrarAccion(TMP_Text textoUI, string mensaje)
    {
        textoUI.text = mensaje;
        textoUI.gameObject.SetActive(true);

        yield return new WaitForSeconds(3f);

        textoUI.gameObject.SetActive(false);

    }


    public void FinishMovement()
    {

        clickToMove.enabled = false;
        hasMoved = true;

        if (!isFriendly)
        {
            hasMoved = true;
            return;
            //FinishAction();
        }

    }

    public void FinishAttack()
    {

        if (isFriendly)
        {
            playerCharacter.targetSelectionPanel.SetActive(false);
        }

        hasAttacked = true;

        FinishAction();

        /*if (!isFriendly)
        {
            FinishAction();
        }*/
    }



    public void FinishAction()
    {
        hasActed = true;
        TurnManager.Instance.CheckEndTurn();
    }
}