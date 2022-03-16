using System;

public class MoveActions
{
    public Action Using { get; set; }
    public Action NotUsing { get; set; }
    public Action UseStarted { get; set; }
    public Action UseStopped { get; set; }

    public Action Stepped { get; set; }
    public Action OnLeftStep { get; set; }
    public Action OnRightStep { get; set; }
}