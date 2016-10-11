using System.IO;
using System;
using System.Collections.Generic;

namespace paradoxSaveEditor
{
    public class fileReader
    {
        int lineNum;
        int charNum;

        List<string> text;

        public fileReader(string fileName, Encoding encoding = Encoding.GetEncoding("iso-8859-1"), int lineInit = 0, int charInit = 0)
        {
            lineNum = lineInit;
            charNum = charInit;
            reader = new StreamReader(fileName, encoding);
            text = reader.Read().Split('\n');
        }

        public char readChar()
    {
        if( text[lineNum].Length < charNum )
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

        public string readLine()
        {
            currentLineNum = lineNum;
            lineNum++;
            return text[currentLineNum];
        }

        public string readLine(int lineRequested)
        {
            return text[lineRequsted];
        }

        public void goTo(int line)
        {
            lineNum = line;
        }
    }
}
