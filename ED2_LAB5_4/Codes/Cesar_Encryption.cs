using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Web;

namespace ED2_LAB5_4.Codes
{
    public class Cesar_Encryption
    {
        static Dictionary<string, int> original = new Dictionary<string, int>();
        static Dictionary<string, int> encryption = new Dictionary<string, int>();
        string route = string.Empty;
        static bool empty = true;
        //Crear diccionario original.
        public void original_generate()
        {
            if(empty)
            {
                var upper = 32;
                var counter = 1;
                for(int x = 0; x < 256; x++)
                {
                    original.Add(Convert.ToString((char)upper), counter);
                    counter++;
                    upper++;
                }
                //Suma al contador del diccionario.
                original.Add("\r\n", counter + 1);
                empty = false;
            }
        }
        //Crear el diccionario para cifrado.
        public void encryption_generate(string key)
        {
            var counter = 1;
            //Añadir clave al diccionario.
            if (key != null)
            {
                foreach (var letter in key)
                {
                    if (!encryption.ContainsKey(Convert.ToString(letter)))
                    {
                        encryption.Add(Convert.ToString(letter), counter);
                        counter++;
                    }
                }
            }
            //Comparación de diccionario original con el de cifrado para el ingreso de letras.
            foreach (var item in original.Keys)
            {
                if (!encryption.ContainsKey(item))
                {
                    encryption.Add(item, counter);
                    counter++;
                }
            }
        }
        //Cifrar texto.
        public void text_encryption(byte[] bytes)
        {
            var text = string.Empty;
            foreach(char letter in bytes)
            {
                //Convertir los caracteres
                var original_value = original.LastOrDefault(x => x.Key == Convert.ToString(letter)).Value;
                var encryption_value = encryption.LastOrDefault(x => x.Value == original_value).Key;
                if(original_value == 0)
                {
                    encryption_value = Convert.ToString(letter);
                }
                text += encryption_value;
            }
            using (var write = new FileStream(route + "\\..\\Files\\archivoCifradoCesar.cif", FileMode.OpenOrCreate))
            {
                using(var writing = new BinaryWriter(write))
                {
                    writing.Seek(0, SeekOrigin.End);
                    writing.Write(System.Text.Encoding.Unicode.GetBytes(text));
                }
            }
        }
        //Descifrar texto.
        private void text_decryption(byte[] bytes)
        {
            var text = string.Empty;
            foreach (char letter in bytes)
            {
                //Conversión de caracteres.
                var encryption_value = encryption.LastOrDefault(x => x.Key == Convert.ToString(letter)).Value;
                var decryption_value = original.LastOrDefault(x => x.Value == encryption_value).Key;
                if(decryption_value == "\0")
                {
                    decryption_value = Convert.ToString(letter);
                }
                if(decryption_value == "\r")
                {
                    decryption_value = original.LastOrDefault(x => x.Value == encryption_value).Key;
                }
                text += decryption_value;
            }
            using (var write = new FileStream(route + "\\..\\Files\\archivoDecifradoCesar.txt", FileMode.OpenOrCreate))
            {
                using(var writing = new BinaryWriter(write))
                {
                    writing.Write(System.Text.Encoding.Unicode.GetBytes(text));
                }
            }
        }

    }
}
