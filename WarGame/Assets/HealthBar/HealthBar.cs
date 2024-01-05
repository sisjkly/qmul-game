using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image Bar;
    public float maxHealth = 100;
    public float healthChangeAmount = 10f; // Blood change value, configurable as required
    public float lerpSpeed = 3;            // Speed of smooth change in blood bars

    private float _health;
    public float Health
    {
        get { return _health; }
        set
        {
            _health = Mathf.Clamp(value, 0, maxHealth); // Make sure the blood level is between 0 and maxHealth.
            // Note: We no longer call BarFiller() directly here.
        }
    }

    private void Start()
    {
        Health = maxHealth;
    }

    private void Update()
    {
        // BarFiller();
    }

// <<<<<<< Updated upstream
//     // private void BarFiller()
//     // {
//     //     // 使用 Mathf.Lerp 平滑地更新血条值
//     //     Bar.fillAmount = Mathf.Lerp(Bar.fillAmount, Health / maxHealth, lerpSpeed * Time.deltaTime);
//     // }
// =======
//     private void BarFiller()
//     {
//         // Smoothly update blood bar values using Mathf.Lerp
//         Bar.fillAmount = Mathf.Lerp(Bar.fillAmount, Health / maxHealth, lerpSpeed * Time.deltaTime);
//     }
// >>>>>>> Stashed changes

    public void AddHealth()
    {
        Health += healthChangeAmount;
    }

    public void ReduceHealth()
    {
        Health -= healthChangeAmount;
    }
}
