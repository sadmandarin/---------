using UnityEngine;

public class EffectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _effect;
    [SerializeField] private Vector3 _effectOffset;

    private void Update()
    {
        SpawnEffct();
    }

    private void SpawnEffct()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 worldPositionAdjusted = worldPosition + _effectOffset;
            RaycastHit rayHit;
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(mouseRay, out rayHit))
            {
                GameObject newParticleEffect = Instantiate(_effect, rayHit.point, _effect.transform.rotation);
            }

        }
    }
}
