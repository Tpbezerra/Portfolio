#include <iostream>
#include <vector>
#include <string>
#include <fstream>
#include <sstream>
#include "Interpreter.h"

using std::string;

void Run(const string& input, std::ostream& output)
{
    Interpreter interpreter(output);

    std::vector<string> tokens;

    string line;
    string temp;
    std::stringstream sStream;
    std::ifstream myFile(input);
    
    if (myFile.is_open())
    {
        while (getline(myFile, line))
        {
            sStream = std::stringstream(line);

            while (std::getline(sStream, temp, ' '))
            {
                tokens.push_back(temp);
            }

            interpreter.evaluate(tokens);

            tokens.clear();
        }
    }
    else
    {
        std::cout << "Unable to open file.";
    }
}

int main()
{
    Run("C@ Code.txt", std::cout);
}