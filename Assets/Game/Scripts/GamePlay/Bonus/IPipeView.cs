public interface IPipeView
{
    PipeType PipeType { get; }
    bool IsDirty { get; }
    void DestroyPipe();

    public bool IsFinishPipe { get; }
}