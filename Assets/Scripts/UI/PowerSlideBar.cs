using UnityEngine;
using UnityEngine.UI;

public class PowerSlideBar : MonoBehaviour
{
    [SerializeField] private Slider powerSlider;

    private void Start()
    {
        if (powerSlider == null)
        {
            Debug.LogError("Power Slider is not assigned!");
            return;
        }

        // Set the slider's range (36 to 64)
        powerSlider.minValue = 36f;
        powerSlider.maxValue = 64f;
    }

    /// <summary>
    /// Sets the current power level.
    /// The input value should be between 0 and 100, which is mapped to the slider's range (36 to 64).
    /// </summary>
    /// <param name="power">A float between 0 and 100.</param>
    public void SetPower(float power)
    {
        if (power < 0f || power > 100f)
        {
            Debug.LogWarning("Power value is out of range (0-100). Clamping to valid range.");
            power = Mathf.Clamp(power, 0f, 100f);
        }

        // Map 0-100 to 36-64 linearly
        float mappedValue = Mathf.Lerp(46.2f, 53.8f, power / 100f);
        powerSlider.value = mappedValue;
    }
}