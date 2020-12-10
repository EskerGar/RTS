using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "Settings/MapSettings", fileName = "MapSettings")]
    public class MapSettings : ScriptableObject
    {
        [SerializeField] private int width;
        [SerializeField] private int height;

        public int Width => width;

        public int Height => height;
    }
}