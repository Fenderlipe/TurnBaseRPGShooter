using UnityEngine;

[CreateAssetMenu(fileName = "Equipment", menuName = "ScriptableObjects/Equipment", order = 2)]

public class Equipment : ScriptableObject
{
    [SerializeField] float maxDurability;
    [SerializeField] float currentDurability;
    [SerializeField] float movementSpeed;
    [SerializeField] float Armor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
