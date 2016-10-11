using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace paradoxSaveEditor
{
    public class fileReader
    {
        int lineNum;
        int charNum;

        string[] text;

        public fileReader(string fileName, Encoding encoding = null, int lineInit = 0, int charInit = 0)
        {
            if ( encoding == null )
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

        public char readChar()
        {
            if( text[lineNum].Length > charNum )
            {
                charNum++;
                return text[lineNum][charNum-1];
            }
            else
            {
                charNum = 0;
                lineNum++;
                return '\n';
            }
        }
  
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

        public string readLine()
        {
            charNum = 0;
            int currentLineNum = lineNum;
            lineNum++;
            return text[currentLineNum];
        }

        public string peekLine()
        {
            return text[lineNum];
        }


        public string readLine(int lineRequested)
        {
            return text[lineRequested];
        }

        public void deleteLine(int index)
        {
            List<string> tempText = new List<string>(text);
            tempText.RemoveAt(index);
            text = tempText.ToArray();
        }

        public void addLine(string line)
        {
            List<string> tempText = new List<string>(text);
            tempText.Insert(lineNum, line);
            text = tempText.ToArray();
        }

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

        public void goTo(int line)
        {
            lineNum = line;
        }

        public int length()
        {
            return text.Length;
        }

        public void moveBy(int line)
        {
            lineNum += line;
        }

        public bool atEnd()
        {
            if ( lineNum == text.Length )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int[] findAll(string pattern, bool exact = false)
        {
            List<int> locations = new List<int>();
            int i = 0;
            while (i < text.Length)
            {
                if ( exact && text[i]==pattern )
                {
                    locations.Add(i);
                }
                if ( !exact && text[i].Contains(pattern))
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

        public void changeLine(int line, string newLine)
        {
            text[line] = newLine;
        }

        public void removeLine(int index)
        {
            List<string> newText = text.ToList<string>();
            newText.RemoveAt(index);
            text = newText.ToArray();
        }

        public void setLineNum(int newLineNum)
        {
            lineNum = newLineNum;
        }

        public void setCharNum(int newCharNum)
        {
            charNum = newCharNum;
        }

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
            while(!atEnd())
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
