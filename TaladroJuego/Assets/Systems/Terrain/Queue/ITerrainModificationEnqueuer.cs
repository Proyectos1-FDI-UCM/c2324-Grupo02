namespace TerrainSystem.Queue
{
    public interface ITerrainModificationEnqueuer<in TSource>
    {
        bool Enqueue(TSource modification);
        bool Dequeue(TSource modification);
    }
}