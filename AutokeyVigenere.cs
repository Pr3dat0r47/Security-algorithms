using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
namespace SecurityLibrary
{
    public class AutokeyVigenere : ICryptographicTechnique<string, string>
    {
        string alphabit = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public string[,] matrix()
        {
            int x = 0;
            string[,] viginere_matrix = new string[26,26];
            for(int i = 0; i < 26; i++) 
            {
                x = i;
                for(int j = 0; j < 26; j++) 
                {
                    viginere_matrix[i, j] = alphabit[x % 26].ToString();
                    x++;
                }
            }
            return viginere_matrix;
        }
        public string Analyse(string plainText, string cipherText)
        {
            string[,] viginere_matrix = new string[26, 26];
            viginere_matrix = matrix();
            string key = "";
            //the key is the index and the alphabit is the value
            Hashtable index_alphabit = new Hashtable();
            //the alphabit is the key and index is the value
            Dictionary<char, int> alphabit_index = new Dictionary<char, int>();
            int x = 0;
            string plain = "", key_stream = key;
            char y = 'a';
            for (char i = 'a'; i <= 'z'; i++)
            {
                alphabit_index.Add(i, x);
                index_alphabit.Add(x, y);
                y++;
                x++;
            }

            for (int i = 0; i < cipherText.Length; i++)
            {
                for (int j = 0; j < 26; j++)
                {
                    //gets the index of the main plain char
                    if (viginere_matrix[(int)alphabit_index[plainText[i]], j] == cipherText[i].ToString())
                    {
                        key_stream += index_alphabit[j];
                    }

                }
            }
            int index = 0,temp = 0;
            
            for(int i = 0; i < key_stream.Length; i++)
            {
                if(key_stream[i] == plainText[index])
                {
                    index = i;
                    temp = 0;
                    
                    while(i != key_stream.Length && key_stream[i] == plainText[temp%plainText.Length] ) 
                    {
                        i++;
                        temp++;
                    }
                }
            }
            key = key_stream.Substring(0, index);
            return key;
        }

        public string Decrypt(string cipherText, string key)
        {
            string[,] viginere_matrix = new string[26, 26];
            viginere_matrix = matrix();
            //the key is the index and the alphabit is the value
            Hashtable index_alphabit = new Hashtable();
            //the alphabit is the key and index is the value
            Dictionary<char, int> alphabit_index = new Dictionary<char, int>();    
            int x = 0;
            string plain = "", key_stream = key;
            char y = 'a';
            for (char i = 'a'; i <= 'z'; i++)
            {
                alphabit_index.Add(i, x);
                index_alphabit.Add(x, y);
                y++;
                x++;
            }
           
            for (int i = 0;i < cipherText.Length;i++) 
            { 
                for (int j = 0; j < 26; j++)
                {
                    //gets the index of the main plain char
                    if(viginere_matrix[(int)alphabit_index[key_stream[i]],j] == cipherText[i].ToString())
                    {
                        plain += index_alphabit[j];
                        key_stream+= index_alphabit[j];
                    }
               
                }
            }
            return plain;
        }

        public string Encrypt(string plainText, string key)
        {
            Hashtable alphabit_index = new Hashtable();
            string[,] viginere_matrix = new string[26, 26];
            viginere_matrix = matrix();
            string key_stream = key;
            int x = 0;
            for(char i = 'a'; i<= 'z'; i++)
            {
                alphabit_index.Add(i, x);
                x++;
            }

            if(key.Length < plainText.Length)
            {
                for(int i = 0; i<plainText.Length - key.Length; i++)
                {
                    key_stream += plainText[i];  
                }
            }
            string cipher = "";
            for (int i = 0; i < plainText.Length; i++)
            {
                cipher += viginere_matrix[(int)alphabit_index[plainText[i]], (int)alphabit_index[key_stream[i]]];
            }

            return cipher;
            
        }
    }
}
