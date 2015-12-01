
public class ScreenSettings : ScreenSelectables
{
    public FxStreak streak;

    public override void Activate()
    {
        base.Activate();
        streak.Activate(0);
    }

    public override void Deactivate()
    {
        base.Deactivate();
        streak.Deactivate();
    }
}
