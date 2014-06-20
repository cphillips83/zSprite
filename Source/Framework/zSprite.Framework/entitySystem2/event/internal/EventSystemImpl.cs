using System;
using System.Collections.Generic;
using System.Threading;

/*
 * Copyright 2013 MovingBlocks
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
namespace org.terasology.entitySystem.@event.@internal
{

	using MethodAccess = com.esotericsoftware.reflectasm.MethodAccess;
	using Objects = com.google.common.@base.Objects;
	using Predicates = com.google.common.@base.Predicates;
	using BiMap = com.google.common.collect.BiMap;
	using HashBiMap = com.google.common.collect.HashBiMap;
	using HashMultimap = com.google.common.collect.HashMultimap;
	using ImmutableList = com.google.common.collect.ImmutableList;
	using Lists = com.google.common.collect.Lists;
	using Maps = com.google.common.collect.Maps;
	using Queues = com.google.common.collect.Queues;
	using SetMultimap = com.google.common.collect.SetMultimap;
	using Sets = com.google.common.collect.Sets;
	using ReflectionUtils = org.reflections.ReflectionUtils;
	using Logger = org.slf4j.Logger;
	using LoggerFactory = org.slf4j.LoggerFactory;
	using SimpleUri = org.terasology.engine.SimpleUri;
	using EntityRef = org.terasology.entitySystem.entity.EntityRef;
	using EventLibrary = org.terasology.entitySystem.metadata.EventLibrary;
	using EventMetadata = org.terasology.entitySystem.metadata.EventMetadata;
	using ComponentSystem = org.terasology.entitySystem.systems.ComponentSystem;
	using BroadcastEvent = org.terasology.network.BroadcastEvent;
	using Client = org.terasology.network.Client;
	using NetworkComponent = org.terasology.network.NetworkComponent;
	using NetworkEvent = org.terasology.network.NetworkEvent;
	using NetworkMode = org.terasology.network.NetworkMode;
	using NetworkSystem = org.terasology.network.NetworkSystem;
	using OwnerEvent = org.terasology.network.OwnerEvent;
	using ServerEvent = org.terasology.network.ServerEvent;
	using BlockComponent = org.terasology.world.block.BlockComponent;


	/// <summary>
	/// An implementation of the EventSystem.
	/// 
	/// @author Immortius <immortius@gmail.com>
	/// </summary>
	public class EventSystemImpl : EventSystem
	{

		private static readonly Logger logger = LoggerFactory.getLogger(typeof(EventSystemImpl));

		private IDictionary<Type, SetMultimap<Type, EventHandlerInfo>> componentSpecificHandlers = Maps.newHashMap();
		private SetMultimap<Type, EventHandlerInfo> generalHandlers = HashMultimap.create();
		private IComparer<EventHandlerInfo> priorityComparator = new EventHandlerPriorityComparator();

		// Event metadata
		private BiMap<SimpleUri, Type> eventIdMap = HashBiMap.create();
		private SetMultimap<Type, Type> childEvents = HashMultimap.create();

		private Thread mainThread;
		private BlockingQueue<PendingEvent> pendingEvents = Queues.newLinkedBlockingQueue();

		private EventLibrary eventLibrary;
		private NetworkSystem networkSystem;

		public EventSystemImpl(EventLibrary eventLibrary, NetworkSystem networkSystem)
		{
			this.mainThread = Thread.CurrentThread;
			this.eventLibrary = eventLibrary;
			this.networkSystem = networkSystem;
		}

		public virtual void process()
		{
			for (PendingEvent @event = pendingEvents.poll(); @event != null; @event = pendingEvents.poll())
			{
				if (@event.Component != null)
				{
					send(@event.Entity, @event.Event, @event.Component);
				}
				else
				{
					send(@event.Entity, @event.Event);
				}
			}
		}

		public virtual void registerEvent(SimpleUri uri, Type eventType)
		{
			eventIdMap.put(uri, eventType);
			logger.debug("Registering event {}", eventType.Name);
			foreach (Type parent in ReflectionUtils.getAllSuperTypes(eventType, Predicates.assignableFrom(typeof(Event))))
			{
				if (!typeof(AbstractConsumableEvent).Equals(parent) && !typeof(Event).Equals(parent))
				{
					childEvents.put(parent, eventType);
				}
			}
			if (shouldAddToLibrary(eventType))
			{
				eventLibrary.register(uri, eventType);
			}
		}

		/// <summary>
		/// Events are added to the event library if they have a network annotation
		/// </summary>
		/// <param name="eventType"> </param>
		/// <returns> Whether the event should be added to the event library </returns>
		private bool shouldAddToLibrary(Type eventType)
		{
			return eventType.getAnnotation(typeof(ServerEvent)) != null || eventType.getAnnotation(typeof(OwnerEvent)) != null || eventType.getAnnotation(typeof(BroadcastEvent)) != null;
		}

		public virtual void registerEventHandler(ComponentSystem handler)
		{
			Type handlerClass = handler.GetType();
			if (!Modifier.isPublic(handlerClass.Modifiers))
			{
//JAVA TO C# CONVERTER WARNING: The .NET Type.FullName property will not always yield results identical to the Java Class.getName method:
				logger.error("Cannot register handler {}, must be public", handler.GetType().FullName);
				return;
			}

//JAVA TO C# CONVERTER WARNING: The .NET Type.FullName property will not always yield results identical to the Java Class.getName method:
			logger.debug("Registering event handler " + handlerClass.FullName);
			foreach (Method method in handlerClass.GetMethods())
			{
				ReceiveEvent receiveEventAnnotation = method.getAnnotation(typeof(ReceiveEvent));
				if (receiveEventAnnotation != null)
				{
					if (!receiveEventAnnotation.netFilter().isValidFor(networkSystem.Mode, false))
					{
						continue;
					}
					Set<Type> requiredComponents = Sets.newLinkedHashSet();
					method.Accessible = true;
					Type[] types = method.ParameterTypes;

					logger.debug("Found method: " + method.ToString());
					if (!types[0].IsSubclassOf(typeof(Event)) || !types[1].IsSubclassOf(typeof(EntityRef)))
					{
						logger.error("Invalid event handler method: {}", method.Name);
						return;
					}

					requiredComponents.addAll(Arrays.asList(receiveEventAnnotation.components()));
					IList<Type> componentParams = Lists.newArrayList();
					for (int i = 2; i < types.Length; ++i)
					{
						if (!types[i].IsSubclassOf(typeof(Component)))
						{
							logger.error("Invalid event handler method: {} - {} is not a component class", method.Name, types[i]);
							return;
						}
						requiredComponents.add((Type) types[i]);
						componentParams.Add((Type) types[i]);
					}

					ByteCodeEventHandlerInfo handlerInfo = new ByteCodeEventHandlerInfo(handler, method, receiveEventAnnotation.priority(), requiredComponents, componentParams);
					addEventHandler((Type) types[0], handlerInfo, requiredComponents);
				}
			}
		}

		public virtual void unregisterEventHandler(ComponentSystem handler)
		{
			foreach (SetMultimap<Type, EventHandlerInfo> eventHandlers in componentSpecificHandlers.Values)
			{
				IEnumerator<EventHandlerInfo> eventHandlerIterator = eventHandlers.values().GetEnumerator();
				while (eventHandlerIterator.MoveNext())
				{
					EventHandlerInfo eventHandler = eventHandlerIterator.Current;
					if (eventHandler.Handler.Equals(handler))
					{
						eventHandlerIterator.remove();
					}
				}
			}

			IEnumerator<EventHandlerInfo> eventHandlerIterator = generalHandlers.values().GetEnumerator();
			while (eventHandlerIterator.MoveNext())
			{
				EventHandlerInfo eventHandler = eventHandlerIterator.Current;
				if (eventHandler.Handler.Equals(handler))
				{
					eventHandlerIterator.remove();
				}
			}
		}

		private void addEventHandler(Type type, EventHandlerInfo handler, ICollection<Type> components)
		{
			if (components.Count == 0)
			{
				generalHandlers.put(type, handler);
				foreach (Type childType in childEvents.get(type))
				{
					generalHandlers.put(childType, handler);
				}
			}
			else
			{
				foreach (Type c in components)
				{
					addToComponentSpecificHandlers(type, handler, c);
					foreach (Type childType in childEvents.get(type))
					{
						addToComponentSpecificHandlers(childType, handler, c);
					}
				}
			}
		}

		private void addToComponentSpecificHandlers(Type type, EventHandlerInfo handlerInfo, Type c)
		{
			SetMultimap<Type, EventHandlerInfo> componentMap = componentSpecificHandlers[type];
			if (componentMap == null)
			{
				componentMap = HashMultimap.create();
				componentSpecificHandlers[type] = componentMap;
			}
			componentMap.put(c, handlerInfo);
		}

		public virtual void registerEventReceiver<T>(EventReceiver<T> eventReceiver, Type eventClass, params Type[] componentTypes) where T : org.terasology.entitySystem.@event.Event
		{
			registerEventReceiver(eventReceiver, eventClass, EventPriority.PRIORITY_NORMAL, componentTypes);
		}

		public virtual void registerEventReceiver<T>(EventReceiver<T> eventReceiver, Type eventClass, int priority, params Type[] componentTypes) where T : org.terasology.entitySystem.@event.Event
		{
			EventHandlerInfo info = new ReceiverEventHandlerInfo<T>(eventReceiver, priority, componentTypes);
			addEventHandler(eventClass, info, Arrays.asList(componentTypes));
		}

		public virtual void unregisterEventReceiver<T>(EventReceiver<T> eventReceiver, Type eventClass, params Type[] componentTypes) where T : org.terasology.entitySystem.@event.Event
		{
			SetMultimap<Type, EventHandlerInfo> eventHandlerMap = componentSpecificHandlers[eventClass];
			if (eventHandlerMap != null)
			{
				ReceiverEventHandlerInfo testReceiver = new ReceiverEventHandlerInfo<T>(eventReceiver, 0, componentTypes);
				foreach (Type c in componentTypes)
				{
					eventHandlerMap.remove(c, testReceiver);
					foreach (Type childType in childEvents.get(eventClass))
					{
						eventHandlerMap.remove(childType, testReceiver);
					}
				}
			}
		}

		public virtual void send(EntityRef entity, Event @event)
		{
			if (Thread.CurrentThread != mainThread)
			{
				pendingEvents.offer(new PendingEvent(entity, @event));
			}
			else
			{
				networkReplicate(entity, @event);

				Set<EventHandlerInfo> selectedHandlersSet = selectEventHandlers(@event.GetType(), entity);
				IList<EventHandlerInfo> selectedHandlers = Lists.newArrayList(selectedHandlersSet);
				selectedHandlers.Sort(priorityComparator);

				if (@event is ConsumableEvent)
				{
					sendConsumableEvent(entity, @event, selectedHandlers);
				}
				else
				{
					sendStandardEvent(entity, @event, selectedHandlers);
				}
			}
		}

		private void sendStandardEvent(EntityRef entity, Event @event, IList<EventHandlerInfo> selectedHandlers)
		{
			foreach (EventHandlerInfo handler in selectedHandlers)
			{
				// Check isValid at each stage in case components were removed.
				if (handler.isValidFor(entity))
				{
					handler.invoke(entity, @event);
				}
			}
		}

		private void sendConsumableEvent(EntityRef entity, Event @event, IList<EventHandlerInfo> selectedHandlers)
		{
			ConsumableEvent consumableEvent = (ConsumableEvent) @event;
			foreach (EventHandlerInfo handler in selectedHandlers)
			{
				// Check isValid at each stage in case components were removed.
				if (handler.isValidFor(entity))
				{
					handler.invoke(entity, @event);
					if (consumableEvent.Consumed)
					{
						return;
					}
				}
			}
		}

		private void networkReplicate(EntityRef entity, Event @event)
		{
			EventMetadata metadata = eventLibrary.getMetadata(@event);
			if (metadata != null && metadata.NetworkEvent)
			{
				logger.debug("Replicating event: {}", @event);
				switch (metadata.NetworkEventType)
				{
					case BROADCAST:
						broadcastEvent(entity, @event, metadata);
						break;
					case OWNER:
						sendEventToOwner(entity, @event);
						break;
					case SERVER:
						sendEventToServer(entity, @event);
						break;
					default:
						break;
				}
			}
		}

		private void sendEventToServer(EntityRef entity, Event @event)
		{
			if (networkSystem.Mode == NetworkMode.CLIENT)
			{
				NetworkComponent netComp = entity.getComponent(typeof(NetworkComponent));
				if (netComp != null)
				{
					networkSystem.Server.send(@event, entity);
				}
			}
		}

		private void sendEventToOwner(EntityRef entity, Event @event)
		{
			if (networkSystem.Mode.Server)
			{
				NetworkComponent netComp = entity.getComponent(typeof(NetworkComponent));
				if (netComp != null)
				{
					Client client = networkSystem.getOwner(entity);
					if (client != null)
					{
						client.send(@event, entity);
					}
				}
			}
		}

		private void broadcastEvent(EntityRef entity, Event @event, EventMetadata metadata)
		{
			if (networkSystem.Mode.Server)
			{
				NetworkComponent netComp = entity.getComponent(typeof(NetworkComponent));
				BlockComponent blockComp = entity.getComponent(typeof(BlockComponent));
				if (netComp != null || blockComp != null)
				{
					Client instigatorClient = null;
					if (metadata.SkipInstigator && @event is NetworkEvent)
					{
						instigatorClient = networkSystem.getOwner(((NetworkEvent) @event).Instigator);
					}
					foreach (Client client in networkSystem.Players)
					{
						if (!client.Equals(instigatorClient))
						{
							client.send(@event, entity);
						}
					}
				}
			}
		}

		public virtual void send(EntityRef entity, Event @event, Component component)
		{
			if (Thread.CurrentThread != mainThread)
			{
				pendingEvents.offer(new PendingEvent(entity, @event, component));
			}
			else
			{
				SetMultimap<Type, EventHandlerInfo> handlers = componentSpecificHandlers[@event.GetType()];
				if (handlers != null)
				{
					IList<EventHandlerInfo> eventHandlers = Lists.newArrayList(handlers.get(component.GetType()));
					eventHandlers.Sort(priorityComparator);
					foreach (EventHandlerInfo eventHandler in eventHandlers)
					{
						if (eventHandler.isValidFor(entity))
						{
							eventHandler.invoke(entity, @event);
						}
					}
				}
			}
		}

		private Set<EventHandlerInfo> selectEventHandlers(Type eventType, EntityRef entity)
		{
			Set<EventHandlerInfo> result = Sets.newHashSet();
			result.addAll(generalHandlers.get(eventType));
			SetMultimap<Type, EventHandlerInfo> handlers = componentSpecificHandlers[eventType];
			if (handlers == null)
			{
				return result;
			}

			foreach (Type compClass in handlers.Keys)
			{
				if (entity.hasComponent(compClass))
				{
					foreach (EventHandlerInfo eventHandler in handlers.get(compClass))
					{
						if (eventHandler.isValidFor(entity))
						{
							result.add(eventHandler);
						}
					}
				}
			}
			return result;
		}

		private class EventHandlerPriorityComparator : IComparer<EventHandlerInfo>
		{

			public virtual int Compare(EventHandlerInfo o1, EventHandlerInfo o2)
			{
				return o2.Priority - o1.Priority;
			}
		}

		private interface EventHandlerInfo
		{
			bool isValidFor(EntityRef entity);

			void invoke(EntityRef entity, Event @event);

			int Priority {get;}

			object Handler {get;}
		}

		private class ReflectedEventHandlerInfo : EventHandlerInfo
		{
			internal ComponentSystem handler;
			internal Method method;
			internal ImmutableList<Type> filterComponents;
			internal ImmutableList<Type> componentParams;
			internal int priority;

			public ReflectedEventHandlerInfo(ComponentSystem handler, Method method, int priority, ICollection<Type> filterComponents, ICollection<Type> componentParams)
			{
				this.handler = handler;
				this.method = method;
				this.filterComponents = ImmutableList.copyOf(filterComponents);
				this.componentParams = ImmutableList.copyOf(componentParams);
				this.priority = priority;
			}

			public virtual bool isValidFor(EntityRef entity)
			{
				foreach (Type component in filterComponents)
				{
					if (!entity.hasComponent(component))
					{
						return false;
					}
				}
				return true;
			}

			public virtual void invoke(EntityRef entity, Event @event)
			{
				try
				{
					object[] @params = new object[2 + componentParams.size()];
					@params[0] = @event;
					@params[1] = entity;
					for (int i = 0; i < componentParams.size(); ++i)
					{
						@params[i + 2] = entity.getComponent(componentParams.get(i));
					}
					method.invoke(handler, @params);
				}
				catch (IllegalAccessException ex)
				{
					logger.error("Failed to invoke event", ex);
				}
				catch (System.ArgumentException ex)
				{
					logger.error("Failed to invoke event", ex);
				}
				catch (InvocationTargetException ex)
				{
					logger.error("Failed to invoke event", ex);
				}
			}

			public virtual int Priority
			{
				get
				{
					return priority;
				}
			}

			public virtual ComponentSystem Handler
			{
				get
				{
					return handler;
				}
			}
		}

		private class ByteCodeEventHandlerInfo : EventHandlerInfo
		{
			internal ComponentSystem handler;
			internal MethodAccess methodAccess;
			internal int methodIndex;
			internal ImmutableList<Type> filterComponents;
			internal ImmutableList<Type> componentParams;
			internal int priority;

			public ByteCodeEventHandlerInfo(ComponentSystem handler, Method method, int priority, ICollection<Type> filterComponents, ICollection<Type> componentParams)
			{


				this.handler = handler;
				this.methodAccess = MethodAccess.get(handler.GetType());
				methodIndex = methodAccess.getIndex(method.Name, method.ParameterTypes);
				this.filterComponents = ImmutableList.copyOf(filterComponents);
				this.componentParams = ImmutableList.copyOf(componentParams);
				this.priority = priority;
			}

			public virtual bool isValidFor(EntityRef entity)
			{
				foreach (Type component in filterComponents)
				{
					if (!entity.hasComponent(component))
					{
						return false;
					}
				}
				return true;
			}

			public virtual void invoke(EntityRef entity, Event @event)
			{
				try
				{
					object[] @params = new object[2 + componentParams.size()];
					@params[0] = @event;
					@params[1] = entity;
					for (int i = 0; i < componentParams.size(); ++i)
					{
						@params[i + 2] = entity.getComponent(componentParams.get(i));
					}
					methodAccess.invoke(handler, methodIndex, @params);
				}
				catch (System.ArgumentException ex)
				{
					logger.error("Failed to invoke event", ex);
				}
			}

			public virtual int Priority
			{
				get
				{
					return priority;
				}
			}

			public virtual ComponentSystem Handler
			{
				get
				{
					return handler;
				}
			}
		}

		private class ReceiverEventHandlerInfo<T> : EventHandlerInfo where T : org.terasology.entitySystem.@event.Event
		{
			internal EventReceiver<T> receiver;
			internal Type[] components;
			internal int priority;

			public ReceiverEventHandlerInfo(EventReceiver<T> receiver, int priority, params Type[] components)
			{
				this.receiver = receiver;
				this.priority = priority;
				this.components = Arrays.copyOf(components, components.Length);
			}

			public virtual bool isValidFor(EntityRef entity)
			{
				foreach (Type component in components)
				{
					if (!entity.hasComponent(component))
					{
						return false;
					}
				}
				return true;
			}

			public virtual void invoke(EntityRef entity, Event @event)
			{
				receiver.onEvent((T) @event, entity);
			}

			public virtual int Priority
			{
				get
				{
					return priority;
				}
			}

			public override bool Equals(object obj)
			{
				if (obj == this)
				{
					return true;
				}
				if (obj is ReceiverEventHandlerInfo)
				{
					ReceiverEventHandlerInfo other = (ReceiverEventHandlerInfo) obj;
					if (Objects.equal(receiver, other.receiver))
					{
						return true;
					}
				}
				return false;
			}

			public override int GetHashCode()
			{
				return Objects.GetHashCode(receiver);
			}

			public virtual object Handler
			{
				get
				{
					return receiver;
				}
			}
		}
	}

}