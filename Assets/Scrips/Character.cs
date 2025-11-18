using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("character name")]
    [SerializeField] string name;
    public int level = 1;
    float currentLife;
    [Header("character Stats")]
    [SerializeField] float maxLife;
    [SerializeField] float baseAttackDamage;
    [SerializeField] float armorValue;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        name = gameObject.name;
        currentLife = maxLife;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void TakeDamage(float damage)
    {
        float finalDamage = damage - armorValue;
        currentLife -= finalDamage;
        IsAlive();
    }

    void IsAlive()
    {
        if (currentLife <= 0)
        {
            Debug.Log(name + " Ha muerto");
            Destroy(gameObject);
        }
    }

}