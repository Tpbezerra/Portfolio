#include <iostream>
#include <vector>
#include <string>
#include <map>
#include <bitset>
#include "Interpreter.h"

using std::string;

bool Interpreter::IsInt(string token)
{
	for (size_t i = 0; i < token.size(); i++)
	{
		if (!isdigit(token[i]))
		{
			if (i > 0 || ((token[0] != '-' && token[0] != '+') || token.size() <= 1))
				return false;
		}
	}

	return true;
}

string Interpreter::Peek()
{
	return Peek(0);
}

string Interpreter::Peek(int steps)
{
	if (tokenPosition + steps >= (int)tokens.size())
		return ETX;

	return tokens[tokenPosition + steps];
}

void Interpreter::Consume(const string& token)
{
	string nextToken = Peek();
	if (nextToken == ETX)
		throw std::runtime_error("Consumed past last token.\n");
	else if (nextToken != token)
		throw std::runtime_error("Could not consume token: " + token + "\n");

	tokenPosition++;
}

int Interpreter::ParseAdditionExp()
{
	int value = ParseMultiplicationExp();

	string nextToken = Peek();
	while (true)
	{
		if (nextToken == "+")
		{
			Consume("+");
			value += ParseMultiplicationExp();
		}
		else if (nextToken == "-")
		{
			Consume("-");
			value -= ParseMultiplicationExp();
		}
		else
			break;

		nextToken = Peek();
	}

	return value;
}

int Interpreter::ParseMultiplicationExp()
{
	int value = ParsePrimaryExp();

	string nextToken = Peek();
	while (true)
	{
		if (nextToken == "*")
		{
			Consume("*");
			value *= ParsePrimaryExp();
		}
		else if (nextToken == "/")
		{
			Consume("/");
			value /= ParsePrimaryExp();
		}
		else
			break;

		nextToken = Peek();
	}

	return value;
}

int Interpreter::ParsePrimaryExp()
{
	string nextToken = Peek();
	int value;

	if (variableMap.find(nextToken) != variableMap.end())
	{
		value = variableMap[nextToken];
		Consume(nextToken);
	}
	else if (IsInt(nextToken))
	{
		if (nextToken[0] == '-' || nextToken[0] == '+')
		{
			char temp = nextToken[0];
			nextToken[0] = ' ';

			value = std::stoi(nextToken);
			nextToken[0] = temp;

			if (temp == '-')
				value *= -1;
		}
		else
			value = std::stoi(nextToken);
		
		Consume(nextToken);
	}
	else if (nextToken == "(")
	{
		Consume("(");
		value = ParseAdditionExp();
		if (Peek() == ")")
			Consume(")");
		else
			throw std::runtime_error("Expected: )\n");
	}
	else
	{
		throw std::runtime_error("Expected int, variable or ( )\n");
	}

	return value;
}

void Interpreter::ParsePrint()
{
	int toPrint = ParseAdditionExp();

	switch (currentConfig)
	{
	case OutputConfig::Decimal:
		outputStream << toPrint;
		break;
	case OutputConfig::Hexadecimal:
		outputStream << "0x" << toPrint;
		break;
	case OutputConfig::Binary:
		outputStream << std::bitset<32>(toPrint).to_string();
		break;
	default:
		break;
	}

	outputStream << std::endl;
}

void Interpreter::ParseConfig()
{
	string nextToken = Peek();

	if (nextToken == "dec")
	{
		outputStream << std::dec;
		currentConfig = OutputConfig::Decimal;
	}
	else if (nextToken == "hex")
	{
		outputStream << std::hex;
		currentConfig = OutputConfig::Hexadecimal;
	}
	else if (nextToken == "bin")
	{
		outputStream << std::dec;
		currentConfig = OutputConfig::Binary;
	}
	else
		throw std::runtime_error("Invalid value for config.");
}

void Interpreter::ParseVariable()
{
	string variableName = Peek();
	Consume(variableName);
	Consume("=");

	int value = ParseAdditionExp();

	variableMap[variableName] = value;
}

void Interpreter::evaluate(const std::vector<string>& tokens)
{
	this->tokens = tokens;
	tokenPosition = 0;

	string nextToken = Peek();
	if (nextToken == "print")
	{
		Consume("print");
		ParsePrint();
	}
	else if (nextToken == "config")
	{
		Consume("config");
		ParseConfig();
	}
	else if (Peek(1) == "=")
	{
		ParseVariable();
	}
	else if (tokens.size() > 0)
	{
		throw std::runtime_error("Unable to parse line.");
	}
}