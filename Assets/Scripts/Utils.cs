using UnityEngine;

namespace Game.Utils
{
    public static class Utils
    {
        public static Vector3 GetRandomPosition() { return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized; }
    }
    
}