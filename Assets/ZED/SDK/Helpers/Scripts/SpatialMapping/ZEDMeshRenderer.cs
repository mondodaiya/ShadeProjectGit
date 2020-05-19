
//======= Copyright (c) Stereolabs Corporation, All rights reserved. ===============

using UnityEngine;

/// <summary>
/// Renders the mesh generated by ZEDSpatialMappingManager in a second, hidden camera created at runtume. 
/// This gets the wireframe style with no performance loss.
/// This script is very similar to how ZEDPlaneRenderer works for Plane Detection. 
/// </summary>
public class ZEDMeshRenderer : MonoBehaviour
{

	private ZEDManager zedManager = null;
    /// <summary>
    /// Reference to the hidden camera we create at runtime. 
    /// </summary>
    private Camera cam;

    /// <summary>
    /// Target texture of the rendering done by the new camera.
    /// </summary>
    private RenderTexture meshTex;

    /// <summary>
    /// Checks if the spatial mapping has started.
    /// </summary>
    private bool hasStarted = false;

    /// <summary>
    /// Checks if the mesh requested is textured. If so, deativate the wireframe.
    /// </summary>
    public bool isTextured = false;

    /// <summary>
    /// Shader used to render the wireframe. Normally Mat_ZED_Wireframe_Video_Overlay. 
    /// </summary>
    private Shader shaderWireframe;

    /// <summary>
    /// Reference to the ZEDRenderingPlane component of the camera we copy. 
    /// </summary>
	private ZEDRenderingPlane renderingPlane;

    /// <summary>
    /// Create the Mesh Rendering pipe
    /// </summary>
    public void Create()
    {

		Transform ObjParent = gameObject.transform;
		int tries = 0;

        while (zedManager == null && tries<5) {
			if (ObjParent!=null) 
				zedManager= ObjParent.GetComponent<ZEDManager> ();
			if (zedManager == null && ObjParent!=null)
				ObjParent = ObjParent.parent;
			tries++;
		}

		if (zedManager == null) {
			return;
		}

		//Create the new GameObject and camera as a child of the corresponding ZED rig camera.
		GameObject go = new GameObject("MeshCamera");
		go.transform.parent = transform;
		go.transform.localPosition = Vector3.zero;
		go.transform.localRotation = Quaternion.identity;
		go.transform.localScale = Vector3.one;
		cam = go.AddComponent<Camera>();
		//go.hideFlags = HideFlags.HideInHierarchy;//This hides the new camera from scene view. Comment this out to see it in the hierarchy. 

		//Set the target texture to a new RenderTexture that will be passed to ZEDRenderingPlane for blending. 
		if (zedManager.zedCamera.IsCameraReady)
		{
			meshTex = new RenderTexture(zedManager.zedCamera.ImageWidth, zedManager.zedCamera.ImageHeight, 0, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Default);
			meshTex.Create();
		}

		//Set the camera's parameters. 
		cam.enabled = false;
		cam.cullingMask = (1 << zedManager.zedCamera.TagInvisibleToZED); //Layer set aside for planes and spatial mapping meshes. 
		cam.targetTexture = meshTex;
		cam.nearClipPlane = 0.1f;
		cam.farClipPlane = 500.0f;
		cam.fieldOfView = zedManager.zedCamera.GetFOV() * Mathf.Rad2Deg;
		cam.projectionMatrix = zedManager.zedCamera.Projection;
		cam.backgroundColor = new Color(0, 0, 0, 0);
		cam.clearFlags = CameraClearFlags.Color;
		cam.renderingPath = RenderingPath.VertexLit;
		cam.depth = 0;
		cam.depthTextureMode = DepthTextureMode.None;

		#if UNITY_5_6_OR_NEWER
		cam.allowMSAA = false;
		cam.allowHDR = false;
		#endif
		cam.useOcclusionCulling = false;

		shaderWireframe = (Resources.Load("Materials/SpatialMapping/Mat_ZED_Wireframe_Video_Overlay") as Material).shader;

		//Set the ZEDRenderingPlane blend texture to the one the new camera renders to.
		renderingPlane = GetComponent<ZEDRenderingPlane>();
		renderingPlane.SetTextureOverlayMapping(meshTex);
        renderingPlane.SetMeshRenderAvailable(false);
		hasStarted = true;
    }
    /// <summary>
    /// Set the rendering available. Used when loading mesh
    /// </summary>
    /// <param name="drawMesh"></param>
    public void UpdateRenderingPlane(bool drawMesh)
    {
        renderingPlane.SetMeshRenderAvailable(drawMesh);
    }

    /// <summary>
    /// Unsubscribes from relevant events. 
    /// </summary>
    private void OnDisable()
    {
		hasStarted = false;
    }

    /// <summary>
    /// Renders the plane each frame, before cameras normally update, so the RenderTexture is ready to be blended. 
    /// </summary>
    void OnPreRender()
    {
        if (zedManager.IsStereoRig)
        {
            if (zedManager != null)
            {
                if (zedManager.IsSpatialMappingDisplay && hasStarted)
                {
                    cam.enabled = true;
                    if (!isTextured)
                    {
                        GL.wireframe = true;
                        cam.RenderWithShader(shaderWireframe, "RenderType");
                        GL.wireframe = false;
                    }
                    else
                    {
                        cam.Render();
                    }
                    cam.enabled = false;
                    if (zedManager.SpatialMappingHasChunks)
                        renderingPlane.SetMeshRenderAvailable(true);
                }
            }
        }

    }
    private void Update()
    {
        if (!zedManager.IsStereoRig)
        {
            if (zedManager != null)
            {
                if (zedManager.IsSpatialMappingDisplay && hasStarted)
                {
                    cam.enabled = true;
                    if (!isTextured)
                    {
                        GL.wireframe = true;
                        cam.RenderWithShader(shaderWireframe, "RenderType");
                        GL.wireframe = false;
                    }
                    else
                    {
                        cam.Render();
                    }
                    cam.enabled = false;
                    if (zedManager.SpatialMappingHasChunks)
                        renderingPlane.SetMeshRenderAvailable(true);
                }
            }
        }
    }

    /// <summary>
    /// Releases the target RenderTexture when the application quits. 
    /// </summary>
    private void OnApplicationQuit()
    {
        if (meshTex != null && meshTex.IsCreated())
        {
            meshTex.Release();
        }
    }
}
