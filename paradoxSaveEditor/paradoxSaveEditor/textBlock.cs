using System.IO;
using System;
using System.Collections.Generic;

namespace paradoxSaveEditor
{
	public class textBlock
	{
		public List<string> lines; ///The lines in the text block
        public char start;
        public char end;

		public textBlock(char startChar, char endChar, StreamReader reader)
		{
            start = startChar;
            int level = 0;
            end = endChar;
            lines = new List<string>();
            string line = "";
            char currentChar = (char)reader.Read();
            while (true)
            {
                if ( currentChar == '\n')
                {
                    if (line != "")
                    {
                        lines.Add(line);
                    }
                    line = "";
                }
                if (currentChar == startChar)
                {
                    level++;
                }
                if (level > 0)
                {
                    line = string.Concat(line, currentChar);
                }
                if (level==1 && currentChar == endChar)
                {
                    lines.Add(line);
                    break;
                }
                if ( currentChar == endChar)
                {
                    level = level - 1;
                }
                if (reader.Peek() != -1)
                {
                    currentChar = (char)reader.Read();
                }
                else
                {
                    break;
                }
            }
            int k = 0;
            while (k < lines.Count)
            {
                lines[k] = lines[k].Replace("\n", "");
                lines[k] = lines[k].Replace("\r", "");
                k++;
            }
		}

        public textBlock(char startChar, char endChar, fileReader reader)
        {
            start = startChar;
            int level = 0;
            end = endChar;
            lines = new List<string>();
            string line = reader.readLine();
            bool started = false;
            while ((!started || level > 0) && !reader.atEnd())
            {
                if (line != "")
                {
                    lines.Add(line);
                }
                level += line.Split(start).Length - 1;
                if ( level > 0 )
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

        public textBlock()
        {
            start = '{';
            end = '}';
            lines = new List<string>();
        }

        public string writeText()
        {
            List<string> linesWithNewlines = new List<string>();
            foreach(string s in lines)
            {
                linesWithNewlines.Add(s);
                linesWithNewlines.Add("\n");
            }
            linesWithNewlines.RemoveAt(linesWithNewlines.Count - 1);
            string result = String.Concat(linesWithNewlines);
            return result;
        }

        public int getPosition(string searchKey, bool exact = false, int layer = 1, int startingFrom = 0)
        {
            int i = startingFrom;
            int currentLayer = 0;
            while ( i<lines.Count )
            {
                string line = lines[i];
                string[] q = line.Split(start);
                currentLayer += line.Split(start).Length - 1;
                currentLayer -= line.Split(end).Length - 1;
                if ((!exact && currentLayer == layer && lines[i].Contains(searchKey)) || lines[i] == searchKey )
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

        public string getLine(int index)
        {
            return lines[index];
        }

        public void addLine(string line, int index)
        {
            lines.Insert(index, line);
        }

        public void removeLine(int index)
        {
            lines.RemoveAt(index);
        }

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
