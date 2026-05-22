using System;
using System.Drawing;
using System.Windows.Forms;

namespace ProjectShip
{
    public partial class FormShip : Form
    {
        private readonly CanvasForShip _canvas;
        private DirectionType _checkBordersState;

        public FormShip()
        {
            InitializeComponent();
            _canvas = new CanvasForShip();
            _canvas.SetPictureSize(pictureBoxShip.Width, pictureBoxShip.Height);
            _checkBordersState = DirectionType.None;
        }

        private void Draw()
        {
            pictureBoxShip.Image = _canvas.DrawCanvas();
        }

        private void ButtonCreateShip_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            DrawingShip ship = new DrawingShip();
            int decks = rand.Next(1, 4);
            ship.Init(
                rand.Next(100, 300),
                rand.Next(1000, 3000),
                Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256)),
                decks
            );

            if (_canvas.InsertShip(ship))
            {
                _canvas.SetShipPosition(rand.Next(10, 100), rand.Next(10, 100));
                Draw();
            }
        }

        private void ButtonMove_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            string name = btn?.Name ?? string.Empty;
            DirectionType direction = DirectionType.None;

            switch (name)
            {
                case "buttonUp": direction = DirectionType.Up; break;
                case "buttonDown": direction = DirectionType.Down; break;
                case "buttonLeft": direction = DirectionType.Left; break;
                case "buttonRight": direction = DirectionType.Right; break;
            }

            if (_canvas.MoveTransport(direction))
                Draw();
        }

        private void ButtonCheckBorders_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            switch (_checkBordersState)
            {
                case DirectionType.None:
                case DirectionType.Down:
                    _canvas.SetShipPosition(rand.Next(10, 100) - 1000, rand.Next(10, 100));
                    _checkBordersState = DirectionType.Left;
                    break;
                case DirectionType.Left:
                    _canvas.SetShipPosition(rand.Next(10, 100), rand.Next(10, 100) - 1000);
                    _checkBordersState = DirectionType.Up;
                    break;
                case DirectionType.Up:
                    _canvas.SetShipPosition(rand.Next(10, 100) + pictureBoxShip.Width, rand.Next(10, 100));
                    _checkBordersState = DirectionType.Right;
                    break;
                case DirectionType.Right:
                    _canvas.SetShipPosition(rand.Next(10, 100), rand.Next(10, 100) + pictureBoxShip.Height);
                    _checkBordersState = DirectionType.Down;
                    break;
            }
            Draw();
        }
    }
}