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

    private void GenerateHitParticles(Vector3 hitPoint)
    {
        // Busca el prefab de partículas en Resources o úsalo si está asignado
        GameObject particlesPrefab = Resources.Load<GameObject>("Sparks");

        if (particlesPrefab != null)
        {
            GameObject particles = Instantiate(particlesPrefab, hitPoint, Quaternion.identity);
            ParticleSystem ps = particles.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                ps.Play();
                // Destruye el objeto después de que terminen las partículas
                Destroy(particles, ps.main.duration + ps.main.startLifetime.constantMax);
            }
        }
        else
        {
            Debug.LogWarning("No se encontró el prefab 'Sparks' en Resources");
        }
    } 
}
