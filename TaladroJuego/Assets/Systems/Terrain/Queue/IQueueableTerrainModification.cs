namespace TerrainSystem.Queue
{
    public interface IQueueableTerrainModification<in TEnqueuer>
    {
        bool AcceptEnqeue<UEnqueuer>(UEnqueuer enqueuer)
            where UEnqueuer : TEnqueuer;
        bool AcceptDequeue<UEnqueuer>(UEnqueuer enqueuer)
            where UEnqueuer : TEnqueuer;
    }
}