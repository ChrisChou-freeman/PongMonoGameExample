using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoPong
{
    public struct MenuItem
    {
        public string itemName;
        public Vector2 positon;
        public int menuIndex;
        public MenuItem(string name, Vector2 positon, int index)
        {
            this.itemName = name;
            this.positon = positon;
            this.menuIndex = index;
        }
    }

    public class Menu
    {
        public static List<MenuItem> menuList;
        private SpriteFont _font;
        private Input _input;
        private int selectIndex = 0;

        public Menu(SpriteFont font, Input input)
        {
            this._font = font;
            this._input = input;
            menuList = new List<MenuItem> {
                new MenuItem(
                    "1P",
                    new Vector2(Pong.originalScreenSize.X / 2 - 20, Pong.originalScreenSize.Y / 2 - 50),
                    0),
                new MenuItem(
                    "2P",
                    new Vector2(Pong.originalScreenSize.X / 2 - 20, Pong.originalScreenSize.Y / 2 ),
                    1)
            };
        }

        private void onMenuStart(int menuIndex)
        {
            if(Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                switch(menuList[menuIndex].itemName)
                {
                    case "1P":
                        Pong.BattelComputer = true;
                        break;
                    case "2P":
                        Pong.BattelComputer = false;
                        break;
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            if(Pong.BattelComputer != null)
                return;
            if(Keyboard.GetState().IsKeyDown(this._input.Down))
            {
                selectIndex ++;
            }else if(Keyboard.GetState().IsKeyDown(this._input.Up))
            {
                selectIndex --;
            }
            selectIndex = Math.Clamp(selectIndex, 0, menuList.Count-1);
            this.onMenuStart(selectIndex);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(Pong.BattelComputer != null)
                return;
            foreach(var item in menuList)
            {
                spriteBatch.DrawString(
                    this._font,
                    item.itemName,
                    item.positon,
                    item.menuIndex==selectIndex?Color.Yellow:Color.Green);
            }
        }
    }
}