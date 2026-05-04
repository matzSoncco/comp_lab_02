using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public PowerUpType type;
    public float timeToHit = 3f;
    public float effectDuration = 5f;

    void Start()
    {
        Destroy(gameObject, timeToHit);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            TurretController player = GameObject.FindGameObjectWithTag("Player").GetComponent<TurretController>();
            if (player != null)
            {
                player.ActivatePowerUp(type, effectDuration);
            }
            
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}