using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameEvent  {

    // Use this for initialization
    public const string CHARACTER_INTERACTIONS = "CHARACTER_INTERACTIONS";

    public const string PANEL_BACK = "PANEL_BACK";
    public const string PANEL_OPEN = "PANEL_OPEN";
    public const string SINGLE_SELECT = "SINGLE_SELECT";
    public const string SWITCH_SCENCE = "SWITCH_SCENCE";
    public const string SWITCH_SCENCE_FINISHAED = "SWITCH_SCENCE_FINISHAED";
    public const string START_MATCH = "START_MATCH";
    public const string END_MATCH = "END_MATCH";


    public static Dictionary<string, GameEventTriggerType> GameEventDic = new Dictionary<string, GameEventTriggerType>() ;

    public static void InitDic()
    {
        GameEventDic.Add(CHARACTER_INTERACTIONS, GameEventTriggerType.CHARACTER_INTERACTIONS);
        //GameEventDic.Add((int)GameEventTriggerType.PANEL_BACK, PANEL_BACK);
        //GameEventDic.Add((int)GameEventTriggerType.PANEL_OPEN, PANEL_OPEN);
        //GameEventDic.Add((int)GameEventTriggerType.SINGLE_SELECT, SINGLE_SELECT);
        //GameEventDic.Add((int)GameEventTriggerType.SWITCH_SCENCE, SWITCH_SCENCE);
        //GameEventDic.Add((int)GameEventTriggerType.SWITCH_SCENCE_FINISHAED, SWITCH_SCENCE_FINISHAED);
        //GameEventDic.Add((int)GameEventTriggerType.START_MATCH, START_MATCH);
        //GameEventDic.Add((int)GameEventTriggerType.END_MATCH, END_MATCH);
        GameEventDic.Add( PANEL_BACK, GameEventTriggerType.PANEL_BACK);
        GameEventDic.Add( PANEL_OPEN, GameEventTriggerType.PANEL_OPEN);
        GameEventDic.Add( SINGLE_SELECT, GameEventTriggerType.SINGLE_SELECT);
        GameEventDic.Add( SWITCH_SCENCE, GameEventTriggerType.SWITCH_SCENCE);
        GameEventDic.Add( SWITCH_SCENCE_FINISHAED, GameEventTriggerType.SWITCH_SCENCE_FINISHAED);
        GameEventDic.Add( START_MATCH, GameEventTriggerType.START_MATCH);
        GameEventDic.Add( END_MATCH, GameEventTriggerType.END_MATCH);
    }
}


