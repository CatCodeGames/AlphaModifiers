using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CatCode.AlphaModifiers
{
    public sealed class MonoAlphaModifier : MonoBehaviour, IAlphaModifier
    {
        [Header("Settings")]
        [SerializeField, Range(0f, 1f)] private float _alpha = 1f;
        [SerializeReference] private List<IAlphaModifierStrategy> _alphaStrategies;

        private MonoAlphaModifier _parent;
        private List<MonoAlphaModifier> _children = new();

        private float _parentAlpha = 1f;

        public MonoAlphaModifier Parent => _parent;
        public IReadOnlyList<MonoAlphaModifier> Children => _children;

        public float Alpha
        {
            get => _alpha;
            set
            {
                _alpha = value;
                UpdateAlpha(_parentAlpha);
            }
        }

        public float TotalAlpha => _alpha * _parentAlpha;

        private void OnEnable()
        {
            Register();
            UpdateAlpha(_parentAlpha);
        }

        private void OnDisable()
        {
            Unregister();
            UpdateAlpha(_parentAlpha);
        }

        private void OnTransformParentChanged()
        {
            Unregister();
            Register();
            UpdateAlpha(_parentAlpha);
        }

        private void OnValidate()
        {
            if (!Application.isPlaying)
                return;
            UpdateAlpha(_parentAlpha);
        }

        private void Register()
        {
            if (transform.parent == null)
            {
                _parentAlpha = 1f;
                return;
            }

            _parent = transform.parent.GetComponent<MonoAlphaModifier>();
            if (_parent == null)
            {
                _parentAlpha = 1f;
                return;
            }

            _parent._children.Add(this);
            _parentAlpha = _parent.TotalAlpha;
        }

        private void Unregister()
        {
            if (_parent == null)
                return;

            _parent._children.Remove(this);
            _parent = null;
            _parentAlpha = 1f;
        }

        private void UpdateAlpha(float parentAlpha)
        {
            _parentAlpha = parentAlpha;

            var totalAlpha = parentAlpha * _alpha;
            for (int i = 0; i < _alphaStrategies.Count; i++)
                _alphaStrategies[i].SetAlpha(totalAlpha);

            for (int i = 0; i < _children.Count; i++)
                _children[i].UpdateAlpha(totalAlpha);
        }

        public MonoAlphaModifier EditorParent
            => Application.isPlaying
                ? _parent
                : transform.parent?.GetComponent<MonoAlphaModifier>();

        public IReadOnlyList<MonoAlphaModifier> EditorChildren
            => Application.isPlaying
                ? _children
                : transform.Cast<Transform>().Select(t => t.GetComponent<MonoAlphaModifier>()).Where(c => c != null).ToList();

        [ContextMenu("Collect for Object")]
        public void CollectStrategiesForObject()
            => _alphaStrategies = AlphaModifierStrategyFactory.CollectStrategies(gameObject, false);

        [ContextMenu("Collect for Branch")]
        public void CollectStrategiesForBranch()
            => _alphaStrategies = AlphaModifierStrategyFactory.CollectStrategies(gameObject, true);
    }
}