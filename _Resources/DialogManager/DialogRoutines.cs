using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Managers
{
    namespace Dialog
    {
        public class DialogRoutines
        {
            public static IEnumerator WriteRoutine(string writtingText, TextMeshProUGUI TextBox, float sSpeed, float mSpeed, float fSpeed, bool withSound, AudioSource writtingAudioSource)
            {
                TextBox.text = "";

                TextBySpeed[] textSentences = DiviteTextBySpeedType(writtingText);

                TextBox.maxVisibleCharacters = 0;

                foreach (TextBySpeed TS in textSentences)
                {
                    TextBox.text += TS.text;
                }

                int totalOfCharacters = 0;
                int numOfCharactersToShow = 0;

                int currentSentence = 0;

                string SenteceToWrite;

                float currentSpeed = mSpeed;

                while (currentSentence < textSentences.Length)
                {
                    SenteceToWrite = textSentences[currentSentence].text;
                    totalOfCharacters += SenteceToWrite.Length;

                    switch (textSentences[currentSentence].speedType)
                    {
                        case SpeedTypes.SLOW:
                            currentSpeed = sSpeed;
                            break;
                        case SpeedTypes.MEDIUM:
                            currentSpeed = mSpeed;
                            break;
                        case SpeedTypes.FAST:
                            currentSpeed = fSpeed;
                            break;
                    }

                    while (numOfCharactersToShow < totalOfCharacters)
                    {
                        numOfCharactersToShow++;

                        TextBox.maxVisibleCharacters = numOfCharactersToShow;

                        if (withSound)
                        {
                            writtingAudioSource.Play();
                        }

                        yield return new WaitForSeconds(1 / currentSpeed);
                    }

                    currentSentence++;
                }
            }

            private static TextBySpeed[] DiviteTextBySpeedType(string TextToDivide)
            {
                int currentCharIndex = 0;
                int currentSentenceInListIndex = 0;

                List<TextBySpeed> SentencesList = new List<TextBySpeed>();
                SentencesList.Add(new TextBySpeed());

                while (currentCharIndex < TextToDivide.Length)
                {
                    if (TextToDivide[currentCharIndex] == '@')
                    {
                        if (SentencesList[currentSentenceInListIndex].text != "")
                        {
                            SentencesList.Add(new TextBySpeed());
                            currentSentenceInListIndex++;
                        }

                        switch (TextToDivide[currentCharIndex + 1])
                        {
                            case 's':
                                SentencesList[currentSentenceInListIndex].speedType = SpeedTypes.SLOW;
                                break;
                            case 'm':
                                SentencesList[currentSentenceInListIndex].speedType = SpeedTypes.MEDIUM;
                                break;
                            case 'f':
                                SentencesList[currentSentenceInListIndex].speedType = SpeedTypes.FAST;
                                break;
                        }

                        currentCharIndex += 2;
                    }
                    else
                    {
                        SentencesList[currentSentenceInListIndex].text += TextToDivide[currentCharIndex];
                        currentCharIndex++;
                    }
                }

                return SentencesList.ToArray();
            }
        }
    }
}

