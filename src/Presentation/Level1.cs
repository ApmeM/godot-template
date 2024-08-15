using System;
using Godot;
using GodotTemplate.LevelSelector;

namespace GodotTemplate.Levels
{
    public class Level1 : ILevelToSelect
    {
        private readonly Random r = new Random();
        public string Name => "Level 1";

        public void Init(Game game)
        {
            for (var i = 0; i < 3; i++)
            {
                game.AddChild(new Sprite
                    {
                        Texture = ResourceLoader.Load<Texture>("res://Presentation/assets/Example_sprite.png"),
                        Position = new Vector2(r.Next(400), r.Next(400))
                    });
            }
        }
    }
}