#pragma once
#include<iostream>

#ifndef String_H
#define String_H

class String
{
private:
	char* value;
	size_t length;
	size_t stringCapacity;

	friend bool operator==(const String& lhs, const String& rhs);

	friend bool operator!=(const String& lhs, const String& rhs);

	friend std::ostream& operator<<(std::ostream& out, const String& rhs);

	friend std::istream& operator>>(std::istream& is, String& obj);

	inline void Invariant();

public:
	// Constructors and destructor.
	String();
	String(const String& rhs);
	String(const char* cstr);

	~String();

	// Operators.
	String& operator=(const String& rhs);

	String& operator+=(const String& rhs);

	String operator+(const String& rhs);

	char& operator[](size_t i);

	const char& operator[](size_t i)const;

	// Other functions.
	char& at(size_t i);

	const char& at(size_t i) const;

	inline size_t size() const;

	inline size_t capacity() const;

	const char* data() const;

	void push_back(const char& c);

	void reserve(size_t newCap);

	void shrink_to_fit();
};

#endif // !String_H