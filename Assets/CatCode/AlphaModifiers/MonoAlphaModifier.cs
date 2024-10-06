using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace CatCode.AlphaModifiers
{
    [ExecuteInEditMode]
    public class MonoAlphaModifier : MonoBehaviour, IAlphaModifier
    {
        private List<MonoAlphaModifier> GetCollection()
            => ListPool<MonoAlphaModifier>.Get();

        private void ReleaseCollection(List<MonoAlphaModifier> list)
            => ListPool<MonoAlphaModifier>.Release(list);

        [SerializeField] private MonoAlphaModifier _parent;
        [SerializeField] private List<MonoAlphaModifier> _children = new();
        [Space]
        [SerializeField, Range(0, 1)] private float _alpha = 1f;
        [SerializeReference] private IAlphaModifierStrategy[] _alphaStrategies = new IAlphaModifierStrategy[0];

        public float Alpha
        {
            get => _alpha;
            set
            {
                _alpha = value;
                var totalAlpha = TotalAlpha;
                UpdateAlpha(totalAlpha);
                for (int i = 0; i < _children.Count; i++)
                    _children[i].Alpha = totalAlpha;
            }
        }

        protected void UpdateAlpha(float alpha)
        {
            for (int i = 0; i < _alphaStrategies.Length; i++)
                _alphaStrategies[i].SetAlpha(alpha);
        }

        public float TotalAlpha => _alpha * (_parent != null ? _parent.TotalAlpha : 1f);

        public void SetParent(MonoAlphaModifier parent)
        {
            if (_parent != null && _parent != parent)
                _parent.RemoveChild(this);
            _parent = parent;
            if (_parent != null)
                _parent.AddChild(this);
        }

        public void FindParent()
        {
            var parent = transform.parent;
            if (parent == null)
                return;
            SetParent(parent.GetComponentInParent<MonoAlphaModifier>(true));
        }

        public void RemoveParent()
            => SetParent(null);


        private void AddChild(MonoAlphaModifier alphaModifier)
        {
            if (_children == null)
                _children = GetCollection();
            if (_children.Contains(alphaModifier))
                return;
            _children.Add(alphaModifier);
        }

        private void RemoveChild(MonoAlphaModifier alphaModifier)
        {
            _children.Remove(alphaModifier);
            if (_children.Count == 0)
            {
                ReleaseCollection(_children);
                _children = null;
            }
        }

        private void Awake()
        {
            FindParent();
        }

        private void OnDestroy()
        {
            RemoveParent();
            if (_children == null)
                return;

            var children = UnityEngine.Pool.ListPool<MonoAlphaModifier>.Get();
            children.AddRange(_children);
            foreach (var child in children)                
                    child.SetParent(null);
            ReleaseCollection(_children);
        }

        private void Update()
        {
            if (Application.isPlaying)
                Alpha = _alpha;
        }

        protected void Reset()
        {
            FindParent();
        }

#if UNITY_EDITOR

        [ContextMenu("Alpha for this Branch")]
        private void GetAllStrategies()
        {
            _alphaStrategies = AlphaModifierTools.GetStrategiesFromBranch(gameObject);
        }

        [ContextMenu("Alpha for this Object")]
        private void GetCurrentStrategy()
        {
            _alphaStrategies = new[] { AlphaModifierTools.GetAlphaModifierStrategy(gameObject) };
        }

#endif
    }
}