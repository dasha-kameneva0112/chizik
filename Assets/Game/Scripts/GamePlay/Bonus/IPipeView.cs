public interface IPipeView
{
    bool IsDirty { get; }
    void DestroyPipe();

    public bool IsFinishPipe { get; }
}