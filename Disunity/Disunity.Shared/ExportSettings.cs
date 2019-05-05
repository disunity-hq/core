﻿using UnityEngine;


namespace Disunity.Shared {

    public class ExportSettings : ScriptableObject {

        /// <summary>
        ///     The Mod's name.
        /// </summary>
        [field: SerializeField] public string Name { get; set; }

        /// <summary>
        ///     The Mod's author.
        /// </summary>
        [field: SerializeField] public string Author { get; set; }

        /// <summary>
        ///     The Mod's description.
        /// </summary>
        [field: SerializeField] public string Description { get; set; }

        /// <summary>
        ///     The Mod's version.
        /// </summary>
        [field: SerializeField] public string Version { get; set; }

        /// <summary>
        ///     The directory to which the Mod will be exported.
        /// </summary>
        [field: SerializeField] public Object[] PreloadAssemblies { get; set; } = { };

        [field: SerializeField] public Object[] RuntimeAssemblies { get; set; } = { };

        [field: SerializeField] public Object[] Artifacts { get; set; } = { };
        [field: SerializeField] public Object[] Prefabs { get; set; } = { };
        [field: SerializeField] public Object[] Scenes { get; set; } = { };

        [field: SerializeField] public string OutputDirectory { get; set; }

        [field: SerializeField] public ContentType ContentTypes { get; set; }

        [field: SerializeField] public BootType BootType { get; set; }

        [field: SerializeField] public string StartupClass { get; set; }

        [field: SerializeField] public string StartupAssembly { get; set; }

        [field: SerializeField] public string StartupScene { get; set; }

        [field: SerializeField] public GameObject StartupPrefab { get; set; }

    }

}