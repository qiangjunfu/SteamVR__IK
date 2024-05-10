using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Timeline.Actions;
#endif
using UnityEngine;
using Valve.VR.InteractionSystem;



public class TeleportAreaExtension : TeleportMarkerBase
{
    //Public properties
    public Bounds meshBounds { get; private set; }

    //Private data
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
        targetMaterial = areaMesh.sharedMaterial;

#if UNITY_URP
			tintColorId = Shader.PropertyToID( "_BaseColor" );
#else
        tintColorId = Shader.PropertyToID("_TintColor");
#endif

        CalculateBounds();
    }


    //-------------------------------------------------
    public void Start()
    {
        //visibleTintColor = Teleport.instance.areaVisibleMaterial.GetColor(tintColorId);
        //highlightedTintColor = Teleport.instance.areaHighlightedMaterial.GetColor(tintColorId);
        //lockedTintColor = Teleport.instance.areaLockedMaterial.GetColor(tintColorId);
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
        //if (!locked)
        //{
        //    highlighted = highlight;

        //    if (highlight)
        //    {
        //        areaMesh.material = Teleport.instance.areaHighlightedMaterial;
        //    }
        //    else
        //    {
        //        areaMesh.material = Teleport.instance.areaVisibleMaterial;
        //    }
        //}
        if (!locked)
        {
            highlighted = highlight;

            if (highlight)
            {
                areaMesh.material = targetMaterial;
            }
            else
            {
                areaMesh.material = targetMaterial;
            }
        }
    }


    //-------------------------------------------------
    public override void SetAlpha(float tintAlpha, float alphaPercent)
    {
        Color tintedColor = GetTintColor();
        tintedColor.a *= alphaPercent;
        areaMesh.material.SetColor(tintColorId, tintedColor);
    }


    //-------------------------------------------------
    public override void UpdateVisuals()
    {
        //if (locked)
        //{
        //    areaMesh.material = Teleport.instance.areaLockedMaterial;
        //}
        //else
        //{
        //    areaMesh.material = Teleport.instance.areaVisibleMaterial;
        //}
        areaMesh.material = targetMaterial;
    }


    //-------------------------------------------------
    public void UpdateVisualsInEditor()
    {
        if (Teleport.instance == null)
            return;

        areaMesh = GetComponent<MeshRenderer>();
        targetMaterial = GetComponent<MeshRenderer>().sharedMaterial;

        //if (locked)
        //{
        //    areaMesh.sharedMaterial = Teleport.instance.areaLockedMaterial;
        //}
        //else
        //{
        //    areaMesh.sharedMaterial = Teleport.instance.areaVisibleMaterial;
        //}
        areaMesh.material = targetMaterial;
    }


    //-------------------------------------------------
    private bool CalculateBounds()
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
}


#if UNITY_EDITOR
//-------------------------------------------------------------------------
[CustomEditor(typeof(TeleportAreaExtension))]
public class TeleportAreaExtensionEditor : Editor
{
    //-------------------------------------------------
    void OnEnable()
    {
        if (Selection.activeTransform != null)
        {
            TeleportAreaExtension teleportArea = Selection.activeTransform.GetComponent<TeleportAreaExtension>();
            if (teleportArea != null)
            {
                teleportArea.UpdateVisualsInEditor();
            }
        }
    }


    //-------------------------------------------------
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (Selection.activeTransform != null)
        {
            TeleportAreaExtension teleportArea = Selection.activeTransform.GetComponent<TeleportAreaExtension>();
            if (GUI.changed && teleportArea != null)
            {
                teleportArea.UpdateVisualsInEditor();
            }
        }
    }
}
#endif

