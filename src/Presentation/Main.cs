using System;
using System.Collections.Generic;
using Godot;
using GodotAnalysers;
using GodotTemplate.Levels;
using GodotTemplate.LevelSelector;
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
        this.levelSelector.SetLevels(new List<ILevelToSelect> { new Level1() });
        this.levelSelector.Connect(nameof(LevelSelector.StartGame), this, nameof(LevelSelected));
    }

    private void LevelSelected(int gameId)
    {
        this.game.Visible = true;
        this.levelSelector.Visible = false;
        this.menuLayer.Visible = false;
        this.levelSelector.GetLevel(gameId).Init(this.game);
    }

    private void AchievementsButtonPressed()
    {
        this.levelSelector.Visible = !this.levelSelector.Visible;
        this.achievementsListContainer.Visible = !this.achievementsListContainer.Visible;

        // See achievements definitions in gd-achievements/achievements.json
        this.achievementList.ReloadList();
    }
}
