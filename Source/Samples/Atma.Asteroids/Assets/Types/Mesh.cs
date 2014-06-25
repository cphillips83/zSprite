using Atma.Asteroids.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Atma.Asteroids.Assets.Types
{
    public interface IMesh : IAsset<MeshData>, IRenderable
    {
        RenderOperationType OperationType { get; }
        //AABB getAABB();

        //TFloatList getVertices();

        // TODO: Remove? At least review.
        //void render();
    }

    //public abstract class Mesh : IReloadableAsset<MeshData>, IRenderable
    //{
    //    public const int VERTEX_SIZE = 3;
    //    public const int TEX_COORD_0_SIZE = 2;
    //    public const int TEX_COORD_1_SIZE = 3;
    //    public const int COLOR_SIZE = 4;
    //    public const int NORMAL_SIZE = 3;

    //    //public abstract AABB getAABB();

    //    public Mesh(AssetUri uri)
    //    {
    //        this.uri = uri;
    //    }

    //    public AssetUri uri { get; private set; }

    //    public bool isDisposed { get; private set; }

    //    //public abstract List<float> getVertices();

    //    public abstract void reload(MeshData data);

    //    protected abstract void ondispose();

    //    public abstract void render();

    //    protected void recalculateAABB()
    //    {

    //    }

    //    public void dispose()
    //    {
    //        if (!isDisposed)
    //        {
    //            isDisposed = true;
    //            ondispose();
    //        }
    //    }

    //    ~Mesh()
    //    {
    //        dispose();
    //    }

    //}
}
