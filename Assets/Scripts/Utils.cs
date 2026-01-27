using UnityEngine;

namespace Game.Utils
{
    public static class Utils
    {
        public static Vector3 GetRandomPosition() { return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized; }

        public static Quaternion GetRotationToMouse2D(Vector3 objectPosition, Camera camera = null,
            float offsetAngle = 0f)
        {
            camera ??= Camera.main;
            
            Vector3 mouseWorldPosition = GameInput.Instance.GetWorldMousePosition();
            mouseWorldPosition.z = objectPosition.z;
            
            Vector2 direction = (mouseWorldPosition - objectPosition).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg +  offsetAngle;
            return Quaternion.Euler(0, 0, angle);
        }
    }
    
}