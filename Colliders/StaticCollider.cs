using Microsoft.Xna.Framework;
using SprintZero1.Entities.EntityInterfaces;
using Size = System.Drawing.Size;

namespace SprintZero1.Colliders
{
    internal class StaticCollider : ICollider
    {
        private const float MinScaleFactor = 0.1f; // Prevents the collider from becoming too small
        private const float MaxScaleFactor = 2f; // prevents the collider from becoming too big
        private Rectangle _collider;

        public int Delta { get { return _delta; } set { _delta = value; } }
        private int _delta;
        public Rectangle Collider { get { return _collider; } set { _collider = value; } }

        private readonly int _offsetX;
        private readonly int _offsetY;
        private Size _colliderDimensions;

        /// <summary>
        /// Create the entities collider
        /// </summary>
        /// <param name="position">The position of the collider</param>
        /// <returns>The entities collider</returns>
        private void CreateCollider(Vector2 position)
        {
            int x = (int)position.X - (_colliderDimensions.Width / 2) + _offsetX;
            int y = (int)position.Y - (_colliderDimensions.Height / 2) + _offsetY;
            _collider = new Rectangle(x, y, _colliderDimensions.Width, _colliderDimensions.Height);
        }

        /// <summary>
        /// Initializes a new instance of the DynamicCollider class.
        /// </summary>
        /// <param name="position">The initial position of the collider in 2D space.</param>
        /// <param name="dimensions">The size of the collider.</param>
        /// <param name="scaleFactor">The scale factor of the collider, defaulting to 1f.</param>
        /// <param name="offsetX">The horizontal offset of the collider, defaulting to 0.</param>
        /// <param name="offsetY">The vertical offset of the collider, defaulting to 0.</param>
        public StaticCollider(Vector2 position, Size dimensions, float scaleFactor = 1f, int offsetX = 0, int offsetY = 0)
        {
            /* Verify scale factor is in range [0.1, 2.00] */
            float _scaleFactor = MathHelper.Max(MinScaleFactor, MathHelper.Min(MaxScaleFactor, scaleFactor));
            /* set offsets for collider position */
            _offsetX = offsetX;
            _offsetY = offsetY;
            /* Modifies the dimensions by the scale factor. */
            _colliderDimensions = new Size((int)(dimensions.Width * _scaleFactor), (int)(dimensions.Height * _scaleFactor));

            CreateCollider(position);
        }
        /// <summary>
        /// Update's the collider
        /// </summary>
        /// <param name="parent">The entity that contains this collider</param>
        public void Update(IEntity parent)
        {
            _collider.X = (int)parent.Position.X - (_colliderDimensions.Width / 2) + _offsetX;
            _collider.Y = (int)parent.Position.Y - (_colliderDimensions.Height / 2) + _offsetY;
        }
    }
}
