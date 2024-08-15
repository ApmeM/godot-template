using Godot;
using GodotAnalysers;

[SceneReference("Game.tscn")]
public partial class Game
{
    public override void _Ready()
    {
        base._Ready();
        this.FillMembers();

        this.achievementNotifications.UnlockAchievement("MyFirstAchievement");
    }
}
