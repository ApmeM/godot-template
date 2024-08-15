namespace GodotTemplate.LevelSelector
{
    public interface ILevelToSelect
    {
        string Name { get; }

        void Init(Game game);
    }
}