#include<iostream>
#include <assert.h>
#include "String.h"

void CopyCArray(char* to, const size_t& toLength, const char* from, const size_t& fromLength)
{
	for (size_t i = 0; i < toLength; i++)
	{
		if (i < fromLength)
			to[i] = from[i];
		else
			to[i] = '\0';
	}
}

// Constructors and destructor.
String::String() : length(0), stringCapacity(8), value(new char[8])
{
	Invariant();
}
String::String(const String& rhs) : length(0), stringCapacity(0), value(nullptr)
{
	if (rhs.data() == nullptr)
		return;

	length = rhs.size();
	stringCapacity = rhs.capacity();
	value = new char[stringCapacity];

	CopyCArray(value, length, rhs.value, rhs.size());

	Invariant();
}
String::String(const char* cstr) : length(0), stringCapacity(0), value(nullptr)
{
	if (cstr == nullptr)
		return;

	length = strlen(cstr);
	stringCapacity = length;
	value = new char[stringCapacity];

	CopyCArray(value, length, cstr, strlen(cstr));

	Invariant();
}
String::~String()
{
	Invariant();
	delete[] value;
}

// Operators.
String& String::operator=(const String& rhs)
{
	if (this == &rhs)
		return *this;

	length = rhs.size();

	if (capacity() < length)
	{
		delete[] value;
		stringCapacity = length;
		value = new char[stringCapacity];
	}

	CopyCArray(value, length, rhs.value, rhs.size());

	Invariant();

	return *this;
}

String& String::operator+=(const String& rhs)
{
	size_t newLength = length + rhs.size();
	if (capacity() >= newLength)
	{
		for (size_t i = 0; i < rhs.size(); i++)
		{
			value[length + i] = rhs[i];
		}
	}
	else
	{
		size_t newCap = stringCapacity * 2;
		while (newCap < newLength)
		{
			newCap *= 2;
		}

		char* temp = new char[newCap];

		for (size_t i = 0; i < newLength; i++)
		{
			if (i < length)
				temp[i] = value[i];
			else
				temp[i] = rhs[i - length];
		}

		delete[] value;

		value = temp;
		stringCapacity = newCap;
	}

	length = newLength;

	Invariant();

	return *this;
}

String String::operator+(const String& rhs)
{
	String temp = *this;
	temp += rhs;

	return temp;
}

char& String::operator[](size_t i)
{
	return value[i];
}

const char& String::operator[](size_t i)const
{
	return value[i];
}

bool operator==(const String& lhs, const String& rhs)
{
	if (lhs.size() != rhs.size())
		return false;

	for (size_t i = 0; i < lhs.size(); i++)
	{
		if (lhs[i] != rhs[i])
			return false;
	}

	return true;
}

bool operator!=(const String& lhs, const String& rhs)
{
	return !(lhs == rhs);
}

std::ostream& operator<<(std::ostream& out, const String& rhs)
{
	for (size_t i = 0; i < rhs.length; i++)
	{
		out << rhs.value[i];
	}

	return out;
}

std::istream& operator>>(std::istream& is, String& obj)
{
	char temp[1000];
	is.getline(temp, 1000);

	obj = temp;

	return is;
}

// Accessors.
size_t String::size() const
{
	return length;
}

size_t String::capacity() const
{
	return stringCapacity;
}

const char* String::data() const
{
	return value;
}

// Other functions.
void String::Invariant()
{
	assert(length <= stringCapacity);
	assert(stringCapacity > 0 && value);
}

char& String::at(size_t i)
{
	if (value && i >= 0 && i < size())
		return value[i];

	throw std::out_of_range("Index is outside the bounds of the array!");
}

const char& String::at(size_t i) const
{
	if (value && i >= 0 && i < size())
		return value[i];

	throw std::out_of_range("Index is outside the bounds of the array!");
}

void String::push_back(const char& c)
{
	if (capacity() < size() + 1)
	{
		char* temp = new char[capacity() * 2];

		for (size_t i = 0; i < size(); i++)
		{
			temp[i] = value[i];
		}

		temp[size()] = c;

		delete[] value;

		value = temp;
		stringCapacity*=2;
	}
	else
	{
		value[size()] = c;
	}

	length++;

	Invariant();
}

void String::reserve(size_t newCap)
{
	if (newCap <= capacity())
		return;

	char* temp = new char[newCap];

	CopyCArray(temp, newCap, value, size());

	delete[] value;

	value = temp;
	stringCapacity = newCap;

	Invariant();
}

void String::shrink_to_fit()
{
	Invariant();

	if (capacity() == size())
		return;

	char* temp = new char[size()];

	CopyCArray(temp, size(), value, size());

	delete[] value;

	value = temp;
	stringCapacity = size();

	Invariant();
}