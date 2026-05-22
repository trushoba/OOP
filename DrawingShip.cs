using System;
using System.Drawing;

namespace ProjectShip
{
    public class DrawingShip
    {
        private EntityShip _entityShip;
        private int? _startPosX;
        private int? _startPosY;

        private readonly int _drawingWidth = 100;
        private readonly int _drawingHeight = 80;

        public int? PosX => _startPosX;
        public int? PosY => _startPosY;
        public double? ShipStep => _entityShip?.Step;
        public int DrawingWidth => _drawingWidth;
        public int DrawingHeight => _drawingHeight;

        public void Init(int speed, double weight, Color bodyColor, int decksCount)
        {
            _entityShip = new EntityShip();
            _entityShip.Init(speed, weight, bodyColor, decksCount);
            _startPosX = null;
            _startPosY = null;
        }

        public void SetPosition(int x, int y)
        {
            _startPosX = x;
            _startPosY = y;
        }

        public void MoveLeft()
        {
            if (_entityShip == null || !_startPosX.HasValue) return;
            _startPosX -= (int)_entityShip.Step;
        }

        public void MoveRight()
        {
            if (_entityShip == null || !_startPosX.HasValue) return;
            _startPosX += (int)_entityShip.Step;
        }

        public void MoveUp()
        {
            if (_entityShip == null || !_startPosY.HasValue) return;
            _startPosY -= (int)_entityShip.Step;
        }

        public void MoveDown()
        {
            if (_entityShip == null || !_startPosY.HasValue) return;
            _startPosY += (int)_entityShip.Step;
        }

        public void DrawTransport(Graphics g)
        {
            if (_entityShip == null || !_startPosX.HasValue || !_startPosY.HasValue)
                return;

            int x = _startPosX.Value;
            int y = _startPosY.Value;

            Pen blackPen = new Pen(Color.Black, 2);

            // ========== 1. Равнобедренная трапеция (корпус) ==========
            int topWidth = 80;
            int bottomWidth = 50;
            int height = 40;

            int topLeft = x + (DrawingWidth - topWidth) / 2;
            int topRight = topLeft + topWidth;
            int bottomLeft = x + (DrawingWidth - bottomWidth) / 2;
            int bottomRight = bottomLeft + bottomWidth;

            Point[] trapezoid = {
                new Point(topLeft, y),
                new Point(topRight, y),
                new Point(bottomRight, y + height),
                new Point(bottomLeft, y + height)
            };

            using (Brush bodyBrush = new SolidBrush(_entityShip.BodyColor))
            {
                g.FillPolygon(bodyBrush, trapezoid);
            }
            g.DrawPolygon(blackPen, trapezoid);

            // ========== 2. Прямоугольник (надстройка) с заливкой ==========
            int rectLeft = topLeft + 15;   // сдвинут вправо
            int rectTop = y - 25;
            int rectRight = rectLeft + 20;  // ширина прямоугольника (только две линии по краям)

            // Заливка прямоугольника
            using (Brush rectBrush = new SolidBrush(Color.LightGray))
            {
                g.FillRectangle(rectBrush, rectLeft, rectTop, rectRight - rectLeft, y - rectTop);
            }
            // Левая и правая вертикальные линии
            g.DrawLine(blackPen, rectLeft, y, rectLeft, rectTop);
            g.DrawLine(blackPen, rectRight, y, rectRight, rectTop);
            // Верхняя горизонтальная линия
            g.DrawLine(blackPen, rectLeft, rectTop, rectRight, rectTop);

            // ========== 3. Маленький несимметричный крест внутри трапеции слева ==========
            int crossX = bottomLeft + 12;
            int crossY = y + height - 12;
            int crossSizeH = 8;
            int crossSizeV = 5;
            g.DrawLine(blackPen, crossX - crossSizeH / 2, crossY, crossX + crossSizeH / 2, crossY);
            g.DrawLine(blackPen, crossX, crossY - crossSizeV / 2, crossX, crossY + crossSizeV / 2);

            // ========== 4. Трубы (стоят прямо на корпусе) ==========
            Brush pipeBrush = new SolidBrush(Color.DimGray);
            int pipeBaseY = y;   // теперь трубы начинаются от верхней стороны трапеции

            int pipe1X = topLeft + 50;
            int pipe2X = topLeft + 62;

            // Первая труба
            g.FillRectangle(pipeBrush, pipe1X, pipeBaseY - 18, 8, 18);
            g.DrawRectangle(blackPen, pipe1X, pipeBaseY - 18, 8, 18);
            // Вторая труба
            g.FillRectangle(pipeBrush, pipe2X, pipeBaseY - 22, 8, 22);
            g.DrawRectangle(blackPen, pipe2X, pipeBaseY - 22, 8, 22);

            // ========== 5. Отсек под топливо ==========
            Brush fuelBrush = new SolidBrush(Color.DarkOliveGreen);
            int fuelX = bottomRight - 22;
            int fuelY = y + height - 15;
            int fuelW = 18;
            int fuelH = 12;
            g.FillRectangle(fuelBrush, fuelX, fuelY, fuelW, fuelH);
            g.DrawRectangle(blackPen, fuelX, fuelY, fuelW, fuelH);
            g.FillEllipse(Brushes.Black, fuelX + 6, fuelY + 3, 6, 6);
        }
    }
}