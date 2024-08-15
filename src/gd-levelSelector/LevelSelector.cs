using Godot;
using GodotAnalysers;
using GodotTemplate.LevelSelector;
using GodotTemplate.Presentation.Utils;
using System.Collections.Generic;

[SceneReference("LevelSelector.tscn")]
public partial class LevelSelector
{
    [Signal]
    public delegate void StartGame(int gameId);

    private readonly List<ILevelToSelect> Levels = new List<ILevelToSelect>();

    public void SetLevels(List<ILevelToSelect> levels)
    {
        this.Levels.Clear();
        this.Levels.AddRange(levels);
        this.ReloadLevels();
    }

    public ILevelToSelect GetLevel(int levelId)
    {
        return this.Levels[levelId];
    }

    public override void _Ready()
    {
        base._Ready();
        this.FillMembers();
        this.ReloadLevels();
    }

    private void ReloadLevels()
    {
        this.levelContainer.ClearChildren();
        for (int i = 0; i < this.Levels.Count; i++)
        {
            var level = this.Levels[i];
            var button = (Button)this.templateButton.Duplicate();
            button.Text = level.Name;
            button.Visible = true;
            button.Connect(CommonSignals.Pressed, this, nameof(OnLevelPressed), new Godot.Collections.Array { i });
            this.levelContainer.AddChild(button);
        }
    }

    private void OnLevelPressed(int gameId)
    {
        EmitSignal(nameof(StartGame), gameId);
    }
}
