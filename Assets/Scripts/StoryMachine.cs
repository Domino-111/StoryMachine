using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryMachine : MonoBehaviour
{
    //This will display "Give me a noun" ec.
    public Text inputRequestDisplay;

    //Display the final story
    public Text storyDisplay;

    public GameObject inputPanel;
    public GameObject storyPanel;

    //Store the story as the player is adding to it
    public string storyPartial;

    //Store the story that remains from the template
    public string storyRemaining;

    public string characterName;

    private bool rememberInput;

    void Start()
    {
        NewStory();
    }

    public void NewStory()
    {
        //Loading a text asset from the Resources folder called "StoryTemplate"
        TextAsset story = Resources.Load<TextAsset>("StoryTemplate");

        //Set our remaining story to the contents of the text file
        storyRemaining = story.text;

        //Clear any existing story
        storyPartial = "";

        characterName = "";

        //Display the correct screen to the user
        inputPanel.SetActive(true);
        storyPanel.SetActive(false);

        //Start reading the story
        ReadRemainingStory();
    }

    private void ReadRemainingStory()
    {
        rememberInput = false;

        //See if the story still contains any '/'
        //string.IndexOf will return the first position of a character in a string
        int nextInputIndex = storyRemaining.IndexOf('/');
        //If IndexOf can't find the character it results in -1

        if (nextInputIndex < 0)
        {
            //If we find no '/' we should end the story
            EndStory();

            return; //Stop here if you reach this line
        }

        //This bit will only trigger if we find a '/' in our remaining story
        //string.Remove() will return a new string which deletes all characters including and after the index provided
        storyPartial += storyRemaining.Remove(nextInputIndex);

        //We can pass a second index into .Remove() to define how many characters to delete... Here we delete everything from the start up to the first '/'
        storyRemaining = storyRemaining.Remove(0, nextInputIndex + 1);

        AskForInput();
    }

    public void AddToStory(string addition)
    {
        //Take user's input and add it to the story
        storyPartial += addition;

        if (rememberInput)
        {
            characterName = addition;
        }

        ReadRemainingStory();
    }

    private void AskForInput()
    {
        //Find the end of the current input request
        int endInputIndex = storyRemaining.IndexOf('/');

        string inputRequest = storyRemaining.Remove(endInputIndex);

        storyRemaining = storyRemaining.Remove(0, endInputIndex + 1);

        if (inputRequest.Contains('~'))
        {
            //If we have not alrady stored the name
            if (characterName == "")
            {
                rememberInput = true;
                inputRequest = inputRequest.Remove(0, 1);
            }

            else
            {
                storyPartial += characterName;
                ReadRemainingStory();
                return;
            }
        }

        inputRequestDisplay.text = "Give me a " + inputRequest;


    }

    private void EndStory()
    {
        storyPartial += storyRemaining;

        storyDisplay.text = storyPartial;

        inputPanel.SetActive(false);
        storyPanel.SetActive(true);
    }
}
