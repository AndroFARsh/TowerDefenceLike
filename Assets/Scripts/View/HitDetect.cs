using System;
using Entitas.Unity;
using Smooth.Algebraics;
using UnityEngine;
using Tuple = Smooth.Algebraics.Tuple;

namespace TowerDefenceLike
{
    public class HitDetect : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            other.gameObject.GetEntityLink().ToOption()
                .Select(link => link.entity as GameEntity)
                .SelectMany(entity => gameObject.GetEntityLink().ToOption()
                    .Select(link => link.entity as GameEntity)
                    .Select(Tuple.Create, entity))
                .Where(t => t.Item1.hasHalth && t.Item2.hasHit)
                .ForEach(t =>
                    t.Item1.ReplaceHalth(t.Item1.halth.max, Math.Max(t.Item1.halth.value - t.Item2.hit.value, 0)));
        }
    }
}