using UnityEngine;

namespace GridBoard
{
    internal class HeroTopInfoController : MonoBehaviour
    {
        [SerializeField] private GridInputManager _input;
        [SerializeField] private HeroTopInfo _heroTopInfo;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private Camera _canvasCamera;
        private void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                var ray = _input.GetRayUnderMouse();
                if (Physics.Raycast(ray, out RaycastHit hit, 100f, _layerMask))
                {
                    Debug.Log("HeroUnderMouse");
                    var mousePosition = Input.mousePosition;
                    mousePosition.z = _canvasCamera.transform.position.z * -1f;

                    _heroTopInfo.ToggleView(true);
                    _heroTopInfo.Move(_canvasCamera.ScreenToWorldPoint(mousePosition));
                }
                else
                {
                    Debug.Log("Hero Not Under Mouse");
                    _heroTopInfo.ToggleView(false);
                }
            }
        }
    }
}
