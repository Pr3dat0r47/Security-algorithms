using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;


namespace SecurityLibrary
{
    public class PlayFair : ICryptographic_Technique<string, string>
    {
        public Hashtable alphabet = new Hashtable();
        void fill_hashtable() 
        {
            
            for(char i='A';i<='Z'; i++)
            {
                if(i == 'J')
                {
                }
                else
                { 
                alphabet.Add(i, 0);
                }
               
             
            }
            
        }

        public char[,] filling_matrix(string text)
        {
            text = text.ToUpper();
            char[,] filled_matrix = new char[5, 5];
            int arraycounterrow = 0,arraycountercolumn=0;
            for (int i = 0; i < text.Length; i++)
            {
                char x = text[i];
                if (x == 'J')
                {
                    x = 'I';
                }
                if ( (int)alphabet[x] == 0)
                {
                    filled_matrix[arraycounterrow,arraycountercolumn] = x;
                    arraycountercolumn++;
                    arraycountercolumn %= 5;
                    if (arraycountercolumn == 0)
                    {
                        arraycounterrow++;
                        arraycounterrow %= 5;//malooosh lazma
                    }
                    alphabet[x] = 1;
                }
                else
                { //pass
                }
            }
            for (char i = 'A'; i <= 'Z'; i++) {  
                if (i == 'J') {
                    continue;               
                }
                else
                {
                    if ((int)alphabet[i] == 0)
                    {
                        filled_matrix[arraycounterrow, arraycountercolumn] = i;
                        arraycountercolumn++;
                        arraycountercolumn %= 5;
                        if (arraycountercolumn == 0)
                        {
                            arraycounterrow++;
                            arraycounterrow %= 5;//malooosh lazma
                        }
                        alphabet[i] = 1;
                    }
                    else
                    { //pass
                    }

                }

            }

            for(char i = 'A';i<= 'Z'; i++)
            {
                alphabet[i] = 0;
            }
            return filled_matrix;
        }
        public string Decrypt(string cipherText, string key)
        {
            cipherText = cipherText.ToUpper();
            char[,] matrix = new char[5, 5];
            fill_hashtable();
            matrix = filling_matrix(key);

            /////////////////////////////////////////////////////////////////////////////////////////////
            ///
            //find the index of the chars
            string sub_text = "";
            cipherText.Replace('J', 'I');

           

            int col1 = 0, row1 = 0, col2 = 0, row2 = 0;
            string plain_text = "";
            for (int i = 0; i < cipherText.Length; i += 2)
            {
                sub_text = cipherText[i].ToString() + cipherText[i + 1].ToString();
                for (int x = 0; x < 5; x++)
                {
                    for (int y = 0; y < 5; y++)
                    {
                        if (sub_text[0] == matrix[x, y])
                        {
                            row1 = x;
                            col1 = y;
                        }
                        else if (sub_text[1] == matrix[x, y])
                        {
                            row2 = x;
                            col2 = y;
                        }



                    }
                }
                if (row1 == row2)
                {
                    plain_text += matrix[row1, ((col1 - 1)+5) % 5].ToString() + matrix[row1, ((col2 - 1)+5) % 5].ToString();
                }
                else if (col1 == col2)
                {
                    plain_text += matrix[((row1 - 1)+5) % 5, col1].ToString() + matrix[((row2 - 1)+5) % 5, col2].ToString();
                }
                else
                {
                    plain_text += matrix[row1, col2].ToString() + matrix[row2, col1].ToString();
                }
            }
            
                if (plain_text[plain_text.Length - 1] == 'X')
                {
                  plain_text =  plain_text.Remove(plain_text.Length - 1,1);
  
                }
                
                for(int i = 1; i < plain_text.Length-2; i+=2)
                {
                    if (plain_text[i] == 'X')
                    {
                        if (plain_text[i - 1] == plain_text[i + 1])
                        {
                            plain_text = plain_text.Remove(i, 1);
                            i--;
                        }
                    }
                
                }
            




            return plain_text.ToLower();


            /////////////////////////////////////////////////////////////////////////////////////////
        }

        public string Encrypt(string plainText, string key)
        {
            plainText = plainText.ToUpper();
            
            char[,] matrix = new char[5, 5];
            fill_hashtable();
            matrix = filling_matrix(key);
            string sub_text = "", new_text = "";
            plainText.Replace('J', 'I');

            


            for (int i = 0; i < plainText.Length; i++) 
            {
                //check if the last char is alone it add X
                
                if(i+1 == plainText.Length) 
                {
                    sub_text = plainText[i].ToString() + "X";
                    new_text += sub_text;
                //if 2 char are equal it add X between them
                }else if(plainText[i] == plainText[i + 1]) {
                    sub_text = plainText[i].ToString() + "X";
                    new_text += sub_text;
                }
                //normal condition takes 2 neighbour chars
                else 
                {
                    sub_text = plainText[i].ToString() + plainText[i+1].ToString();
                    new_text += sub_text;
                    i++;
                }
            }

            //find the index of the chars
            int col1=0, row1=0,col2=0,row2=0;
            string cipher_text = "";
            for (int i = 0; i < new_text.Length; i+=2)
            {
                sub_text = new_text[i].ToString() + new_text[i + 1].ToString();
                for(int x = 0; x < 5; x++) 
                {
                    for(int y = 0; y < 5; y++) 
                    {
                        if(sub_text[0] == matrix[x,y])
                        {
                            row1 = x;
                            col1 = y;
                        }else if(sub_text[1] == matrix[x, y]) 
                        {
                            row2 = x;
                            col2 = y;
                        }
                       


                    }
                }
                if (row1 == row2)
                {
                    cipher_text += matrix[row1, (col1 + 1) % 5].ToString() + matrix[row1, (col2 + 1) % 5].ToString();
                }
                else if (col1 == col2)
                {
                    cipher_text += matrix[(row1 + 1) % 5, col1].ToString() + matrix[(row2 + 1) % 5, col2].ToString();
                }
                else
                {
                    cipher_text += matrix[row1, col2].ToString() + matrix[row2, col1].ToString();
                }
            }

            //after finding the index start working on the 
           
            //for(int i = 0;i < new_text.Length; i += 2) 
            //{
                
            //}

            return cipher_text;
        }
    }
}
