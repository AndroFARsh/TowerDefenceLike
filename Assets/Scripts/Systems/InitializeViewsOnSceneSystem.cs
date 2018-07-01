using Entitas;
using Entitas.Unity;
using Smooth.Algebraics;
using Smooth.Slinq;
using UnityEngine;

namespace TowerDefenceLike
{
    public class InitializeViewsOnSceneSystem : IInitializeSystem, ITearDownSystem
    {
        private readonly Contexts contexts;

        public InitializeViewsOnSceneSystem(Contexts c)
        {
            contexts = c;
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
                    var entity = contexts.game.CreateEntity();
                    var entityLink = tupel.Item1.GetEntityLink();
                    if (entityLink == null) tupel.Item1.Link(entity, contexts.game);
                    tupel.Item2.InitializeView(entity, contexts);
                });
        }

        public void TearDown()
        {
            GameObject.FindObjectsOfTypeAll(typeof(GameObject))
                .Slinq()
                .Select(obj => obj as GameObject)
                .Select(go => Tuple.Create(go, go.GetComponent<IView>()))
                .Where(tupel => tupel.Item1 != null && tupel.Item2 != null)
                .ForEach(tupel =>
                {
                    var entity = tupel.Item1.GetEntityLink().entity as GameEntity;
                    if (entity != null) tupel.Item2.DestroyView(entity, contexts);
                    tupel.Item1.Unlink();
                });
        }
    }
}