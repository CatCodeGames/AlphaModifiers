using System;
using System.Collections.Generic;
using UnityEngine;

namespace CatCode.AlphaModifiers
{
    [Serializable]
    public sealed class ParticleSystemAlphaStrategy : IAlphaModifierStrategy
    {
        private struct Data
        {
            public GradientAlphaKey[] minAlphaKeys;
            public GradientAlphaKey[] maxAlphaKeys;
            public ParticleSystemGradientMode mode;
            public Gradient gradientMin;
            public Gradient gradientMax;
        }

        private bool _initialized;
        private int _count;
        private Data[] _data;

        [SerializeField] private List<ParticleSystem> _targets = new();

        public ParticleSystemAlphaStrategy() { }

        public ParticleSystemAlphaStrategy(List<ParticleSystem> targets)
        {
            _targets = targets;
        }

        private void Initialize()
        {
            if (_initialized)
                return;
            _initialized = true;

            _count = _targets.Count;
            _data = new Data[_count];

            for (int i = 0; i < _count; i++)
            {
                var module = _targets[i].colorOverLifetime;
                var minMaxGradient = module.color;

                _data[i] = new Data()
                {
                    minAlphaKeys = minMaxGradient.gradientMin?.alphaKeys,
                    maxAlphaKeys = minMaxGradient.gradientMax.alphaKeys,
                    mode = minMaxGradient.mode,
                    gradientMin = minMaxGradient.gradientMin,
                    gradientMax = minMaxGradient.gradientMax
                };
            }
        }

        public void SetAlpha(float value)
        {
            Initialize();

            for (int i = 0; i < _count; i++)
            {
                var data = _data[i];

                switch (data.mode)
                {
                    case ParticleSystemGradientMode.Gradient:
                        GradientUtils.UpdateAlphaKeys(data.gradientMax, data.maxAlphaKeys, value);
                        break;
                    case ParticleSystemGradientMode.TwoGradients:
                        GradientUtils.UpdateAlphaKeys(data.gradientMin, data.minAlphaKeys, value);
                        GradientUtils.UpdateAlphaKeys(data.gradientMax, data.maxAlphaKeys, value);
                        break;
                }
            }
        }
    }
}