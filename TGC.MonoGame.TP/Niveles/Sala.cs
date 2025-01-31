﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TGC.MonoGame.TP.Elements;
using TGC.MonoGame.TP.Geometries;

namespace TGC.MonoGame.TP.Niveles
{

    public class Sala
    {
        public const string ContentFolderEffects = "Effects/";

        private Effect Effect { get; set; }

        public Cube Piso { get; set; }
        public Cube ParedOeste { get; set; }
        public Cube ParedEste { get; set; }
        public Cube ParedNorteIzq { get; set; }
        public Cube ParedNorteDer { get; set; }
        public Cube Techo { get; set; }

        public CheckPointWall checkpoint;

        public Vector3 Posicion;
        public static float Size = 100f;

        public Texture2D floorTex { get; set; }
        private Texture2D wallTex { get; set; }
        private Texture2D obsText { get; set; }

        public RenderTarget2D noShadowsRender;

        public RenderTarget2D noEnviromentRender;

        public Sala(ContentManager content, GraphicsDevice graphicsDevice, Vector3 posicion)
        {
            Posicion = posicion;

            floorTex = content.Load<Texture2D>("Textures/madera");
            wallTex = content.Load<Texture2D>("Textures/stones");

            Piso = new Cube(graphicsDevice, content, posicion, floorTex);
            Piso.WorldUpdate(new Vector3(Size, 1f, Size), new Vector3(0, 0, 0) + Posicion, Quaternion.Identity);

            ParedOeste = new Cube(graphicsDevice, content, posicion, wallTex);
            ParedOeste.WorldUpdate(new Vector3(Size, Size, 1f), new Vector3(0, Size / 2, Size / 2) + Posicion, Quaternion.Identity);

            ParedEste = new Cube(graphicsDevice, content, posicion, wallTex);
            ParedEste.WorldUpdate(new Vector3(Size, Size, 1f), new Vector3(0, Size / 2, -Size / 2) + Posicion, Quaternion.Identity);

            ParedNorteIzq = new Cube(graphicsDevice, content, posicion, wallTex);
            ParedNorteIzq.WorldUpdate(new Vector3(1f, Size, Size * 0.45f), new Vector3(50, Size / 2, Size * 0.275f) + Posicion, Quaternion.Identity);

            ParedNorteDer = new Cube(graphicsDevice, content, posicion, wallTex);
            ParedNorteDer.WorldUpdate(new Vector3(1f, Size, Size * 0.45f), new Vector3(50, Size / 2, -Size * 0.275f) + Posicion, Quaternion.Identity);

            Techo = new Cube(graphicsDevice, content, posicion, wallTex);
            Techo.WorldUpdate(new Vector3(Size, 1f, Size), new Vector3(0, Size, 0) + Posicion, Quaternion.Identity);

            checkpoint = new CheckPointWall(graphicsDevice, content, posicion);
            checkpoint.WorldUpdate(new Vector3(1f, Size, Size * 0.1f), new Vector3(Size / 2, Size / 2, 0) + Posicion, Quaternion.Identity);

            noShadowsRender = new RenderTarget2D(graphicsDevice, 1, 1, false,
                SurfaceFormat.Single, DepthFormat.Depth24, 0, RenderTargetUsage.PlatformContents);

            noEnviromentRender = new RenderTarget2D(graphicsDevice, 1, 1, false,
                SurfaceFormat.Single, DepthFormat.Depth24, 0, RenderTargetUsage.PlatformContents);
        }

        public virtual void Draw(GameTime gameTime, Matrix view, Matrix projection)
        {

            Piso.Draw(view, projection);
            ParedOeste.Draw(view, projection);
            ParedEste.Draw(view, projection);
            ParedNorteIzq.Draw(view, projection);
            ParedNorteDer.Draw(view, projection);
            //Techo.Draw(view, projection);
        }

        public virtual void DrawTranslucent(GameTime gameTime, Matrix view, Matrix projection)
        {
            checkpoint.Draw(view, projection, (float)gameTime.TotalGameTime.TotalSeconds);
        }

        public virtual List<TP.Elements.Object> GetPhysicalObjects() {
            List<TP.Elements.Object> l = new List<TP.Elements.Object>();
            l.Add(Piso);
            l.Add(ParedEste);
            l.Add(ParedOeste);
            l.Add(ParedNorteDer);
            l.Add(ParedNorteIzq);
            l.Add(Techo);
            return l;
        }
        public virtual List <TP.Elements.LogicalObject> GetLogicalObjects()
        {
            List<TP.Elements.LogicalObject> logicalObjects = new List<TP.Elements.LogicalObject>();
            logicalObjects.Add(checkpoint);
            return logicalObjects;
        }
            public virtual void Update(GameTime gameTime) { }
        public virtual void RestartLogicalObjects()
        {
            var logicalObjects = GetLogicalObjects();
            foreach (TP.Elements.LogicalObject logicalObject in logicalObjects)
            {
                logicalObject.Restart();
            }
        }
    }
}
