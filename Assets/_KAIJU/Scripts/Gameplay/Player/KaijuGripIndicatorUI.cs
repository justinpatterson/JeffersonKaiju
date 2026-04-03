using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KaijuGripIndicatorUI : MonoBehaviour
{
    public KaijuGripPhaseBehavior gripPhaseTarget;
    public TextMeshProUGUI stateTextLabel;
    public Slider gripStrengthSlider;

    private void Update()
    {
        if(gripPhaseTarget != null) 
        {
            gripStrengthSlider.value = gripPhaseTarget.GetGripPercentage();
            stateTextLabel.text = gripPhaseTarget.phase.ToString();
        }
    }
}
