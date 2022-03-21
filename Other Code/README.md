# Other

This is the code for some programs that were for school assignments. They represent a variety of uses and were 
written in 2 different languages: C# and C++. Unless disclosed, all code was written by me.

## Area Calculator (Oct - 2018)

A tiny program where a user can input a few numbers in powershell and get the product of all of them. Alternatively 
(if the powerShell bool is false) the user can use the standard console interface to retrieve the product of two 
numbers.

## Chat Box (Nov - 2019)

A program that allows to users to connect to one-another over an internet connection and send messages using a 
windows-forms application.

## Connection - Client Test (Nov - 2019)

A program to test client connection and a lead up to the **Chat Box** program.

## Connection - Server Test (Nov - 2019)

A program to test server creation and listening leading up to the **Chat Box** program.

## Decision Tree Example (Nov - 2021)

This is a small code file that serves as a base for any decision tree I would want to implement. It was created as 
part of an AI course and was then implemented into another bigger project.

## Encryption Program (Sep - 2019)

This was a program made during a programming competition for students all over the world (wherein I came top 50).
The task for this program was to make an encryption/decryption program where the letters in the input-word would 
"bounce around" a box. To then get the coded version of the word, the program would go from left to right, top to 
bottom and create the word out of any letters it passed by. To decode a word, the program would bounce the letters 
arround like for it did when encrypting. This is to find which positions would be occupied. Then it would place the 
letters from the coded word in each slot, in the same order as when encrypting (top left to bottom right). 
Finally it would bounce through again, this time picking up letters as it went along. The result is the decoded 
word. There was also letter replacement, each letter in the word would be replaced by the corresponding letter in 
a replacement alphabet.

![Display](Encryption Program (Sep - 2019)/Encryption Display.png)

Following the red line from the **T** shows the path that the word would take. The coded word would then be composed 
of each letter in the order it arrived at them, starting from the top-left moving right and down. In this case the 
result would be **"Thsoam"**.

## Force Calculator Kinetic-Potential (Oct - 2018)

Small console program where a user can input what type of force they would want to calculate (kinetic or potential), 
input the required parameters **(mass, velocity, height)** and recieve the result.

## Force Calculator Particles (Nov - 2018)

Small windows-forms program that gives the electromagnetic force between two charged particles given the charge of 
each and the distance between them.

## Multithreading Example 1 - Music-Drawing (Jun - 2021)

This was the first multithreading assignment. We were tasked with creating a program that could draw text that 
teleported around a panel, draw a spinning triangle, and play music. Each on its own thread.

## Multithreading Example 2 - String Transfer (Apr - 2021)

The second multithreading assignment was to create a program that could write and read a string on two separate 
threads. We synced the two threads so that the reader could only read once the writer had written something new, 
and the writer would only write when the reader had finished reading. To compare, there is an option to do it 
without sync.

## Multithreading Example 3 - Producer-Consumer (May- 2021)

This third assignment was to create a program where producers were producing different items (the examples are 
foodstuffs) and placing them in storage. At the same time consumers were taking the produced items out of storage. 
Each producer and each consumer was to run on its own thread and they use semaphores to sync up the placement, 
respectively removal, of items.

## Multithreading Example 4 - Text Replacer (May - 2021)

The fourth multithreading assignment was to read the text from a file and replace specific words with other words. 
This was to be done by using a buffer that would fill with each of the words from the original file. There would 
then be three threads that would access this buffer, a writer, a modifier, and a reader. The writer would 
set the word at the write position in the buffer to the next word in the input string. The modifier would then 
check if the word at the modify position was a word to be replaced. Finally the reader would get the word from the 
read position in the buffer and add it to the output window.

Because these actions (writing, modifying, reading) take place on separate threads, they risk running "out of 
order". To prevent this they will only continue if the elemnt at their respective buffer position lives up to their 
expected criteria. The writer will only write if the reader has read from it, the reader will only read if the 
modifier has checked it and the modifier will only check if it has a newly written word.

These rules are enforced by the use of a **Monitor**.

## Person Registry (Sep - 2021)

DISCLOSER: The **Main.cpp** file was not written by me.

This project was to create a registry of people where one could add, remove, search for, empty, and print out 
the registry.

## Quoridor AI (Oct - 2021)

This was a project where we were tasked with creating an AI-player that could play and, given the right conditions, 
win a game of **Quoridor**. The code included was all written by me, on its own, however, it wont play the game. 
This code was applied onto other code created by **Olle Lindeberg** that ran the game itself. This code would then 
play the game.

It works by creating a graph representing the game-board where nodes are the tiles that a player can move on and 
the edges between them represent where a player can get to from any one node. The program then checks for the 
shortest path to its destination as well as the opponent's shortest path. If the program has walls remaining and 
the opponent is closer to their destination than the program, consider placing a wall. When placing a wall, go 
through the nodes in the opponent's path and check how much a wall placed there would impact them, compared to 
itself. Once all nodes have been checked, make sure that the impact on the opponent is greater than the impact on 
iteslf. If it is, place a wall at whatever position was found to have the greatest impact. If it is not, don't 
place a wall.

If the program placed a wall, that is the end of its turn. If it didn't (because it is closer than the opponent, or 
because it has no walls left, or because the impact of a wall would be greater on itself), move. When moving, all 
it does is get the next node in the path and move to it.

## Sorting Testing (Sep - 2021)

This is an expansion on the **Person Registry** project. This assignment was testing how to perform sorting with 
the standard library sorter. To make the person registry sortable, **Person** was given a "less than" operator.

## String Creation (Sep - 2021)

This assignment was to create a custom string class with some of the functions of **std::string**. The functions 
implemented were some operators (namely "**=**", "**+=**", "**+**", "**[]**", "**==**", "**!=**", "**<<**", 
"**>>**"), and other functions such as: "**push_back**", "**reserve**", and "**shrink_to_fit**". Each function 
that used another, existing, string as input, was not to use the same address as the input. It should create its 
own copy of that string instead.

## String Replacer (Sep - 2021)

This was an assignment to find specific strings in another string (or char[]) and replace them with a third string. 
The three variants were: substituting strings in another string, substituting characters with string in a C-String, 
and replacing strings from an input stream. The method "**listPrimes**" is separate and is used to list all of 
the primes up to the input int.

## Table Calculator (Oct - 2018)

Program that displays the mathematic tables for a few low numbers. Which table is displayed (*, /, +, -) can be 
changed by the user using a dropdown menu. What number it is applied to can be changed using another dropdown.

## Text Analyser (Oct - 2019)

Opens a file and prints out the amount of lines, words, letters, characters and cases of "the".

## Txt Code Parser (Oct - 2021)

This is a parser that takes 'code' from a file and parses them to output correct text to the console. I can handle 
simple mathematics and can output in **decimal**, **hexadecimal**, and **binary**. Finally it can create variables 
with attached values.
