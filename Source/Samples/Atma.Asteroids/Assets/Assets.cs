using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Atma.Asteroids.Assets.Types;
using Atma.Engine;

namespace Atma.Asteroids.Assets
{
    public static class Assets
    {
        public static IMesh generateAsset(AssetType type, MeshData data)
        {
            var uri = new AssetUri(AssetType.MESH, "Temp", Guid.NewGuid().ToString());
            return CoreRegistry.require<AssetManager>(AssetManager.Uri).generateAsset<IMesh, MeshData>(uri, data);
        }
    }
}
