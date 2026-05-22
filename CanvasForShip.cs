using System;
using System.Drawing;

namespace ProjectShip
{
    public class CanvasForShip
    {
        private DrawingShip _drawingShip;
        private int? _canvasWidth;
        private int? _canvasHeight;

        public void SetPictureSize(int width, int height)
        {
            _canvasWidth = width;
            _canvasHeight = height;
        }

        public bool InsertShip(DrawingShip ship)
        {
            if (!_canvasWidth.HasValue || !_canvasHeight.HasValue)
                return false;

            if (ship.DrawingWidth > _canvasWidth.Value || ship.DrawingHeight > _canvasHeight.Value)
                return false;

            _drawingShip = ship;
            return true;
        }

        public void SetShipPosition(int x, int y)
        {
            if (!_canvasWidth.HasValue || !_canvasHeight.HasValue || _drawingShip == null)
                return;

            int width = _drawingShip.DrawingWidth;
            int height = _drawingShip.DrawingHeight;

            if (x < 0)
                x = 0;
            else if (x + width > _canvasWidth.Value)
                x = _canvasWidth.Value - width;

            if (y < 0)
                y = 0;
            else if (y + height > _canvasHeight.Value)
                y = _canvasHeight.Value - height;

            _drawingShip.SetPosition(x, y);
        }

        public bool MoveTransport(DirectionType direction)
        {
            if (!_canvasWidth.HasValue || !_canvasHeight.HasValue ||
                _drawingShip == null ||
                !_drawingShip.PosX.HasValue || !_drawingShip.PosY.HasValue ||
                !_drawingShip.ShipStep.HasValue)
                return false;

            int step = (int)_drawingShip.ShipStep.Value;
            int newX = _drawingShip.PosX.Value;
            int newY = _drawingShip.PosY.Value;
            int width = _drawingShip.DrawingWidth;
            int height = _drawingShip.DrawingHeight;

            switch (direction)
            {
                case DirectionType.Left:
                    if (newX - step >= 0)
                    {
                        _drawingShip.MoveLeft();
                        return true;
                    }
                    break;
                case DirectionType.Up:
                    if (newY - step >= 0)
                    {
                        _drawingShip.MoveUp();
                        return true;
                    }
                    break;
                case DirectionType.Right:
                    if (newX + step + width <= _canvasWidth.Value)
                    {
                        _drawingShip.MoveRight();
                        return true;
                    }
                    break;
                case DirectionType.Down:
                    if (newY + step + height <= _canvasHeight.Value)
                    {
                        _drawingShip.MoveDown();
                        return true;
                    }
                    break;
            }
            return false;
        }

        public Bitmap DrawCanvas()
        {
            if (!_canvasWidth.HasValue || !_canvasHeight.HasValue)
                return null;

            Bitmap bmp = new Bitmap(_canvasWidth.Value, _canvasHeight.Value);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.LightBlue);
                if (_drawingShip != null)
                    _drawingShip.DrawTransport(g);
            }
            return bmp;
        }
    }
}