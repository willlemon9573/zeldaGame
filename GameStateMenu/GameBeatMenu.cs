using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SprintZero1.GameStateMenu
{
    public class GameBeatMenu : GameStateAbstract
    {
        private bool isFlashing = true;
        private bool isMerging = false;
        private bool hasMerged = false;
        private bool shouldDrawFlashing = false;
        private bool isWaiting = false;

        private float flashTimer = 0;
        private float mergeTimer = 0;
        private float waitTimer = 0;
        private float mergePosition = 0;

        private int flashCount = 0;

        private const float flashMaxTime = 0.4f; // Time interval for flashing
        private const int flashAmount = 4; // Number of flashes
        private const float mergeFinishTime = 3.0f; // Time duration for merging animation
        private const float waitTime = 2.0f; // Waiting time between flashing and merging
        private const int RGB_BLACK = 225; // RGB value for white overlay color
        private const int RGB_ALPHA = 60; // Alpha (transparency) value for white overlay

        private float scrollPosition;
        private string creditsText = "Thank you \n for playing! \n Created by: EnterGame\n...\nTeamMember:\n Zihe Wang\n Aaron Heishman\n Muhammad Gheith\n Chih-Hsiang Jarek Tseng\n Will Lemon. ";
        private SpriteFont creditsFont;
        private bool creditsRolled = false;

        public GameBeatMenu(Game1 game) : base(game)
        {
            Color WhiteOverlay = new Color(RGB_BLACK, RGB_BLACK, RGB_BLACK, RGB_ALPHA);
            creditsFont = game.Content.Load<SpriteFont>("PauseSetting");
            scrollPosition = HEIGHT;
            _overlay.SetData(new[] { WhiteOverlay }); 
        }

        public override void Update(GameTime gameTime)
        {
            if (isFlashing)
            {
                updateFlashTime(gameTime);
            }

            if (isWaiting)
            {
                updateWaitingTime(gameTime);
            }

            if (isMerging)
            {
                updateMerge(gameTime);
            }
        }
        private void updateFlashTime(GameTime gameTime)
        {
            flashTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (flashTimer >= flashMaxTime)
            {
                flashTimer = 0;
                flashCount++;
                shouldDrawFlashing = !shouldDrawFlashing;

                if (flashCount >= flashAmount)
                {
                    isFlashing = false;
                    isWaiting = true;
                    shouldDrawFlashing = false;
                }
            }
        }
        private void updateWaitingTime(GameTime gameTime)
        {
            waitTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (waitTimer >= waitTime)
            {
                isWaiting = false;
                isMerging = true;
                waitTimer = 0;
            }
        }

        private void updateMerge(GameTime gameTime)
        {
            mergeTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            mergePosition = MathHelper.Lerp(0, WIDTH / 2, mergeTimer / mergeFinishTime);
            if (mergeTimer >= mergeFinishTime)
            {
                isMerging = false;
                hasMerged = true;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            if (shouldDrawFlashing)
            {
                spriteBatch.Draw(_overlay, new Rectangle(0, 0, WIDTH, HEIGHT), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.11f);
            }

            if (isMerging)
            {
                Texture2D blackTexture = new Texture2D(_GraphicsDevice, 1, 1);
                blackTexture.SetData(new[] { Color.Black });
                spriteBatch.Draw(blackTexture, new Rectangle(0, 0, (int)mergePosition, HEIGHT), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.01f);
                spriteBatch.Draw(blackTexture, new Rectangle(WIDTH - (int)mergePosition, 0, (int)mergePosition, HEIGHT), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.01f);
            }

            if (hasMerged)
            {
                Texture2D fullBlackTexture = new Texture2D(_GraphicsDevice, 1, 1);
                fullBlackTexture.SetData(new[] { Color.Black });
                spriteBatch.Draw(fullBlackTexture, new Rectangle(0, 0, WIDTH, HEIGHT), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.01f);

                if (!creditsRolled) 
                {
                    Vector2 textPosition = new Vector2(20, scrollPosition);
                    spriteBatch.DrawString(creditsFont, creditsText, textPosition, Color.White,0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

                    scrollPosition -= 1;
                    if (scrollPosition < -creditsFont.MeasureString(creditsText).Y)
                    {
                        creditsRolled = true;
                    }
                }
            }

        }
    }
}
