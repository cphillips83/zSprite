using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zSpriteOld.Core;

namespace zSpriteOld.Samples
{
    public class SampleBrowser:IDisposable
    {
        public void Dispose()
        {

        }
        //protected virtual Sample LoadSamples()
        //{
        //    string dir = "../samples";
        //    var samples = new SampleSet();

        //    PluginManager.Instance.LoadDirectory(dir);

        //    foreach (IPlugin plugin in PluginManager.Instance.InstalledPlugins)
        //    {
        //        if (plugin is SamplePlugin)
        //        {
        //            var pluginInstance = (SamplePlugin)plugin;
        //            this.LoadedSamplePlugins.Add(pluginInstance.Name);
        //            foreach (SdkSample sample in pluginInstance.Samples)
        //            {
        //                this.LoadedSamples.Add(sample);
        //            }
        //        }
        //    }

        //    foreach (SdkSample sample in this.LoadedSamples)
        //    {
        //        if (!this.SampleCategories.Contains(sample.Metadata["Category"]))
        //        {
        //            this.SampleCategories.Add(sample.Metadata["Category"]);
        //        }
        //    }

        //    if (this.LoadedSamples.Count > 0)
        //    {
        //        this.SampleCategories.Add("All");
        //    }
        //    return null;
        //}

        ///// <summary>
        ///// This function encapsulates the entire lifetime of the context.
        ///// </summary>
        //public virtual void Go()
        //{
        //    Go(null);
        //}

        ///// <summary>
        ///// This function encapsulates the entire lifetime of the context.
        ///// </summary>
        ///// <param name="initialSample"></param>
        //public virtual void Go(Sample initialSample)
        //{
        //    bool firstRun = true;

        //    while (!this.IsLastRun)
        //    {
        //        this.IsLastRun = true; // assume this is our last run

        //        CreateRoot();
        //        if (!OneTimeConfig())
        //        {
        //            return;
        //        }

        //        // if the context was reconfigured, set requested renderer
        //        if (!firstRun)
        //        {
        //            this.Root.RenderSystem = this.Root.RenderSystems[this.NextRenderer];
        //        }

        //        Setup();

        //        // restore the last sample if there was one or, if not, start initial sample
        //        if (!firstRun)
        //        {
        //            RecoverLastSample();
        //        }
        //        else if (initialSample != null)
        //        {
        //            RunSample(initialSample);
        //        }

        //        this.Root.StartRendering(); // start the render loop

        //        this.ConfigurationManager.SaveConfiguration(this.Root, this.NextRenderer);

        //        Shutdown();
        //        if (this.Root != null)
        //        {
        //            this.Root.Dispose();
        //        }
        //        firstRun = false;
        //    }
        //}
    }
}
