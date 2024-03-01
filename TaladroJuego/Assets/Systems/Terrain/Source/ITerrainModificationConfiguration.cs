namespace TerrainSystem
{
    public interface ITerrainModificationConfiguration
    {
        float Radius { get; }
        float Strength { get; }
        float Falloff { get; }
    }
}