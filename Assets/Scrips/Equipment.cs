using UnityEngine;

[CreateAssetMenu(fileName = "Equipment", menuName = "ScriptableObjects/Equipment", order = 1)]

public class Equipment : ScriptableObject
{
    [SerializeField] float maxDurability;
    [SerializeField] float currentDurability;
    [SerializeField] float movementSpeed;
    [SerializeField] float armor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentDurability = 100f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
