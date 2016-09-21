using UnityEngine;
using System.Collections;

namespace MurkaTestTask.View
{
    public class MouseFollow : MonoBehaviour
    {

        #region Fields

        [SerializeField] private float distance = 10;
        [SerializeField] private TrailRenderer trail = null;
        [SerializeField] private TestMouse testMouse = null;
        [SerializeField] private LineRenderer lineRenderer = null;

        #endregion

        #region Unity Events

        private void Awake()
        {
            if (lineRenderer != null)
                lineRenderer.enabled = true;
            else
                lineRenderer = GetComponent<LineRenderer>();
        }

        private void Update()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 position = ray.GetPoint(distance);
            transform.position = position;
            if (Input.GetMouseButton(0))
            {
                trail.enabled = true;
                testMouse.enabled = true;

            }
            else
            {
                trail.enabled = false;
                testMouse.enabled = false;
            }
        }

        #endregion

    }
}
