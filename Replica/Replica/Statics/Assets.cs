﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using Replica.Entities;
using Replica.Entities.Blocks;

namespace Replica.Statics
{
    public class Assets
    {
        public static Texture2D dna;
        public static Texture2D happy;
        public static Texture2D play;
        public static Texture2D exit;
        public static Texture2D levelselection;
        public static Texture2D pix;

        public static Texture2D lvl00;
        public static Texture2D lvl01;
        public static Texture2D lvl02;

        public static Model model;
        public static Model wallModel;
        public static Model testModel;

        public static SoundEffect doorClosing;
        public static SoundEffect doorOpening;
        public static SoundEffect jumping;

        public static void Loadcontent(ContentManager Manager)
        {
            play            = Manager.Load<Texture2D>("Textures\\game");
            exit            = Manager.Load<Texture2D>("Textures\\exit");
            levelselection  = Manager.Load<Texture2D>("Textures\\levelselection");
            dna             = Manager.Load<Texture2D>("Textures\\dna");
            happy           = Manager.Load<Texture2D>("Textures\\happy");
            pix             = Manager.Load<Texture2D>("Textures\\pix");

            lvl00           = Manager.Load<Texture2D>("Textures\\lvl00");
            lvl01           = Manager.Load<Texture2D>("Textures\\lvl01");
            lvl02           = Manager.Load<Texture2D>("Textures\\lvl02");

            model           = Manager.Load<Model>("Models\\p1_wedge");
            wallModel       = Manager.Load<Model>("Models\\test");

            doorClosing = Manager.Load<SoundEffect>("Sounds\\doorClosing");
            doorOpening = Manager.Load<SoundEffect>("Sounds\\doorOpening");
            jumping = Manager.Load<SoundEffect>("Sounds\\jumping");
        }
    }
}

    
