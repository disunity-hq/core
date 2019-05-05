﻿using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;


namespace Disunity.Interface {

    /// <summary>
    ///     A MonoBehaviour class with some added functionality for Mods.
    /// </summary>
    public abstract class ModBehaviour : MonoBehaviour, IModHandler {

        /// <summary>
        ///     This Mod's ContentHandler, which provides the Mod's prefabs, scenes and Instantiate and AddComponent methods.
        /// </summary>
        public ContentHandler ContentHandler { get; set; }

        /// <summary>
        ///     Called when the Mod is loaded.
        /// </summary>
        /// <param name="contentHandler">The Mod's ContentHandler</param>
        public virtual void OnLoaded(ContentHandler contentHandler) {
            ContentHandler = contentHandler;
        }

        /// <summary>
        ///     Called when the mod is unloaded.
        /// </summary>
        public void OnUnloaded() {
        }

        /// <summary>
        ///     Use this instead of the static Instantiate methods in UnityEngine.Object. This ensures objects are spawned in the
        ///     right scene.
        /// </summary>
        /// <param name="original">An existing object that you want to make a copy of.</param>
        /// <param name="position">Position for the new object.</param>
        /// <param name="rotation">Orientation of the new object.</param>
        /// <returns>A clone of the original object.</returns>
        public new Object Instantiate(Object original, Vector3 position, Quaternion rotation) {
            if (ContentHandler == null) {
                Debug.LogWarning(
                    "ModBehaviour " + GetType().Name +
                    " has not been initialized. Please do not use Instantiate() in Awake() or OnEnable().", gameObject);
                return null;
            }

            var o = ContentHandler.Instantiate(original, position, rotation);

            if (o is GameObject go) {
                SceneManager.MoveGameObjectToScene(go, gameObject.scene);
            }

            return o;
        }

        /// <summary>
        ///     Use this instead of the static methods in UnityEngine.Object. This ensures objects are spawned in the right scene.
        /// </summary>
        /// <param name="original">An existing object that you want to make a copy of.</param>
        /// <returns>A clone of the original object.</returns>
        public new Object Instantiate(Object original) {
            return Instantiate(original, Vector3.zero, Quaternion.identity);
        }

        /// <summary>
        ///     Use this instead of the static methods in UnityEngine.Object. This ensures objects are spawned in the right scene.
        /// </summary>
        /// <typeparam name="T">The type of the original.</typeparam>
        /// <param name="original">An existing Object to copy.</param>
        /// <returns>A clone of the original object.</returns>
        public new T Instantiate<T>(T original) where T : Object {
            if (ContentHandler == null) {
                Debug.LogWarning("ModBehaviour " + GetType().Name +
                                 " has not been intialized. Please do not use Instantiate() in Awake() or OnEnable().");
                return default;
            }

            var o = ContentHandler.Instantiate(original);

            if (o is GameObject go) {
                SceneManager.MoveGameObjectToScene(go, gameObject.scene);
            }

            return o;
        }

        /// <summary>
        ///     Add a Component to this Component's GameObject.
        /// </summary>
        /// <typeparam name="T">The Component to add.</typeparam>
        /// <returns>The Added Component.</returns>
        public T AddComponent<T>() where T : Component {
            return AddComponent<T>(gameObject);
        }

        /// <summary>
        ///     Add a Component to a GameObject
        /// </summary>
        /// <typeparam name="T">The Type of the Component to add.</typeparam>
        /// <param name="gobj">The GameObject to add the Component to.</param>
        /// <returns>The added Component</returns>
        public T AddComponent<T>(GameObject gobj) where T : Component {
            if (ContentHandler == null) {
                Debug.LogWarning(
                    "ModBehaviour " + GetType().Name +
                    " has not been initialized. Please do not use AddComponent() in Awake() or OnEnable().", gobj);
                return default;
            }

            return ContentHandler.AddComponent<T>(gobj);
        }

        /// <summary>
        ///     Add a Component this Component's GameObject
        /// </summary>
        /// <param name="componentType">The Type of the Component to add.</param>
        /// <returns>The added Component</returns>
        public Component AddComponent(Type componentType) {
            return AddComponent(componentType, gameObject);
        }

        /// <summary>
        ///     Add a Component to a GameObject
        /// </summary>
        /// <param name="componentType">The Type of the Component to add.</param>
        /// <param name="gobj">The GameObject to add the Component to.</param>
        /// <returns>The added Component</returns>
        public Component AddComponent(Type componentType, GameObject gobj) {
            if (ContentHandler == null) {
                Debug.LogWarning(
                    "ModBehaviour " + GetType().Name +
                    " has not been intialized. Please do not use AddComponent() in Awake() or OnEnable().", gobj);
                return null;
            }

            return ContentHandler.AddComponent(componentType, gobj);
        }

        /// <summary>
        ///     Remove an object.
        /// </summary>
        /// <param name="obj">The Object to destroy.</param>
        public new void Destroy(Object obj) {
            if (ContentHandler == null) {
                Debug.LogWarning(
                    "ModBehaviour " + GetType().Name +
                    " has not been intialized. Please do not use Destroy() in Awake() or OnEnable().", gameObject);
                return;
            }

            ContentHandler.Destroy(obj);
        }

    }

}