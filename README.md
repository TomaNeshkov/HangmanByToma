# The "Hangman" Game: By Toma
This is my c# recreation of the hame "Hangman"

![image](https://github.com/TomaNeshkov/HangmanByToma/assets/126071897/32e3ab72-7765-4176-9b6a-2f51271752cf)

 ["Hangman"](https://en.wikipedia.org/wiki/Hangman_(game)) is an old, very popular game that almost everybody knows about. You start off with a number of blank spaces, which you have to fill-in using a variety of characters, from A to Z - you must guess the word, avoiding pitfalls, or the hangman will die and you'll lose!

# Input:
* You have to input a letter from <kbd>A</kbd> to <kbd>Z</kbd>.
* Then, you press <kbd>Enter</kbd>.

# Output:
- The program reads your input and determines wether or not the letter is contained in the randomly chosen word. If it is, you will see the blank spaces get updated with your inputed symbols. If not, you will see the hangman slowly appear.
- If you lose, you are greeted with a custom losing animation, and shortly after a death screen which shows you the word that you couldn't guess. After that you can determine if you want to play again, or not.
- If you win, you are greeted with a winning screen, in which you are given the options to play again or quit.

# How does it work?
- The source code for this project is relatively simple. It uses several methods to extract a random word from an array of exactly 2999 words, which is then chosen for the round. Then, your input gets constantly compared to the word. If your input matches any of the letters from the word, it gets updated. Otherwise, one of many frames for the hangman appears in order. The more mistakes you have, the more frames you pass through, until you reach the final one, which triggers the animation of the hangman dying. If you succesfully guess the word, the game loop ends and you are greeted with the victory screen.

Here is a link straight to the source code: https://github.com/TomaNeshkov/HangmanByToma/blob/main/HangmanByToma/HangmanByToma/Hangman.cs

Here is a link to the words.txt file, which is used to choose the word for the round: https://github.com/TomaNeshkov/HangmanByToma/blob/main/HangmanByToma/HangmanByToma/words.txt

# <span style="font-size:32px;">Screenshots:</span>
![image](https://github.com/TomaNeshkov/HangmanByToma/assets/126071897/58330165-af32-44a0-8b52-a8d0af9bf1d0)

![image](https://github.com/TomaNeshkov/HangmanByToma/assets/126071897/6bb35115-c612-4d43-a709-b5df103cb35a)

![image](https://github.com/TomaNeshkov/HangmanByToma/assets/126071897/89b9fe1c-09ab-4cdf-bb5c-6037b42d0760)
 - Win screen

![image](https://github.com/TomaNeshkov/HangmanByToma/assets/126071897/78fce1ef-7106-497f-9273-badd3d1c6d2d) - Loss screen


# Live Demo:

[Start Live Demo](<script src="https://gist.github.com/TomaNeshkov/495b38d3ab43ecec2d0a17c16fc998a2.js"></script>)
