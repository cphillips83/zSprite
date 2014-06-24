using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Atma.Asteroids.Assets
{
    public delegate ASSET AssetFactory<DATA, ASSET>(AssetUri uri, DATA data)
        where DATA : IAssetData
        where ASSET : IAsset<DATA>;

    public delegate IAsset<IAssetData> AssetFactory2(AssetUri uri, IAssetData data);
        
}
