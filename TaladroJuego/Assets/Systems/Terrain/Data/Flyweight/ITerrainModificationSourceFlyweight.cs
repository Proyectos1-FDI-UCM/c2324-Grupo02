namespace TerrainSystem.Data.Flyweight
{
    public interface ITerrainModificationSourceFlyweight<out TModificationSource, in TFromSource>
    {
        TModificationSource CreateFrom<UFromSource>(UFromSource terrainModificationSource)
            where UFromSource : ITerrainModificationSource, TFromSource;
    }

    public interface ITerrainModificationSourceFlyweight<out TModificationSource> :
        ITerrainModificationSourceFlyweight<TModificationSource, object>
    {
        TModificationSource ITerrainModificationSourceFlyweight<TModificationSource, object>.CreateFrom<UFromSource>(UFromSource terrainModificationSource) =>
            Create();
        TModificationSource Create();
    }
}