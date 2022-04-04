using UnityEngine;

[CreateAssetMenu]
public class LevelConfig : ScriptableObject
{
    [SerializeField] private Vector3 playerPosition;
    [SerializeField] private int levelIndex;

    public int LevelIndex => levelIndex;

    public Vector3 PlayerPosition => playerPosition;
}