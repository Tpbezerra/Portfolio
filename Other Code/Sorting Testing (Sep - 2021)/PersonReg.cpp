#include <iostream>
#include "PersonReg.h"

Person::Person()
{
	name = "";
	address = "";
}

Person::Person(const string& name, const string& address)
{
	this->name = name;
	this->address = address;
}

bool Person::operator<(const Person& test)
{
	return (name < test.name || (name == test.name && address < test.address));
}

string Person::Name() const
{
	return name;
}

string Person::Address() const
{
	return address;
}

void Person::Print() const
{
	std::cout << name << " bor p� " << address << "\n";
}

PersonMedTel::PersonMedTel()
{
	name = "";
	address = "";
	nummer = "";
}

PersonMedTel::PersonMedTel(const string& name, const string& address, const string& nummer)
{
	this->name = name;
	this->address = address;
	this->nummer = nummer;
}

void PersonMedTel::Print() const
{
	std::cout << name << " bor p� " << address << " och har numret " << nummer << "\n";
}

// --- PersonReg ---

PersonReg::PersonReg(int maxCapacity)
{
	personer = new Person[maxCapacity];
	kapacitet = maxCapacity;
	storlek = 0;
}

PersonReg::~PersonReg()
{
	delete[](personer);
}

Person* PersonReg::Begin()
{
	return personer;
}

Person* PersonReg::End()
{
	return (personer + storlek);
}

bool PersonReg::L�ggTill(const Person* toAdd)
{
	return L�ggTillTest(toAdd->Name(), toAdd->Address());
}

bool PersonReg::L�ggTillTest(const string& name, const string& address)
{
	if (storlek >= kapacitet)
		return false;

	*(personer + storlek) = Person(name, address);
	storlek++;

	return true;
}

void PersonReg::TaBortEntry(Person* toRemove)
{
	int startIndex = 0;
	for (Person* ptr = personer; ptr != (personer + storlek); ptr++)
	{
		if (ptr == toRemove)
			break;

		startIndex++;
	}

	if (startIndex == storlek)
		return;

	for (Person* ptr = (personer + startIndex); ptr != (personer + storlek - 1); ptr++)
	{
		*ptr = *(ptr + 1);
	}

	storlek--;
}

Person* PersonReg::S�kNamn(const string& namn) const
{
	for (Person* ptr = personer; ptr != (personer + storlek); ptr++)
	{
		if (ptr->Name() == namn)
			return ptr;
	}

	return nullptr;
}

Person* PersonReg::S�kFritt(const string& s�kEfter, Person* startOnNext) const
{
	Person* start = startOnNext ? (startOnNext + 1) : personer;
	int iterations = 0;

	for (Person* ptr = start; ptr != (personer + storlek); ptr++)
	{
		if (ptr->Name().find(s�kEfter) != string::npos || ptr->Address().find(s�kEfter) != string::npos)
			return ptr;

		iterations++;
		if (iterations > storlek)
			break;
	}

	return nullptr;
}

void PersonReg::Print() const
{
	for (Person* ptr = personer; ptr != (personer + storlek); ptr++)
	{
		ptr->Print();
	}
}

void PersonReg::T�m()
{
	storlek = 0;
}