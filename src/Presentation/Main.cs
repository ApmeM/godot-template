using Godot;
using GodotTemplate.Presentation.Utils;

[SceneReference("Main.tscn")]
public partial class Main
{
    public override void _Ready()
    {
        base._Ready();
        this.FillMembers();

        // For debug purposes all achievements can be reset
        // this.di.localAchievementRepository.ResetAchievements();
        this.achievementsButton.Connect(CommonSignals.Pressed, this, nameof(AchievementsButtonPressed));
        this.ratingButton.Connect(CommonSignals.Pressed, this, nameof(RatingButtonPressed));
        foreach (LevelButton b in this.GetTree().GetNodesInGroup(Groups.LevelButton))
        {
            b.Connect(CommonSignals.Pressed, this, nameof(GameStartButtonPressed), new Godot.Collections.Array { b });
        }
    }

    private void GameStartButtonPressed(LevelButton button)
    {
        this.gameContainer.ClearChildren();
        var game = button.GameToStart.Instance<Game>();
        this.gameContainer.AddChild(game);
        game.Connect(nameof(Game.LevelPassed), this, nameof(LevelPassed), new Godot.Collections.Array(button));
        game.Connect(nameof(Game.ExitClick), this, nameof(ExitGameClicked), new Godot.Collections.Array(true, button));
        this.gameContainer.Visible = true;
        this.menuLayer.Visible = false;
        game.NextLevelVisible = button.NextLevelButton != null && !button.NextLevelButton.IsEmpty();
    }

    private void LevelPassed(LevelButton button)
    {
        var nextLevel = button.GetNextLevel();
        if (nextLevel == null)
        {
            return;
        }
        nextLevel.Visible = true;
    }

    private void ExitGameClicked(bool openNextLevel, LevelButton button)
    {
        this.gameContainer.ClearChildren();
        this.gameContainer.Visible = false;
        this.menuLayer.Visible = true;

        var nextLevel = button.GetNextLevel();
        if (openNextLevel && nextLevel != null)
        {
            GameStartButtonPressed(nextLevel);
        }
    }

    private void AchievementsButtonPressed()
    {
        {
            // See achievements definitions in gd-achievements/achievements.json
            this.achievementList.ReloadList();
            this.customPopup.Show();
        }
    }


    private void RatingButtonPressed()
    {
        if (this.googlePlay.IsEnabled())
        {
            this.googlePlay.leaderboardsShowAll();
        }
        else
        {
            GD.Print("Not implemented");
        }
    }
}
