using Godot;
using GodotTemplate.Presentation.Utils;

[SceneReference("Game.tscn")]
public partial class Game
{
    [Signal]
    public delegate void LevelPassed();
    
    [Signal]
    public delegate void ExitClick();
    
    [Export]
    public bool NextLevelVisible;

    public override void _Ready()
    {
        base._Ready();
        this.FillMembers();

        this.achievementNotifications.UnlockAchievement("MyFirstAchievement");
        this.exit.Connect(CommonSignals.Pressed, this, nameof(ExitClicked));
        this.passed.Connect(CommonSignals.Pressed, this, nameof(PassedClicked));
    }

    private void ExitClicked()
    {
        this.EmitSignal(nameof(ExitClick));
    }

    private void PassedClicked()
    {
        this.EmitSignal(nameof(LevelPassed));
    }
}
