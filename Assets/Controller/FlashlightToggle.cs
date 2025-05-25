using UnityEngine;
using System.Collections;

public class Flashlight : MonoBehaviour
{
    public Light flashlight;
    public KeyCode toggleKey = KeyCode.F;

    public float baseIntensity = 1f;
    public float flickerIntensity = 0.2f; // Scintillement doux
    public float flickerSpeed = 5f;

    public float minFlickerDelay = 3f;
    public float maxFlickerDelay = 10f;
    public float flickerDipIntensity = 0.2f; // Intensité très faible pendant le bug
    public float flickerDuration = 0.3f;     // Durée du bug

    private bool isOn = false;
    private float nextFlickerTime;
    private bool isFlickering = false;

    void Start()
    {
        if (flashlight == null)
            flashlight = GetComponent<Light>();

        flashlight.enabled = false;
        flashlight.intensity = baseIntensity;
        ScheduleNextFlicker();
    }

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            isOn = !isOn;
            flashlight.enabled = isOn;
        }

        if (isOn)
        {
            FlickerSmooth();

            if (!isFlickering && Time.time >= nextFlickerTime)
            {
                StartCoroutine(RandomFlickerDip());
            }
        }
    }

    void FlickerSmooth()
    {
        float noise = Mathf.PerlinNoise(Time.time * flickerSpeed, 0f);
        flashlight.intensity = baseIntensity + (noise - 0.5f) * flickerIntensity;
    }

    IEnumerator RandomFlickerDip()
    {
        isFlickering = true;

        float originalIntensity = flashlight.intensity;

        // Diminue l’intensité rapidement
        flashlight.intensity = flickerDipIntensity;

        yield return new WaitForSeconds(flickerDuration);

        // Retour à la normale
        flashlight.intensity = originalIntensity;

        ScheduleNextFlicker();
        isFlickering = false;
    }

    void ScheduleNextFlicker()
    {
        nextFlickerTime = Time.time + Random.Range(minFlickerDelay, maxFlickerDelay);
    }
}
