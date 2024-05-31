//======= Copyright (c) Valve Corporation, All rights reserved. ===============
//
// Purpose: An area that the player can teleport to
//
//=============================================================================

using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Valve.VR.InteractionSystem
{
    //-------------------------------------------------------------------------
    public class TeleportArea__2 : TeleportMarkerBase
    {
        // Public properties
        public Bounds meshBounds { get; private set; }

        // Private data
        private MeshRenderer areaMesh;
        private Terrain terrain;
        private int tintColorId = 0;
        private Color visibleTintColor = Color.clear;
        private Color highlightedTintColor = Color.clear;
        private Color lockedTintColor = Color.clear;
        private bool highlighted = false;

        public Material areaVisibleMaterial;
        public Material areaLockedMaterial;
        public Material areaHighlightedMaterial;

        //-------------------------------------------------
        public void Awake()
        {
            areaMesh = GetComponent<MeshRenderer>();
            terrain = GetComponent<Terrain>();

#if UNITY_URP
            tintColorId = Shader.PropertyToID("_BaseColor");
#else
            tintColorId = Shader.PropertyToID("_TintColor");
#endif

            CalculateBounds();
        }

        //-------------------------------------------------
        public void Start()
        {
            if (areaMesh != null && areaVisibleMaterial != null && areaHighlightedMaterial != null && areaLockedMaterial != null)
            {
                visibleTintColor = areaVisibleMaterial.GetColor(tintColorId);
                highlightedTintColor = areaHighlightedMaterial.GetColor(tintColorId);
                lockedTintColor = areaLockedMaterial.GetColor(tintColorId);
            }
        }

        //-------------------------------------------------
        public override bool ShouldActivate(Vector3 playerPosition)
        {
            return true;
        }

        //-------------------------------------------------
        public override bool ShouldMovePlayer()
        {
            return true;
        }

        //-------------------------------------------------
        public override void Highlight(bool highlight)
        {
            if (!locked)
            {
                highlighted = highlight;

                if (highlight)
                {
                    SetMaterial(areaHighlightedMaterial);
                }
                else
                {
                    SetMaterial(areaVisibleMaterial);
                }
            }
        }

        //-------------------------------------------------
        public override void SetAlpha(float tintAlpha, float alphaPercent)
        {
            Color tintedColor = GetTintColor();
            tintedColor.a *= alphaPercent;
            SetColor(tintedColor);
        }

        //-------------------------------------------------
        public override void UpdateVisuals()
        {
            if (locked)
            {
                SetMaterial(areaLockedMaterial);
            }
            else
            {
                SetMaterial(areaVisibleMaterial);
            }
        }

        //-------------------------------------------------
        private bool CalculateBounds()
        {
            if (terrain != null)
            {
                meshBounds = new Bounds(terrain.transform.position, terrain.terrainData.size);
                return true;
            }

            MeshFilter meshFilter = GetComponent<MeshFilter>();
            if (meshFilter == null)
            {
                return false;
            }

            Mesh mesh = meshFilter.sharedMesh;
            if (mesh == null)
            {
                return false;
            }

            meshBounds = mesh.bounds;
            return true;
        }

        //-------------------------------------------------
        private Color GetTintColor()
        {
            if (locked)
            {
                return lockedTintColor;
            }
            else
            {
                if (highlighted)
                {
                    return highlightedTintColor;
                }
                else
                {
                    return visibleTintColor;
                }
            }
        }

        //-------------------------------------------------
        private void SetMaterial(Material material)
        {
            if (material == null)
                return;

            if (areaMesh != null)
            {
                areaMesh.material = material;
            }
            else if (terrain != null)
            {
                terrain.materialTemplate = material;
            }
        }

        //-------------------------------------------------
        private void SetColor(Color color)
        {
            if (areaMesh != null && areaMesh.material != null)
            {
                areaMesh.material.SetColor(tintColorId, color);
            }
            else if (terrain != null && terrain.materialTemplate != null)
            {
                terrain.materialTemplate.SetColor(tintColorId, color);
            }
        }
    }

#if UNITY_EDITOR
    //-------------------------------------------------------------------------
    [CustomEditor(typeof(TeleportArea__2))]
    public class TeleportAreaEditor : Editor
    {
        //-------------------------------------------------
        void OnEnable()
        {
            if (Selection.activeTransform != null)
            {
                TeleportArea__2 teleportArea = Selection.activeTransform.GetComponent<TeleportArea__2>();
                if (teleportArea != null)
                {
                    teleportArea.UpdateVisuals();
                }
            }
        }

        //-------------------------------------------------
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (Selection.activeTransform != null)
            {
                TeleportArea__2 teleportArea = Selection.activeTransform.GetComponent<TeleportArea__2>();
                if (GUI.changed && teleportArea != null)
                {
                    teleportArea.UpdateVisuals();
                }
            }
        }
    }
#endif
}
