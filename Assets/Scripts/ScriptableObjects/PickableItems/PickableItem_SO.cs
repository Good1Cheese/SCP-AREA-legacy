public abstract class PickableItem_SO : Item_SO
{
    protected PlayerInstaller PlayerInstaller { get; set; }

    public virtual void GetDependencies(PlayerInstaller playerInstaller)
    {
        PlayerInstaller = PlayerInstaller;
    }

    public abstract void Use();

    public virtual bool ShouldItemNotBeUsed() => false;
}

