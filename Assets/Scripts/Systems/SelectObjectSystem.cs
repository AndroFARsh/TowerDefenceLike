using Entitas;
using Entitas.Unity;
using Smooth.Algebraics;
using Smooth.Slinq;
using UnityEngine;

namespace TowerDefenceLike
{
    public class SelectObjectSystem : IExecuteSystem
    {
        private readonly int layerMask = LayerMask.GetMask("Selecteble"); 
        private readonly GameContext m_gameСontext;
        private readonly MetaContext m_metaСontext;
        private readonly IGroup<GameEntity> m_group;

        public SelectObjectSystem(Contexts contexts)
        {
            m_gameСontext = contexts.game;
            m_metaСontext = contexts.meta;

            m_group = contexts.game.GetGroup(GameMatcher.BuildDialog);
        }

        public void Execute()
        {
            if (!Input.GetMouseButtonDown(0)) return;

            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var raycastHit = default(RaycastHit);
            if (Physics.Raycast(ray, out raycastHit, float.PositiveInfinity, layerMask)) {
                m_group.GetEntities().Slinq()
                    .ForEach(entity => entity.isRelease = true);
                
                raycastHit.ToOption()
                    .Where(hit => hit.transform != null)
                    .Select(hit => hit.transform.gameObject.GetEntityLink())
                    .Where(link => link != null && link.entity != null)
                    .Select(link => link.entity as GameEntity)
                    .Where(entity => entity.isSelecteble)
                    .ForEach(entity =>
                    {
                        var viewService = m_metaСontext.viewService.value;
                        
                        var newEntity = m_gameСontext.CreateEntity();
                        newEntity.AddInitializePoint(entity.position.value);
                        viewService.Borrow(newEntity, "BuildDialogHUD");
                       
                        
//                        newEntity.AddBuild("Guns/Crossbow.001", entity.position.value);
//                        entity.isSelecteble = false;
                    });
            }
        }
    }
}