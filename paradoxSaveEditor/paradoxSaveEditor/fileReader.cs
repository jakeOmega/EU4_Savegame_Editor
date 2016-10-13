using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace paradoxSaveEditor
{
    /// <summary>
    /// A class for reading files and changing them in a way convienient for save game editing
    /// </summary>
    public class fileReader
    {
        int lineNum;
        int charNum;

        string[] text;

        /// <summary>
        /// Initialize a fileReader with a file name
        /// </summary>
        /// <param name="fileName">The file name you want to read</param>
        /// <param name="encoding">The character encoding used by the file</param>
        /// <param name="lineInit">What line the reader should start at, defaults to 0</param>
        /// <param name="charInit">The character in the starting line to start at, defaults to zero</param>
        public fileReader(string fileName, Encoding encoding = null, int lineInit = 0, int charInit = 0)
        {
            if (encoding == null)
            {
                encoding = Encoding.GetEncoding("iso-8859-1");
            }
            lineNum = lineInit;
            charNum = charInit;
            StreamReader reader = new StreamReader(fileName, encoding);
            string s = reader.ReadToEnd();
            text = s.Split('\n');
            reader.Close();
        }

        /// <summary>
        /// Reads one charater from the current position
        /// </summary>
        /// <returns>The next character</returns>
        public char readChar()
        {
            if (text[lineNum].Length > charNum)
            {
                charNum++;
                return text[lineNum][charNum - 1];
            }
            else
            {
                charNum = 0;
                lineNum++;
                return '\n';
            }
        }

        /// <summary>
        /// Peeks at the next character without advancing the current position
        /// </summary>
        /// <returns>The next character</returns>
        public char peekChar()
        {
            if (text[lineNum].Length < charNum)
            {
                return text[lineNum][charNum - 1];
            }
            else
            {
                return '\n';
            }
        }

        /// <summary>
        /// Reads the next line from the current position
        /// </summary>
        /// <returns>The next line</returns>
        public string readLine()
        {
            charNum = 0;
            int currentLineNum = lineNum;
            lineNum++;
            return text[currentLineNum];
        }

        /// <summary>
        /// Returns the next line from the current position without advancing to the next line
        /// </summary>
        /// <returns>The next line</returns>
        public string peekLine()
        {
            return text[lineNum];
        }

        /// <summary>
        /// Reads a particular line
        /// </summary>
        /// <param name="lineRequested">The line number of the line to read</param>
        /// <returns>The line at the specified position</returns>
        public string readLine(int lineRequested)
        {
            return text[lineRequested];
        }

        /// <summary>
        /// Deletes a line at a particular position
        /// </summary>
        /// <param name="index">The index of the line to delete</param>
        public void deleteLine(int index)
        {
            List<string> tempText = new List<string>(text);
            tempText.RemoveAt(index);
            text = tempText.ToArray();
        }

        /// <summary>
        /// Add a line to the current position
        /// </summary>
        /// <param name="line">The text of the line to add</param>
        public void addLine(string line)
        {
            List<string> tempText = new List<string>(text);
            tempText.Insert(lineNum, line);
            text = tempText.ToArray();
        }

        /// <summary>
        /// Add a line to the specified position
        /// </summary>
        /// <param name="line">The text of the line to add</param>
        /// <param name="index">The index at which to insert the line</param>
        public void addLine(string line, int index)
        {
            List<string> tempText = new List<string>(text);
            tempText.Insert(index, line);
            text = tempText.ToArray();
            if (lineNum >= index)
            {
                lineNum++;
            }
        }

        /// <summary>
        /// Set the current position of the reader to a specified line
        /// </summary>
        /// <param name="line">The line to go to</param>
        public void goTo(int line)
        {
            lineNum = line;
            charNum = 0;
        }

        /// <summary>
        /// The number of lines in the text of the file being parsed
        /// </summary>
        /// <returns></returns>
        public int length()
        {
            return text.Length;
        }

        /// <summary>
        /// Shift the current line by a specified amount
        /// </summary>
        /// <param name="line">The number of lines to move from the current line</param>
        public void moveBy(int line)
        {
            lineNum += line;
            charNum = 0;
        }

        /// <summary>
        /// Checks if the reader is at the end of the file
        /// </summary>
        /// <returns>True if we are at the end of the file, zero otherwise</returns>
        public bool atEnd()
        {
            if (lineNum >= text.Length)
            {
                return true;
            }
            else
            {
                return false;
            }
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
            while (i < text.Length)
            {
                if (exact && text[i] == pattern)
                {
                    locations.Add(i);
                }
                if (!exact && text[i].Contains(pattern))
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
        /// <param name="exact">A boolean on if the found lines must match they pattern exactly (or just contain it)</param>
        /// <returns>An array of locations matching pattern</returns>
        public int[] findAll(string pattern, int layer, char start, char end, bool exact = false)
        {
            int currentLayer = 0;
            List<int> locations = new List<int>();
            int i = 0;
            while (i < text.Length)
            {
                string line = text[i];
                currentLayer += line.Split(start).Length - 1;
                currentLayer -= line.Split(end).Length - 1;
                if (exact && text[i] == pattern && layer == currentLayer)
                {
                    locations.Add(i);
                }
                if (!exact && text[i].Contains(pattern) && layer == currentLayer)
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
        /// <returns>an array of idexes for the lines matching pattern</returns>
        public int[] findAll(Func<string, bool> pattern, int layer, char start, char end)
        {
            int currentLayer = 0;
            List<int> locations = new List<int>();
            int i = 0;
            while (i < text.Length)
            {
                string line = text[i];
                currentLayer += line.Split(start).Length - 1;
                currentLayer -= line.Split(end).Length - 1;
                if (pattern(text[i]) && layer == currentLayer)
                {
                    locations.Add(i);
                }
                i++;
            }
            return locations.ToArray();
        }

        /// <summary>
        /// Find the first line that contain or exactly match a specified search key
        /// </summary>
        /// <param name="pattern">The text to be in the found lines</param>
        /// <param name="layer">The layer at which we want results</param>
        /// <param name="start">The starting character for a sub-block layer (usually {)</param>
        /// <param name="end">The ending character for a sub-block layer (ususally })</param>
        /// <param name="exact">A boolean on if the found lines must match they pattern exactly (or just contain it)</param>
        /// <returns>an array of idexes for the lines matching pattern</returns>
        public int getPosition(string pattern, int layer, char start, char end, bool exact = false)
        {
            int currentLayer = 0;
            int i = 0;
            while (i < text.Length)
            {
                string line = text[i];
                currentLayer += line.Split(start).Length - 1;
                currentLayer -= line.Split(end).Length - 1;
                if (exact && text[i] == pattern && layer == currentLayer)
                {
                    return i;
                }
                if (!exact && text[i].Contains(pattern) && layer == currentLayer)
                {
                    return i;
                }
                i++;
            }
            return -1;
        }

        /// <summary>
        /// Change one line's text
        /// </summary>
        /// <param name="line">The line number to change</param>
        /// <param name="newLine">The new text of the line</param>
        public void changeLine(int line, string newLine)
        {
            text[line] = newLine;
        }

        /// <summary>
        /// Deletes one line
        /// </summary>
        /// <param name="index">index of the line to delete</param>
        public void removeLine(int index)
        {
            List<string> newText = text.ToList<string>();
            newText.RemoveAt(index);
            text = newText.ToArray();
        }

        /// <summary>
        /// Change the current line of the reader
        /// </summary>
        /// <param name="newLineNum">The new current line index</param>
        public void setLineNum(int newLineNum)
        {
            lineNum = newLineNum;
        }

        /// <summary>
        /// Change the current character position of the reader
        /// </summary>
        /// <param name="newCharNum">The new current character index</param>
        public void setCharNum(int newCharNum)
        {
            charNum = newCharNum;
        }

        /// <summary>
        /// Write the text (with any chages made) to disk
        /// </summary>
        /// <param name="outFile">The file location to save to</param>
        /// <param name="encoding">The encoding to use, defaults to iso-8859-1</param>
        public void writeFile(string outFile, Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.GetEncoding("iso-8859-1");
            }
            int oldLine = lineNum;
            int oldChar = charNum;
            string line;
            StreamWriter writer = new StreamWriter(outFile, false, encoding);
            lineNum = 0;
            charNum = 0;
            while (!atEnd())
            {
                line = readLine();
                line = line.Replace("\n", "");
                line = line.Replace("\r", "");
                if (atEnd())
                {
                    writer.Write(line);
                }
                else
                {
                    writer.WriteLine(line);
                }
            }
            lineNum = oldLine;
            charNum = oldChar;
            writer.Close();
        }
    }
}
