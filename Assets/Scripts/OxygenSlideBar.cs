using UnityEngine;
using UnityEngine.UI;

public class OxygenSlideBar : MonoBehaviour
{
    [SerializeField] private Slider oxygenSlider;

    private void Start()
    {
        if (oxygenSlider == null)
        {
            Debug.LogError("Oxygen Slider is not assigned!");
            return;
        }

        // Set the slider's range (62 to 88)
        oxygenSlider.minValue = 62f;
        oxygenSlider.maxValue = 88f;
    }

    /// <summary>
    /// Sets the current oxygen amount.
    /// The input value should be between 0 and 100, which is mapped to the slider's range (62 to 88).
    /// </summary>
    /// <param name="oxygen">A float between 0 and 100.</param>
    public void SetOxygen(float oxygen)
    {
        if (oxygen < 0f || oxygen > 100f)
        {
            Debug.LogWarning("Oxygen value is out of range (0-100). Clamping to valid range.");
            oxygen = Mathf.Clamp(oxygen, 0f, 100f);
        }

        // Map 0-100 to 62-88 linearly
        float mappedValue = Mathf.Lerp(78.12f, 84.88f, oxygen / 100f);
        oxygenSlider.value = mappedValue;
    }
}
