using Entitas;
using Entitas.Unity;
using Smooth.Algebraics;
using UnityEngine;

namespace TowerDefenceLike
{
    public class SelectObjectSystem : IExecuteSystem
    {
        private readonly GameContext m_context;
        
        public SelectObjectSystem(Contexts contexts)
        {
            m_context = contexts.game;
        }

        public void Execute()
        {
            if (!Input.GetMouseButtonDown(0)) return;
            
            var hit = new RaycastHit();
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
                hit.ToOption()
                    .Where(raycastHit => raycastHit.transform != null)
                    .Select(raycastHit => raycastHit.transform.gameObject.GetEntityLink())
                    .Select(link => link.entity as GameEntity)
                    .Where(entity => entity.isSelecteble)
                    .ForEach(entity =>
                    {
                        var newEntity = m_context.CreateEntity();
                        newEntity.AddBuild("Towers/Tower1", entity.position.value());
                        entity.isSelecteble = false;
                    });
        }
    }
}