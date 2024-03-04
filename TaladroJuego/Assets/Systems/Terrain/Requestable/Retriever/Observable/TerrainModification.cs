namespace TerrainSystem.Requestable.Retriever.Observable
{
    public readonly struct TerrainModification
    {
        public readonly uint terrainType;
        public readonly float amount;

        public TerrainModification(uint terrainType, float amount)
        {
            this.terrainType = terrainType;
            this.amount = amount;
        }
    }
}