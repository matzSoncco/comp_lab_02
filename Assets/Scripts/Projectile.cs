using UnityEngine;

public class Projectile : MonoBehaviour
{
    [HideInInspector] public bool isLaser = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            TurretController turret = GameObject.FindGameObjectWithTag("Player").GetComponent<TurretController>();
            if (turret != null)
            {
                turret.AddScore(10); 
            }

            Destroy(other.gameObject);

            if (!isLaser)
            {
                Destroy(gameObject);
            }
        }
    }
}