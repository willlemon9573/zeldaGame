using Microsoft.Xna.Framework;
using SprintZero1.Entities.EnemyEnetities;
using SprintZero1.Entities.EntityInterfaces;
using SprintZero1.Enums;
using SprintZero1.LevelFiles;
using System.Xml;

namespace SprintZero1.XMLParsers.XMLEntityBuilder
{
    internal class XMLEnemyEntity : EntityBase
    {
        private const int X = 0;
        private const int Y = 1;
        private const int Width = 2;
        private const int Height = 3;
        private const int Size = 4;
        private const string BoundaryElement = "Boundary";


        private float _entityHealth;
        private Rectangle bossBoundary;

        public void ParseBossBoundary(XmlReader reader)
        {
            int[] boundaryInfo = new int[Size];
            int i = 0;
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element && i < Size)
                {
                    boundaryInfo[i] = reader.ReadElementContentAsInt();
                    i++;
                }
                else if (reader.NodeType == XmlNodeType.EndElement && reader.Name == BoundaryElement)
                {
                    break;
                }
            }
            int x = boundaryInfo[X];
            int y = boundaryInfo[Y];
            int width = boundaryInfo[Width];
            int height = boundaryInfo[Height];
            bossBoundary = new Rectangle(x, y, width, height);
        }

        public float EntityHealth { set => _entityHealth = value; }
        public override IEntity CreateEntity()
        {
            Vector2 position = new Vector2(_entityPositionX, _entityPositionY);
            return new EnemyEntityWithoutProjectile(position, _entityHealth, _entityName);
        }

        public IEntity CreateBossEntity(RemoveDelegate remover)
        {
            Vector2 position = new Vector2(_entityPositionX, _entityPositionY);
            Direction bossDirection = Direction.North; // using a default value as we only have one boss
            return new AquamentusEntity(_entityHealth, bossDirection, position, bossBoundary, remover);

        }

        public IEntity CreateEntityWithprojectile()
        {
            Vector2 position = new Vector2(_entityPositionX, _entityPositionY);
            return new EnemyEntityWithProjectile(position, _entityHealth, _entityName);
        }
    }
}
