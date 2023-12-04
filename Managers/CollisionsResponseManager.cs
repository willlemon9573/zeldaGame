using SprintZero1.Colliders;
using SprintZero1.Colliders.DoorColliders;
using SprintZero1.Colliders.EntityColliders;
using SprintZero1.Colliders.ItemColliders;
using SprintZero1.Commands.CollisionCommands;
using SprintZero1.Entities.EntityInterfaces;
using System;
using System.Collections.Generic;

namespace SprintZero1.Managers
{
    internal class CollisionsResponseManager
    {
        private readonly Dictionary<Tuple<Type, Type>, Action<ICollidableEntity, ICollidableEntity>> colliderDict = new Dictionary<Tuple<Type, Type>, Action<ICollidableEntity, ICollidableEntity>>()
        {
            { new Tuple<Type, Type>(typeof(PlayerCollider), typeof(OpenDoorCollider)), (entity1, entity2) => new EnterNextRoomCommand(entity1, entity2).Execute() },
            { new Tuple<Type, Type>(typeof(PlayerCollider), typeof(LockedDoorCollider)), (entity1, entity2) => new UnlockDoorCommand(entity1, entity2).Execute() },
            { new Tuple<Type, Type>(typeof(PlayerCollider), typeof(StackableItemCollider)), (entity1, entity2) => new PickUpStackableItemCommand(entity1, entity2).Execute() },
            { new Tuple<Type, Type>(typeof(PlayerCollider), typeof(LootableItemCollider)), (entity1, entity2) => new  PickupDungeonItemCommand(entity1, entity2).Execute()},
            { new Tuple<Type, Type>(typeof(PlayerCollider), typeof(EquipmentWithoutPlayerCollider)), (entity1, entity2) => new  PickupEquipmentWithoutPlayer(entity1, entity2).Execute()},
            { new Tuple<Type, Type>(typeof(PlayerCollider), typeof(EquipmentWithPlayerCollider)), (entity1, entity2) => new  PickupEquipmentWithPlayer(entity1, entity2).Execute()},
            { new Tuple<Type, Type>(typeof(PlayerCollider), typeof(HeartContainerCollider)), (entity1, entity2) => new  PickUpHeartContainer(entity1, entity2).Execute()},
            { new Tuple<Type, Type>(typeof(PlayerCollider), typeof(PushBackCollider)), (entity1, entity2) => new PushBackCommand(entity1, entity2).Execute() },
            { new Tuple<Type, Type>(typeof(PlayerCollider), typeof(BlockedDoorCollider)), (entity1, entity2) => new PushBackCommand(entity1, entity2).Execute() },
            { new Tuple<Type, Type>(typeof(PlayerCollider), typeof(WallCollider)), (entity1, entity2) => new PushBackCommand(entity1, entity2).Execute() },
            { new Tuple<Type, Type>(typeof(PlayerCollider), typeof(MovableBlockCollider)), (entity1, entity2) => new PushBlockCommand(entity1, entity2).Execute()},
            { new Tuple<Type, Type>(typeof(PlayerCollider), typeof(BreakableWallCollider)), (entity1, entity2) => new PushBackCommand(entity1, entity2).Execute()},
            { new Tuple<Type, Type>(typeof(PlayerCollider), typeof(ReplenishingHeartContainer)), (entity1, entity2) => new PickupReplenishingHeartCommand(entity1, entity2).Execute()},
            { new Tuple<Type, Type>(typeof(PlayerCollider), typeof(TriforceCollider)), (entity1, entity2) => new PickupTriforcePieceCommand(entity1, entity2).Execute()},
            { new Tuple<Type, Type>(typeof(PlayerSwordCollider), typeof(EnemyCollider)), (entity1, entity2) => new SwordEnemyCollisionCommand(entity1, entity2).Execute()},
            { new Tuple<Type, Type>(typeof(PlayerSwordCollider), typeof(BossCollider)), (entity1, entity2) => new SwordEnemyCollisionCommand(entity1, entity2).Execute()},
            { new Tuple<Type, Type>(typeof(EnemyCollider), typeof(PushBackCollider)), (entity1, entity2) => new PushBackCommand(entity1, entity2).Execute() },
            { new Tuple<Type, Type>(typeof(EnemyCollider), typeof(OpenDoorCollider)), (entity1, entity2) => new PushBackCommand(entity1, entity2).Execute() },
            { new Tuple<Type, Type>(typeof(EnemyCollider), typeof(LockedDoorCollider)), (entity1, entity2) => new PushBackCommand(entity1, entity2).Execute() },
            { new Tuple<Type, Type>(typeof(EnemyCollider), typeof(WallCollider)), (entity1, entity2) => new PushBackCommand(entity1, entity2).Execute() },
            { new Tuple<Type, Type>(typeof(EnemyCollider), typeof(BlockedDoorCollider)), (entity1, entity2) => new PushBackCommand(entity1, entity2).Execute() },
            { new Tuple<Type, Type>(typeof(EnemyCollider), typeof(BreakableWallCollider)), (entity1, entity2) => new PushBackCommand(entity1, entity2).Execute() },
            { new Tuple<Type, Type>(typeof(PlayerCollider), typeof(EnemyCollider)), (entity1, entity2) => new PlayerEnemyTouchDamageCommand(entity1).Execute()},
            { new Tuple<Type, Type>(typeof(PlayerCollider), typeof(BossCollider)), (entity1, entity2) => new PlayerEnemyTouchDamageCommand(entity1).Execute() },
            { new Tuple<Type, Type>(typeof(PlayerBoomerangCollider), typeof(EnemyCollider)), (entity1, entity2) => new PauseEnemyCommand(entity1, entity2).Execute()},
            { new Tuple<Type, Type>(typeof(PlayerProjectileCollider), typeof(EnemyCollider)), (entity1, entity2) => new ProjectileDamageCommand(entity1, entity2).Execute() },
            { new Tuple<Type, Type>(typeof(PlayerBoomerangCollider), typeof(WallCollider)), (entity1, entity2) => new ProjectileWallCollisionCommand(entity1, entity2).Execute()},
            { new Tuple<Type, Type>(typeof(PlayerBoomerangCollider), typeof(BlockedDoorCollider)), (entity1, entity2) => new ProjectileWallCollisionCommand(entity1, entity2).Execute()},
            { new Tuple<Type, Type>(typeof(PlayerBoomerangCollider), typeof(OpenDoorCollider)), (entity1, entity2) => new ProjectileWallCollisionCommand(entity1, entity2).Execute()},
            { new Tuple<Type, Type>(typeof(PlayerBoomerangCollider), typeof(LockedDoorCollider)), (entity1, entity2) => new ProjectileWallCollisionCommand(entity1, entity2).Execute()},
            { new Tuple<Type, Type>(typeof(PlayerBoomerangCollider), typeof(BreakableWallCollider)), (entity1, entity2) => new ProjectileWallCollisionCommand(entity1, entity2).Execute()},
            { new Tuple<Type, Type>(typeof(PlayerProjectileCollider), typeof(WallCollider)), (entity1, entity2) => new ProjectileWallCollisionCommand(entity1, entity2).Execute() },
            { new Tuple<Type, Type>(typeof(PlayerBombExplosionCollider), typeof(BreakableWallCollider)), (entity1, entity2) => new BreakWallCommand(entity1, entity2).Execute() },
            { new Tuple<Type, Type>(typeof(PlayerBombExplosionCollider), typeof(BossCollider)), (entity1, entity2) => new ProjectileDamageCommand(entity1, entity2).Execute() },
            { new Tuple<Type, Type>(typeof(PlayerBombExplosionCollider), typeof(EnemyCollider)), (entity1, entity2) => new ProjectileDamageCommand(entity1, entity2).Execute() },
            { new Tuple<Type, Type>(typeof(EnemyBoomerangCollider), typeof(PlayerCollider)), (entity1, entity2) => new EnemyBoomerangDamageCommand(entity1, entity2).Execute() },
            { new Tuple<Type, Type>(typeof(EnemyProjectileCollider), typeof(PlayerCollider)), (entity1, entity2) => new ProjectileDamageCommand(entity1, entity2).Execute() },
            { new Tuple<Type, Type>(typeof(EnemyProjectileCollider), typeof(WallCollider)), (entity1, entity2) => new ProjectileWallCollisionCommand(entity1, entity2).Execute() },
            { new Tuple<Type, Type>(typeof(EnemyProjectileCollider), typeof(BlockedDoorCollider)), (entity1, entity2) => new ProjectileWallCollisionCommand(entity1, entity2).Execute() },
            { new Tuple<Type, Type>(typeof(EnemyProjectileCollider), typeof(OpenDoorCollider)), (entity1, entity2) => new ProjectileWallCollisionCommand(entity1, entity2).Execute() },
            { new Tuple<Type, Type>(typeof(EnemyProjectileCollider), typeof(BreakableWallCollider)), (entity1, entity2) => new ProjectileWallCollisionCommand(entity1, entity2).Execute() },
            { new Tuple<Type, Type>(typeof(EnemyProjectileCollider), typeof(LockedDoorCollider)), (entity1, entity2) => new ProjectileWallCollisionCommand(entity1, entity2).Execute()}
        };

        /// <summary>
        /// Call for a collision response between two objects
        /// </summary>
        /// <param name="collidableEntityA">The first entity involved in the collision</param>
        /// <param name="collidableEntityB">The second entity involved in the collision</param>
        public void CollisionResponse(ICollidableEntity collidableEntityA, ICollidableEntity collidableEntityB)
        {
            Tuple<Type, Type> colliderTypes = new Tuple<Type, Type>(collidableEntityA.Collider.GetType(), collidableEntityB.Collider.GetType());
            /*Check if the collider contains the following collision happening*/
            if (colliderDict.TryGetValue(colliderTypes, out var action))
            {
                action(collidableEntityA, collidableEntityB);
            }
        }
    }
}
