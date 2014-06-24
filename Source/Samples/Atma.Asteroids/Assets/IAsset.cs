using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Atma.Asteroids.Assets
{
    public interface IReloadableAsset<T> : IAsset<T>
        where T : IAssetData
    {
        void reload(T data);

    }

    public interface IAsset<out T>
        where T: IAssetData
    {
        AssetUri uri { get; }

        //T reload();

        void dispose();

        bool isDisposed { get; }
    }
}
