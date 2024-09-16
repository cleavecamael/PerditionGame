using UnityEngine;

public class FeverMeterController : MonoBehaviour
{
    public FeverMeterScore feverMeterScore;
    public FlickerLight2D flickerLight2D;
    public GameConstants gameConstants;

    void Update()
    {
        // Debug.Log(feverMeterScore.CurrentFever);
        if (feverMeterScore.CurrentFever >= gameConstants.maxFever / 5 && feverMeterScore.CurrentFever < 2 * gameConstants.maxFever / 5)
        {
            var emissionModule = GetComponent<ParticleSystem>().emission;
            emissionModule.rateOverTime = 5f;
            flickerLight2D.maxIntensity = 0f;
            flickerLight2D.minIntensity = 0f;
        }
        else if (feverMeterScore.CurrentFever >= 2 * gameConstants.maxFever / 5 && feverMeterScore.CurrentFever < 3 * gameConstants.maxFever / 5)
        {
            var emissionModule = GetComponent<ParticleSystem>().emission;
            emissionModule.rateOverTime = 10f;
            flickerLight2D.maxIntensity = 0f;
            flickerLight2D.minIntensity = 0f;
        }
        else if (feverMeterScore.CurrentFever >= 3 * gameConstants.maxFever / 5 && feverMeterScore.CurrentFever < 4 * gameConstants.maxFever / 5)
        {
            var emissionModule = GetComponent<ParticleSystem>().emission;
            emissionModule.rateOverTime = 20f;
            flickerLight2D.maxIntensity = 2f;
            flickerLight2D.minIntensity = 1f;
        }
        else if (feverMeterScore.CurrentFever >= 4 * gameConstants.maxFever / 5)
        {
            var emissionModule = GetComponent<ParticleSystem>().emission;
            emissionModule.rateOverTime = 50f;
            flickerLight2D.maxIntensity = 6f;
            flickerLight2D.minIntensity = 1f;
        }
        else
        {
            var emissionModule = GetComponent<ParticleSystem>().emission;
            emissionModule.rateOverTime = 0f;
            flickerLight2D.maxIntensity = 0f;
            flickerLight2D.minIntensity = 0f;
        }

    }
}