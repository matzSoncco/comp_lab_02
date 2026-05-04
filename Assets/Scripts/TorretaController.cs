using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class TurretController : MonoBehaviour
{
    [Header("Movimiento")]
    public float rotationSpeed = 600f;

    [Header("Configuracion de Disparo")]
    public GameObject projectilePrefab;
    public Transform firePoint;

    [Header("Sistema de Puntos")] 
    public int score = 0;
    public TextMeshProUGUI scoreText; 

    [Header("Estado de Power-Ups")]
    private PowerUpType currentPower;
    private bool isPowerUpActive = false;
    private float powerTimer = 0f;

    [Header("UI Game Over")]
    public GameObject gameOverPanel;

    [Header("UI Vidas")]
    public GameObject heartPrefab;
    public Transform healthParent;
    private List<GameObject> hearts = new List<GameObject>();
    private int vidas = 3;

    void Start()
    {
        ActualizarInterfaz();
    }

    public void ActualizarVidasVisual(int vidasActuales)
    {
        foreach (GameObject h in hearts) Destroy(h);
        hearts.Clear();

        for (int i = 0; i < vidasActuales; i++)
        {
            GameObject newHeart = Instantiate(heartPrefab, healthParent);
            hearts.Add(newHeart);
        }
    }
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = (mousePos - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
        
        if (isPowerUpActive)
        {
            powerTimer -= Time.deltaTime;
            if (powerTimer <= 0) isPowerUpActive = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            HandleShooting();
        }
    }

    void HandleShooting()
    {
        if (isPowerUpActive && currentPower == PowerUpType.MultiShot)
        {
            CreateProjectile(0f);
            CreateProjectile(15f);
            CreateProjectile(-15f);
        }
        else
        {
            CreateProjectile(0f);
        }
    }

    void CreateProjectile(float angleOffset)
    {
        if (projectilePrefab == null || firePoint == null) return;

        Quaternion rotation = firePoint.rotation * Quaternion.Euler(0, 0, angleOffset);
        GameObject bullet = Instantiate(projectilePrefab, firePoint.position, rotation);
        
        Projectile bulletScript = bullet.GetComponent<Projectile>();
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        float finalForce = 20f;

        if (isPowerUpActive)
        {
            switch (currentPower)
            {
                case PowerUpType.MegaShot:
                    bullet.transform.localScale = new Vector3(3f, 3f, 1f);
                    break;
                case PowerUpType.Laser:
                    finalForce = 80f;
                    if (bulletScript != null) bulletScript.isLaser = true;

                    bullet.GetComponent<SpriteRenderer>().color = Color.cyan;

                    bullet.transform.localScale = new Vector3(0.3f, 5f, 1f); 
                    break;
            }
        }

        if (rb != null)
        {
            rb.AddForce(bullet.transform.up * finalForce, ForceMode2D.Impulse);
        }

        Destroy(bullet, 3f);
    }

    public void ActivatePowerUp(PowerUpType type, float duration)
    {
        if (type == PowerUpType.Repair)
        {
            vidas = Mathf.Min(vidas + 1, 3);
            ActualizarInterfaz();
        }
        else
        {
            currentPower = type;
            isPowerUpActive = true;
            powerTimer = duration;
        }
        Debug.Log("Power-Up: " + type);
    }

    public void TakeDamage(int danio)
    {
        if (vidas <= 0) return;

        vidas -= danio;
        
        if (vidas < 0) vidas = 0;
        
        ActualizarInterfaz();

        if (vidas <= 0)
        {
            Morir();
        }
    }

    public void AddScore(int puntos)
    {
        score += puntos;
        ActualizarInterfaz();
    }

    void ActualizarInterfaz()
    {
        vidas = Mathf.Clamp(vidas, 0, 3);
        if (scoreText != null) scoreText.text = "PUNTOS: " + score;

        ActualizarVidasVisual(vidas);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            TakeDamage(1);
            Destroy(other.gameObject); 
        }
    }

    void Morir()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void ReiniciarJuego()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

public enum PowerUpType { MegaShot, Laser, Repair, MultiShot }