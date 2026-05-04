using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    [Header("Ajustes de Movimiento")]
    public float scrollSpeedX = 0.05f;
    public float scrollSpeedY = 0.1f;
    
    private Material mat;

    void Start()
    {
        mat = GetComponent<MeshRenderer>().material;
    }

    void Update()
    {
        float offsetX = Time.time * scrollSpeedX;
        float offsetY = Time.time * scrollSpeedY;

        mat.mainTextureOffset = new Vector2(offsetX, offsetY);
    }
}