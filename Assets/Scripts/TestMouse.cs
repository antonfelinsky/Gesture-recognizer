
using System.Collections.Generic;
using UnityEngine;
using System;

namespace MurkaTestTask.View
{

    [RequireComponent(typeof (LineRenderer))]
    public class TestMouse : MonoBehaviour
    {
        #region Fields

        private int lineCount = 0;
        private LineRenderer lineRenderer;
        private Camera thisCamera;
        private Vector3[] initialPositions;
        private Vector3 lastPos = Vector3.one*float.MaxValue;
        [SerializeField] private float startWidth = 5.0f;
        [SerializeField] private float endWidth = 5.0f;
        [SerializeField] private float threshold = 0.001f;
        private List<Vector3> linePoints = new List<Vector3>();

        #endregion

        #region UnityEvents

        void OnEnable()
        {
            lineRenderer.enabled = true;
        }

        void OnDisable()
        {
            linePoints.Clear();
            UpdateLine();
        }

        void Awake()
        {
            thisCamera = Camera.main;
            lineRenderer = GetComponent<LineRenderer>();
        }

        void Update()
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = thisCamera.nearClipPlane;
            Vector3 mouseWorld = thisCamera.ScreenToWorldPoint(mousePos);

            float dist = Vector3.Distance(lastPos, mouseWorld);
            if (dist <= threshold)
                return;

            lastPos = mouseWorld;
            if (linePoints == null)
                linePoints = new List<Vector3>();
            linePoints.Add(mouseWorld);

            UpdateLine();
        }

        #endregion

        #region Methods

        void UpdateLine()
        {
            lineRenderer.SetWidth(startWidth, endWidth);
            lineRenderer.SetVertexCount(linePoints.Count);

            for (int i = lineCount; i < linePoints.Count; i++)
            {
                lineRenderer.SetPosition(i, linePoints[i]);
            }
            lineCount = linePoints.Count;
        }

        #endregion
    }
}