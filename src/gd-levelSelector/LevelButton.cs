using Godot;

[SceneReference("LevelButton.tscn")]
[Tool]
public partial class LevelButton
{
    [Export]
    public PackedScene GameToStart;

    [Export]
    public NodePath NextLevelButton;

    public override void _Ready()
    {
        base._Ready();
        this.AddToGroup(Groups.LevelButton);
    }

    public LevelButton GetNextLevel()
    {
        if (this.NextLevelButton.IsEmpty())
        {
            return null;
        }

        return this.GetNode<LevelButton>(this.NextLevelButton);
    }
}
