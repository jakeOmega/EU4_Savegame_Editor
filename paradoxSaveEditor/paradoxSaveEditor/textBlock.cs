using System.IO;
using System;
using System.Collections.Generic;

namespace paradoxSaveEditor
{
    //A class for reading and parsing a block of text in an EU4 save game,
    //with the block encapsulated by curly brackets {}
    public class textBlock
    {
        public List<string> lines; ///The lines in the text block
        public char start; //The character that starts a block (usually "{")
        public char end; //The character that ends a block (usually "}")

        /// <summary>
        /// Creates a text block using a stream reader class to read from the file
        /// </summary>
        /// <param name="startChar">The character that begins a text block (usually {)</param>
        /// <param name="endChar">Th character that ends a text block (usually })</param>
        /// <param name="reader">The stream reader from which to read the text</param>
		public textBlock(char startChar, char endChar, StreamReader reader)
        {
            start = startChar;
            int level = 0;//how many sub-blocks deep we are
            end = endChar;
            lines = new List<string>();
            //build up lines character-by-character, and add them to our list of lines
            string line = "";
            char currentChar = (char)reader.Read();
            while (true)
            {
                if (currentChar == '\n')
                {
                    if (line != "")
                    {
                        lines.Add(line);
                    }
                    line = "";
                }
                //we are inside a sub-block
                if (currentChar == startChar)
                {
                    level++;
                }
                if (level > 0)
                {
                    line = string.Concat(line, currentChar);
                }
                //We just ended our outer block
                if (level == 1 && currentChar == endChar)
                {
                    lines.Add(line);
                    break;
                }
                //We just ended our current sub-block
                if (currentChar == endChar)
                {
                    level = level - 1;
                }
                //If we're not done with the file, read the next character
                if (reader.Peek() != -1)
                {
                    currentChar = (char)reader.Read();
                }
                else
                {
                    break;
                }
            }
            //Get rid of any extra new lines or carriage returns
            int k = 0;
            while (k < lines.Count)
            {
                lines[k] = lines[k].Replace("\n", "");
                lines[k] = lines[k].Replace("\r", "");
                k++;
            }
        }

        /// <summary>
        /// Creates a text block using a file reader class to read from the file
        /// </summary>
        /// <param name="startChar">The character that begins a text block (usually {)</param>
        /// <param name="endChar">Th character that ends a text block (usually })</param>
        /// <param name="reader">The file reader from which to read the text</param>
        public textBlock(char startChar, char endChar, fileReader reader)
        {
            start = startChar;
            int level = 0;
            end = endChar;
            lines = new List<string>();
            string line = reader.readLine();
            bool started = false;
            //While we are not done - we've started the text block, haven't finished it, and haven't finished the file
            while ((!started || level > 0) && !reader.atEnd())
            {
                if (line != "")
                {
                    lines.Add(line);
                }
                level += line.Split(start).Length - 1;
                if (level > 0)
                {
                    started = true;
                }
                level -= line.Split(end).Length - 1;
                line = reader.readLine();
            }
            int k = 0;
            while (k < lines.Count)
            {
                lines[k] = lines[k].Replace("\n", "");
                lines[k] = lines[k].Replace("\r", "");
                k++;
            }
        }

        /// <summary>
        /// A blank textBox
        /// </summary>
        public textBlock()
        {
            start = '{';
            end = '}';
            lines = new List<string>();
        }

        /// <summary>
        /// Converts textBlock object into plain text
        /// </summary>
        /// <returns>text of textBlock</returns>
        public string writeText()
        {
            //Add in new line characters
            List<string> linesWithNewlines = new List<string>();
            foreach (string s in lines)
            {
                linesWithNewlines.Add(s);
                linesWithNewlines.Add("\n");
            }
            linesWithNewlines.RemoveAt(linesWithNewlines.Count - 1);
            string result = String.Concat(linesWithNewlines);
            return result;
        }

        /// <summary>
        /// Finds the line number in which a search key is contained or the line in which
        /// a search key matches the line - returns -1 if key not found
        /// </summary>
        /// <param name="searchKey">the text to search the textBlock for</param>
        /// <param name="exact">A boolean of wheather the containing line must match the search key exactly (as opposed to merely containing it)</param>
        /// <param name="layer">At what layer of sub-block the key must be found</param>
        /// <param name="startingFrom">What line to begin the search from</param>
        /// <returns></returns>
        public int getPosition(string searchKey, bool exact = false, int layer = 1, int startingFrom = 0)
        {
            int i = startingFrom;
            int currentLayer = 0;
            while (i < lines.Count)
            {
                string line = lines[i];
                string[] q = line.Split(start);
                currentLayer += line.Split(start).Length - 1;
                currentLayer -= line.Split(end).Length - 1;
                if ((!exact && currentLayer == layer && lines[i].Contains(searchKey)) || lines[i] == searchKey)
                {
                    return i;
                }
                else
                {
                    i = i + 1;
                }
            }
            return -1;
        }

        /// <summary>
        /// Gets the text of a line by line number
        /// </summary>
        /// <param name="index">line number of desired line</param>
        /// <returns>text of that line</returns>
        public string getLine(int index)
        {
            return lines[index];
        }

        /// <summary>
        /// Insert a line at a specified index
        /// </summary>
        /// <param name="line">The line to insert</param>
        /// <param name="index">Where the specified line should be inserted</param>
        public void addLine(string line, int index)
        {
            lines.Insert(index, line);
        }

        /// <summary>
        /// Deletes a specified line
        /// </summary>
        /// <param name="index">The index of the line to be deleted</param>
        public void removeLine(int index)
        {
            lines.RemoveAt(index);
        }

        /// <summary>
        /// Find all lines that contain or exactly match a specified search key
        /// </summary>
        /// <param name="pattern">The text to be in the found lines</param>
        /// <param name="exact">A boolean on if the found lines must match they pattern exactly (or just contain it)</param>
        /// <returns>an array of idexes for the lines matching pattern</returns>
        public int[] findAll(string pattern, bool exact = false)
        {
            List<int> locations = new List<int>();
            int i = 0;
            while (i < lines.Count)
            {
                if (exact && lines[i] == pattern)
                {
                    locations.Add(i);
                }
                if (!exact && lines[i].Contains(pattern))
                {
                    locations.Add(i);
                }
                i++;
            }
            return locations.ToArray();
        }

        /// <summary>
        /// Finds all lines for which pattern(line) is true, at specified layer
        /// </summary>
        /// <param name="pattern">Boolean function lines must make true</param>
        /// <param name="layer">The layer at which we want results</param>
        /// <param name="start">The starting character for a sub-block layer (usually {)</param>
        /// <param name="end">The ending character for a sub-block layer (ususally })</param>
        /// <returns>An array of locations matching pattern</returns>
        public int[] findAll(Func<string, bool> pattern, int layer, char start, char end)
        {
            int currentLayer = 0;
            List<int> locations = new List<int>();
            int i = 0;
            while (i < lines.Count)
            {
                string line = lines[i];
                currentLayer += line.Split(start).Length - 1;
                currentLayer -= line.Split(end).Length - 1;
                if (pattern(lines[i]) && layer == currentLayer)
                {
                    locations.Add(i);
                }
                i++;
            }
            return locations.ToArray();
        }

        /// <summary>
        /// Find all lines that contain or exactly match a specified search key
        /// </summary>
        /// <param name="pattern">The text to be in the found lines</param>
        /// <param name="layer">The layer at which we want results</param>
        /// <param name="start">The starting character for a sub-block layer (usually {)</param>
        /// <param name="end">The ending character for a sub-block layer (ususally })</param>
        /// <param name="exact">A boolean on if the found lines must match they pattern exactly (or just contain it)</param>
        /// <returns>an array of idexes for the lines matching pattern</returns>
        public int[] findAll(string pattern, int layer, char start, char end, bool exact = false)
        {
            int currentLayer = 0;
            List<int> locations = new List<int>();
            int i = 0;
            while (i < lines.Count)
            {
                currentLayer += lines[i].Split(start).Length - 1;
                currentLayer -= lines[i].Split(end).Length - 1;
                if (exact && lines[i] == pattern && layer == currentLayer)
                {
                    locations.Add(i);
                }
                if (!exact && lines[i].Contains(pattern) && layer == currentLayer)
                {
                    locations.Add(i);
                }
                i++;
            }
            return locations.ToArray();
        }
    };
}
