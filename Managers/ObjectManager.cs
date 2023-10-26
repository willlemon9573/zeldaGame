using SprintZero1.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprintZero1.Managers
{
    internal static class ObjectManager
    {
        private static List<IEntity> OnScreenEntities = new List<IEntity>();
        private static List<IEntity> AddEntities = new List<IEntity>();
        private static List<IEntity> RemoveEntities = new List<IEntity>();

        public static void AddOnScreenEntity(IEntity entity)
        {
            AddEntities.Add(entity);
        }

        public static void RemoveOnScreenEntity(IEntity entity)
        {
            RemoveEntities.Add(entity);
        }

        public static void Update()
        {
            OnScreenEntities.AddRange(AddEntities);
            OnScreenEntities = OnScreenEntities.Except(RemoveEntities).ToList();
            
        }
    }
}
