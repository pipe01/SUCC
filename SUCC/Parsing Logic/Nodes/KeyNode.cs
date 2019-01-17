﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace SUCC
{
    /// <summary>
    /// Represents a line of text in a SUCC file that contains data addressed by key.
    /// </summary>
    internal class KeyNode : Node
    {
        public KeyNode(string rawText) : base(rawText) { }
        public KeyNode(int indentation, string key) : base(indentation)
        {
            // todo add filestyle stuff here
            int spacesBeforeColon = 0;
            int spacesAfterColon = 1;

            RawText += 
                key.AddSpaces(spacesBeforeColon) 
                + ":".AddSpaces(spacesAfterColon);
        }

        public string Key
        {
            get
            {
                var text = GetDataText();
                int ColonIndex = GetColonIndex(text);

                text = text.Substring(0, ColonIndex);
                text = text.TrimEnd(); // remove trailing spaces
                return text;
            }
        }

        public override string Value
        {
            get
            {
                var text = GetDataText();
                int ColonIndex = GetColonIndex(text);

                text = text.Substring(ColonIndex + 1);
                text = text.TrimStart();
                return text;
                // note that trailing spaces are already trimmed in GetDataText()
            }
            set
            {
                var text = GetDataText();
                int colonIndex = GetColonIndex(text);

                string aftercolon = text.Substring(colonIndex + 1);
                int spacesAfterColon = aftercolon.GetIndentationLevel();

                SetDataText(
                    text.Substring(startIndex: 0, length: colonIndex + spacesAfterColon + 1) + value
                    );
            }
        }

        private int GetColonIndex(string text)
        {
            int ColonIndex = text.IndexOf(':');

            if (ColonIndex < 0)
                throw new FormatException("Key node comprised of the following text: " + RawText + " did not contain the character ':'");
            return ColonIndex;
        }
    }
}