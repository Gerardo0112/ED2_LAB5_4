using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Web;


namespace ED2_LAB5_4.Codes
{
    public class Spiral_Encryption
    {
        string route = string.Empty;
        string text = string.Empty;
        string text_m = string.Empty;
        //Lectura de archivo.
        public void lecture(string file)
        {
            int length = 100000;
            var bytes = new byte[length];

            using (var stream = new FileStream(file, FileMode.Open))
            {
                using (var reading = new BinaryReader(stream))
                {
                    while (reading.BaseStream.Position != reading.BaseStream.Length)
                    {
                        bytes = reading.ReadBytes(length);
                        foreach (char letter in bytes)
                        {
                            if (letter != 0)
                            {
                                text += letter;
                            }
                        }
                    }
                }
            }
        }
        //Escritura en cifrado.
        public void file_encrypted(string text_)
        {
            //Escribir texto cifrado en el archivo.
            using (var write = new FileStream(route + "\\..\\Files\\archivoCifradoEspiral.cif", FileMode.OpenOrCreate))
            {
                using (var writing = new BinaryWriter(write))
                {
                    writing.Seek(0, SeekOrigin.End);
                    writing.Write(System.Text.Encoding.Unicode.GetBytes(text_));
                }
            }
            text_m = string.Empty;
        }
        //Escritura en descifrado.
        public void file_decrypted(string text_)
        {
            //Escribir texto descifrado en el archivo.
            using (var write = new FileStream(route + "\\..\\Files\\archivoDescifradoEspiral.txt", FileMode.OpenOrCreate))
            {
                using (var writing = new BinaryWriter(write))
                {
                    writing.Seek(0, SeekOrigin.End);
                    writing.Write(System.Text.Encoding.Unicode.GetBytes(text_));
                }
            }
        }
        //Recorrido vertical
        public void down_path(int value_m, int value_n, char[,] matrix)
        {
            //Recorrer matriz en espiral
            int x, aux_m = 0, aux_n = 0;
            while (aux_m < value_m && aux_n < value_n)
            {
                for (x = aux_m; x < value_m; ++x)
                {
                    text_m += matrix[x, aux_n];
                }
                aux_n++;
                for (x = aux_n; x < value_n; ++x)
                {
                    text_m += matrix[value_m - 1, x];
                }
                value_m--;
                if (aux_n < value_n)
                {
                    for (x = value_m - 1; x >= aux_m; --x)
                    {
                        text_m += matrix[x, value_n - 1];
                    }
                    value_n--;
                }
                if (aux_m < value_m)
                {
                    for (x = value_n - 1; x >= aux_n; --x)
                    {
                        text_m += matrix[aux_m, x];
                    }
                    aux_m++;
                }
            }
            file_encrypted(text_m);
        }
        //Recorrido horizontal.
        public void right_path(int value_m, int value_n, char[,] matrix)
        {
            //Recorrer matriz en espiral
            int x, aux_m = 0, aux_n = 0;
            while (aux_m < value_m && aux_n < value_n)
            {
                for (x = aux_n; x < value_n; ++x)
                {
                    text_m += matrix[aux_m, x];
                }
                aux_m++;
                for (x = aux_m; x < value_m; ++x)
                {
                    text_m += matrix[x, value_n - 1];
                }
                value_n--;
                if (aux_m < value_m)
                {
                    for (x = value_n - 1; x >= aux_n; --x)
                    {
                        text_m += matrix[value_m - 1, x];
                    }
                    value_m--;
                }
                if (aux_n < value_n)
                {
                    for (x = value_m - 1; x >= aux_m; --x)
                    {
                        text_m += matrix[x, aux_n];
                    }
                    aux_n++;
                }

            }
            file_encrypted(text_m);

        }
    }
}
