#include <iostream>
#include <string>

using std::string;

void listPrimes(short n)
{
	if (n < 2)
		return;

	short arrayLength = n - 1;
	short* numbers = new short[arrayLength];
	numbers[0] = 2;

	short currentNumber = 3;
	for (short i = 1; i < arrayLength; i++)
	{
		if (currentNumber % 2 != 0)
			numbers[i] = currentNumber;
		else
			numbers[i] = -1;

		currentNumber++;
	}

	short primeCount = 1;
	short index = 1;
	while (index < arrayLength)
	{
		for (short i = index; i < arrayLength; i++)
		{
			if (numbers[i] != -1)
			{
				currentNumber = numbers[i];
				index = i;
				primeCount++;
				break;
			}
		}

		for (short i = index + currentNumber; i < arrayLength; i += currentNumber)
		{
			numbers[i] = -1;
		}

		index++;
	}

	short* primeNumbers = new short[primeCount];
	
	index = 0;
	for (short i = 0; i < arrayLength; i++)
	{
		if (numbers[i] != -1)
		{
			primeNumbers[index] = numbers[i];
			index++;
		}
	}

	for (short i = 0; i < primeCount; i++)
	{
		std::cout << primeNumbers[i] << "\n";
	}

	delete[] numbers;
	delete[] primeNumbers;
}

void substitute_str(std::string& iostring, const std::string& before, const std::string& after)
{
	string newString;
	newString.reserve(iostring.length() * after.length());

	bool valid;
	for (size_t i = 0; i < iostring.length(); i++)
	{
		valid = false;
		if (iostring.at(i) == before.at(0) && (i + before.length() - 1) < iostring.length())
		{
			valid = true;
			for (size_t j = 0; j < before.length(); j++)
			{
				if (iostring.at(i + j) != before.at(j))
				{
					valid = false;
					break;
				}
			}
		}

		if (valid)
		{
			i += before.length() - 1;

			newString += after;
		}
		else
		{
			newString += iostring.at(i);
		}
	}

	newString.shrink_to_fit();

	iostring = newString;
}

char* substitute_cstr(const char* input, const char before, const char* after)
{
	size_t inputLength = strlen(input);
	size_t afterLength = strlen(after);
	size_t beforeCount = 0;

	for (size_t i = 0; i < inputLength; i++)
	{
		if (input[i] == before)
			beforeCount++;
	}

	size_t outputLength = inputLength + (beforeCount * afterLength) - beforeCount;
	char* output = new char[outputLength + 1];

	size_t outputIndex = 0;
	for (size_t i = 0; i < inputLength; i++)
	{
		if (input[i] == before)
		{
			for (size_t j = 0; j < afterLength; j++)
			{
				*(output + outputIndex) = after[j];
				outputIndex++;
			}
		}
		else
		{
			*(output + outputIndex) = input[i];
			outputIndex++;
		}
	}

	output[outputLength] = '\0';

	return output;
}

void substitute_strm(std::istream& input, std::ostream& output, const std::string& before, const std::string& after)
{
	string original;
	std::getline(input >> std::ws, original);

	string newString;
	newString.reserve(original.length() * after.length());

	bool valid;
	for (size_t i = 0; i < original.length(); i++)
	{
		valid = false;
		if (original.at(i) == before.at(0) && (i + before.length() - 1) < original.length())
		{
			valid = true;
			for (size_t j = 0; j < before.length(); j++)
			{
				if (original.at(i + j) != before.at(j))
				{
					valid = false;
					break;
				}
			}
		}

		if (valid)
		{
			i += before.length() - 1;

			newString += after;
		}
		else
		{
			newString += original.at(i);
		}
	}

	newString.shrink_to_fit();

	output << newString;
}

#pragma region Update Uppgifter

void Uppgift1(short& input1)
{
	std::cout << "Please enter \"n\" for uppgift 1:\n";

	std::cin >> input1;
	std::cout << "\n";

	listPrimes(input1);
	std::cout << "\n";

	// Flush the input buffer.
	std::cin.clear();
	std::cin.ignore(INT_MAX, '\n');
}

void Uppgift2(string& input2, string& replaceString, string& replaceWithString)
{
	std::cout << "Please enter the string to compare for uppgift 2:\n";
	std::getline(std::cin >> std::ws, input2);
	std::cout << "Please enter what to replace:\n";
	std::getline(std::cin >> std::ws, replaceString);
	std::cout << "Please enter what to replace it with:\n";
	std::getline(std::cin >> std::ws, replaceWithString);
	std::cout << "\n";

	substitute_str(input2, replaceString, replaceWithString);

	std::cout << input2 << "\n";
}

void Uppgift3(char input3[], char replaceChar[], char replaceWithChar[], char result[])
{
	std::cout << "Please enter the string to compare for uppgift 3:\n";
	std::cin.getline(input3, 100);
	std::cout << "Please enter what to replace:\n";
	std::cin.getline(replaceChar, 100);
	std::cout << "Please enter what to replace it with:\n";
	std::cin.getline(replaceWithChar, 100);
	std::cout << "\n";

	result = substitute_cstr(input3, replaceChar[0], replaceWithChar);

	std::cout << result << "\n";
}

void Uppgift4(string& replaceString, string& replaceWithString)
{
	std::cout << "Please enter what to replace:\n";
	std::getline(std::cin >> std::ws, replaceString);
	std::cout << "Please enter what to replace it with:\n";
	std::getline(std::cin >> std::ws, replaceWithString);
	std::cout << "Please enter the string to compare for uppgift 4:\n";
	substitute_strm(std::cin, std::cout, replaceString, replaceWithString);
	std::cout << "\n\n";
}

#pragma endregion

int main()
{
	short input1 = 1;

	string input2;
	string replaceString;
	string replaceWithString;

	char input3[100];
	char replaceChar[100];
	char replaceWithChar[100];
	char* result = new char[0];

	short input;

	bool run = true;
	while (run)
	{
		input = -999;
		std::cout << "Which task would you like to test (-1 will exit the program): ";
		std::cin >> input;
		std::cin.ignore(1, '\n');
		std::cout << "\n";

		if (input == 1) // --- Uppgift 1 ---
		{
			Uppgift1(input1);
		}
		else if (input == 2) // --- Uppgift 2 ---
		{
			Uppgift2(input2, replaceString, replaceWithString);
		}
		else if (input == 3) // --- Uppgift 3 ---
		{
			Uppgift3(input3, replaceChar, replaceWithChar, result);
		}
		else if (input == 4) // --- Uppgift 3 ---
		{
			Uppgift4(replaceString, replaceWithString);
		}
		else if (input == -1)
		{
			run = false;
		}
		else
		{
			std::cout << "Invalid input.\n";
		}
	}

	delete[] result;
}