using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KaijuGripIndicatorUI : MonoBehaviour
{
    public KaijuGripPhaseBehavior gripPhaseTarget;
    public TextMeshProUGUI stateTextLabel;
    public Transform meterPointerPivot;
    //public Slider gripStrengthSlider;

    public Vector2 meterRotationRange = new Vector2(0, -250f);

    private void Update()
    {
        if(gripPhaseTarget != null) 
        {
            //gripStrengthSlider.value = gripPhaseTarget.GetGripPercentage();
            float percentage = gripPhaseTarget.GetGripPercentage();
            float targetRotation = Mathf.Lerp(meterRotationRange.x, meterRotationRange.y, percentage);
            meterPointerPivot.localEulerAngles = new Vector3(0,0,targetRotation);

            stateTextLabel.text = gripPhaseTarget.phase.ToString();
        }
    }
}
