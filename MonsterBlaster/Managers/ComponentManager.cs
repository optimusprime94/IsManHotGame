using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonsterBlaster.Components;

namespace MonsterBlaster.Content
{
    public class ComponentManager
    {
        private static ComponentManager cm;
        private uint nextid;
        private Dictionary<Type, Dictionary<uint, IEngineComponent>> Components = new Dictionary<Type, Dictionary<uint, IEngineComponent>>();
        private ComponentManager()
        {
            
        }

        public static ComponentManager Get()
        {
            if (cm == null)
            {
                cm = new ComponentManager();
            }
            return cm;
        }

        public uint NewEntity()
        {
            return nextid++;
        }

        public T EntityComponent<T>(uint id)
        {
            if (!Components.ContainsKey(typeof(T)))
            {
                Components.Add(typeof(T), new Dictionary<uint, IEngineComponent>());
            }
            var components = Components[typeof(T)];
            if (components.ContainsKey(id))
            {
                return (T) components[id];
            }
            else
            {
                return default(T);
            }      
        }

        /// <summary>
        /// Get a component that also has another given component.
        /// </summary>
        /// <typeparam name="hasT"> if it has this component </typeparam>
        /// <typeparam name="getT"> get this component </typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public getT GetEntityComponentWhere<hasT, getT>(uint id)
        {
            if (!Components.ContainsKey(typeof(hasT)))
            {
                Components.Add(typeof(getT), new Dictionary<uint, IEngineComponent>());
            }
            var components = Components[typeof(hasT)];
            if (components.ContainsKey(id))
            {
                return (getT)components[id];
            }
            else
            {
                return default(getT);
            }
        }

        public void AddComponentToEntity(IEngineComponent component, uint id)
        {
            if (!Components.ContainsKey(component.GetType()))
            {
                Components.Add(component.GetType(), new Dictionary<uint, IEngineComponent>());
            }
            var components = Components[component.GetType()];
            components.Add(id, component);
        }

        public Dictionary<uint, IEngineComponent> GetComponents<TComponentType>()
        {
            if (!Components.ContainsKey(typeof(TComponentType)))
            {
                Components.Add(typeof(TComponentType), new Dictionary<uint, IEngineComponent>());
            }
            return Components[typeof(TComponentType)];
        }

        public void DeleteEntity(uint id)
        {
            foreach(var component in Components)
            {
                component.Value.Remove(id);
            }
        }

        public void ClearComponents()
        {
            Components.Clear();
        }

    }
}
