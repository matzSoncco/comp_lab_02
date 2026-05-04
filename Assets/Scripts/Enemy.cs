using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    private Transform target;
    private float rotationSpeed;

    void Start() 
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null) 
        {
            target = playerObject.transform;
        }
        else 
        {
            Debug.LogError("Error en la etiqueta del objeto");
        }

        rotationSpeed = Random.Range(-100f, 100f);
    }

    void Update()
    {
        if (target == null) return;

        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }

    void Die()
    {
        TurretController turret = GameObject.FindGameObjectWithTag("Player").GetComponent<TurretController>();
        if (turret != null) turret.AddScore(10);

        Destroy(gameObject);
    }
}