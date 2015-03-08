﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace GameName3
{
    public class NPC : Entity
    {
        public int health;
        public NPC(int x, int y, int t, Texture2D tex )
        {
            this.x = x;
            this.y = y;
            this.spriteType = t;
            this.tex = tex;
        }
    }
}
