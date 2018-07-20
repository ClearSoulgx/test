namespace UnityEngine.EventSystems
{
    /// <summary>
    /// This class is capable of triggering one or more remote functions from a specified event.
    /// Usage: Attach it to an object with a collider, or to a GUI Graphic of your choice.
    /// NOTE: Doing this will make this object intercept ALL events, and no event bubbling will occur from this object!
    /// </summary>

    public enum GameEventTriggerType
    {
       CHARACTER_INTERACTIONS = 0 ,

        PANEL_BACK,
      PANEL_OPEN ,
      SINGLE_SELECT  ,
      SWITCH_SCENCE   ,
      SWITCH_SCENCE_FINISHAED  ,
      START_MATCH  ,
      END_MATCH 
}

  
}