using Entitas;
using Entitas.Unity;
using Smooth.Algebraics;
using Smooth.Slinq;
using UnityEngine;

namespace TowerDefenceLike
{
    public class InitializeViewsOnSceneSystem : IInitializeSystem, ITearDownSystem
    {
        private readonly Contexts m_contexts;

        public InitializeViewsOnSceneSystem(Contexts contexts)
        {
            m_contexts = contexts;
        }

        public void Initialize()
        {
            GameObject.FindObjectsOfTypeAll(typeof(GameObject))
                .Slinq()
                .Select(obj => obj as GameObject)
                .Where(obj => obj.active)
                .Select(go => Tuple.Create(go, go.GetComponent<IView>()))
                .Where(tupel => tupel.Item1 != null && tupel.Item2 != null)
                .ForEach(tupel =>
                {
                    var entity = m_contexts.game.CreateEntity();
                    tupel.Item2.Link(entity, m_contexts.game);
                    tupel.Item2.InitializeView(entity, m_contexts);
                });
        }

        public void TearDown()
        {
            GameObject.FindObjectsOfTypeAll(typeof(GameObject))
                .Slinq()
                .Select(obj => obj as GameObject)
                .Select(go => Tuple.Create(go.GetEntityLink(), go.GetComponent<IView>()))
                .Where(tupel => tupel.Item2 != null)
                .ForEach(tupel =>
                {
                    if (tupel.Item1 != null && tupel.Item1.entity != null)
                        tupel.Item2.DestroyView((GameEntity)tupel.Item1.entity, m_contexts);
                    
                    tupel.Item2.Unlink();
                });
        }
    }
}