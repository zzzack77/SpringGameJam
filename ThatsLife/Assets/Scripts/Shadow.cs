using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Shadow : MonoBehaviour
{
    public Vector3 shadowOffset = Vector3.zero;
    public Vector3 shadowScale = new Vector3(1, 0.5f, 1);
    public Vector3 rotation = new Vector3(0, 0, 5);
    public Material material;

    GameObject shadow;
    GameObject pivot;

    void Start()
    {
        // Create pivot at base of plant
        pivot = new GameObject("ShadowPivot");
        pivot.transform.parent = transform;
        pivot.transform.localPosition = Vector3.zero;

        // Create shadow as child of pivot
        shadow = new GameObject("Shadow");
        shadow.transform.parent = pivot.transform;
        shadow.transform.localPosition = shadowOffset;

        // Copy sprite
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        SpriteRenderer sr = shadow.AddComponent<SpriteRenderer>();
        sr.sprite = renderer.sprite;
        sr.material = material;

        sr.sortingLayerName = renderer.sortingLayerName;
        sr.sortingOrder = renderer.sortingOrder - 1;

        
    }

    void LateUpdate()
    {
        shadow.transform.localPosition = shadowOffset;
        pivot.transform.localPosition = -shadowOffset;

        shadow.transform.localScale = shadowScale;

        // Rotate pivot instead of shadow
        pivot.transform.localRotation = Quaternion.Euler(rotation);
    }
}
