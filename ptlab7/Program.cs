using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace ptlab7
{
    class Program
    {
        static void Main(string[] args)
        {
            SortedDictionary<string, long> directoryElements = new SortedDictionary<string, long>(new NameComparer());
            AddToDictionary(new DirectoryInfo(args[0]),directoryElements);
            SerializeDictionary(directoryElements);
            directoryElements = DeserializeDictionary();
            foreach (var name in directoryElements)
            {
                Console.WriteLine("{0} -> {1}", name.Key, name.Value);
            }
            DisplayDirectoryRecursive(new DirectoryInfo(args[0]), 0);


        }
        static void DisplayDirectoryRecursive(FileSystemInfo fsi, int depth)
        {

            int size = Directory.GetFiles(fsi.FullName).Length + Directory.GetDirectories(fsi.FullName).Length;
            for (int i = 0; i < depth; i++) Console.Write("   ");
            Console.Write("{0} ({1}) {2}\n", fsi.Name, size,fsi.GetRahs());

            foreach (string entry in Directory.GetDirectories(fsi.FullName))
            {
                DisplayDirectoryRecursive(new DirectoryInfo(entry), depth + 1);
            }

            foreach (string entry in Directory.GetFiles(fsi.FullName))
            {
                FileInfo fi = new FileInfo(entry);
                for (int i = 0; i < depth + 1; i++) Console.Write("   ");
                Console.Write("{0} {1} bajtow {2}\n", fi.Name, fi.Length, fi.GetRahs());
            }
            return;
        }
        static void AddToDictionary(DirectoryInfo dir, SortedDictionary<string, long> dictionary)
        {
            foreach (DirectoryInfo entry in dir.GetDirectories())
            {
                int size = Directory.GetFiles(entry.FullName).Length + Directory.GetDirectories(entry.FullName).Length;
                dictionary.Add(entry.Name, size);
            }
            foreach (FileInfo entry in dir.GetFiles())
            {
                dictionary.Add(entry.Name, entry.Length);
            }
        }

        static void SerializeDictionary(SortedDictionary<string,long> dictionary)
        {
            FileStream fs = new FileStream("Dictionary.dat", FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(fs, dictionary);
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to serialize. Reason: " + e.Message);
                throw;
            }
            finally
            {
                fs.Close();
            }
        }

        static SortedDictionary<string, long> DeserializeDictionary()
        {
            SortedDictionary<string, long> dictionary = null;
            FileStream fs = new FileStream("Dictionary.dat", FileMode.Open);
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                dictionary = (SortedDictionary<string, long>)formatter.Deserialize(fs);
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to deserialize. Reason: " + e.Message);
                throw;
            }
            finally
            {
                fs.Close();
            }
            return dictionary;
        }
    }
}