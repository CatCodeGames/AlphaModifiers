using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Pools;

namespace CatCode.AlphaModifiers
{
    public static partial class AlphaModifierStrategyFactory
    {
        public static List<IAlphaModifierStrategy> CollectStrategies(GameObject root, bool includeChildren)
        {
            using var builder = new StrategyBuildContext();

            if (!includeChildren)
            {
                builder.CollectComponentsFrom(root.transform);
            }
            else
            {

                using var stackHandle = StackPool<Transform>.Get(out var stack);

                CollectStrategiesFromChildren(root.transform, stack);

                while (stack.Count > 0)
                {
                    var current = stack.Pop();

                    if (current.TryGetComponent<MonoAlphaModifier>(out _))
                        continue;

                    CollectStrategiesFromChildren(current, stack);
                }
            }

            var result = builder.BuildStrategies();
            return result;

            void CollectStrategiesFromChildren(Transform parent, Stack<Transform> stack)
            {
                builder.CollectComponentsFrom(parent);
                for (int i = 0; i < parent.childCount; i++)
                    stack.Push(parent.GetChild(i));
            }
        }
    }
}