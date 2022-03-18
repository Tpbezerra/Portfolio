#ifndef Inter
#define Inter

#include <iostream>
#include <vector>
#include <string>
#include <map>

using std::string;

class Interpreter
{
private:
#pragma region Variables

	std::map<string, int> variableMap;

	std::ostream& outputStream;

	std::vector<string> tokens;

	int tokenPosition = 0;

	enum class OutputConfig
	{
		Decimal,
		Hexadecimal,
		Binary
	};
	OutputConfig currentConfig = OutputConfig::Decimal;

	const string ETX = "\003";

#pragma endregion

	bool IsInt(string token);

	string Peek();

	string Peek(int steps);

	void Consume(const string& token);

	int ParseAdditionExp();

	int ParseMultiplicationExp();

	int ParsePrimaryExp();

	void ParsePrint();

	void ParseConfig();

	void ParseVariable();

public:
	Interpreter(std::ostream& out_stream) : variableMap(std::map<string, int>()), outputStream(out_stream) {}

	void evaluate(const std::vector<string>& tokens);
};

#endif // !Inter