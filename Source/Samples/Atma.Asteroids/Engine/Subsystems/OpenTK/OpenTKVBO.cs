using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ES11 = OpenTK.Graphics.ES11;
using TK = OpenTK;

namespace Atma.Asteroids.Engine.Subsystems.OpenTK
{
    public class OpenTKVBO
    {
        public string name;
        public int vertexId;
        public int indexId;

        public OpenTKVBO()
        {
            vertexId = ES11.GL.GenBuffer();
            indexId = ES11.GL.GenBuffer();
        }

        public void dispose()
        {
            ES11.GL.DeleteBuffer(vertexId);
            ES11.GL.DeleteBuffer(indexId);
        }

        public void bind()
        {
            ES11.GL.BindBuffer(ES11.All.ArrayBuffer, vertexId);
            ES11.GL.BindBuffer(ES11.All.ElementArrayBuffer, indexId);
        }

        public void load(float[] vertices, ushort[] indices, int stride)
        {
            bind();

            //ES11.GL.BindBuffer(ES11.All.ElementArrayBuffer, indexId);
            ES11.GL.BufferData(ES11.All.ElementArrayBuffer, (IntPtr)(indices.Length * sizeof(ushort)), indices, ES11.All.StaticDraw);

            //ES11.GL.BindBuffer(ES11.All.ArrayBuffer, vertexId);
            ES11.GL.BufferData(ES11.All.ArrayBuffer, (IntPtr)(vertices.Length * sizeof(float)), vertices, ES11.All.StaticDraw);

            unbind();
        }

        public void unbind()
        {

            ES11.GL.BindBuffer(ES11.All.ElementArrayBuffer, 0);
            ES11.GL.BindBuffer(ES11.All.ArrayBuffer, 0);
        }
    }
}
