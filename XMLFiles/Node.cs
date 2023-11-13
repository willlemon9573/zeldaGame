/*using SprintZero1.Entities;
using SprintZero1.Factories;
using SprintZero1.Sprites;
using System.Numerics;

namespace SprintZero1.XMLFiles
{
    internal class Node
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string Name { get; set; }
        public int DestinationOrHealth { get; set; }
        public int frame { get; set; }
        public int isBoss { get; set; }

        public Node(int x, int y, string name, int destination, int Frame, int Boss)
        {
            X = x;
            Y = y;
            Name = name;
            DestinationOrHealth = destination;
            frame = Frame;
            isBoss = Boss;
        }

        public IEntity AddBlockToGame()
        {
            Vector2 entityPos = new Vector2(X, Y);
            ISprite entitySprite = TileSpriteFactory.Instance.CreateNewTileSprite(Name);
            return new LevelBlockEntity(entitySprite, entityPos);
           
        }

        public IEntity AddFloorToGame()
        {
            Vector2 entityPos = new Vector2(X, Y);
            ISprite entitySprite = TileSpriteFactory.Instance.CreateFloorSprite(Name);
            return new BackgroundSpriteEntity(entitySprite, entityPos);

        }

        public IEntity AddWallToGame()
        {
            Vector2 entityPos = new Vector2(X, Y);
            int DimX = 0, DimY = 0;
            if (Name.Contains("horizontal")) {
                DimX = 80;
                DimY = 32;
            } else if (Name.Contains("vertical")){
                DimX = 32;
                DimY = 72;
            }
            Vector2 entityDimension = new Vector2(DimX, DimY);
            ISprite entitySprite = TileSpriteFactory.Instance.CreateNewTileSprite(Name);
            return new InvisibleWallEntity(entitySprite, entityPos, entityDimension);
        
        }

        public IEntity AddDoorToGame() 
        {   
            Vector2 entityPosition = new Vector2(X, Y);
            ISprite entitySprite = TileSpriteFactory.Instance.CreateNewTileSprite(Name);
            return new LevelDoorEntity(entitySprite,entityPosition, DestinationOrHealth);
        }

        public IEntity AddItemToGame()
        {
            Vector2 entityPosition = new Vector2(X, Y);
            
             ISprite entitySprite = ItemSpriteFactory.Instance.CreateAnimatedItemSprite(Name,5);
            
            return new LevelBlockEntity(entitySprite, entityPosition);
        }
        public IEntity AddEnemyToGame()
        {
            Vector2 entityPosition = new Vector2(X, Y);
            if (isBoss == 1)
            {
                return null;
                //return new EnemyEntityWithoutDirection(entityPosition, DestinationOrHealth, Name, true);
            }
            else {
                return new EnemyEntityWithoutProjectile(entityPosition, DestinationOrHealth, Name);
            }
            
        }

    }
}
*/