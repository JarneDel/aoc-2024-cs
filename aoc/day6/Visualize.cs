using SkiaSharp;
using System.IO;

namespace aoc.day6
{
    public class MapVisualizer
    {
        private readonly Entity[,] _map;

        public MapVisualizer(Entity[,] map)
        {
            _map = map;
        }

        public void Save(string filePath)
        {
            int cellSize = 10;
            int width = _map.GetLength(0) * cellSize;
            int height = _map.GetLength(1) * cellSize;

            var info = new SKImageInfo(width, height, SKColorType.Rgba8888, SKAlphaType.Premul);
            using var surface = SKSurface.Create(info);
            var canvas = surface.Canvas;
            canvas.Clear(SKColors.White);

            for (int x = 0; x < _map.GetLength(0); x++)
            {
                for (int y = 0; y < _map.GetLength(1); y++)
                {
                    SKColor color = GetColorForEntity(_map[x, y]);
                    using var paint = new SKPaint
                    {
                        Color = color,
                        Style = SKPaintStyle.Fill,
                        IsAntialias = false
                    };

                    var rect = SKRect.Create(x * cellSize, y * cellSize, cellSize, cellSize);
                    canvas.DrawRect(rect, paint);
                }
            }

            canvas.Flush();

            using var image = surface.Snapshot();
            using var data = image.Encode(SKEncodedImageFormat.Jpeg, 100);
            using var stream = File.OpenWrite(filePath);
            data.SaveTo(stream);
        }

        private SKColor GetColorForEntity(Entity entity)
        {
            return entity switch
            {
                Entity.Empty => new SKColor(255, 255, 255, 255),
                Entity.Obstacle => new SKColor(0, 0, 0, 255),
                Entity.Player => new SKColor(0, 0, 255, 255),
                Entity.Visited => new SKColor(128, 128, 128, 255),
                _ => new SKColor(255, 0, 0, 255)
            };
        }
    }
}