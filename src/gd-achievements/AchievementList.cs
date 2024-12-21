using Godot;
using GodotTemplate.Achievements;
using GodotTemplate.Presentation.Utils;

[SceneReference("AchievementList.tscn")]
public partial class AchievementList
{
    [Export]
    public PackedScene AchievementNotificationScene;

    [Export]
    public NodePath GodotPlayGameServicePath;

    private IAchievementRepository achievementRepository;

    public override void _Ready()
    {
        base._Ready();
        FillMembers();
    }

    public async void ReloadList()
    {
        this.achievementsContainer.ClearChildren();
        this.loadingContainer.Visible = true;

        var achievements = await GetRepository().GetForList();

        this.loadingContainer.Visible = false;
        foreach (var data in achievements)
        {
            var notification = (AchievementNotification)AchievementNotificationScene.Instance();
            this.achievementsContainer.AddChild(notification);
            notification.SetAchievement(data);
        }
    }

    private IAchievementRepository GetRepository()
    {
        if (this.achievementRepository == null)
        {
            if (this.GodotPlayGameServicePath.IsEmpty())
            {
                this.achievementRepository = this.di.localAchievementRepository;
            }
            else
            {
                var googlePlay = this.GetNode<GodotPlayGameService>(this.GodotPlayGameServicePath);
                if (!googlePlay.IsEnabled())
                {
                    this.achievementRepository = this.di.localAchievementRepository;
                }
                else
                {
                    this.achievementRepository = googlePlay;
                }
            }
        }
        return this.achievementRepository;
    }
}
