using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Shadow : MonoBehaviour
{
    private TimeManager timeManager;
    private GrowPlant growPlant;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Material material;


    [Header("Sunrise")]
    [SerializeField] private Vector3 shadowOffset_Sunrise = new Vector3(0.16f, 0.4f, 0f);
    [SerializeField] private Vector3 shadowScale_Sunrise = new Vector3(-1f, 1.65f, 1f);
    [SerializeField] private Vector3 shadowRotation_Sunrise = new Vector3(0, 0, 90);

    [Header("Midday")]
    [SerializeField] private Vector3 shadowOffset_Midday = new Vector3(0f, 0.26f, 0f);
    [SerializeField] private Vector3 shadowScale_Midday = new Vector3(-1f, 0.8f, 1f);
    [SerializeField] private Vector3 shadowRotation_Midday = new Vector3(0, 0, 180);

    [Header("Sunset")]
    [SerializeField] private Vector3 shadowOffset_Sunset = new Vector3(-0.16f, 0.4f, 0f);
    [SerializeField] private Vector3 shadowScale_Sunset = new Vector3(-1f, 1.65f, 1f);
    [SerializeField] private Vector3 shadowRotation_Sunset = new Vector3(0, 0, 270);

    [Header("Current Transform")]
    [SerializeField] private Vector3 shadowOffset;
    [SerializeField] private Vector3 shadowScale;
    [SerializeField] private Vector3 rotation;

    private GameObject shadow;
    private GameObject pivot;

    [Header("Time")]
    [SerializeField] private float dayDuration = 60f;
    [SerializeField] private float timer = 0f;


    void Start()
    {
        timeManager = FindAnyObjectByType<TimeManager>();
        growPlant = GetComponent<GrowPlant>();


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
        spriteRenderer = shadow.AddComponent<SpriteRenderer>();
        if (growPlant != null && growPlant.StageIndex != 0) { spriteRenderer.sprite = renderer.sprite; }
        spriteRenderer.material = material;

        spriteRenderer.sortingLayerName = renderer.sortingLayerName;
        spriteRenderer.sortingOrder = renderer.sortingOrder - 1;
    }

    void LateUpdate()
    {
        if (growPlant != null && growPlant.StageIndex == 0) { return; }
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer.sprite != renderer.sprite)
        {
            spriteRenderer.sprite = renderer.sprite;
        }


        timer += Time.deltaTime;

        // Loop the day
        if (timer > dayDuration)
            timer = 0f;

        float t = timer / dayDuration;

        Vector3 currentOffset;
        Vector3 currentScale;
        Vector3 currentRotation;

        if (t < 0.5f)
        {
            // Sunrise -> Midday
            float lerpT = t / 0.5f;

            currentOffset = Vector3.Lerp(shadowOffset_Sunrise, shadowOffset_Midday, lerpT);
            currentScale = Vector3.Lerp(shadowScale_Sunrise, shadowScale_Midday, lerpT);
            currentRotation = Vector3.Lerp(shadowRotation_Sunrise, shadowRotation_Midday, lerpT);
        }
        else
        {
            // Midday -> Sunset
            float lerpT = (t - 0.5f) / 0.5f;

            currentOffset = Vector3.Lerp(shadowOffset_Midday, shadowOffset_Sunset, lerpT);
            currentScale = Vector3.Lerp(shadowScale_Midday, shadowScale_Sunset, lerpT);
            currentRotation = Vector3.Lerp(shadowRotation_Midday, shadowRotation_Sunset, lerpT);
        }

        // Apply values
        shadowOffset = currentOffset;
        shadowScale = currentScale;
        rotation = currentRotation;

        shadow.transform.localPosition = shadowOffset;
        pivot.transform.localPosition = -shadowOffset;

        shadow.transform.localScale = shadowScale;
        pivot.transform.localRotation = Quaternion.Euler(rotation);
    }
}
