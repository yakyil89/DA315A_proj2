using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace _15_spelet_av_Yakup_Yildiz
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D boardTex;

        Vector2 offset = new Vector2(75, 75);
        Vector2 position;

        MouseState mouseState, oldmouseState;

        const int tileWidth = 150;
        const int tileHeight = 150;

        // Dessa tal avgör hur många bitar det ska vara i spelet
        // Konstanta värden, går ej att ändra på senare!
        const int nrofcols = 4;
        const int nrofrows = 4;

        Random rand;

        Square[,] tiles;
        Square[,] tempTiles;
        // Skapa en höjd + bredd för brickorna och låt inte värdena ändras!

        double timer;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }


        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            graphics.PreferredBackBufferHeight = 750;
            graphics.PreferredBackBufferWidth = 750;
            graphics.ApplyChanges();

            IsMouseVisible = true;

            rand = new Random();

            boardTex = Content.Load<Texture2D>("cactaur");

            tiles = new Square[nrofcols, nrofrows];
            tempTiles = new Square[nrofcols, nrofrows];
            timer = 1000;
            // sätt en index på en tile
            int index = 0;


            for (int column = 0; column < tiles.GetLength(0); column++)
            {
                for (int row = 0; row < tiles.GetLength(1); row++)
                {
                    position = new Vector2(column * 150, row * 150);
                    tiles[column, row] = new Square(boardTex, position + offset, new Rectangle
                        (column * 150, row * 150, tileWidth, tileHeight), Color.White, index++);

                }
            }
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            //shuffleTiles();

            //timer = timer + gameTime.ElapsedGameTime.Milliseconds;

            //if (timer <= 0)
            //{
            //    swapPos(1, 1, 2, 2);
            //    timer = 1000;
            //}

            oldmouseState = mouseState;
            mouseState = Mouse.GetState();


            if (timer > 200)
            {
                oldmouseState = mouseState;
                timer = 0;
            }

            foreach (Square t in tiles)
            {
                t.Update();
            }

            //for (int column = 0; column < tiles.GetLength(0); column++)
            //{
            //    for (int row = 0; row < tiles.GetLength(1); row++)
            //    {
                    //Rectangle boundingBox = new Rectangle(column * 150 + 75, row * 150 + 75, tileWidth, tileHeight);
                    // Lägg till kod som ändrar plats på två "tiles" vid klick
                    //if (mouseState.LeftButton == ButtonState.Pressed &&
                    //        oldmouseState.LeftButton == ButtonState.Released)
                    //{
                        //if (boundingBox.Contains(mouseState.X, mouseState.Y))
                        //{
                        //}
            //        }

            //    }
            //}





            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();

            drawTiles();
            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void drawTiles()
        {
            for (int column = 0; column < tiles.GetLength(0); column++)
            {
                for (int row = 0; row < tiles.GetLength(1); row++)
                {
                    if (tiles[column, row] == null) continue;
                    tiles[column, row].Draw(spriteBatch);
                }
            }
        }

        private void swapPos(int posX1, int posY1, int posX2, int posY2)
        {
            int tempX, tempY;
            tempX = (int)tiles[posX1, posY1].position.X;
            tempY = (int)tiles[posX1, posY1].position.Y;
            tiles[posX1, posY1].target.X = tiles[posX2, posY2].position.X;
            tiles[posX1, posY1].target.Y = tiles[posX2, posY2].position.Y;
            tiles[posX2, posY2].target.X = tempX;
            tiles[posX2, posY2].target.Y = tempY;

        }

        private void shuffleTiles()
        {
            // ha en metod som blandar alla tiles
            tempTiles = new Square[nrofcols, nrofrows];
            for (int column = 0; column < nrofcols; column++)
            {
                for (int row = 0; row < nrofrows; row++)
                {
                    if (column == 0 && row == 0)
                        break;

                    int newCol;
                    int newRow;
                    do
                    {
                        newCol = rand.Next(0, nrofcols);
                        newRow = rand.Next(0, nrofrows);
                    } while (tempTiles[newCol, newRow] != null || (newCol == nrofcols - 1 && newRow == nrofrows - 1));
                    tempTiles[newCol, newRow] = tiles[column, row];
                }
            }
            tiles = tempTiles;
        }
    }
}
