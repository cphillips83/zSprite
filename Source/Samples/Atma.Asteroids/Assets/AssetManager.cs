using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Atma.Core;
using Atma.Engine;

namespace Atma.Asteroids.Assets
{
    public class AssetManager
    {
        public static readonly GameUri Uri = "engine:assets";

        private static readonly Logger logger = Logger.getLogger(typeof(AssetManager));

        private Dictionary<int, AssetFactory2> _factories = new Dictionary<int, AssetFactory2>();

        public void setFactory<DATA, ASSET>(AssetType type, AssetFactory<DATA, ASSET> factory)
            where DATA : IAssetData
            where ASSET : IAsset<DATA>
        {
            //factory("", 
        }

        public void setFactory2<DATA, ASSET>(AssetType type, AssetFactory2 factory)
            where DATA : IAssetData
            where ASSET : IAsset<DATA>
        {
            _factories.Add(type.id, factory);
        }

        public T generateAsset<T, U>(AssetUri uri, U data)
            where T : IAsset<U>
            where U : IAssetData
        {
            if (!uri.isValid())
            {
                logger.warn("Invalid asset uri: {0}", uri);
                return default(T);
            }

            AssetFactory2 factory;
            if (!_factories.TryGetValue(uri.type.id, out factory))
            {
                logger.warn("Unsupported asset type: {0}", uri.type);
                return default(T);
            }

            var t = factory(uri, data);

            if (t is T)
                return (T)t;

            if (t != null)
                logger.error("factory returned a type '{0} 'that wasn't of T", t.GetType());

            return default(T);
        }
    }
}
