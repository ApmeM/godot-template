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

        this.startGame.Connect(CommonSignals.Pressed, this, nameof(LevelSelected));
        this.achievementsButton.Connect(CommonSignals.Pressed, this, nameof(AchievementsButtonPressed));
    }

    private void LevelSelected()
    {
        this.game.Visible = true;
        this.menuLayer.Visible = false;
    }

    private void AchievementsButtonPressed()
    {
        // See achievements definitions in gd-achievements/achievements.json
        this.achievementList.ReloadList();
        this.customPopup.Show();
    }
}
