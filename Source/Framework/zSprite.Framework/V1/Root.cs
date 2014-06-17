﻿#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using System.Diagnostics;
using zSpriteOld.Scripts;
using zSpriteOld.Collections;
using zSpriteOld.Managers;
using zSpriteOld.Systems;
using System.Text;
using System.Reflection;
#endregion


namespace zSpriteOld
{
    public class Root
    {
        private static Root _instance = null;
        public static Root instance
        {
            get
            {
                if (_instance == null)
                    throw new ArgumentNullException("_instance");
                return _instance;
            }
        }

        #region Entities
        //public event Action<IEntity> add;
        //public event Action<IEntity> remove;

        //private List<IEntity> _entities = new List<IEntity>(1024);
        //private List<IEntity> _addEntities = new List<IEntity>(64);
        //private List<IEntity> _removeEntities = new List<IEntity>(64);

        //private List<ISystem> _systems = new List<ISystem>();
        //private List<ISystem> _initSystems = new List<ISystem>();
        //private List<ISystem> _destroySystems = new List<ISystem>();

        #endregion

        /// <summary>
        /// Holds instance of LogManager
        /// </summary>
        private readonly Core.LogManager logMgr;

        public readonly LogManager logging = new LogManager();
        public readonly TimeManager time = new TimeManager();
        public readonly ResourceManager resources = new ResourceManager();
        public readonly RenderManager graphics = new RenderManager();
        public readonly ScreenManager screen = new ScreenManager();
        public readonly InputManager input = new InputManager();
        public readonly GUIManager gui = new GUIManager();

        //internal  EventManager eventManager = new EventManager();

        //internal  readonly MessageObjectCache messageCache = new MessageObjectCache();

        internal ContentManager content;
        internal readonly List<GameObject> destroyList = new List<GameObject>();
        internal readonly List<GameObject> firstInit = new List<GameObject>();
        internal readonly Dictionary<int, GameObject> indexedGO = new Dictionary<int, GameObject>();

        internal readonly QuadTree<Collider> physicsQuadTree = new QuadTree<Collider>(new Vector2(16, 16), 20);
        internal readonly QuadTree<Collider2> physicsQuadTree2 = new QuadTree<Collider2>(new Vector2(16, 16), 20);
        internal readonly QuadTree<Rigidbody> rigidbodiesQuadTree = new QuadTree<Rigidbody>(new Vector2(16, 16), 5);

        internal readonly List<Script> newScriptList = new List<Script>();
        internal readonly List<GameObject> newGameObjects = new List<GameObject>();

        internal List<GameObject> enabledList = new List<GameObject>();
        public readonly GameObject RootObject = new GameObject("Root Game Object");

        private Stopwatch timer = new Stopwatch();
        public long lastUpdate = 0;
        public long totalUpdate = 0;
        public long lastFixedUpdate = 0;
        public long totalFixedUpdate = 0;

        public int totalGOs { get { return indexedGO.Count; } }

        public Root()
            : this("zsprite.log")
        {

        }

        public Root(string logFilename)
        {
            _instance = this;
            var info = new StringBuilder();

            // write the initial info at the top of the log
            info.AppendFormat("*********zSpriteOld Engine Log *************\n");
            info.AppendFormat("{0}\n", Copyright);
            info.AppendFormat("Version: {0}\n", Version);
            info.AppendFormat("Operating System: {0}\n", Environment.OSVersion.ToString());
            var isMono = Type.GetType("Mono.Runtime") != null;
            info.AppendFormat("{1} Framework: {0}\n", Environment.Version.ToString(), isMono ? "Mono" : ".Net");

            // Initializes the Log Manager singleton
            if (Core.LogManager.Instance == null)
            {
                new LogManager();
            }

            this.logMgr = Core.LogManager.Instance;

            //if logFileName is null, then just the Diagnostics (debug) writes will be made
            // create a new default log
            this.logMgr.CreateLog(logFilename, true, true);

            this.logMgr.Write(info.ToString());
            this.logMgr.Write("*-*-* zSpriteOld Initializing");


            new Core.PluginManager();
            Core.PluginManager.Instance.LoadAll();
        }

        /// <summary>
        /// Returns the current version of the Engine assembly.
        /// </summary>
        public string Version
        {
            get
            {
                // returns the file version of this assembly
#if SILVERLIGHT || WINDOWS_PHONE
				var fullName = Assembly.GetExecutingAssembly().ToString();
				var a = fullName.IndexOf( "Version=" ) + 8;
				var b = fullName.IndexOf( ",", a );
				return fullName.Substring( a, b - a );
#else
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
#endif
            }
        }

        /// <summary>
        /// Specifies the name of the engine that will be used where needed (i.e. log files, etc).
        /// </summary>
        public string Copyright
        {
            get
            {
                var attribute =
                    (AssemblyCopyrightAttribute)
                    Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(AssemblyCopyrightAttribute), false);

                if (attribute != null)
                {
                    return attribute.Copyright;
                }
                else
                {
                    return "";
                }
            }
        }

        private int uniqueId = 0;
        public int nextId()
        {
            return uniqueId++;
        }
        //internal  List<GameObject> visibleList = new List<GameObject>();

        private double accumulator = 0.0;
        public bool isRunning = false;

        internal void start(GraphicsDevice device, ContentManager content)
        {            
            _instance = this;
            this.content = content;
            this.content.RootDirectory = string.Empty;

            time.init();
            graphics.init(device);
            screen.init();
            input.init();
            resources.init();
            gui.init();

            //screen.SetResolution(graphics.graphicsDevice.DisplayMode.Height, graphics.graphicsDevice.DisplayMode.Width, false);

            reload();

            //messageCache.cache("update");
            //messageCache.cache("fixedupdate");
            //messageCache.cache("render");

            //System.Threading.Thread.CurrentThread.Name = "Client Thread";
            //try
            //{
            //    isRunning = true;
            //    var sw = new Stopwatch();
            //    sw.Start();
            //    while (isRunning)
            //    {
            //        //if (WindowEventMonitor.Instance.MessagePump != null)
            //        //    WindowEventMonitor.Instance.MessagePump();

            //        update(sw.Elapsed.TotalSeconds);
            //        System.Threading.Thread.Sleep(10);
            //    }
            //    sw.Stop();

            //}
            //finally
            //{
            //    cleanup();
            //}
        }

        //public void addEntity(IEntity e)
        //{
        //    _addEntities.Add(e);
        //}

        //public void removeEntity(IEntity e)
        //{
        //    _removeEntities.Add(e);
        //}

        //public void addSystem(ISystem system)
        //{
        //    //_systems.Add(system);
        //    _initSystems.Add(system);
        //}

        //public void removeSystem(ISystem system)
        //{
        //    _destroySystems.Add(system);
        //    //_systems.Remove(system);
        //}

        private void processNewScripts()
        {
            foreach (var c in newScriptList)
            {
                if (c.enabled)
                    c.addToEvents();
            }
            newScriptList.Clear();
        }

        internal void update(double newTime)
        {
            _instance = this;

            //#region Entities
            //for (var i = 0; i < _destroySystems.Count; i++)
            //{
            //    _destroySystems[i].destroy();
            //    _systems.Remove(_destroySystems[i]);
            //}

            //_destroySystems.Clear();

            //for (var i = 0; i < _initSystems.Count; i++)
            //{
            //    _initSystems[i].init();
            //    _systems.Add(_initSystems[i]);
            //}

            //_initSystems.Clear();

            //while (_removeEntities.Count > 0)
            //{
            //    var next = _removeEntities[_removeEntities.Count - 1];
            //    var index = _entities.IndexOf(next);

            //    if (index == -1)
            //        throw new ArgumentOutOfRangeException("index");

            //    if (_addEntities.Count > 0)
            //    {
            //        _entities[index] = _addEntities[_addEntities.Count - 1];
            //        _addEntities.RemoveAt(_addEntities.Count - 1);

            //        if (add != null)
            //            add(_entities[index]);
            //    }

            //    if (remove != null)
            //        remove(_removeEntities[_removeEntities.Count - 1]);

            //    _removeEntities.RemoveAt(_removeEntities.Count - 1);
            //}

            //while (_addEntities.Count > 0)
            //{
            //    _entities.Add(_addEntities[_addEntities.Count - 1]);
            //    _addEntities.RemoveAt(_addEntities.Count - 1);

            //    if (add != null)
            //        add(_entities[_entities.Count - 1]);
            //}

            //for (var i = 0; i < _systems.Count; i++)
            //    _systems[i].update();

            //#endregion

            foreach (var go in destroyList)
                go.internalDestroy();

            destroyList.Clear();

            processNewScripts();

            while (Event.Count("init") > 0)
            {
                Event.Invoke("init");
                Event.Clear("init");

                processNewScripts();

                foreach (var newgo in newGameObjects)
                    newgo.parent.forceSendMessage("childadded", newgo);

                newGameObjects.Clear();
            }

            Root.instance.input.update();

            double frameTime = newTime - time.realtimeSinceStartupD;

            // note: max frame time to avoid spiral of death          
            if (frameTime > time.maximumDeltaTimeD * time.timeScale)
                frameTime = time.maximumDeltaTimeD * time.timeScale;

            accumulator += frameTime;

            if (accumulator >= time.fixedDeltaTimeD)
            {
                while (accumulator >= time.fixedDeltaTimeD)
                {
                    accumulator -= time.fixedDeltaTimeD;

                    time.updateFixed(time.fixedDeltaTimeD);

                    lastFixedUpdate = 0;
                    timer.Reset();
                    timer.Start();

                    //call fixed update   
                    Event.Invoke("beforefixedupdate");
                    Event.Invoke("fixedupdate");
                    Event.Invoke("afterfixedupdate");

                    timer.Stop();
                    lastFixedUpdate = timer.ElapsedMilliseconds;
                    totalFixedUpdate += lastFixedUpdate;
                    //messageCache.invoke("fixedupdate");
                    //Console.WriteLine("fixed update");
                }
            }

            time.update(newTime, frameTime);

            lastUpdate = 0;
            timer.Reset();
            timer.Start();

            Event.Invoke("beforeupdate");
            Event.Invoke("update");
            Event.Invoke("afterupdate");

            timer.Stop();
            lastUpdate = timer.ElapsedMilliseconds;
            totalUpdate += lastUpdate;

            //messageCache.invoke("update");

            //call update
            //Console.WriteLine("update {0}", time.smoothedTimeDeltaD);
        }

        internal void draw()
        {
            _instance = this;

            //for (var i = 0; i < _systems.Count; i++)
            //    _systems[i].render();

            graphics.drawCallsThisFrame = 0;
            graphics.spritesSubmittedThisFrame = 0;

            //Event.Invoke("render");
            //messageCache.invoke("render");
            Camera.drawAll();
            gui.render();
            //physicsQuadTree2.Render(Color.Blue, Color.Green);
            graphics.drawCalls = graphics.drawCallsThisFrame;
            graphics.spritesSubmitted = graphics.spritesSubmittedThisFrame;

        }

        internal void cleanup()
        {
            _instance = this;
            resources.cleanup();
        }

        internal void reload()
        {
            _instance = this;

            RootObject.destroy();
            foreach (var go in destroyList)
                go.internalDestroy();

            resources.reload();
        }

        public GameObject find(int id)
        {
            GameObject go;
            indexedGO.TryGetValue(id, out go);
            return go;
        }

    }
}
