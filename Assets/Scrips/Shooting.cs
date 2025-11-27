using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField]ParticleSystem particleSparks;


    public void Shoot(Vector3 enemyPosition, float weaponRange)
    {
        if (isOnLoS(enemyPosition, weaponRange))
        {
            particleSparks.Play();
            Debug.Log("Enemigo en linea de tiro");
        }
        else
        {
            Debug.Log("Enemigo no esta en linea de tiro");
        }
    }
    public bool isOnLoS(Vector3  enemyPosition, float weaponRange)
    {
        
        RaycastHit hit;

        if (Physics.Raycast(transform.position, enemyPosition, out hit, weaponRange))
        {
            Character character = hit.collider.GetComponent<Character>();

            if (character != null)
            {
                return true;
            }
        }
        return false;
    }
}
