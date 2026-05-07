using UnityEngine;

public class SpawnOnImpact : MonoBehaviour
{
    [SerializeField] private GameObject _objectToSpawn;
    [SerializeField] private Vector2 _spawnOffset = Vector2.zero;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        ContactPoint2D contact = collision.GetContact(0);
        Vector3 spawnPosition = contact.point + _spawnOffset;
        spawnPosition.z = transform.position.z;
        Instantiate(_objectToSpawn, spawnPosition, Quaternion.identity);
    }
}
