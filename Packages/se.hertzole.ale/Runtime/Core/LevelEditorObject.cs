﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace Hertzole.ALE
{
#if UNITY_EDITOR
    [DisallowMultipleComponent]
    [AddComponentMenu("")]
#endif
    public class LevelEditorObject : MonoBehaviour, ILevelEditorObject, IEquatable<LevelEditorObject>, IEquatable<ILevelEditorObject>
    {
        private struct ValueInfo
        {
            public int id;
            public object value;

            public ValueInfo(int id, object value)
            {
                this.id = id;
                this.value = value;
            }
        }

        private bool gotComponents = false;
        private bool gotPlayModeObjects = false;

        private IExposedToLevelEditor[] exposedComponents;
        private ILevelEditorPlayModeObject[] playModeObjects;

        private Dictionary<int, ValueInfo[]> savedValues = new Dictionary<int, ValueInfo[]>();

        public string Name
        {
            get { return gameObject.name; }
            set
            {
                if (gameObject.name != value)
                {
                    gameObject.name = value;
                    OnStateChanged?.Invoke(this, new LevelEditorObjectStateArgs(gameObject.name, gameObject.activeInHierarchy));
                }
            }
        }
        public bool IsActive
        {
            get { return gameObject.activeInHierarchy; }
            set
            {
                if (gameObject.activeSelf != value)
                {
                    gameObject.SetActive(value);
                    OnStateChanged?.Invoke(this, new LevelEditorObjectStateArgs(gameObject.name, gameObject.activeInHierarchy));
                }
            }
        }

        public string ID { get; set; }
        public int InstanceID { get; set; }

        public ILevelEditorObject Parent { get; set; }

        public List<ILevelEditorObject> Children { get; set; } = new List<ILevelEditorObject>();

        GameObject ILevelEditorObject.MyGameObject { get { return gameObject; } }

        public event EventHandler<LevelEditorObjectStateArgs> OnStateChanged;

        public IExposedToLevelEditor[] GetExposedComponents()
        {
            CacheExposedComponents();

            return exposedComponents;
        }

        public void ApplyExposedData(LevelEditorComponentData[] components)
        {
            CacheExposedComponents();

            for (int i = 0; i < components.Length; i++)
            {
                for (int j = 0; j < exposedComponents.Length; j++)
                {
                    if (components[i].type == exposedComponents[j].TypeName)
                    {
                        LevelEditorPropertyData[] properties = components[i].properties;
                        for (int k = 0; k < properties.Length; k++)
                        {
                            object value = properties[k].value;

                            // Required to get the proper component that it needs.
                            Type valueType = exposedComponents[j].GetValueType(properties[k].id);
                            if (valueType.IsSubclassOf(typeof(Component)))
                            {
                                if (value != null)
                                {
                                    ILevelEditorObject targetObj = (ILevelEditorObject)value;
                                    value = targetObj.MyGameObject.GetComponent(valueType);
                                }
                            }
                            exposedComponents[j].SetValue(properties[k].id, value, false);
                        }
                    }
                }
            }
        }

        public void StartPlayMode()
        {
            CachePlayModeObjects();

            for (int i = 0; i < exposedComponents.Length; i++)
            {
                ExposedProperty[] properties = exposedComponents[i].GetProperties();
                for (int ii = 0; ii < properties.Length; ii++)
                {
                    savedValues[i][ii] = new ValueInfo(ii, exposedComponents[i].GetValue(ii));
                }
            }

            if (playModeObjects != null)
            {
                for (int i = 0; i < playModeObjects.Length; i++)
                {
                    playModeObjects[i].OnStartPlayMode();
                }
            }
        }

        public void StopPlayMode()
        {
            for (int i = 0; i < exposedComponents.Length; i++)
            {
                ExposedProperty[] properties = exposedComponents[i].GetProperties();
                for (int ii = 0; ii < properties.Length; ii++)
                {
                    exposedComponents[i].SetValue(savedValues[i][ii].id, savedValues[i][ii].value, true);
                }
            }

            if (playModeObjects != null)
            {
                for (int i = 0; i < playModeObjects.Length; i++)
                {
                    playModeObjects[i].OnStopPlayMode();
                }
            }
        }

        public void AddChild(ILevelEditorObject child)
        {
            if (child == null)
            {
                return;
            }

#if !ALE_STRIP_SAFETY
            if (Children.Contains(child))
            {
                throw new NotSupportedException("There can not be any duplicate children in the childrens list.");
            }
#endif

            Children.Add(child);
            child.MyGameObject.transform.SetParent(transform);
        }

        public void RemoveChild(ILevelEditorObject child)
        {
            if (child == null)
            {
                return;
            }

            Children.Remove(child);
        }

        private void CacheExposedComponents()
        {
            if (gotComponents)
            {
                return;
            }

            exposedComponents = GetComponents<IExposedToLevelEditor>();
            if (exposedComponents.Length > 1)
            {
                Array.Sort(exposedComponents, (x, y) => x.Order.CompareTo(y.Order));
            }

            for (int i = 0; i < exposedComponents.Length; i++)
            {
                savedValues.Add(i, new ValueInfo[exposedComponents[i].GetProperties().Length]);
            }

            gotComponents = true;
        }

        private void CachePlayModeObjects()
        {
            if (gotPlayModeObjects)
            {
                return;
            }

            playModeObjects = GetComponentsInChildren<ILevelEditorPlayModeObject>();
            gotPlayModeObjects = true;
        }

        public bool Equals(LevelEditorObject other)
        {
            return Equals((ILevelEditorObject)other);
        }

        public bool Equals(ILevelEditorObject other)
        {
            return other != null && other.InstanceID == InstanceID && other.ID == ID;
        }
    }
}
