﻿using JackosAdventure.Simulation.World;
using System.Drawing;
using System.Windows.Forms;

namespace JackosAdventure.MapEditor
{
    public partial class Editor : UserControl
    {
        public Map Map { get; }
        public string? CurrentTypeSelection { get; internal set; }
        public int CellWidth { get; private set; }
        public int CellHeight { get; private set; }

        public Editor()
        {
            Map = new Map(100, 100);
            Map.Init();

            DoubleBuffered = true;

            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (Size.Width < 1 || Size.Height < 1)
                return;

            CellWidth = Size.Width / Map.Width;
            CellHeight = Size.Height / Map.Height;

            using var pen = new Pen(Color.Black);

            using var brush = new SolidBrush(Color.White);

            for (int x = 0; x < Map.Width; x++)
            {
                for (int y = 0; y < Map.Height; y++)
                {
                    var tile = Map.Tiles[x, y];
                    var color = GetColorFromTile(tile);

                    if (color != brush.Color)
                    {
                        brush.Color = color;
                    }

                    var rectangle = new Rectangle(x * CellWidth, y * CellHeight, CellWidth, CellHeight);
                    e.Graphics.FillRectangle(brush, rectangle);
                    e.Graphics.DrawRectangle(pen, rectangle);
                }
            }

            base.OnPaint(e);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            if (CurrentTypeSelection is null)
                return;

            var x = e.Location.X / CellWidth;
            var y = e.Location.Y / CellHeight;

            Map.Tiles[x, y] = new Tile(CurrentTypeSelection);
            Invalidate();
        }

        private static Color GetColorFromTile(Tile tile)
        {
            if (tile == null)
            {
                return Color.White;
            }

            switch (tile.Name)
            {
                case "grass":
                    return Color.Green;
                case "grass_blue":
                    return Color.Blue;
                default:
                    return Color.White;
            }
        }
    }
}
