using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Atma.Asteroids.Assets
{
    public abstract class AbstractAsset<T> : IAsset<T>
        where T: IAssetData
    {
        public AssetUri uri { get; private set; }

        public bool isDisposed { get; private set; }

        public AbstractAsset(AssetUri uri)
        {
            this.uri = uri;
        }

        public void dispose()
        {
            if (!isDisposed)
            {
                isDisposed = true;
                ondispose();
            }
        }

        ~AbstractAsset()
        {
            dispose();
        }

        protected abstract void ondispose();

        public override bool Equals(object obj)
        {
            if (obj is AbstractAsset<T>)
                return Equals(obj as AbstractAsset<T>);

            if (obj is IAsset<T>)
                return Equals(obj as IAsset<T>);

            return false;
        }

        public bool Equals(IAsset<T> other)
        {
            if (other != null && this.uri == other.uri)
                return true;

            return false;
        }

        public bool Equals(AbstractAsset<T> other)
        {
            if (other != null && this.uri == other.uri)
                return true;

            return false;
        }

        public override int GetHashCode()
        {
            return uri.GetHashCode();
        }

        public static bool operator ==(AbstractAsset<T> a, AbstractAsset<T> b)
        {
            return a.uri == b.uri;
        }

        public static bool operator !=(AbstractAsset<T> a, AbstractAsset<T> b)
        {
            return a.uri != b.uri;
        }
    }
}
