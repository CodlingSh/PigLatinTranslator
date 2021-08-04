using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PigLatinTranslator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private bool hasPunctuation(string currentWord)
        {
            if (currentWord.Contains('.') ||
                currentWord.Contains(',') ||
                currentWord.Contains('!') ||
                currentWord.Contains('?') ||
                currentWord.Contains('"') ||
                currentWord.Contains(':') ||
                currentWord.Contains(';'))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// This function will translate the word to pig latin and return the translated word
        /// </summary>
        /// <param name="currentWord"></param>
        /// <returns>currentWord</returns>
        private string translateWord(string currentWord)
        {
            // Variables
            string part1, part2;
            part1 = part2 = "";
            bool isCapitalized = false;

            // Check if the first letter of the original word is capitalized
            if (Char.IsUpper(currentWord[0]))
            {
                isCapitalized = true;
            }

            // Make the word lowercase
            currentWord = currentWord.ToLower();

            // Check for vowels (except "Y")
            for (int i = 0; i < currentWord.Length; i++)
            {
                if (currentWord[i] == 'a' ||
                    currentWord[i] == 'e' ||
                    currentWord[i] == 'i' ||
                    currentWord[i] == 'o' ||
                    currentWord[i] == 'u' ||
                    currentWord[i] == 'y' ||
                    currentWord[i] == 'A' ||
                    currentWord[i] == 'E' ||
                    currentWord[i] == 'I' ||
                    currentWord[i] == 'O' ||
                    currentWord[i] == 'U' ||
                    currentWord[i] == 'Y')
                {
                    if (i == 0) // If first letter is a vowel
                    {
                        currentWord += "way";
                        break;
                    }
                    else // If first letter is not a vowel
                    {
                        currentWord = currentWord.Substring(i, currentWord.Length - i) + currentWord.Substring(0, i) + "ay"; ;
                        break;
                    }
                }
            }

            // Capitalize the first letter if necessary
            if (isCapitalized)
            {
                currentWord = Char.ToUpper(currentWord[0]) + currentWord.Substring(1, currentWord.Length - 1);
            }

            return currentWord;
        }

        /// <summary>
        /// This function takes a string, then separates that string into alphabetical characters and punctuation symbols.
        /// It then translates all the alphabetical characters into pig latin, reconstructs the word, and then returns it.
        /// </summary>
        /// <param name="currentWord"></param>
        /// <returns>finalString</returns>
        private string splitWord(string currentWord)
        {   
            // Variables
            string puncPos = "";
            string letterPos = "";
            string finalString = "";
            bool puncStart = false;
            bool puncEnd = false;
            ArrayList finalWords = new ArrayList();
            ArrayList finalPunc = new ArrayList();
            char[] alphabet = new char[] {'\'', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };

            // Set boolean if string starts with punctuation
            if (!alphabet.Contains(currentWord[0]))
            {
                puncStart = true;
            }

            // Set boolean if string ends with punctuation
            if (!alphabet.Contains(currentWord[currentWord.Length - 1]))
            {
                puncEnd = true;
            }

            // loop through the chars in the string and figure out which ones are letters and punctuation
            for (int i = 0; i < currentWord.Length; i++)
            {
                if (!alphabet.Contains(currentWord[i])) // if char is not a letter
                {
                    puncPos += currentWord[i];
                    letterPos += ' ';
                }
                else // if char is a letter
                {
                    puncPos += ' ';
                    letterPos += currentWord[i];                       
                }
            }

            // arrays that hold the splitted up strings
            string[] wordsToBeTranslated = letterPos.Split(' ');
            string[] punctuation = puncPos.Split(' ');
            
            // Remove the space characters for the words and add them to an arrayList to make them easier to work with. (Might be a bit redundant, consider a better way)
            for (int i = 0; i < wordsToBeTranslated.Length; i++)
            {
                if (wordsToBeTranslated[i] != "")
                {
                    finalWords.Add(translateWord(wordsToBeTranslated[i]));                    
                }
            }

            // Remove the space characters for the punctuation
            for (int i = 0; i < punctuation.Length; i++)
            {
                if (punctuation[i] != "")
                {
                    finalPunc.Add(punctuation[i]);
                }
            }       

            // Reconstruct the string with translated words
            if (puncStart == true && puncEnd == true) // If punctuation appears at the beggining and end of string
            {
                for (int i = 0; i < finalPunc.Count; i++)
                {   
                    if (i != finalPunc.Count - 1) // If at the final punctuation mark
                    {
                        finalString += "" + finalPunc[i] + finalWords[i]; 
                    } 
                    else
                    {
                        finalString += finalPunc[i];
                    }
                }
            } 
            else if (puncStart == true && puncEnd == false) // If punctuation appears at the beggining of string
            {
                for (int i = 0; i < finalPunc.Count; i++)
                {
                    finalString += "" + finalPunc[i] + finalWords[i];
                }
            } 
            else if (puncStart == false && puncEnd == true) // If punctuation appears at the end of string
            {
                for (int i = 0; i < finalPunc.Count; i++)
                {
                    finalString += "" + finalWords[i] + finalPunc[i];
                }
            } 
            else if (puncStart == false && puncEnd == false) // If punctuation only appears somewhere in the middle of the string
            {
                for (int i = 0; i < finalWords.Count; i++)
                {   
                    if (i != finalWords.Count - 1) // If at the final word
                    {
                        finalString += "" + finalWords[i] + finalPunc[i];                    
                    }
                    else
                    {
                        finalString += finalWords[i];
                    }
                }
            }

            return finalString;
        }

        // Event listener for when translateBtn is clicked
        private void translateBtn_Click(object sender, EventArgs e)
        {
            // Variables
            string[] words = englishTxt.Text.Split(' ');

            // Clear the Pig Latin text box
            pigTxt.Text = "";

            for (int i = 0; i < words.Length; i++)
            {
                if (hasPunctuation(words[i]))
                {
                    words[i] = splitWord(words[i]);
                }
                else
                {
                    words[i] = translateWord(words[i]);
                }
            }

            // Display the translated message
            for (int i = 0; i < words.Length; i++)
                if (i != words.Length - 1)
                {
                    pigTxt.Text += words[i] + ' ';
                } 
                else
                {
                    pigTxt.Text += words[i];
                }

        }

        // Event listener for when clearBtn is clicked
        private void clearBtn_Click(object sender, EventArgs e) // Clear both text fields when "clearBtn" is clicked
        {
            englishTxt.Text = ""; // Clear top text box
            pigTxt.Text = ""; // Clear bottom text box
        }
    }
}