using UnityEngine;

[CreateAssetMenu(fileName = "ScoreUpdateChannelSO", menuName = "ScriptableObjects/Channels/InGame/ScoreUpdateChannel", order = 1)]
public class ScoreUpdateChannelSO : EventChannelOneArg<int> {}