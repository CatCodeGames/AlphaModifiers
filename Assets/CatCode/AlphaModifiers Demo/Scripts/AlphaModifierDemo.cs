using UnityEngine;
using UnityEngine.UI;

namespace CatCode.AlphaModifiers
{
    public class AlphaModifierDemo : MonoBehaviour
    {
        [SerializeField] private MonoAlphaModifier _parentAlpha;
        [SerializeField] private MonoAlphaModifier _childrenAlpha;
        [Space]
        [SerializeField] private Slider _parentSlider;
        [SerializeField] private Slider _childrenSlider;

        private void OnEnable()
        {
            _parentSlider.value = _parentAlpha.Alpha;
            _childrenSlider.value = _childrenAlpha.Alpha;

            _parentSlider.onValueChanged.AddListener(OnParentValueChanged);
            _childrenSlider.onValueChanged.AddListener(OnChildValueChanged);
        }

        private void OnDisable()
        {
            _parentSlider.onValueChanged.RemoveListener(OnParentValueChanged);
            _childrenSlider.onValueChanged.RemoveListener(OnChildValueChanged);
        }

        private void OnParentValueChanged(float value)
            => _parentAlpha.Alpha = value;

        private void OnChildValueChanged(float value)
            => _childrenAlpha.Alpha = value;
    }
}