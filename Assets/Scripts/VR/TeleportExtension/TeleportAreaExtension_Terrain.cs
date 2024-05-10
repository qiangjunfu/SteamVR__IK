using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class TeleportAreaExtension_Terrain : TeleportMarkerBase
{
    // Public properties
    public Bounds meshBounds { get; private set; }

    // Private data
    private MeshRenderer areaMesh;
    private int tintColorId = 0;
    private Color visibleTintColor = Color.clear;
    private Color highlightedTintColor = Color.clear;
    private Color lockedTintColor = Color.clear;
    private bool highlighted = false;

    [SerializeField] bool markerActive2 = false;
    [SerializeField] Material targetMaterial;
    [SerializeField] private List<MeshRenderer> areaMeshChildren = new List<MeshRenderer>();
    [SerializeField] List<Material> targetMaterialChildren = new List<Material>();

    //-------------------------------------------------
    public void Awake()
    {
        markerActive = markerActive2;

        areaMesh = GetComponent<MeshRenderer>();

        if (areaMesh != null)
        {
            targetMaterial = areaMesh.sharedMaterial;

#if UNITY_URP
            tintColorId = Shader.PropertyToID("_BaseColor");
#else
            tintColorId = Shader.PropertyToID("_TintColor");
#endif

            CalculateBounds();
        }
    }

    //-------------------------------------------------
    public void Start()
    {
        if (areaMesh != null)
        {
            // You can set the initial colors here if you have a visible tint color set up
            // This part is commented out since we don't have the Teleport.instance in your code snippet
            // visibleTintColor = Teleport.instance.areaVisibleMaterial.GetColor(tintColorId);
            // highlightedTintColor = Teleport.instance.areaHighlightedMaterial.GetColor(tintColorId);
            // lockedTintColor = Teleport.instance.areaLockedMaterial.GetColor(tintColorId);
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
        if (areaMesh != null && !locked)
        {
            highlighted = highlight;
            areaMesh.material = targetMaterial; // Adjust this line if you have different materials for highlighted/non-highlighted states
        }
    }

    //-------------------------------------------------
    public override void SetAlpha(float tintAlpha, float alphaPercent)
    {
        if (areaMesh != null)
        {
            Color tintedColor = GetTintColor();
            tintedColor.a *= alphaPercent;
            areaMesh.material.SetColor(tintColorId, tintedColor);
        }
    }

    //-------------------------------------------------
    public override void UpdateVisuals()
    {
        if (areaMesh != null)
        {
            // Adjust material based on lock status
            areaMesh.material = targetMaterial;
        }
    }

    //-------------------------------------------------
    private bool CalculateBounds()
    {
        if (areaMesh != null)
        {
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
        return false;
    }

    //-------------------------------------------------
    private Color GetTintColor()
    {
        if (locked)
        {
            return lockedTintColor;
        }
        else if (highlighted)
        {
            return highlightedTintColor;
        }
        return visibleTintColor;
    }
}
