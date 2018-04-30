// AUTHOR: Qiaofang Deng
// FILENAME: wordEncrypter.cs
// DATE: 4/29/2018
// REVISION HISTORY: version 1
//
// Description:
// This wordEncrypter is playing a sports name guessing game, it has a list of words 
// Then creates an EncryptWord object, reads one word
// from the list, and encrypt this word by a shift code between 1 and 20.
// Then lets the user to guess the shift code. If the guess number is not
// equals to the shift code, prints the hints and the word which is decoded
// by the guess number. Prints the statics after bingo. Then prints
// the original sports name. If the user wants to play again, another
// EncryptWord object will be created, reads another word from the list, and
// start over.
// Do not construct the object by passing arguments.
// The words in the file is finite, if the user keep playing, the sports name
// may be repeated. After creating the object, please turnOnEncrypting(),
// otherwise words passed in the object will not be encrypted.
// Do not reset the object when the user is guessing the shift code.
//
// Assumptions:
// Each word in the file should not be less than 4 characters, and
// it should be formed by capital of lower case letters. Otherwise,
// the word will not be encrypted, "invalidInput" will be printed,
// and the user will be asked if he wants to play again.
// ShiftCode is between 1 and 20.
// The guess word is randomly generated from the wordList
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleApp1
{
    class Driver
    {
        //fields
        string[] wordList = { "football", "soccer", "baseball", "swimming", "tennis" }; //word list
        EncryptWord wordEncrypter; //the encryptword object
        string wordToGuess; //the word to guess

        int highGuesses; //number of high guesses
        int lowGuesses;  //number of low guesses
        int updatedHighGuesses; //the updated number of high guesses
        int updatedLowGuesses; //the updated number of low guesses
        string encryptedWord; //the encrypted word
        
        //constructor
        public Driver()
        {
            Random rnd = new Random();
            int randomNumber = rnd.Next(0, wordList.Length);
            wordToGuess = wordList[randomNumber];

            highGuesses = 0;
            lowGuesses = 0;
            updatedHighGuesses = 0;
            updatedLowGuesses = 0;
            encryptedWord = "";
            wordEncrypter = new EncryptWord();
        }

        public void Gamestart()
        {
            // start the game
            Console.WriteLine("\nReady? Go! The encrypter status is: "
                                + wordEncrypter.GetStatus());
            encryptedWord = wordEncrypter.Encryptword(wordToGuess);
            Console.WriteLine("\nThe encrypted sports name is: " +
                                encryptedWord);
        }

        public void AskGuessNumber()
        {
            Console.Write("\n\nPlease guess the shift code(between 1 and 20): ");
        }

        public bool CheckGuessNumber(int guessNum)
        {
            bool flag = false;
            wordEncrypter.CheckGuessNumber(guessNum);
            highGuesses = wordEncrypter.GetHighGuesses();
            lowGuesses = wordEncrypter.GetLowGuesses();
            wordEncrypter.UpdateStatistics(guessNum);
            updatedHighGuesses = wordEncrypter.GetHighGuesses();
            updatedLowGuesses = wordEncrypter.GetLowGuesses();
            if (updatedHighGuesses > highGuesses)
            {
                Console.WriteLine("Your guess is high! \n");
                Console.WriteLine("Using your shift code, the decoded word is: " +
                        wordEncrypter.Decode(encryptedWord, guessNum));
            }
            else if (updatedLowGuesses > lowGuesses)
            {
                Console.WriteLine("Your guess is low! \n");
                Console.WriteLine("Using your shift code, the decoded word is: " +
                wordEncrypter.Decode(encryptedWord, guessNum));
            } else
            {
                flag = true;
                Console.WriteLine("\nBingo! You have guessed " + wordEncrypter.GetNumberOfQueries() +
                    " times.\nYou have " + wordEncrypter.GetHighGuesses() +
                    " high guesses. \n" + wordEncrypter.GetLowGuesses() +
                    " low guesses. \n" + "Average guess value is " +
                wordEncrypter.GetAverageGuessValue());
                Console.WriteLine("\nThe sports name is: " + wordToGuess);
                Console.Read();
            }
            return flag;
        }

        //Description: print welcome message
        public void Welcome()
        {
            Console.WriteLine("Welcome to the guessing game! \n" +

            "In this game, an encrypted sports name is printed on the screen, " +

            "the name is encrypted by a shift code.\n" +

            "For example, if the original sports name is \"hiking\", and the " +

            "shift code is 1, then the name will be encrypted to \"ijljoh\".\n" +

            "Please guess the shift code. The word decoded by your shift code " +

            "will be displayed.\nIf your guess is correct, the statistics will" +

            " be displayed on the screen.");

        }

        //Description: print Goodbye message
        public void Goodbye()
        {
            Console.WriteLine("\nThanks for using the program! Goodbye!");

        }
    }
}
