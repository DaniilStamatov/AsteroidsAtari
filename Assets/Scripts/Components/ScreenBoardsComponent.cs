using UnityEngine;

namespace Assets.Scripts.Components
{
    public class ScreenBoardsComponent : MonoBehaviour
    {
        [SerializeField] private string _tag;

        private bool _isiVisible = true;

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag(_tag))
            {
                _isiVisible = false;
            }
        }

        private void Update()
        {
            var previousPosition = transform.position;
            if (!_isiVisible)
            {
                Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);
                Vector3 currentPosition = transform.position;

                if (viewportPosition.x < 0 || viewportPosition.x > 1)
                {
                    Vector3 screenEdge;
                    if (viewportPosition.x > 1)
                    {
                        screenEdge = Camera.main.ScreenToWorldPoint(Vector3.zero);
                    }
                    else
                    {
                        screenEdge = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight, 1));
                    }
                    currentPosition.x = screenEdge.x;
                }
                if (viewportPosition.y < 0 || viewportPosition.y > 1)
                {
                    Vector3 screenEdge;
                    if (viewportPosition.y > 1)
                    {
                        screenEdge = Camera.main.ScreenToWorldPoint(Vector3.zero);
                    }
                    else
                    {
                        screenEdge = Camera.main.ScreenToWorldPoint(new Vector3(0, Camera.main.pixelHeight, 1));
                    }
                    currentPosition.y = screenEdge.y;
                }
                transform.position = currentPosition;
                transform.position = new Vector3(transform.position.x, transform.position.y, previousPosition.z);
            }
        }
    }
}

