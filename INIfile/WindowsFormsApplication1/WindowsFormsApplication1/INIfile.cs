using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using WindowsFormsApplication1;

    class INIfile
    {
        private String _iniFileName = "";

        private String _separator = "=*|[]|@*=";

        private Hashtable data = new Hashtable();

        public INIfile(String fName){
            OpenSettings(fName);
        }
        private void OpenSettings(String fileName)
        {
            String input = null;
            _iniFileName = fileName;
            data.Clear();
            // create a writer and open the file
            try
            {
                TextReader tw = new StreamReader(fileName);
                while ((input = tw.ReadLine()) != null)
                {
                    if (input.Length > 3)
                    {
                        if (input[0] != '#' && input[0] != ';')
                        {
                            String[] result = input.Split(new String[] {  _separator}, StringSplitOptions.None);
                           
                            data.Add(result[0].Trim(), result[1].Trim());
                        }
                    }
                }
                // close the stream
                tw.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                SaveSettings(_iniFileName);
                OpenSettings(_iniFileName);

            }
        }


        public Hashtable getAll()
        {
           // ArrayList _list = null;
           // foreach (DictionaryEntry entry in data)
           // {
           //     Console.WriteLine("{0} = {1}", entry.Key, entry.Value);
            //}
           // return _list;
            Hashtable _ret = new Hashtable(data);
            return (_ret);
        }

        private void SaveSettings(String fileName)
        {
            // create a writer and open the file
            TextWriter tw = new StreamWriter(fileName);

            foreach (DictionaryEntry de in data){
                tw.WriteLine("{0}{1}{2}", de.Key, _separator, de.Value);
            }
            // close the stream
            tw.Close();
        }

        public String getValue(String key)
        {
            if (data.ContainsKey(key))
            {
                return (data[key].ToString());
            }
            return ("");
        }

        public bool setValue(String key,String val)
        {
            if (data.ContainsKey(key)) {
                data[key] = val;
            } else{
                data.Add(key, val);
            }
            SaveSettings(_iniFileName);
            return (true);
        }

        private String _getKey(String line)
        {
            String[] result = line.Split(new Char[] { '=' });
            return result[0].Trim(); 
        }

        private String _getValue(String line)
        {
            String[] result = line.Split(new Char[] { '=' });
            return result[1].Trim(); 
        }
    }
