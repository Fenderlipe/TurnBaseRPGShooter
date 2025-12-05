using UnityEngine;
using TMPro;

public class HealthUI : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private Character character;
    [SerializeField] private TMP_Text healthText;

    void Start()
    {
        UpdateHealthUI();
    }

    void Update()
    {
        UpdateHealthUI();
    }

    void UpdateHealthUI()
    {
        if (character != null && healthText != null)
        {
            healthText.text = "Vida: " + character.GetCurrentLife() + " / " + character.GetMaxLife();
        }
    }
}