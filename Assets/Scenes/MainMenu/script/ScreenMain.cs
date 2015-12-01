
public class ScreenMain : ScreenSelectables
{
    public FxStreak streakL, streakR;

    public override void Activate()
    {
        base.Activate();
        streakL.Activate(-3);
        streakR.Activate(3);
    }

    public override void Deactivate()
    {
        base.Deactivate();
        streakL.Deactivate();
        streakR.Deactivate();
    }
}
