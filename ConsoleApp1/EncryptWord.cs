// AUTHOR: Qiaofang Deng
// FILENAME: EncryptWord.cs
// DATE: 15/4/2018
// REVISION HISTORY: version 1
//
// Description:
// When an EncryptWord object is created, it generates a shift code, takes
// words from the file or user input, then encrypted words with
// the shift code, then output the encrypted words.
// For example, if the shift code is 3, the letter ‘a’ will be encrypted as
// ‘d’; ‘b’ encrypted as ‘e’, … ,‘w’ as ‘z’, ‘x’ as ‘a’,‘y’ as ‘b’ and ‘z’
// as ‘c’. It also allows user to decode the word with a shift code,
// and queries about the shift value. It yields statistics-- number of queries,
// high guesses, low guesses and average guess value. 
// The state of the object may be ‘off’ or ‘on’, and, also, may be ‘reset’. 
// When the state is on, the shift code is a random number between 1 and 20. 
// When the state is off, the shift code is 0.
// Valid input for the object is word with capital letters or lower case
// letters. Invalid input is words less than 4 letters or words with
// punctuation or numbers. If the input is invalid, output will be 
// "invalidInput"
// This class has the following fields: shiftCode, numberOfQueries, highGuesses
// lowGuesses, averageGuessValue, totalGuessValue, status.
// This class has no dependency.
//
//
// Assumptions:
// Legal input is words with capital or lower case letters, words less than 4
// characters or words with numbers and punctuation will be regarded as
// invalid input.
// When invalid input is entered, it will not be encrypted and will output
// "invalidInput"
// When reset, a new shift code is generated.
// Constructor does not support creating the object with any arguments.
// When using the encryptWord(string) and decode(string,int), the string
// argument should be a word which is not less than 4 letters and
// with capital letters or lower case letters.
// turnOnEncrypting() is to turn on the encrypter and generate a shift code,
// reset() is to regenerate a shift code
//
// Interface Invariants:
// Copy not supported.
// The shift code is always between 1 and 20
// When the EncryptWord object is created, the state of the object is 
// "on".
// Do not reset when the user is still guessing the shift code, otherwise, it
// will change the shift code to a new random number.
// Before using the public methods, the object should be created.
// The statistics will not change unless the user start to guess the shift code
//
// Anticipated Use:
// The EncryptWord object should be created before all public functions are
// called. 
// getNumberOfQueries(), getHighGuesses(), getLowGuesses(), 
// getAverageGuessValue()can be used to get the statics, but only after user 
// guesses about the shiftCode so that the statics can be updated.
// checkGuessNumber(int) is provided to check if the user's guess about the
// shiftCode is correct.
// updateStatistics(int) is provided to update the statistics after each 
// user guess
// getStatus() is provided to get the status of the EncryptWord object
// reset()is provided to reset the shiftCode to a number between 1 and 20.
// encryptWord(string) is provided to encrypt the string with the shiftCode
// decode(string,int) is provided to decode the string by the integer
// Do not reset when the user is still guessing the shift code, otherwise, it
// will change the shift code to a new random number.
//
// State/Dependencies:
// This class has the following fields: shiftCode, numberOfQueries, highGuesses
// lowGuesses, averageGuessValue, totalGuessValue, status.
// The state of the object is always "on", and has a shiftCode between 1 and 20
// unless the private function turnOffEncrypting() is called to change the 
// state to "off" ,then the shiftCode will be changed to 0.
// This class is independent.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleApp1
{
    class EncryptWord
    {
        private const int NUMBER_OF_LETTERS = 26; //number of letters
        private const int LARGEST_SHIFT_CODE = 20; //largest shift code
        private const int SMALLEST_SHIFT_CODE = 1; //smallest shift code
        private const int ASCII_OF_FIRST_CAPITAL_LETTER = 65; //ascii of first capital letter
        private const int ASCII_OF_LAST_CAPITAL_LETTER = 90; //ascii of last capital letter
        private const int ASCII_OF_FIRST_LOWERCASE_LETTER = 97; //ascii of 1st lowercase letter
        private const int ASCII_OF_LAST_LOWERCASE_LETTER = 122;//ascii of last lowercase letter
        private const int LEAST_WORD_LENGTH = 4; //least word length

        //fields
        private int shiftCode; // describe the shift code
        private int numberOfQueries; // describe the number of queries about shift code
        private int highGuesses; //describe the high guesses
        private int lowGuesses; //describe the lowGuesses
        //private double averageguessvalue; //describe the average of guess values
        private int totalGuessValue;  //total guess value
        private string status;  //the status of encrypting, on or off

        // description: construct an EncruptWord object
        // precondition: do not pass in any arguments
        // postcondition: shiftCode =0; numberOfQueries = 0; status = "off";
        //                highGuesses = 0;lowGuesses = 0;
        //	            averageGuessValue = 0;totalGuessValue = 0;
        public EncryptWord()
        {
            shiftCode = 0;
            numberOfQueries = 0;
            highGuesses = 0;
            lowGuesses = 0;
            //averageGuessValue = 0;
            totalGuessValue = 0;

            status = "off";
            TurnOnEncrypting();
        }

        // description: get number of queries
        // precondition: object created
        // postcondition: numberOfQueries doesn't change
        public int GetNumberOfQueries()
        {
            return numberOfQueries;
        }

        // description: get the number of high guesses
        // precondition: object created
        // postcondition: highGuesses doesn't change
        public int GetHighGuesses()
        {
            return highGuesses;
        }

        // description: get the number of low guesses
        // precondition: object created
        // postcondition: lowGuesses doesn't change
        public int GetLowGuesses()
        {
            return lowGuesses;
        }

        // description: get the average guess value
        // precondition: none
        // postcondition: averageGuessValue doesn't change
        public double GetAverageGuessValue()
        {
            return (double)totalGuessValue / (double)numberOfQueries;
        }

        // description: check if the guess number is equals to the shift code
        // precondition: pass in the guessing number
        // postcondition: shift code doesn't change
        public bool CheckGuessNumber(int number)
        {
            if (number == shiftCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // description: update the statistics
        // precondition: pass in the guessing number
        // postcondition: the numberOfQuerie will increase by 1, highGuesses
        //                or lowGuesses may increased by 1, averageGuessValue will
        //                change
        public void UpdateStatistics(int number)
        {
            numberOfQueries++;
            if (number < shiftCode)
            {
                lowGuesses++;
                totalGuessValue += number;
            }
            else if (number > shiftCode)
            {
                highGuesses++;
                totalGuessValue += number;
            }
            else
            {
                totalGuessValue += number;
            }
        }

        // description: encrypt the word, and return the encrypted word
        // precondition: pass in a word which is not less than 4 letters and
        //                with capital letters or lower case letters.
        // postcondition: encrypted the string which is passed in
        public string Encryptword(string word)
        {
            int asciiOfCharacter;  //ascii of the character
            int asciiAfterEncrypting;  //ascii of the character after encrypting
            char charAfterEncrypting;  //the character after encrypting
            string wordAfterEncrypting = ""; //the word after encrypting
            if (!InputValidation(word))
            {
                return "invalidInput";
            }
            else
            {
                for (int i = 0; i < word.Length; i++)
                {
                    asciiOfCharacter = word[i];
                    asciiAfterEncrypting = asciiOfCharacter + shiftCode;
                    if (asciiOfCharacter <= ASCII_OF_LAST_CAPITAL_LETTER &&
                            asciiAfterEncrypting > ASCII_OF_LAST_CAPITAL_LETTER)
                    {
                        asciiAfterEncrypting = asciiAfterEncrypting -
                                ASCII_OF_LAST_CAPITAL_LETTER
                                + ASCII_OF_FIRST_CAPITAL_LETTER - 1;
                    }
                    else if (asciiOfCharacter > ASCII_OF_FIRST_LOWERCASE_LETTER &&
                           asciiAfterEncrypting > ASCII_OF_LAST_LOWERCASE_LETTER)
                    {
                        asciiAfterEncrypting = asciiAfterEncrypting
                                - ASCII_OF_LAST_LOWERCASE_LETTER
                                + ASCII_OF_FIRST_LOWERCASE_LETTER - 1;
                    }
                    charAfterEncrypting = (char)asciiAfterEncrypting;
                    wordAfterEncrypting += charAfterEncrypting;
                }
                return wordAfterEncrypting;
            }
        }



        // description: get the status of the encrypter, it could be "on" or "off"
        // precondition: object created
        // postcondition: doesn't change the status
        public string GetStatus()
        {
            return status;
        }

        // description: reset the encrypter, give a new shift code to the object
        // precondition: none
        // postcondition: the shift code will be changed to a new random number
        //                between 1-20
        public void Reset()
        {
            shiftCode = RandomNumber();
        }

        // description: decode the string with the guessing number
        // precondition: pass in a string a word which is not less than 4 letters and
        //               with capital letters or lower case letters, and an integer
        // postcondition: decoded the string with the given integer
        public string Decode(string word, int guessNumber)
        {
            int asciiOfCharacter;  //ascii of the character
            int asciiAfterDecoding;  //ascii of the character after Decoding
            char charAfterDecoding;  //the character after Decodingg
            string wordAfterDecoding = ""; //the word after encrypting
            guessNumber = guessNumber % NUMBER_OF_LETTERS;
            if (!InputValidation(word))
            {
                return "invalidInput";
            }
            else
            {
                for (int i = 0; i < word.Length; i++)
                {
                    asciiOfCharacter = word[i];
                    asciiAfterDecoding = asciiOfCharacter - guessNumber;
                    if (asciiOfCharacter <= ASCII_OF_LAST_CAPITAL_LETTER &&
                            asciiAfterDecoding < ASCII_OF_FIRST_CAPITAL_LETTER)
                    {
                        asciiAfterDecoding = ASCII_OF_LAST_CAPITAL_LETTER -
                                ASCII_OF_FIRST_CAPITAL_LETTER + 1 + asciiAfterDecoding;
                    }
                    else if (asciiOfCharacter >= ASCII_OF_FIRST_LOWERCASE_LETTER &&
                           asciiAfterDecoding < ASCII_OF_FIRST_LOWERCASE_LETTER)
                    {
                        asciiAfterDecoding = asciiAfterDecoding +
                                ASCII_OF_LAST_LOWERCASE_LETTER
                                - ASCII_OF_FIRST_LOWERCASE_LETTER + 1;
                    }
                    charAfterDecoding = (char)asciiAfterDecoding;
                    wordAfterDecoding += charAfterDecoding;
                }
                return wordAfterDecoding;
            }
        }



        // description: turn off the encrypter, shift code = 0; status = "off"
        // precondition: none
        // postcondition: shift code = 0; status = "off"
        private void TurnOffEncrypting()
        {
            shiftCode = 0;
            status = "off";
        }

        // description: check the input validation
        // precondition: pass in a string
        // postcondition: will not change the string, return false if the string is
        //                not a string a word which is not less than 4 letters and
        //                with capital letters or lower case letters
        private bool InputValidation(string word)
        {
            for (int i = 0; i < word.Length; i++)
            {
                if (word[i] < ASCII_OF_FIRST_CAPITAL_LETTER
                        || word[i] > ASCII_OF_LAST_LOWERCASE_LETTER)
                {
                    return false;
                }
                else if (word[i] > ASCII_OF_LAST_CAPITAL_LETTER &&
                       word[i] < ASCII_OF_FIRST_LOWERCASE_LETTER)
                {
                    return false;
                }
                else if (word.Length < LEAST_WORD_LENGTH)
                {
                    return false;
                }
            }
            return true;
        }

        // description: create a random number between 1 and 20
        // precondition: none
        // postcondition: a random number created
        private int RandomNumber()
        {
            //int randomNumber; //random number
            //srand((unsigned int)time(NULL));
            //randomNumber = rand() % LARGEST_SHIFT_CODE + SMALLEST_SHIFT_CODE;
            //return randomNumber;
            Random rnd = new Random();
            int randomNumber = rnd.Next(1, 21);
            return randomNumber;
        }

        // description: turn on the encrypter, set the status to "on"
        //              and give the shift code a random number
        // precondition: none
        // postcondition: status = "on"; shift code is a random number between 1-20
        private void TurnOnEncrypting()
        {
            shiftCode = RandomNumber();
            status = "on";
        }

    }
}
