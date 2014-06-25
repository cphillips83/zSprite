using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Atma.Asteroids.Assets.Types
{
    public class MeshData : IAssetData
    {
        public Vector3[] vertices;
        public Vector2[] texCoord0;
        public Vector2[] texCoord1;
        public Vector3[] normals;
        public Vector4[] colors;
        public int[] indices;

        public MeshData()
        {
        }

        public Vector3[] getVertices()
        {
            return vertices;
        }

        public Vector2[] getTexCoord0()
        {
            return texCoord0;
        }

        public Vector2[] getTexCoord1()
        {
            return texCoord1;
        }

        public Vector3[] getNormals()
        {
            return normals;
        }

        public Vector4[] getColors()
        {
            return colors;
        }

        public int[] getIndices()
        {
            return indices;
        }
    }
}
