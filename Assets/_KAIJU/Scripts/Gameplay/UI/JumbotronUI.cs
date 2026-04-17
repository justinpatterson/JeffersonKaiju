using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class JumbotronUI : MonoBehaviour
{
    public TextMeshProUGUI cityHealthText;
    public Slider prMeter;
    public GameplayData gameDataReference;

    private void LateUpdate()
    {
        cityHealthText.text = "City Health: " + gameDataReference.CityHealth.ToString("000") + "%";
        prMeter.value = (gameDataReference.CityHealth*1f) / 100f;
    }
}
