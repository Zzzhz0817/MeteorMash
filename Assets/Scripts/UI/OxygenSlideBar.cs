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

        oxygenSlider.minValue = 0;
        oxygenSlider.maxValue = 100f;
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

        // Map linearly
        float mappedValue = Mathf.Lerp(62f, 88f, oxygen / 100f);
        oxygenSlider.value = mappedValue;
    }
}
