using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Drawing;

/*
 *  Player manager
 */

namespace FirstGame
{
    class PlayerManager
    {
        public List<Player> players;
        int numPlayers = 0;
        public Vector2 rPos = new Vector2(400, 450);

        public PlayerManager()
        {

            players = new List<Player>();
        }

        public void addPlayer()
        {

            players.Add(new Player());
        }

        public void update( WallManager walls){

            for (int i = 0; i < players.Count; i++)
                players.ElementAt(i).Update(walls);
        }

        public void die()
        {

            for (int i = 0; i < players.Count; i++)
                players.ElementAt(i).reset(rPos);
        }

        public  void Draw(SpriteBatch spriteBatch, SpriteFont font){

            for (int i = 0; i < players.Count; i++)
                players.ElementAt(i).Draw(spriteBatch, font);
        }

        public void Initialize(Texture2D texture, Vector2 position)
        {
            for (int i = 0; i < players.Count; i++)
                players.ElementAt(i).Initialize(texture, position);
        }
    }
}
