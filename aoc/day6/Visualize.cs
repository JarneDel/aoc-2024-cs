using SkiaSharp;

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

            SKImageInfo info = new(width, height, SKColorType.Rgba8888, SKAlphaType.Premul);
            using SKSurface? surface = SKSurface.Create(info);
            SKCanvas? canvas = surface.Canvas;
            canvas.Clear(SKColors.White);

            for (int x = 0; x < _map.GetLength(0); x++)
            {
                for (int y = 0; y < _map.GetLength(1); y++)
                {
                    SKColor color = GetColorForEntity(_map[x, y]);
                    using SKPaint? paint = new()
                    {
                        Color = color,
                        Style = SKPaintStyle.Fill,
                        IsAntialias = false
                    };

                    SKRect rect = SKRect.Create(x * cellSize, y * cellSize, cellSize, cellSize);
                    canvas.DrawRect(rect, paint);
                }
            }

            canvas.Flush();

            using SKImage? image = surface.Snapshot();
            using SKData? data = image.Encode(SKEncodedImageFormat.Jpeg, 100);
            using FileStream? stream = File.OpenWrite(filePath);
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