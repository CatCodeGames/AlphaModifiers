using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CatCode.AlphaModifiers
{
    public class AlphaModifierExample : MonoBehaviour
    {
        [SerializeField] private MonoAlphaModifier _parentAlpha;
        [SerializeField] private MonoAlphaModifier _childAlpha;
        [Space]
        [SerializeField] private Slider _parentSlider;
        [SerializeField] private Slider _childSlider;

        private void OnEnable()
        {
            _parentSlider.onValueChanged.AddListener(OnParentValueChanged);
            _childSlider.onValueChanged.AddListener(OnChildValueChanged);

            OnParentValueChanged(_parentSlider.value);
            OnChildValueChanged(_childSlider.value);
        }

        private void OnDisable()
        {
            _parentSlider.onValueChanged.RemoveListener(OnParentValueChanged);
            _childSlider.onValueChanged.RemoveListener(OnChildValueChanged);
        }

        private void OnParentValueChanged(float value)
            => _parentAlpha.Alpha = value;

        private void OnChildValueChanged(float value)
            => _childAlpha.Alpha = value;
    }
}