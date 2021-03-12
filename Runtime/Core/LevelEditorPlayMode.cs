﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Hertzole.ALE
{
#if UNITY_EDITOR
    [DisallowMultipleComponent]
    [AddComponentMenu("ALE/Play Mode", 2)]
#endif
    public class LevelEditorPlayMode : MonoBehaviour, ILevelEditorPlayMode
    {
        [Header("References")]
        [SerializeField, RequireType(typeof(ILevelEditorObjectManager))]
        private GameObject objectManager = null;
        [SerializeField, RequireType(typeof(ILevelEditor))]
        private GameObject levelEditor = null;

        [SerializeField]
        private InputActionReference stopPlayModeAction = null;
        [SerializeField]
        private bool disableCameraDuringPlayMode = true;
        [SerializeField]
        private PlayModeRequirement[] playModeRequirements = null;

        [Space]

        [SerializeField]
        protected GameObject playerPrefab = null;
        [SerializeField]
        protected string spawnpointTag = "Respawn";

        protected bool isPlaying;

        protected GameObject activePlayer;
        protected GameObject[] spawnpointObjects;

        protected List<ILevelEditorObject> allObjects;

        protected ILevelEditorObjectManager ObjectManager { get; private set; }
        protected ILevelEditor LevelEditor { get; private set; }

        public bool IsPlaying { get { return isPlaying; } }

        public event System.Action OnStartPlayMode;
        public event System.Action OnStopPlayMode;

        protected virtual void Awake()
        {
            ObjectManager = objectManager.GetComponent<ILevelEditorObjectManager>();
            if (levelEditor != null)
            {
                LevelEditor = levelEditor.GetComponent<ILevelEditor>();
            }
        }

        private void OnEnable()
        {
#if !ALE_STRIP_SAFETY || UNITY_EDITOR
            if (stopPlayModeAction == null)
            {
                throw new System.NullReferenceException("No Stop Play Mode Action set on LevelEditorPlayMode.");
            }
#endif

            stopPlayModeAction.action.performed += OnInputStopPlayMode;

            stopPlayModeAction.action.Enable();
        }

        private void OnDisable()
        {
#if !ALE_STRIP_SAFETY || UNITY_EDITOR
            if (stopPlayModeAction == null)
            {
                throw new System.NullReferenceException("No Stop Play Mode Action set on LevelEditorPlayMode.");
            }
#endif

            stopPlayModeAction.action.performed -= OnInputStopPlayMode;

            stopPlayModeAction.action.Disable();
        }

        private void OnInputStopPlayMode(InputAction.CallbackContext ctx)
        {
            if (ctx.performed && isPlaying)
            {
                StopPlayMode(LevelEditor);
            }
        }

        public virtual bool CanStartPlayMode(out string failReason)
        {
            if (isPlaying)
            {
                failReason = "You're somehow already playing.";
                return false;
            }

            for (int i = 0; i < playModeRequirements.Length; i++)
            {
                int count = ObjectManager.GetObjectCount(playModeRequirements[i].objectID);
                if (count < playModeRequirements[i].minAmount)
                {
                    failReason = "You need to have at least " + playModeRequirements[i].minAmount + " of " + ObjectManager.GetResource(playModeRequirements[i].objectID).Name + " to play a level.";
                    return false;
                }
            }

            failReason = string.Empty;
            return true;
        }

        public virtual bool CanStopPlayMode(out string failReason)
        {
            failReason = string.Empty;
            return true;
        }

        public virtual void StartPlayMode(ILevelEditor levelEditor)
        {
            SpawnPlayer();
            StartPlayModeObjects();
            ToggleEditorCamera(false);

            isPlaying = true;
            OnStartPlayMode?.Invoke();
        }

        protected virtual void SpawnPlayer()
        {
            Vector3 spawnPos = Vector3.zero;
            Quaternion spawnRot = Quaternion.identity;
            spawnpointObjects = GameObject.FindGameObjectsWithTag(spawnpointTag);
            if (spawnpointObjects != null && spawnpointObjects.Length > 0)
            {
                int spawnpointIndex = Random.Range(0, spawnpointObjects.Length);
                for (int i = 0; i < spawnpointObjects.Length; i++)
                {
                    if (i == spawnpointIndex)
                    {
                        spawnPos = spawnpointObjects[i].transform.position;
                        spawnRot = spawnpointObjects[i].transform.rotation;
                    }
                    else
                    {
                        spawnpointObjects[i].SetActive(false);
                    }
                }
            }

            if (playerPrefab != null)
            {
                activePlayer = Instantiate(playerPrefab, spawnPos, spawnRot);
            }
        }

        protected virtual void StartPlayModeObjects()
        {
            allObjects = ObjectManager.GetAllObjects();
            if (allObjects != null && allObjects.Count > 0)
            {
                for (int i = 0; i < allObjects.Count; i++)
                {
                    allObjects[i].StartPlayMode();
                }
            }
        }

        public virtual void StopPlayMode(ILevelEditor levelEditor)
        {
            DestroyPlayer();
            StopPlayModeObjects();
            ToggleEditorCamera(true);

            isPlaying = false;
            OnStopPlayMode?.Invoke();
        }

        protected virtual void DestroyPlayer()
        {
            if (activePlayer != null)
            {
                Destroy(activePlayer.gameObject);
            }

            if (spawnpointObjects != null && spawnpointObjects.Length > 0)
            {
                for (int i = 0; i < spawnpointObjects.Length; i++)
                {
                    spawnpointObjects[i].SetActive(true);
                }
            }
        }

        protected virtual void StopPlayModeObjects()
        {
            if (allObjects != null && allObjects.Count > 0)
            {
                for (int i = 0; i < allObjects.Count; i++)
                {
                    allObjects[i].StopPlayMode();
                }
            }
        }

        protected virtual void ToggleEditorCamera(bool isActive)
        {
            if (disableCameraDuringPlayMode && LevelEditor != null)
            {
                LevelEditor.LevelEditorCamera.CameraComponent.gameObject.SetActive(isActive);
            }
        }
    }
}