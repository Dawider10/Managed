﻿using System;
using System.IO;
using System.Reflection;
using Noesis;

namespace NoesisApp
{
    public class EmbeddedFontProvider : FontProvider
    {
        public EmbeddedFontProvider(Assembly assembly, string ns) : this(assembly, ns, null)
        {
        }

        public EmbeddedFontProvider(Assembly assembly, string ns, FontProvider provider)
        {
            _assembly = assembly;
            _namespace = ns;
            _provider = provider;

            RegisterFontResources();

            if (_provider != null)
            {
                _provider.FontChanged += (Uri baseUri, string familyName, FontWeight weight,
                    FontStretch stretch, FontStyle style) =>
                {
                    _provider.RaiseFontChanged(baseUri, familyName, weight, stretch, style);
                };
            }
        }

        private void RegisterFontResources()
        {
            if (_assembly == null || string.IsNullOrEmpty(_namespace)) return;

            string component = _assembly.GetName().Name;
            foreach (string name in _assembly.GetManifestResourceNames())
            {
                if (name.EndsWith(".ttf", StringComparison.OrdinalIgnoreCase) ||
                    name.EndsWith(".ttc", StringComparison.OrdinalIgnoreCase) ||
                    name.EndsWith(".otf", StringComparison.OrdinalIgnoreCase))
                {
                    string resource = name.Substring(_namespace.Length + 1);
                    int lastDot = resource.LastIndexOf('.', resource.Length - 5);
                    string folder = lastDot != -1 ? resource.Substring(0, lastDot).Replace('.', '/') : string.Empty;
                    string filename = lastDot != -1 ? resource.Substring(lastDot + 1) : resource;

                    RegisterFont(new Uri(folder, UriKind.RelativeOrAbsolute), filename);

                    folder = $"/{component};component/{folder}";
                    RegisterFont(new Uri(folder, UriKind.RelativeOrAbsolute), filename);
                }
            }
        }

        public override FontSource MatchFont(Uri baseUri, string familyName,
            ref FontWeight weight, ref FontStretch stretch, ref FontStyle style)
        {
            if (_provider != null)
            {
                FontSource font = _provider.MatchFont(baseUri, familyName, ref weight, ref stretch, ref style);
                if (font.file != null)
                {
                    return font;
                }
            }

            return base.MatchFont(baseUri, familyName, ref weight, ref stretch, ref style);
        }

        public override bool FamilyExists(Uri baseUri, string familyName)
        {
            if (_provider != null)
            {
                if (_provider.FamilyExists(baseUri, familyName))
                {
                    return true;
                }
            }

            return base.FamilyExists(baseUri, familyName);
        }

        public override void ScanFolder(Uri folder)
        {
            if (_provider != null)
            {
                _provider.ScanFolder(folder);
            }
        }

        public override Stream OpenFont(Uri folder, string filename)
        {
            Stream stream = null;

            if (_provider != null)
            {
                stream = _provider.OpenFont(folder, filename);
            }

            if (stream == null)
            {
                string path = folder.GetPath();
                if (path.Length > 0 && !path.EndsWith("/")) path += "/";
                path += filename;

                stream = Application.GetAssemblyResource(_assembly, _namespace, path);
            }

            return stream;
        }

        private Assembly _assembly;
        private string _namespace;
        private FontProvider _provider;
    }
}
