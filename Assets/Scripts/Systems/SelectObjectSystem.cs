using Entitas;
using Entitas.Unity;
using Smooth.Algebraics;
using UnityEngine;

namespace TowerDefenceLike
{
    public class SelectObjectSystem : IExecuteSystem
    {
        private readonly int layerMask = LayerMask.GetMask("Selecteble"); 
        private readonly GameContext m_context;
        
        public SelectObjectSystem(Contexts contexts)
        {
            m_context = contexts.game;
        }

        public void Execute()
        {
            if (!Input.GetMouseButtonDown(0)) return;

            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var raycastHit = default(RaycastHit);
            if (Physics.Raycast(ray, out raycastHit, float.PositiveInfinity, layerMask))
                raycastHit.ToOption()
                    .Where(hit => hit.transform != null)
                    .Select(hit => hit.transform.gameObject.GetEntityLink())
                    .Where(link => link != null && link.entity != null)
                    .Select(link => link.entity as GameEntity)
                    .Where(entity => entity.isSelecteble)
                    .ForEach(entity =>
                    {
                        var newEntity = m_context.CreateEntity();
                        newEntity.AddBuild("Towers/Tower1", entity.position.value);
                        entity.isSelecteble = false;
                    });
        }
    }
}