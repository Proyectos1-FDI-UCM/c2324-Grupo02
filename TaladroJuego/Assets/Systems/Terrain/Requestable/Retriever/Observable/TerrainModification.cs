namespace TerrainSystem.Requestable.Retriever.Observable
{
    public readonly struct TerrainModification
    {
        public const int SIZE_OF = (sizeof(uint) * 2) + sizeof(float);

        public readonly uint terrainType;
        public readonly uint modificationSourceIndex;
        public readonly float amount;

        public TerrainModification(uint terrainType, uint modificationSourceIndex, float amount)
        {
            this.terrainType = terrainType;
            this.modificationSourceIndex = modificationSourceIndex;
            this.amount = amount;
        }

        public TerrainModification WithTerrainType(uint terrainType) =>
            new TerrainModification(terrainType, modificationSourceIndex, amount);

        public TerrainModification WithModificationSourceIndex(uint modificationSourceIndex) =>
            new TerrainModification(terrainType, modificationSourceIndex, amount);

        public TerrainModification WithAmount(float amount) =>
            new TerrainModification(terrainType, modificationSourceIndex, amount);
    }
}