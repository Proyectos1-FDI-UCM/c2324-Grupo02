namespace TerrainSystem.Data
{
    public interface ITerrainModificationConfiguration
    {
        float Radius { get; }
        float Strength { get; }
        float Falloff { get; }

        uint Type { get; }
    }
}