using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Atma.Asteroids.Assets;
using Atma.Asteroids.Assets.Types;
using Atma.Core;
using OpenGL = OpenTK.Graphics.OpenGL;
using ES11 = OpenTK.Graphics.ES11;
using TK = OpenTK;

namespace Atma.Asteroids.Engine.Subsystems.OpenTK
{
    public class OpenTKMesh : AbstractAsset<MeshData>, IMesh//, Mesh//, IAsset<IAssetData>
    {
        private static readonly Logger logger = Logger.getLogger(typeof(OpenTKMesh));

        private const int FLOAT_SIZE = 4;

        private const int VERTEX_SIZE = 3;
        private const int TEX_COORD_0_SIZE = 2;
        private const int TEX_COORD_1_SIZE = 3;
        private const int COLOR_SIZE = 4;
        private const int NORMAL_SIZE = 3;

        //private MeshData data;

        private int stride;
        private int vertexOffset;
        private int texCoord0Offset;
        private int texCoord1Offset;
        private int colorOffset;
        private int normalOffset;

        private bool hasTexCoord0;
        private bool hasTexCoord1;
        private bool hasColor;
        private bool hasNormal;

        private OpenTKVBO vbo = null;

        //private int vboVertexBuffer;
        //private int vboIndexBuffer;
        private int vertexCount;
        private int indexCount;

        public OpenTKMesh(AssetUri uri, MeshData data)
            : base(uri)
        {
            reload(data);

        }

        //public override List<float> getVertices()
        //{
        //    throw new NotImplementedException();
        //}

        public RenderOperationType OperationType { get; set; }

        public void reload(MeshData data)
        {
            buildMesh(data);
        }

        private void buildMesh(MeshData data)
        {
            //this.data = data;
            //this.data = newData;
            stride = 0;
            vertexCount = 0;
            indexCount = 0;
            vertexOffset = 0;
            texCoord0Offset = 0;
            texCoord1Offset = 0;
            colorOffset = 0;
            normalOffset = 0;
            hasTexCoord0 = false;
            hasTexCoord1 = false;
            hasNormal = false;
            hasColor = false;

            if (vbo != null)
                vbo.dispose();


            var verts = data.getVertices();
            if (verts != null && verts.Length > 0)
            {
                var indx = data.getIndices();
                if (indx != null && indx.Length > 0)
                {
                    vbo = new OpenTKVBO();
                    vertexCount = verts.Length;
                    indexCount = indx.Length;

                    var tex0 = data.getTexCoord0();
                    var tex1 = data.getTexCoord1();
                    var norm = data.getNormals();
                    var colr = data.getColors();

                    hasTexCoord0 = tex0 != null && tex0.Length == vertexCount;
                    hasTexCoord1 = tex1 != null && tex1.Length == vertexCount;
                    hasNormal = norm != null && norm.Length == vertexCount;
                    hasColor = colr != null && colr.Length == vertexCount;

                    vertexOffset = 0;
                    stride = VERTEX_SIZE;
                    if (hasTexCoord0)
                    {
                        texCoord0Offset = stride * FLOAT_SIZE;
                        stride += TEX_COORD_0_SIZE;
                    }

                    if (hasTexCoord1)
                    {
                        texCoord1Offset = stride * FLOAT_SIZE;
                        stride += TEX_COORD_1_SIZE;
                    }

                    if (hasNormal)
                    {
                        normalOffset = stride * FLOAT_SIZE;
                        stride += NORMAL_SIZE;
                    }

                    if (hasColor)
                    {
                        colorOffset = stride * FLOAT_SIZE;
                        stride += COLOR_SIZE;
                    }

                    var parts = new float[stride * vertexCount];
                    //var indices = new ushort[vertexCount];

                    var partIndex = 0;
                    for (var i = 0; i < vertexCount; i++)
                    {
                        parts[partIndex++] = verts[i].X;
                        parts[partIndex++] = verts[i].Y;
                        parts[partIndex++] = verts[i].Z;

                        if (hasTexCoord0)
                        {
                            parts[partIndex++] = tex0[i].X;
                            parts[partIndex++] = tex0[i].Y;
                        }

                        if (hasTexCoord1)
                        {
                            parts[partIndex++] = tex1[i].X;
                            parts[partIndex++] = tex1[i].Y;
                        }

                        if (hasNormal)
                        {
                            parts[partIndex++] = norm[i].X;
                            parts[partIndex++] = norm[i].Y;
                            parts[partIndex++] = norm[i].Z;

                        }

                        if (hasColor)
                        {
                            parts[partIndex++] = colr[i].X;
                            parts[partIndex++] = colr[i].Y;
                            parts[partIndex++] = colr[i].Z;
                            parts[partIndex++] = colr[i].W;
                        }

                    }

                    vbo.load(parts, indx, stride);

                    ////List<TFloatIterator> parts = Lists.newArrayList();
                    ////TIntList partSizes = new TIntArrayList();
                    //int vertexCount = newData.getVertices().Length;
                    //int vertexSize = VERTEX_SIZE;
                    //parts.add(newData.getVertices().iterator());
                    //partSizes.add(VERTEX_SIZE);

                    //if (newData.getTexCoord0() != null && newData.getTexCoord0().size() / TEX_COORD_0_SIZE == vertexCount)
                    //{
                    //    parts.add(newData.getTexCoord0().iterator());
                    //    partSizes.add(TEX_COORD_0_SIZE);
                    //    texCoord0Offset = vertexSize * FLOAT_SIZE;
                    //    vertexSize += TEX_COORD_0_SIZE;
                    //    hasTexCoord0 = true;
                    //}
                    //if (newData.getTexCoord1() != null && newData.getTexCoord1().size() / TEX_COORD_1_SIZE == vertexCount)
                    //{
                    //    parts.add(newData.getTexCoord1().iterator());
                    //    partSizes.add(TEX_COORD_1_SIZE);
                    //    texCoord1Offset = vertexSize * FLOAT_SIZE;
                    //    vertexSize += TEX_COORD_1_SIZE;
                    //    hasTexCoord1 = true;
                    //}
                    //if (newData.getNormals() != null && newData.getNormals().size() / NORMAL_SIZE == vertexCount)
                    //{
                    //    parts.add(newData.getNormals().iterator());
                    //    partSizes.add(NORMAL_SIZE);
                    //    normalOffset = vertexSize * FLOAT_SIZE;
                    //    vertexSize += NORMAL_SIZE;
                    //    hasNormal = true;
                    //}
                    //if (newData.getColors() != null && newData.getColors().size() / COLOR_SIZE == vertexCount)
                    //{
                    //    parts.add(newData.getColors().iterator());
                    //    partSizes.add(COLOR_SIZE);
                    //    colorOffset = vertexSize * FLOAT_SIZE;
                    //    vertexSize += COLOR_SIZE;
                    //    hasColor = true;
                    //}
                    //stride = vertexSize * FLOAT_SIZE;
                    //indexCount = newData.getIndices().size();

                    //createVertexBuffer(parts, partSizes, vertexCount, vertexSize);
                    //createIndexBuffer(newData.getIndices());

                    //aabb = AABB.createEncompasing(newData.getVertices());
                }
            }
        }

        protected override void ondispose()
        {
            if (vbo != null)
                vbo.dispose();

            vbo = null;
            hasTexCoord0 = false;
            hasTexCoord1 = false;
            hasColor = false;
            hasNormal = false;
            stride = 0;
            vertexOffset = 0;
            texCoord0Offset = 0;
            texCoord1Offset = 0;
            colorOffset = 0;
            normalOffset = 0;
            indexCount = 0;
            //if (vboVertexBuffer != 0)
            //{
            //    //bufferPool.dispose(vboVertexBuffer);
            //    vboVertexBuffer = 0;
            //}
            //if (vboIndexBuffer != 0)
            //{
            //    //bufferPool.dispose(vboIndexBuffer);
            //    vboIndexBuffer = 0;
            //}
        }

        public void render()
        {
            if (!isDisposed)
            {
                //OpenGL.GL.Begin(OpenGL.PrimitiveType.Triangles);

                //OpenGL.GL.Color3(Color.MidnightBlue.r, Color.MidnightBlue.g, Color.MintCream.b);
                //OpenGL.GL.Vertex2(-1.0f, 1.0f);
                //OpenGL.GL.Color3(Color.SpringGreen.r, Color.SpringGreen.g, Color.SpringGreen.b);
                //OpenGL.GL.Vertex2(0.0f, -1.0f);
                //OpenGL.GL.Color3(Color.Ivory.r, Color.Ivory.g, Color.Ivory.b);
                //OpenGL.GL.Vertex2(1.0f, 1.0f);

                //OpenGL.GL.End();

                preRender();
                dorender();
                postrender();
            }
            else
            {
                logger.error("Attempted to render disposed mesh: {0}", uri);
            }
        }

        public void preRender()
        {
            if (!isDisposed)
            {
                //OpenGL.GL.Begin(OpenGL.PrimitiveType.Triangles);
                //return;

                //glEnableClientState(GL_VERTEX_ARRAY);
                ES11.GL.EnableClientState(ES11.EnableCap.VertexArray);


                if (hasTexCoord0 || hasTexCoord1)
                {
                    //glEnableClientState(GL_TEXTURE_COORD_ARRAY);
                    ES11.GL.EnableClientState(ES11.EnableCap.TextureCoordArray);
                }
                if (hasColor)
                {
                    //glEnableClientState(GL_COLOR_ARRAY);
                    ES11.GL.EnableClientState(ES11.EnableCap.ColorArray);
                }
                if (hasNormal)
                {
                    //glEnableClientState(GL_NORMAL_ARRAY);
                    ES11.GL.EnableClientState(ES11.EnableCap.NormalArray);
                }

                //GL15.glBindBuffer(GL15.GL_ARRAY_BUFFER, vboVertexBuffer);
                //ES11.GL.BindBuffer(ES11.All.ArrayBuffer, vboVertexBuffer);

                //GL15.glBindBuffer(GL15.GL_ELEMENT_ARRAY_BUFFER, vboIndexBuffer);
                //ES11.GL.BindBuffer(ES11.All.ElementArrayBuffer, vboIndexBuffer);
                vbo.bind();

                //glVertexPointer(VERTEX_SIZE, GL11.GL_FLOAT, stride, vertexOffset);
                ES11.GL.VertexPointer(VERTEX_SIZE, ES11.VertexPointerType.Float, 0, (IntPtr)vertexOffset);


                if (hasTexCoord0)
                {
                    //GL13.glClientActiveTexture(GL13.GL_TEXTURE0);
                    ES11.GL.ClientActiveTexture(ES11.TextureUnit.Texture0);
                    //glTexCoordPointer(TEX_COORD_0_SIZE, GL11.GL_FLOAT, stride, texCoord0Offset);
                    ES11.GL.TexCoordPointer(TEX_COORD_0_SIZE, ES11.TexCoordPointerType.Float, stride, (IntPtr)texCoord0Offset);
                }

                if (hasTexCoord1)
                {
                    //GL13.glClientActiveTexture(GL13.GL_TEXTURE1);
                    ES11.GL.ClientActiveTexture(ES11.TextureUnit.Texture1);
                    //glTexCoordPointer(TEX_COORD_1_SIZE, GL11.GL_FLOAT, stride, texCoord1Offset);
                    ES11.GL.TexCoordPointer(TEX_COORD_1_SIZE, ES11.TexCoordPointerType.Float, stride, (IntPtr)texCoord1Offset);
                }

                if (hasColor)
                {
                    //glColorPointer(COLOR_SIZE, GL11.GL_FLOAT, stride, colorOffset);
                    ES11.GL.ColorPointer(COLOR_SIZE, ES11.ColorPointerType.Float, stride, (IntPtr)colorOffset);
                }

                if (hasNormal)
                {
                    //glNormalPointer(GL11.GL_FLOAT, stride, normalOffset);
                    ES11.GL.NormalPointer(ES11.NormalPointerType.Float, stride, (IntPtr)normalOffset);
                }
            }
            else
            {
                logger.error("Attempted to render disposed mesh: {0}", uri);
            }
        }

        private void dorender()
        {
            if (!isDisposed)
            {

                //GL11.glDrawElements(GL11.GL_TRIANGLES, indexCount, GL_UNSIGNED_INT, 0);
                ES11.GL.DrawElements(ES11.PrimitiveType.Triangles, indexCount, ES11.All.UnsignedShort, IntPtr.Zero);
                //ES11.GL.DrawArrays(ES11.PrimitiveType.Triangles, 0, indexCount);
            }
            else
            {
                logger.error("Attempted to render disposed mesh: {0}", uri);
            }
        }

        private void postrender()
        {
            if (!isDisposed)
            {
                vbo.unbind();

                //OpenGL.GL.End();
                //return;
                if (hasNormal)
                {
                    //glDisableClientState(GL_NORMAL_ARRAY);
                    ES11.GL.DisableClientState(ES11.EnableCap.NormalArray);
                }
                if (hasColor)
                {
                    //glDisableClientState(GL_COLOR_ARRAY);
                    ES11.GL.DisableClientState(ES11.EnableCap.ColorArray);
                }
                if (hasTexCoord0 || hasTexCoord1)
                {
                    //glDisableClientState(GL_TEXTURE_COORD_ARRAY);
                    ES11.GL.DisableClientState(ES11.EnableCap.TextureCoordArray);
                }
                //glDisableClientState(GL_VERTEX_ARRAY);
                ES11.GL.DisableClientState(ES11.EnableCap.VertexArray);

                //GL15.glBindBuffer(GL15.GL_ARRAY_BUFFER, 0);
                //ES11.GL.BindBuffer(ES11.All.ArrayBuffer, 0);
                //GL15.glBindBuffer(GL15.GL_ELEMENT_ARRAY_BUFFER, 0);
                //ES11.GL.BindBuffer(ES11.All.ElementArrayBuffer, 0);
            }
            else
            {
                logger.error("Attempted to render disposed mesh: {0}", uri);
            }
        }
    }
}
