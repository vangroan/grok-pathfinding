using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Grok.Pathfinding
{
    [ExecuteInEditMode]
    public class CellMap : MonoBehaviour
    {
        static Vector2Int CellSize = new Vector2Int(32, 32);

        public Vector2Int Dimensions;

        private Array2D<CellData> _cells;
        public Array2D<CellData> Cells;

        public int Width => _cells.Width;
        public int Height => _cells.Height;

        #region Unity Lifecycle
        protected void Awake()
        {
            _cells = new Array2D<CellData>(Mathf.Max(1, Dimensions.x), Mathf.Max(1, Dimensions.y));
            GenerateMesh();
            GenerateTexture();
        }

        protected void Update()
        {

        }
        #endregion

        void GenerateMesh()
        {
            Debug.Log("Generate Mesh");

            var meshFilter = GetComponent<MeshFilter>();
            if (meshFilter == null)
            {
                Debug.LogWarning("MeshFilter is null");
                return;
            }

            var verts = new List<Vector3>();
            var uvs = new List<Vector2>();
            var tris = new List<int>();

            foreach (var pair in _cells.Pairs())
            {
                int index = verts.Count;
                var pos = (Vector2)pair.Coord;

                verts.Add(pos);
                verts.Add(pos + new Vector2(1f, 0f));
                verts.Add(pos + new Vector2(1f, 1f));
                verts.Add(pos + new Vector2(0f, 1f));

                uvs.Add(new Vector2(0f, 0f));
                uvs.Add(new Vector2(1f, 0f));
                uvs.Add(new Vector2(1f, 1f));
                uvs.Add(new Vector2(0f, 1f));

                tris.Add(index);
                tris.Add(index + 1);
                tris.Add(index + 2);

                tris.Add(index);
                tris.Add(index + 2);
                tris.Add(index + 3);
            }

            var mesh = new Mesh();
            mesh.indexFormat = IndexFormat.UInt32;
            mesh.vertices = verts.ToArray();
            mesh.SetUVs(0, uvs);
            mesh.triangles = tris.ToArray();
            meshFilter.mesh = mesh;
        }

        void GenerateTexture()
        {
            Debug.Log("Generate Texture");

            var meshRenderer = GetComponent<MeshRenderer>();
            if (meshRenderer == null)
            {
                Debug.LogWarning("MeshRenderer is null");
                return;
            }

            var texture = new Texture2D(CellSize.x, CellSize.y, TextureFormat.ARGB32, false);

            for (int y = 0; y < CellSize.y; y++)
            {
                for (int x = 0; x < CellSize.x; x++)
                {
                    if (x == 0 || y == 0 || x == CellSize.x - 1 || y == CellSize.y - 1)
                    {
                        texture.SetPixel(x, y, new Color(0.8f, 0.8f, 0.8f, 1f));
                    }
                    else
                    {
                        texture.SetPixel(x, y, new Color(0.9f, 0.9f, 0.9f, 1f));
                    }
                }
            }

            texture.Apply();
            meshRenderer.sharedMaterial.mainTexture = texture;
        }
    }
}
