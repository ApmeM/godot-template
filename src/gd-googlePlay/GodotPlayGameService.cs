using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Godot;
using GodotTemplate.Achievements;
using Newtonsoft.Json;

[SceneReference("GodotPlayGameService.tscn")]
public partial class GodotPlayGameService : IAchievementRepository
{
    ///  <summary>
    /// Represents the Android plugin for the GodotPlayGameService.
    /// Tried alternatives: 
    /// 1. https://github.com/cgisca/PGSGP
    /// JNI DETECTED ERROR IN APPLICATION: JNI CallVoidMethodA called with pending exception java.lang.ClassCastException: org.godotengine.godot.Godot cannot be cast to android.app.Activity
    ///   at void io.cgisca.godot.gpgs.PlayGameServicesGodot.initialize(boolean, boolean, java.lang.String) (PlayGameServicesGodot.kt:185)
    ///   at void io.cgisca.godot.gpgs.PlayGameServicesGodot.init(boolean) (PlayGameServicesGodot.kt:168)
    ///   
    /// 2. https://github.com/Kopfenheim/godot-gpgs
    /// Too complext installation (Recompile the Godot source for android).
    /// 
    /// 3. https://github.com/godot-sdk-integrations/godot-play-game-services
    /// For godot 4
    /// 
    /// 4. https://github.com/Iakobs/godot-google-play-game-services-android-plugin
    /// This implementation
    /// 
    /// </summary>
    public Godot.Object Plugin { get; private set; }
    const string plugin_name = "GodotGooglePlayGameServices";

    public override void _Ready()
    {
        if (Plugin == null)
        {
            // if this pat is not working - check godot export settings and enable plugin.
            if (Engine.HasSingleton(plugin_name))
            {
                // For debug connection do:
                // adb shell
                // logcat | grep "APP NOT CORRECTLY CONFIGURED TO USE GOOGLE PLAY GAME SERVICES"
                Plugin = Engine.GetSingleton(plugin_name);
                initialize();
            }
            else
            {
                GD.PrintErr("No plugin found.");
            }
        }
    }

    public bool IsEnabled()
    {
        return Plugin != null;
    }

    // Plugin methods are taken from https://github.com/Iakobs/godot-google-play-game-services-android-plugin/blob/main/app/src/main/java/com/jacobibanez/godot/gpgs/GodotGooglePlayGameServices.kt
    // Plugin signals are taken from https://github.com/Iakobs/godot-google-play-game-services-android-plugin/blob/main/app/src/main/java/com/jacobibanez/godot/gpgs/signals/Signals.kt

    #region Initialize
    public void initialize()
    {
        Plugin.Call("initialize");
    }
    #endregion

    #region Achievements

    public void achievementsIncrement(string achievementId, int amount, bool immediate)
    {
        Plugin.Call("achievementsIncrement", achievementId, amount, immediate);
    }
    public void achievementsLoad(bool forceReload)
    {
        Plugin.Call("achievementsLoad", forceReload);
    }
    public void achievementsReveal(string achievementId, bool immediate)
    {
        Plugin.Call("achievementsReveal", achievementId, immediate);
    }
    public void achievementsSetSteps(string achievementId, int amount, bool immediate)
    {
        Plugin.Call("achievementsSetSteps", achievementId, amount, immediate);
    }
    public void achievementsShow()
    {
        Plugin.Call("achievementsShow");
    }
    public void achievementsUnlock(string achievementId, bool immediate)
    {
        Plugin.Call("achievementsUnlock", achievementId, immediate);
    }

    #endregion

    #region Events

    public void eventsIncrement(string eventId, int amount)
    {
        Plugin.Call("eventsIncrement", eventId, amount);
    }

    public void eventsLoad(bool forceReload)
    {
        Plugin.Call("eventsLoad", forceReload);
    }

    public void eventsLoadByIds(bool forceReload, string[] eventIds)
    {
        // Type might be wrong, need to check
        Plugin.Call("eventsLoadByIds", forceReload, eventIds);
    }

    #endregion

    #region Leaderboards

    public void leaderboardsShowAll()
    {
        Plugin.Call("leaderboardsShowAll");
    }

    public void leaderboardsShow(string leaderboardId)
    {
        Plugin.Call("leaderboardsShow", leaderboardId);
    }

    public void leaderboardsShowForTimeSpan(string leaderboardId, int timeSpan)
    {
        Plugin.Call("leaderboardsShowForTimeSpan", leaderboardId, timeSpan);
    }

    public void leaderboardsShowForTimeSpanAndCollection(string leaderboardId, int timeSpan, int collection)
    {
        Plugin.Call("leaderboardsShowForTimeSpanAndCollection", leaderboardId, timeSpan, collection);
    }

    public void leaderboardsSubmitScore(string leaderboardId, float score)
    {
        Plugin.Call("leaderboardsSubmitScore", leaderboardId, score);
    }

    public void leaderboardsLoadPlayerScore(string leaderboardId, int timeSpan, int collection)
    {
        Plugin.Call("leaderboardsLoadPlayerScore", leaderboardId, timeSpan, collection);
    }

    public void leaderboardsLoadAll(bool forceReload)
    {
        Plugin.Call("leaderboardsLoadAll", forceReload);
    }

    public void leaderboardsLoad(string leaderboardId, bool forceReload)
    {
        Plugin.Call("leaderboardsLoad", leaderboardId, forceReload);
    }

    public void leaderboardsLoadPlayerCenteredScores(string leaderboardId, int timeSpan, int collection, int maxResults, bool forceReload)
    {
        Plugin.Call("leaderboardsLoadPlayerCenteredScores", leaderboardId, timeSpan, collection, maxResults, forceReload);
    }

    public void leaderboardsLoadTopScores(string leaderboardId, int timeSpan, int collection, int maxResults, bool forceReload)
    {
        Plugin.Call("leaderboardsLoadTopScores", leaderboardId, timeSpan, collection, maxResults, forceReload);
    }

    #endregion

    #region Players

    public void playersCompareProfile(string otherPlayerId)
    {
        Plugin.Call("playersCompareProfile", otherPlayerId);
    }

    public void playersCompareProfileWithAlternativeNameHints(string otherPlayerId, string otherPlayerInGameName, string currentPlayerInGameName)
    {
        Plugin.Call("playersCompareProfileWithAlternativeNameHints", otherPlayerId, otherPlayerInGameName, currentPlayerInGameName);
    }

    public void playersLoadCurrent(bool forceReload)
    {
        Plugin.Call("playersLoadCurrent", forceReload);
    }

    public void playersLoadFriends(int pageSize, bool forceReload, bool askForPermission)
    {
        Plugin.Call("playersLoadFriends", pageSize, forceReload, askForPermission);
    }

    public void playersSearch()
    {
        Plugin.Call("playersSearch");
    }

    #endregion

    #region SignIn 

    public void signInIsAuthenticated()
    {
        Plugin.Call("signInIsAuthenticated");
    }

    public void signInRequestServerSideAccess(string serverClientId, bool forceRefreshToken)
    {
        Plugin.Call("signInRequestServerSideAccess", serverClientId, forceRefreshToken);
    }

    public void signInShowPopup()
    {
        Plugin.Call("signInShowPopup");
    }
    #endregion

    #region Snapshots
    public void snapshotsLoadGame(string fileName)
    {
        Plugin.Call("snapshotsLoadGame", fileName);
    }

    public void snapshotsSaveGame(string fileName, string description, byte[] saveData, int playedTimeMillis, int progressValue)
    {
        // ByteArray might be of a different type
        Plugin.Call("snapshotsSaveGame", fileName, description, saveData, playedTimeMillis, progressValue);
    }

    public void snapshotsShowSavedGames(string title, bool allowAddButton, bool allowDelete, int maxSnapshots)
    {
        Plugin.Call("snapshotsShowSavedGames", title, allowAddButton, allowDelete, maxSnapshots);
    }

    #endregion

    #region IAchievementRepository

    // Taken from https://developers.google.com/android/reference/com/google/android/gms/games/achievement/Achievement
    private class AchievementFromPlay
    {
        [JsonProperty("lastUpdatedTimestamp")]
        public long LastUpdatedTimestampMilliseconds;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("description")]
        public string Description;

        [JsonProperty("totalSteps")]
        public int TotalSteps;

        [JsonProperty("achievementId")]
        public string AchievementId;

        [JsonProperty("currentSteps")]
        public int CurrentSteps;

        [JsonProperty("state")]
        public string State; //"STATE_REVEALED",  "STATE_HIDDEN", "STATE_UNLOCKED"

        [JsonProperty("unlockedImageUri")]
        public string UnlockedImageUri;

        [JsonProperty("revealedImageUri")]
        public string RevealedImageUri;

        [JsonProperty("type")]
        public string Type; //"TYPE_STANDARD",  "TYPE_INCREMENTAL"

        [JsonProperty("xpValue")]
        public int XP;
    }

    public bool ProgressAchievement(string key, int progress)
    {
        this.achievementsIncrement(key, progress, true);
        return false; // Google popup will appear, no need to show custom.
    }

    public bool UnlockAchievement(string key)
    {
        this.achievementsUnlock(key, true);
        return false; // Google popup will appear, no need to show custom.
    }

    public async Task<Achievement> GetAchievement(string key)
    {
        this.achievementsLoad(false);
        var result = await this.ToSignal(Plugin, "achievementsLoaded");
        var json = result[0]?.ToString();
        return JsonConvert.DeserializeObject<List<AchievementFromPlay>>(json)
            .Where(a => a.AchievementId == key)
            .Select(ToAchievement)
            .FirstOrDefault();
    }

    public async Task<IEnumerable<Achievement>> GetForList()
    {
        this.achievementsLoad(false);
        var result = await this.ToSignal(Plugin, "achievementsLoaded");
        var json = result[0]?.ToString();
        return JsonConvert.DeserializeObject<List<AchievementFromPlay>>(json).Select(ToAchievement);
    }

    private Achievement ToAchievement(AchievementFromPlay achievement)
    {
        return new Achievement
        {
            Achieved = achievement.State == "STATE_UNLOCKED" ? true : false,
            CurrentProgress = achievement.CurrentSteps,
            UnlockDate = new DateTime(1970, 1, 1).AddMilliseconds(achievement.LastUpdatedTimestampMilliseconds),
            Goal = achievement.TotalSteps,
            Name = achievement.Name,
            IconPath = achievement.UnlockedImageUri,
            Description = achievement.Description
        };
    }
    #endregion
}


