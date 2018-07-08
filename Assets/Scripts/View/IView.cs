using Entitas;
using Entitas.Unity;
using UnityEngine;

namespace TowerDefenceLike
{
    public interface IView
    {
        void InitializeView(GameEntity entity, Contexts contexts);

        void DestroyView(GameEntity entity, Contexts contexts);
    }
    
    public static class ViewExtentions
    {
        public static void Link(this IView view, IEntity entity, IContext context)
        {
            if (!(view is MonoBehaviour)) return;
            
            var go = ((MonoBehaviour) view).gameObject;
            var link = go.GetEntityLink();
            if (link == null || link.entity == null)
                go.Link(entity, context);
        }
        
        public static void Unlink(this IView view)
        {
            if (!(view is MonoBehaviour)) return;
            
            var go = ((MonoBehaviour) view).gameObject;
            var link = go.GetEntityLink();
            if (link != null && link.entity != null)
                go.Unlink();
        }
    }
}