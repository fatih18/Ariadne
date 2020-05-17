// @desc Maze Generation Algorithm
// @author Omar Huseynov
// @date 17th May 2020

using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
        public int m_numRings;                                  /* num of rings circling the maze */
        public float m_distanceDelta;                           /* distance between each ring */
        public float m_fatness;                                 /* wideness of each ring */
        public int m_numSides;                                  /* resolution of each ring */
        public float m_height;                                  /* position on the y-axis */
        public Material m_mazeMaterial;                         /* material to be applied to each ring */

        private GameObject[] m_rings;                           /* buffer containing all the rings */

        public void Start()
        {
                Transform parent = gameObject.transform;
                m_rings = GenMaze(m_numSides, m_numRings, parent, m_fatness, m_distanceDelta);
        }

        private float ToRadians(float angle) { return angle * Mathf.PI / 180.0f; }

        private GameObject[] GenMaze(int numSides, int numRings, Transform parent, float fatness, float distanceDelta)
        {
                GameObject[] rings = new GameObject[numRings];

                float distanceSum = 1.0f;
                for (int i = 0; i < numRings; ++i)
                {
                        GameObject ring = GenRing(i, numSides, distanceSum, fatness);
                        ring.transform.parent = parent;
                        rings[i] = ring;
                        distanceSum += distanceDelta;
                }

                return rings;
        }

        private Mesh GenMesh(int numSides, float scaleFactor, float fatness)
        {
                Mesh mesh = new Mesh();

                float theta = 135.0f;
                int numVertices = numSides * 3;
                int numFaces = numSides * 9;

                Vector3[] vertices = new Vector3[numVertices];
                for (int i = 0; i < numVertices; ++i)
                {
                        float multiplier = i >= numSides ? fatness + scaleFactor : scaleFactor;

                        float sin_theta = Mathf.Sin(ToRadians(theta)) * multiplier;
                        float cos_theta = Mathf.Cos(ToRadians(theta)) * multiplier;

                        vertices[i] = new Vector3(cos_theta, m_height, sin_theta);
                        theta -= 360.0f / numSides;
                }

                int[] triangles = new int[numFaces];
                for (int i = 0; i < numSides * 3; i += 3)
                {
                        triangles[i] = i / 3;
                        triangles[i + 1] = numSides * 2 + i / 3;
                        triangles[i + 2] = (i / 3 + 1) % numSides;
                }

                int ctr0 = 0;
                int ctr1 = numSides;
                int ctr2 = numSides * 2;
                for (int i = numSides * 3; i < numFaces; i += 3)
                {
                        ctr0 %= numSides;
                        ctr1 = ctr1 % (numSides * 2) == 0 ? numSides : ctr1;

                        triangles[i] = ctr0;

                        bool i_even = (numSides + i) % 2 == 0;
                        if (i_even)
                        {
                                triangles[i + 1] = ctr1;
                                triangles[i + 2] = ctr2;
                                ++ctr0;
                                ++ctr1;
                        }
                        else
                        {
                                triangles[i + 1] = ctr2;
                                triangles[i + 2] = ctr1;
                                ++ctr2;
                        }
                }

                mesh.vertices = vertices;
                mesh.triangles = triangles;
                mesh.RecalculateNormals();

                return mesh;
        }

        private GameObject GenRing(int id, int numSides, float scaleFactor, float fatness)
        {
                GameObject ring = new GameObject("ring" + id, typeof(MeshFilter), typeof(MeshRenderer));
                Mesh mesh = GenMesh(numSides, scaleFactor, fatness);

                ring.GetComponent<MeshFilter>().mesh = mesh;
                ring.GetComponent<MeshRenderer>().material = m_mazeMaterial;

                return ring;
        }
}
