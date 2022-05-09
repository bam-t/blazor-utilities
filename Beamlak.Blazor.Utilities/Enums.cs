using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beamlak.Blazor.Utilities
{
    public static class Enums
    {
        public enum Color
        {
            Dark,
            Inherit,
            Light
        }

        public enum EditorMode
        {
            Inline,
            Normal,
            Readonly
        }

        public enum ToolbarOption
        {
            Full,
            Minimal
        }

        public enum BrowserStorageType
        {
            LocalStorage,
            SessionStorage,
        }
    }
}
