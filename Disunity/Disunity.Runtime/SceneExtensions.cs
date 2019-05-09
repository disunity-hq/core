﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;


namespace Disunity.Runtime {

    /// <summary>
    ///     Extensions for the Scene class.
    /// </summary>
    public static class SceneExtensions {

        /// <summary>
        ///     Unload this Scene.
        /// </summary>
        /// <param name="self">A Scene instance.</param>
        public static void Unload(this Scene self) {
            if (self.isLoaded && self.IsValid()) {
                SceneManager.UnloadSceneAsync(self.name);
            }
        }

        /// <summary>
        ///     Get a Component of Type T in this Scene. Returns the first found Component.
        /// </summary>
        /// <typeparam name="T">A Type that derives from Component</typeparam>
        /// <param name="self">A Scene instance.</param>
        /// <returns>A Component of Type T or null if none is found.</returns>
        public static T GetComponentInScene<T>(this Scene self) {
            if (!self.isLoaded || !self.IsValid()) {
                return default;
            }

            foreach (var go in self.GetRootGameObjects()) {
                var component = go.GetComponentInChildren<T>(true);
                if (component != null) {
                    return component;
                }
            }

            return default;
        }

        /// <summary>
        ///     Get a Component of Type componentType in this Scene. Returns the first found Component.
        /// </summary>
        /// <param name="self">A Scene instance.</param>
        /// <param name="componentType">The Component Type to look for</param>
        /// <returns>A component of Type componentType or null if none is found.</returns>
        public static Component GetComponentInScene(this Scene self, Type componentType) {
            if (!self.isLoaded || !self.IsValid()) {
                return null;
            }

            foreach (var go in self.GetRootGameObjects()) {
                var component = go.GetComponentInChildren(componentType, true);
                if (component != null) {
                    return component;
                }
            }

            return null;
        }

        /// <summary>
        ///     Get all components of Type T in this Scene.
        /// </summary>
        /// <typeparam name="T">A Type that derives from Component.</typeparam>
        /// <param name="self">A Scene instance.</param>
        /// <returns>An array of found Components of Type T.</returns>
        public static T[] GetComponentsInScene<T>(this Scene self) {
            var components = new List<T>();

            if (!self.isLoaded || !self.IsValid()) {
                return components.ToArray();
            }

            foreach (var go in self.GetRootGameObjects()) {
                components.AddRange(go.GetComponentsInChildren<T>(true));
            }

            return components.ToArray();
        }

        /// <summary>
        ///     Get all Components of type componentType in this Scene.
        /// </summary>
        /// <param name="self">A Scene Instance.</param>
        /// <param name="componentType">A Type that derives from Component.</param>
        /// <returns>An array of found Components of Type componentType.</returns>
        public static Component[] GetComponentsInScene(this Scene self, Type componentType) {
            var components = new List<Component>();

            if (!self.isLoaded || !self.IsValid()) {
                return components.ToArray();
            }

            foreach (var go in self.GetRootGameObjects()) {
                components.AddRange(go.GetComponentsInChildren(componentType, true));
            }

            return components.ToArray();
        }

        /// <summary>
        ///     Instantiate a GameObject and place it in this Scene.
        /// </summary>
        /// <param name="self">A Scene instance.</param>
        /// <param name="original">An existing object that you want to make a copy of.</param>
        /// <returns>The instantiated GameObject.</returns>
        public static GameObject Instantiate(this Scene self, GameObject original) {
            var o = Object.Instantiate(original);

            if (!self.isLoaded || !self.IsValid()) {
                return o;
            }

            if (o != null) {
                SceneManager.MoveGameObjectToScene(o, self);
                return o;
            }

            return null;
        }

    }

}